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
using LeeSkiBee_ProxyChecker.Net.Proxies;

namespace LeeSkiBee_ProxyChecker
{
    public partial class MainForm : Form
    {
        private const string STATUS_WORKING = "Working!";
        private const string STATUS_FAILED = "Failed!";
        private const string COUNT_TEXT_PREFIX = "Count: ";
        private const string PROXY_COLUMN_NAME = "Proxy";
        private const int COLUMN_INDEX_STATUS = 1;

        private object proxyDataGridLock = new object();
        private ProxyChecker[] proxyCheckThreads;
        private int currentTestCount;

        public MainForm()
        {
            InitializeComponent();
            proxyCheckThreads = new ProxyChecker[(int)ThreadsAmount.Maximum];
            for (int i = 0; i < proxyCheckThreads.Length; i++)
            {

                proxyCheckThreads[i] = new ProxyChecker();
                proxyCheckThreads[i].Result = new Action<int, bool, int>(OnProxyResult);
            }
            string[] proxyList = {"test", "test2"};
            AddProxyListToGrid(proxyList);
        }

        private void AddProxyListToGrid(string[] proxies)
        {
            for (int i = 0; i < proxies.Length; i++)
            {
                ProxyGridView.Rows.Add(proxies[i]);
            }
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

        private string GetProxiesWithStatusOf(string searchStatus)
        {
            if (ProxyGridView.Rows.Count <= 0)
            {
                return "";  //Nothing to search through, just return nothing.
            }
            StringBuilder proxies = new StringBuilder();
            string currentProxyStatus = null;
            foreach (DataGridViewRow row in ProxyGridView.Rows)
            {
                currentProxyStatus = row.Cells[PROXY_COLUMN_NAME].Value.ToString();
                if (currentProxyStatus == searchStatus)
                {
                    proxies.AppendLine(row.Cells[PROXY_COLUMN_NAME].Value.ToString());
                }
            }
            return proxies.ToString();
        }

        private void ProxyGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CountText.Text = COUNT_TEXT_PREFIX + ProxyGridView.Rows.Count;
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
                        AddProxyListToGrid(fileLines);
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
            string fileContent = GetProxiesWithStatusOf(STATUS_WORKING);
            if (result == DialogResult.OK)  //prevents saves when the user hit cancel.
            {
                SaveFile(SaveFileDialog.FileName, fileContent);
            }
        }

        private void SaveFailedProxies_Click(object sender, EventArgs e)
        {
            DialogResult result = SaveFileDialog.ShowDialog();
            string fileContent = GetProxiesWithStatusOf(STATUS_FAILED);
            if (result == DialogResult.OK)  //prevents saves when the user hit cancel.
            {
                SaveFile(SaveFileDialog.FileName, fileContent);
            }
        }

        private void ClearProxyGrid_Click(object sender, EventArgs e)
        {
            ProxyGridView.Rows.Clear();
        }

        private void LockGUI(bool b)
        {
            LoadFile.Enabled = !b;
            SaveWorkingProxies.Enabled = !b;
            SaveFailedProxies.Enabled = !b;
            URL.Enabled = !b;
            ThreadsAmount.Enabled = !b;
            RequestTimeout.Enabled = !b;
            ClearProxyGrid.Enabled = !b;
            TestProxies.Enabled = !b;
            CancelTest.Enabled = b;
        }

        private void TestProxies_Click(object sender, EventArgs e)
        {
            LockGUI(true);
            currentTestCount = 0;   //@TODO - Prevent race condition by setting value before for loop. 
            for (int i = 0; i < ((int)ThreadsAmount.Value); i++)
            {
                Task.Factory.StartNew(
                            () => proxyCheckThreads[i].CheckProxyHTTPAccess(
                            currentTestCount, 
                            i, 
                            URL.Text,
                            ProxyGridView.Rows[currentTestCount].Cells[PROXY_COLUMN_NAME].Value.ToString(), 
                            (int)RequestTimeout.Value
                            )
                );
            }
        }

        public void OnProxyResult(int proxyIndex, bool result, int threadIndex)
        {
            lock (proxyDataGridLock)
            {
                if (result)
                {
                    ProxyGridView.Rows[proxyIndex].Cells[COLUMN_INDEX_STATUS].Value = STATUS_WORKING;
                }
                else
                {
                    ProxyGridView.Rows[proxyIndex].Cells[COLUMN_INDEX_STATUS].Value = STATUS_FAILED;
                }
            }
        }
    }
}
