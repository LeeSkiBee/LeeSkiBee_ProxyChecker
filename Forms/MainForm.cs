using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LeeSkiBee_ProxyChecker
{
    public partial class MainForm : Form
    {
        private const string COUNT_TEXT_PREFIX = "Count: ";
        private const string PROXY_COLUMN_NAME = "Proxy";
        private object ProxyDataGridLock = new object();

        public MainForm()
        {
            InitializeComponent();
            string[] proxyList = {"test", "test2"};
            addProxyListToGrid(proxyList);
        }

        private void addProxyListToGrid(string[] proxies)
        {
            for (int i = 0; i < proxies.Length; i++)
            {
                ProxyGridView.Rows.Add(proxies[i]);
            }
        }

        private void saveFile(string filePath, string contents)
        {
            File.WriteAllText(filePath, contents);
        }

        private string getProxiesWithStatusOf(string status)
        {
            StringBuilder proxies = new StringBuilder();
            string currentProxy = null;
            foreach (DataGridViewRow row in ProxyGridView.Rows)
            {
                currentProxy = row.Cells[PROXY_COLUMN_NAME].Value.ToString();
                if (currentProxy == status)
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
    }
}
