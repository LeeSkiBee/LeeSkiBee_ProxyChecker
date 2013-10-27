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

        public MainForm()
        {
            InitializeComponent();
            int maxChecks = (int)ThreadsAmount.Maximum;
            proxyCheckThreads = new Thread[maxChecks];
            proxyCheckObjects = new ProxyChecker[maxChecks];
            for (int i = 0; i < maxChecks; i++)
            {
                proxyCheckObjects[i] = new ProxyChecker();
                proxyCheckObjects[i].HTTPCheckResult = new Action<ProxyCheckResult>(this.OnProxyResult);
            }
            string[] proxyList = {"test", "test2"};
            checkingProxies = false;
            AddProxyList(proxyList);
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
                MessageBox.Show("Unable to save file due to insufficient permissions." + 
                    "Please run this application with an account with the correct permissions" + 
                    "or save the file to a different folder.");
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
            int remainder = (proxiesAmount % threads);
            int proxiesPerThread = (proxiesAmount - remainder) / threads;
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
        }

        private void FinishCheckingProxies()
        {
            LockGUI(false);
            checkingProxies = false;
        }

        private void ClearProxyList_Click(object sender, EventArgs e)
        {
            ProxyList.Items.Clear();
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
    }
}
