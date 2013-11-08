using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using LeeSkiBee_ProxyChecker.Net.Proxies;

namespace LeeSkiBee_ProxyChecker
{
    public partial class MainForm : Form
    {
        private object proxyDataGridLock = new object();
        private Thread[] proxyCheckThreads;
        private ProxyChecker[] proxyCheckObjects;
        private int expectedResponsesAmount;
        private int responsesAmount;
        private bool checkingProxies;

        private const string TEST_SUCCESSFUL = "Connection test result: Successful!";
        private const string TEST_FAILED = "Connection test result: Failed!";
        private const string STATUS_IDEAL = "Ideal";
        private const string STATUS_TESTING = "Testing proxies...";

        public MainForm()
        {
            InitializeComponent();
            StatusText.Text = STATUS_IDEAL;
            int maxChecks = (int)ThreadsAmount.Maximum;
            proxyCheckThreads = new Thread[maxChecks];
            proxyCheckObjects = new ProxyChecker[maxChecks];
            for (int i = 0; i < maxChecks; i++)
            {
                proxyCheckObjects[i] = new ProxyChecker();
                proxyCheckObjects[i].HTTPCheckResult = new Action<ProxyCheckResult>(this.OnProxyResult);
            }
            checkingProxies = false;
        }

        private void AddProxyList(string[] proxies)
        {
            foreach (string proxy in proxies)
            {
                ProxyList.Items.Add(proxy);
            }
            ProxyListCountText.Text = ProxyList.Items.Count.ToString();
        }

        private void SaveFile(string filePath, string contents)
        {
            try
            {
                File.WriteAllText(filePath, contents);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Unable to save file due to insufficient permissions. Please run this application with an account with the correct permissions or save the file to a different folder.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Unable to create file.");
            }
        }

        private string GetProxyList(ref ListBox list)
        {
            if (list.Items.Count <= 0)
            {
                return "";  //Nothing to search through, just return nothing.
            }
            StringBuilder proxies = new StringBuilder();
            foreach (string row in list.Items)
            {
                proxies.AppendLine(row);
            }
            return proxies.ToString();
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            string[] fileLines;
            DialogResult result = OpenFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    fileLines = File.ReadAllLines(OpenFileDialog.FileName);
                    if (fileLines.Length >= 1)
                    {
                        AddProxyList(fileLines);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Unable to read file. Please ensure the file exists and the application has sufficient permissions to read it.");
                }
            }
        }

        private void SaveWorkingProxies_Click(object sender, EventArgs e)
        {
            SaveProxies(ref WorkingProxyList);
        }

        private void SaveFailedProxies_Click(object sender, EventArgs e)
        {
            SaveProxies(ref FailedProxyList);
        }

        private void SaveProxies(ref ListBox lb)
        {
            DialogResult result = SaveFileDialog.ShowDialog();
            string fileContent = GetProxyList(ref lb);
            if (result == DialogResult.OK)  //prevents saves when the user hit cancel.
            {
                SaveFile(SaveFileDialog.FileName, fileContent);
            }
        }

        private void LockGUI(bool b)
        {
            LoadFile.Enabled = !b;
            SaveWorkingProxies.Enabled = !b;
            SaveFailedProxies.Enabled = !b;
            URL.Enabled = !b;
            ThreadsAmount.Enabled = !b;
            RequestTimeout.Enabled = !b;
            ClearProxyList.Enabled = !b;
            TestProxies.Enabled = !b;
            CancelTest.Enabled = b;
            TestConnection.Enabled = !b;
        }

        private void TestProxies_Click(object sender, EventArgs e)
        {
            BeginCheckingProxies();
            int proxiesAmount = ProxyList.Items.Count;
            expectedResponsesAmount = proxiesAmount;
            responsesAmount = 0;
            int threads = (int)ThreadsAmount.Value;
            for (int i = 0; i < threads; i++)
            {
                proxyCheckThreads[i] = new Thread(new ParameterizedThreadStart(proxyCheckObjects[i].CheckProxyHTTPAccess_List));
            }
            foreach (ProxyChecker checker in proxyCheckObjects)
            {
                checker.TestURL = new Uri(URL.Text);
                checker.HTTPCheckTimeout = (int)(RequestTimeout.Value);
            }
            int remainder = (proxiesAmount % threads);
            int proxiesPerThread = (proxiesAmount - remainder) / threads;
            if ((proxiesAmount <= threads) || (proxiesPerThread == 0))
            {
                proxiesPerThread = 1;
            }
            string[][] proxies = new string[threads][];
            int proxyPositionCount = 0;
            for (int i = 0; i < threads; i++)
            {
                int proxiesThisThread = proxiesPerThread;
                if ((i + 1) >= threads)
                {
                    proxiesThisThread += remainder;
                }
                proxies[i] = new string[proxiesThisThread];
                for (int n = 0; n < proxiesThisThread; n++)
                {
                    proxies[i][n] = ProxyList.Items[proxyPositionCount].ToString();
                    proxyPositionCount++;
                }
                proxyCheckThreads[i].Start(proxies[i]);
                if (proxyPositionCount >= proxiesAmount)
                {
                    break;
                }
            }
        }

