namespace LeeSkiBee_ProxyChecker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadFile = new System.Windows.Forms.Button();
            this.SaveWorkingProxies = new System.Windows.Forms.Button();
            this.SaveFailedProxies = new System.Windows.Forms.Button();
            this.URL = new System.Windows.Forms.TextBox();
            this.URLText = new System.Windows.Forms.Label();
            this.ThreadsText = new System.Windows.Forms.Label();
            this.ThreadsAmount = new System.Windows.Forms.NumericUpDown();
            this.TestProxies = new System.Windows.Forms.Button();
            this.CancelTest = new System.Windows.Forms.Button();
            this.ProxyListCountText = new System.Windows.Forms.Label();
            this.RequestTimeout = new System.Windows.Forms.NumericUpDown();
            this.TimeoutText = new System.Windows.Forms.Label();
            this.MilisecondText = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ClearProxyList = new System.Windows.Forms.Button();
            this.ProxyList = new System.Windows.Forms.ListBox();
            this.WorkingProxyList = new System.Windows.Forms.ListBox();
            this.WorkingProxyListCountText = new System.Windows.Forms.Label();
            this.FailedProxyList = new System.Windows.Forms.ListBox();
            this.FailedProxyListCountText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(12, 12);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(282, 56);
            this.LoadFile.TabIndex = 1;
            this.LoadFile.Text = "Add Proxy List";
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // SaveWorkingProxies
            // 
            this.SaveWorkingProxies.Location = new System.Drawing.Point(12, 91);
            this.SaveWorkingProxies.Name = "SaveWorkingProxies";
            this.SaveWorkingProxies.Size = new System.Drawing.Size(282, 23);
            this.SaveWorkingProxies.TabIndex = 2;
            this.SaveWorkingProxies.Text = "Save Working Proxies";
            this.SaveWorkingProxies.UseVisualStyleBackColor = true;
            this.SaveWorkingProxies.Click += new System.EventHandler(this.SaveWorkingProxies_Click);
            // 
            // SaveFailedProxies
            // 
            this.SaveFailedProxies.Location = new System.Drawing.Point(12, 120);
            this.SaveFailedProxies.Name = "SaveFailedProxies";
            this.SaveFailedProxies.Size = new System.Drawing.Size(282, 23);
            this.SaveFailedProxies.TabIndex = 3;
            this.SaveFailedProxies.Text = "Save Failed Proxies";
            this.SaveFailedProxies.UseVisualStyleBackColor = true;
            this.SaveFailedProxies.Click += new System.EventHandler(this.SaveFailedProxies_Click);
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(400, 17);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(214, 20);
            this.URL.TabIndex = 4;
            this.URL.Text = "http://www.google.com";
            // 
            // URLText
            // 
            this.URLText.AutoSize = true;
            this.URLText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLText.Location = new System.Drawing.Point(352, 14);
            this.URLText.Name = "URLText";
            this.URLText.Size = new System.Drawing.Size(42, 21);
            this.URLText.TabIndex = 6;
            this.URLText.Text = "URL:";
            // 
            // ThreadsText
            // 
            this.ThreadsText.AutoSize = true;
            this.ThreadsText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThreadsText.Location = new System.Drawing.Point(326, 43);
            this.ThreadsText.Name = "ThreadsText";
            this.ThreadsText.Size = new System.Drawing.Size(68, 21);
            this.ThreadsText.TabIndex = 7;
            this.ThreadsText.Text = "Threads:";
            // 
            // ThreadsAmount
            // 
            this.ThreadsAmount.Location = new System.Drawing.Point(400, 43);
            this.ThreadsAmount.Name = "ThreadsAmount";
            this.ThreadsAmount.Size = new System.Drawing.Size(214, 20);
            this.ThreadsAmount.TabIndex = 8;
            this.ThreadsAmount.ThousandsSeparator = true;
            this.ThreadsAmount.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // TestProxies
            // 
            this.TestProxies.Location = new System.Drawing.Point(483, 187);
            this.TestProxies.Name = "TestProxies";
            this.TestProxies.Size = new System.Drawing.Size(131, 23);
            this.TestProxies.TabIndex = 9;
            this.TestProxies.Text = "Test Proxies";
            this.TestProxies.UseVisualStyleBackColor = true;
            this.TestProxies.Click += new System.EventHandler(this.TestProxies_Click);
            // 
            // CancelTest
            // 
            this.CancelTest.Location = new System.Drawing.Point(483, 216);
            this.CancelTest.Name = "CancelTest";
            this.CancelTest.Size = new System.Drawing.Size(131, 23);
            this.CancelTest.TabIndex = 10;
            this.CancelTest.Text = "Cancel";
            this.CancelTest.UseVisualStyleBackColor = true;
            // 
            // ProxyListCountText
            // 
            this.ProxyListCountText.AutoSize = true;
            this.ProxyListCountText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProxyListCountText.Location = new System.Drawing.Point(12, 165);
            this.ProxyListCountText.Name = "ProxyListCountText";
            this.ProxyListCountText.Size = new System.Drawing.Size(50, 19);
            this.ProxyListCountText.TabIndex = 11;
            this.ProxyListCountText.Text = "Count:";
            // 
            // RequestTimeout
            // 
            this.RequestTimeout.Location = new System.Drawing.Point(400, 66);
            this.RequestTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.RequestTimeout.Name = "RequestTimeout";
            this.RequestTimeout.Size = new System.Drawing.Size(214, 20);
            this.RequestTimeout.TabIndex = 13;
            this.RequestTimeout.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // TimeoutText
            // 
            this.TimeoutText.AutoSize = true;
            this.TimeoutText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeoutText.Location = new System.Drawing.Point(324, 66);
            this.TimeoutText.Name = "TimeoutText";
            this.TimeoutText.Size = new System.Drawing.Size(70, 21);
            this.TimeoutText.TabIndex = 12;
            this.TimeoutText.Text = "Timeout:";
            // 
            // MilisecondText
            // 
            this.MilisecondText.AutoSize = true;
            this.MilisecondText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MilisecondText.Location = new System.Drawing.Point(400, 89);
            this.MilisecondText.Name = "MilisecondText";
            this.MilisecondText.Size = new System.Drawing.Size(131, 19);
            this.MilisecondText.TabIndex = 14;
            this.MilisecondText.Text = "( 1000 = 1 Second )";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Title = "Select proxy list";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Title = "Save proxy list";
            // 
            // ClearProxyList
            // 
            this.ClearProxyList.Location = new System.Drawing.Point(12, 348);
            this.ClearProxyList.Name = "ClearProxyList";
            this.ClearProxyList.Size = new System.Drawing.Size(130, 23);
            this.ClearProxyList.TabIndex = 15;
            this.ClearProxyList.Text = "Clear";
            this.ClearProxyList.UseVisualStyleBackColor = true;
            this.ClearProxyList.Click += new System.EventHandler(this.ClearProxyList_Click);
            // 
            // ProxyList
            // 
            this.ProxyList.FormattingEnabled = true;
            this.ProxyList.Location = new System.Drawing.Point(12, 187);
            this.ProxyList.Name = "ProxyList";
            this.ProxyList.Size = new System.Drawing.Size(130, 147);
            this.ProxyList.TabIndex = 16;
            // 
            // WorkingProxyList
            // 
            this.WorkingProxyList.FormattingEnabled = true;
            this.WorkingProxyList.Location = new System.Drawing.Point(164, 187);
            this.WorkingProxyList.Name = "WorkingProxyList";
            this.WorkingProxyList.Size = new System.Drawing.Size(130, 147);
            this.WorkingProxyList.TabIndex = 18;
            // 
            // WorkingProxyListCountText
            // 
            this.WorkingProxyListCountText.AutoSize = true;
            this.WorkingProxyListCountText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkingProxyListCountText.Location = new System.Drawing.Point(164, 165);
            this.WorkingProxyListCountText.Name = "WorkingProxyListCountText";
            this.WorkingProxyListCountText.Size = new System.Drawing.Size(50, 19);
            this.WorkingProxyListCountText.TabIndex = 17;
            this.WorkingProxyListCountText.Text = "Count:";
            // 
            // FailedProxyList
            // 
            this.FailedProxyList.FormattingEnabled = true;
            this.FailedProxyList.Location = new System.Drawing.Point(317, 187);
            this.FailedProxyList.Name = "FailedProxyList";
            this.FailedProxyList.Size = new System.Drawing.Size(130, 147);
            this.FailedProxyList.TabIndex = 20;
            // 
            // FailedProxyListCountText
            // 
            this.FailedProxyListCountText.AutoSize = true;
            this.FailedProxyListCountText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailedProxyListCountText.Location = new System.Drawing.Point(317, 165);
            this.FailedProxyListCountText.Name = "FailedProxyListCountText";
            this.FailedProxyListCountText.Size = new System.Drawing.Size(50, 19);
            this.FailedProxyListCountText.TabIndex = 19;
            this.FailedProxyListCountText.Text = "Count:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 383);
            this.Controls.Add(this.FailedProxyList);
            this.Controls.Add(this.FailedProxyListCountText);
            this.Controls.Add(this.WorkingProxyList);
            this.Controls.Add(this.WorkingProxyListCountText);
            this.Controls.Add(this.ProxyList);
            this.Controls.Add(this.ClearProxyList);
            this.Controls.Add(this.MilisecondText);
            this.Controls.Add(this.RequestTimeout);
            this.Controls.Add(this.TimeoutText);
            this.Controls.Add(this.ProxyListCountText);
            this.Controls.Add(this.CancelTest);
            this.Controls.Add(this.TestProxies);
            this.Controls.Add(this.ThreadsAmount);
            this.Controls.Add(this.ThreadsText);
            this.Controls.Add(this.URLText);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.SaveFailedProxies);
            this.Controls.Add(this.SaveWorkingProxies);
            this.Controls.Add(this.LoadFile);
            this.Name = "MainForm";
            this.Text = "LeeSkiBee\'s Proxy Checker";
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadFile;
        private System.Windows.Forms.Button SaveWorkingProxies;
        private System.Windows.Forms.Button SaveFailedProxies;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label URLText;
        private System.Windows.Forms.Label ThreadsText;
        private System.Windows.Forms.NumericUpDown ThreadsAmount;
        private System.Windows.Forms.Button TestProxies;
        private System.Windows.Forms.Button CancelTest;
        private System.Windows.Forms.Label ProxyListCountText;
        private System.Windows.Forms.NumericUpDown RequestTimeout;
        private System.Windows.Forms.Label TimeoutText;
        private System.Windows.Forms.Label MilisecondText;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button ClearProxyList;
        private System.Windows.Forms.ListBox ProxyList;
        private System.Windows.Forms.ListBox WorkingProxyList;
        private System.Windows.Forms.Label WorkingProxyListCountText;
        private System.Windows.Forms.ListBox FailedProxyList;
        private System.Windows.Forms.Label FailedProxyListCountText;
    }
}

