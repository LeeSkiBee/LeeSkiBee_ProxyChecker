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
            this.ProxyGridView = new System.Windows.Forms.DataGridView();
            this.Proxy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoadFile = new System.Windows.Forms.Button();
            this.SaveWorkingProxies = new System.Windows.Forms.Button();
            this.SaveFailedProxies = new System.Windows.Forms.Button();
            this.URL = new System.Windows.Forms.TextBox();
            this.URLText = new System.Windows.Forms.Label();
            this.ThreadsText = new System.Windows.Forms.Label();
            this.ThreadsAmount = new System.Windows.Forms.NumericUpDown();
            this.TextProxies = new System.Windows.Forms.Button();
            this.CancelTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // ProxyGridView
            // 
            this.ProxyGridView.AllowUserToAddRows = false;
            this.ProxyGridView.AllowUserToDeleteRows = false;
            this.ProxyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Proxy,
            this.Status});
            this.ProxyGridView.Location = new System.Drawing.Point(12, 163);
            this.ProxyGridView.Name = "ProxyGridView";
            this.ProxyGridView.ReadOnly = true;
            this.ProxyGridView.RowHeadersVisible = false;
            this.ProxyGridView.Size = new System.Drawing.Size(606, 176);
            this.ProxyGridView.TabIndex = 0;
            // 
            // Proxy
            // 
            this.Proxy.HeaderText = "Proxy";
            this.Proxy.Name = "Proxy";
            this.Proxy.ReadOnly = true;
            this.Proxy.Width = 300;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 300;
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(12, 12);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(282, 56);
            this.LoadFile.TabIndex = 1;
            this.LoadFile.Text = "Load Proxy List";
            this.LoadFile.UseVisualStyleBackColor = true;
            // 
            // SaveWorkingProxies
            // 
            this.SaveWorkingProxies.Location = new System.Drawing.Point(12, 91);
            this.SaveWorkingProxies.Name = "SaveWorkingProxies";
            this.SaveWorkingProxies.Size = new System.Drawing.Size(282, 23);
            this.SaveWorkingProxies.TabIndex = 2;
            this.SaveWorkingProxies.Text = "Save Working Proxies";
            this.SaveWorkingProxies.UseVisualStyleBackColor = true;
            // 
            // SaveFailedProxies
            // 
            this.SaveFailedProxies.Location = new System.Drawing.Point(12, 120);
            this.SaveFailedProxies.Name = "SaveFailedProxies";
            this.SaveFailedProxies.Size = new System.Drawing.Size(282, 23);
            this.SaveFailedProxies.TabIndex = 3;
            this.SaveFailedProxies.Text = "Save Failed Proxies";
            this.SaveFailedProxies.UseVisualStyleBackColor = true;
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
            this.ThreadsText.Location = new System.Drawing.Point(326, 48);
            this.ThreadsText.Name = "ThreadsText";
            this.ThreadsText.Size = new System.Drawing.Size(68, 21);
            this.ThreadsText.TabIndex = 7;
            this.ThreadsText.Text = "Threads:";
            // 
            // ThreadsAmount
            // 
            this.ThreadsAmount.Location = new System.Drawing.Point(400, 48);
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
            // TextProxies
            // 
            this.TextProxies.Location = new System.Drawing.Point(330, 355);
            this.TextProxies.Name = "TextProxies";
            this.TextProxies.Size = new System.Drawing.Size(131, 23);
            this.TextProxies.TabIndex = 9;
            this.TextProxies.Text = "Test Proxies";
            this.TextProxies.UseVisualStyleBackColor = true;
            // 
            // CancelTest
            // 
            this.CancelTest.Location = new System.Drawing.Point(483, 355);
            this.CancelTest.Name = "CancelTest";
            this.CancelTest.Size = new System.Drawing.Size(131, 23);
            this.CancelTest.TabIndex = 10;
            this.CancelTest.Text = "Cancel";
            this.CancelTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 392);
            this.Controls.Add(this.CancelTest);
            this.Controls.Add(this.TextProxies);
            this.Controls.Add(this.ThreadsAmount);
            this.Controls.Add(this.ThreadsText);
            this.Controls.Add(this.URLText);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.SaveFailedProxies);
            this.Controls.Add(this.SaveWorkingProxies);
            this.Controls.Add(this.LoadFile);
            this.Controls.Add(this.ProxyGridView);
            this.Name = "MainForm";
            this.Text = "LeeSkiBee\'s Proxy Checker";
            ((System.ComponentModel.ISupportInitialize)(this.ProxyGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProxyGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proxy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button LoadFile;
        private System.Windows.Forms.Button SaveWorkingProxies;
        private System.Windows.Forms.Button SaveFailedProxies;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label URLText;
        private System.Windows.Forms.Label ThreadsText;
        private System.Windows.Forms.NumericUpDown ThreadsAmount;
        private System.Windows.Forms.Button TextProxies;
        private System.Windows.Forms.Button CancelTest;
    }
}