        public void OnProxyResult(ProxyCheckResult e)
        {
            lock (proxyDataGridLock)
            {
                if ((e.Proxy != null) && (e.Proxy.Address == null)) 
                {
                    //No address value in the proxy means the request did not use a proxy
                    //thus it is a result from a connection test without a proxy.
                    string resultText = null;
                    if (e.Result)
                    {
                        resultText = TEST_SUCCESSFUL;
                    }
                    else
                    {
                        resultText = TEST_FAILED;
                    }
                    MessageBox.Show(resultText, "Test results");
                }
                else
                {
                    Action<string, ListBox, Label> start = new Action<string, ListBox, Label>(this.AddProxyToList);
                    if (e.Result)
                    {
                        this.Invoke(start, e.AddressAndPort, WorkingProxyList, WorkingProxyListCountText);
                    }
                    else
                    {
                        this.Invoke(start, e.AddressAndPort, FailedProxyList, FailedProxyListCountText);
                    }
                } 
            }
        }

        private void AddProxyToList(string proxy, ListBox lb, Label boxCountLabel)
        {
            if (proxy != null)
            {
                lb.Items.Add(proxy);
                boxCountLabel.Text = lb.Items.Count.ToString();
            }
            responsesAmount += 1;
            if (responsesAmount >= expectedResponsesAmount)
            {
                FinishCheckingProxies();
            }
        }

        private void BeginCheckingProxies()
        {
            LockGUI(true);
            checkingProxies = true;
            StatusText.Text = STATUS_TESTING;
        }

        private void FinishCheckingProxies()
        {
            LockGUI(false);
            checkingProxies = false;        
            StatusText.Text = STATUS_IDEAL;
        }

        private void ClearProxyList_Click(object sender, EventArgs e)
        {
            ProxyList.Items.Clear();
            WorkingProxyList.Items.Clear();
            FailedProxyList.Items.Clear();
        }

        private void CancelTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < proxyCheckThreads.Length; i++)
            {
                try
                {
                    proxyCheckThreads[i].Abort();
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Source);
                }
            }
            FinishCheckingProxies();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkingProxies)
            {
                this.CancelTest_Click(this, null);
            }     
        }

        private void TestConnection_Click(object sender, EventArgs e)
        {
            this.Enabled = false;   //Prevent other tasks being started while testing.
            proxyCheckObjects[0].TestConnection(new Uri(URL.Text), (int)RequestTimeout.Value);
            this.Enabled = true;    //Re-enable form usage.
        }

        private void HowToUse_Click(object sender, EventArgs e)
        {
            //Building the string in the code means that the only file
            //required is the EXE file, rather than multiple files.
            //Should be moved to an external data file if the program becomes considerably more complex.
            StringBuilder howto = new StringBuilder();
            howto.AppendLine("1) Add proxies via the 'Add Proxy List' button.");
            howto.AppendLine();
            howto.AppendLine("2) Set the URL for testing (the faster the website loads, the better)");
            howto.AppendLine();
            howto.AppendLine("3) Set the number of connections (threads) at once");
            howto.AppendLine();
            howto.AppendLine("4) Set the timeout value (how long without a response until the request is considered to have failed)");
            howto.AppendLine();
            howto.AppendLine("5) Ensure the program has been setup properly by using the test connection button.");
            howto.AppendLine();
            howto.AppendLine("6) Click the 'Test proxies' button to being testing and wait for the application to finish.");
            howto.AppendLine();
            howto.AppendLine("7) Use the save buttons on the left to save the new lists of working and not-working proxies.");
            MessageBox.Show(howto.ToString(), "How to use:");
        }
    }
}
