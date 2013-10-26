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
        private const string COUNT_TEXT_PREFIX = "Count: ";
        private const int COLUMN_INDEX_PROXY = 0;
        private const int COLUMN_INDEX_STATUS = 1;

        private object proxyDataGridLock = new object();
        private Thread[] proxyCheckThreads;
        private ProxyChecker[] proxyCheckObjects;

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
                proxyCheckThreads[i] = new Thread(new ParameterizedThreadStart(proxyCheckObjects[i].CheckProxyHTTPAccess_List));
            }
            string[] proxyList = {"test", "test2"};
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
            DialogResult result = SaveFileDialog.ShowDialog();
            string fileContent = GetProxyList(ref WorkingProxyList);
            if (result == DialogResult.OK)  //prevents saves when the user hit cancel.
            {
                SaveFile(SaveFileDialog.FileName, fileContent);
            }
        }

        private void SaveFailedProxies_Click(object sender, EventArgs e)
        {
            DialogResult result = SaveFileDialog.ShowDialog();
            string fileContent = GetProxyList(ref FailedProxyList);
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
            LockGUI(true);
            int proxiesAmount = ProxyList.Items.Count;
            int threads = (int)ThreadsAmount.Value;
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
                Action<string, ListBox> start = new Action<string, ListBox>(this.AddProxyToList);
                if (e.Result)
                {
                    this.Invoke(start, e.AddressAndPort, WorkingProxyList);
                }
                else
                {
                    this.Invoke(start, e.AddressAndPort, FailedProxyList);
                }
            }
        }

        private void AddProxyToList(string proxy, ListBox lb)
        {
            lb.Items.Add(proxy);
            FailedProxyListCountText.Text = lb.Items.Count.ToString();
        }

        private void ClearProxyList_Click(object sender, EventArgs e)
        {
            ProxyList.Items.Clear();
        }
    }
}
