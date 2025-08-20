namespace Central
{
    partial class TransferStatusManager
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvTransfers = new System.Windows.Forms.DataGridView();
            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.lblTotalFiles = new System.Windows.Forms.Label();
            this.lblPendingFiles = new System.Windows.Forms.Label();
            this.lblUploadedFiles = new System.Windows.Forms.Label();
            this.lblFailedFiles = new System.Windows.Forms.Label();
            this.lblSuccessRate = new System.Windows.Forms.Label();
            this.lblLastTransfer = new System.Windows.Forms.Label();
            this.progressOverall = new System.Windows.Forms.ProgressBar();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnFilterStatus = new System.Windows.Forms.Button();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRetryFailed = new System.Windows.Forms.Button();
            this.btnCleanup = new System.Windows.Forms.Button();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfers)).BeginInit();
            this.groupBoxStats.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTransfers
            // 
            this.dgvTransfers.AllowUserToAddRows = false;
            this.dgvTransfers.AllowUserToDeleteRows = false;
            this.dgvTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransfers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransfers.Location = new System.Drawing.Point(12, 160);
            this.dgvTransfers.Name = "dgvTransfers";
            this.dgvTransfers.ReadOnly = true;
            this.dgvTransfers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransfers.Size = new System.Drawing.Size(960, 350);
            this.dgvTransfers.TabIndex = 0;
            // 
            // groupBoxStats
            // 
            this.groupBoxStats.Controls.Add(this.progressOverall);
            this.groupBoxStats.Controls.Add(this.lblLastTransfer);
            this.groupBoxStats.Controls.Add(this.lblSuccessRate);
            this.groupBoxStats.Controls.Add(this.lblFailedFiles);
            this.groupBoxStats.Controls.Add(this.lblUploadedFiles);
            this.groupBoxStats.Controls.Add(this.lblPendingFiles);
            this.groupBoxStats.Controls.Add(this.lblTotalFiles);
            this.groupBoxStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxStats.Location = new System.Drawing.Point(12, 12);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(400, 140);
            this.groupBoxStats.TabIndex = 1;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Transfer Statistics";
            // 
            // lblTotalFiles
            // 
            this.lblTotalFiles.AutoSize = true;
            this.lblTotalFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTotalFiles.Location = new System.Drawing.Point(15, 25);
            this.lblTotalFiles.Name = "lblTotalFiles";
            this.lblTotalFiles.Size = new System.Drawing.Size(52, 15);
            this.lblTotalFiles.TabIndex = 0;
            this.lblTotalFiles.Text = "Total: 0";
            // 
            // lblPendingFiles
            // 
            this.lblPendingFiles.AutoSize = true;
            this.lblPendingFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPendingFiles.Location = new System.Drawing.Point(15, 45);
            this.lblPendingFiles.Name = "lblPendingFiles";
            this.lblPendingFiles.Size = new System.Drawing.Size(65, 15);
            this.lblPendingFiles.TabIndex = 1;
            this.lblPendingFiles.Text = "Pending: 0";
            // 
            // lblUploadedFiles
            // 
            this.lblUploadedFiles.AutoSize = true;
            this.lblUploadedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblUploadedFiles.Location = new System.Drawing.Point(15, 65);
            this.lblUploadedFiles.Name = "lblUploadedFiles";
            this.lblUploadedFiles.Size = new System.Drawing.Size(71, 15);
            this.lblUploadedFiles.TabIndex = 2;
            this.lblUploadedFiles.Text = "Uploaded: 0";
            // 
            // lblFailedFiles
            // 
            this.lblFailedFiles.AutoSize = true;
            this.lblFailedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblFailedFiles.Location = new System.Drawing.Point(15, 85);
            this.lblFailedFiles.Name = "lblFailedFiles";
            this.lblFailedFiles.Size = new System.Drawing.Size(56, 15);
            this.lblFailedFiles.TabIndex = 3;
            this.lblFailedFiles.Text = "Failed: 0";
            // 
            // lblSuccessRate
            // 
            this.lblSuccessRate.AutoSize = true;
            this.lblSuccessRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSuccessRate.Location = new System.Drawing.Point(200, 25);
            this.lblSuccessRate.Name = "lblSuccessRate";
            this.lblSuccessRate.Size = new System.Drawing.Size(108, 15);
            this.lblSuccessRate.TabIndex = 4;
            this.lblSuccessRate.Text = "Success Rate: 0%";
            // 
            // lblLastTransfer
            // 
            this.lblLastTransfer.AutoSize = true;
            this.lblLastTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLastTransfer.Location = new System.Drawing.Point(200, 45);
            this.lblLastTransfer.Name = "lblLastTransfer";
            this.lblLastTransfer.Size = new System.Drawing.Size(84, 15);
            this.lblLastTransfer.TabIndex = 5;
            this.lblLastTransfer.Text = "Last Transfer: -";
            // 
            // progressOverall
            // 
            this.progressOverall.Location = new System.Drawing.Point(200, 85);
            this.progressOverall.Name = "progressOverall";
            this.progressOverall.Size = new System.Drawing.Size(180, 20);
            this.progressOverall.TabIndex = 6;
            // 
            // groupBoxFilters
            // 
            this.groupBoxFilters.Controls.Add(this.btnFilterStatus);
            this.groupBoxFilters.Controls.Add(this.cmbStatusFilter);
            this.groupBoxFilters.Controls.Add(this.label2);
            this.groupBoxFilters.Controls.Add(this.nudDays);
            this.groupBoxFilters.Controls.Add(this.label1);
            this.groupBoxFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxFilters.Location = new System.Drawing.Point(430, 12);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(280, 90);
            this.groupBoxFilters.TabIndex = 2;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Filters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Days:";
            // 
            // nudDays
            // 
            this.nudDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.nudDays.Location = new System.Drawing.Point(85, 23);
            this.nudDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(60, 21);
            this.nudDays.TabIndex = 1;
            this.nudDays.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudDays.ValueChanged += new System.EventHandler(this.nudDays_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Status:";
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Items.AddRange(new object[] {
            "All",
            "Pending",
            "Uploading",
            "Uploaded",
            "Failed",
            "Processed"});
            this.cmbStatusFilter.Location = new System.Drawing.Point(85, 52);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(100, 23);
            this.cmbStatusFilter.TabIndex = 3;
            // 
            // btnFilterStatus
            // 
            this.btnFilterStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnFilterStatus.Location = new System.Drawing.Point(200, 50);
            this.btnFilterStatus.Name = "btnFilterStatus";
            this.btnFilterStatus.Size = new System.Drawing.Size(60, 25);
            this.btnFilterStatus.TabIndex = 4;
            this.btnFilterStatus.Text = "Filter";
            this.btnFilterStatus.UseVisualStyleBackColor = true;
            this.btnFilterStatus.Click += new System.EventHandler(this.btnFilterStatus_Click);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.chkAutoRefresh);
            this.groupBoxActions.Controls.Add(this.btnCleanup);
            this.groupBoxActions.Controls.Add(this.btnRetryFailed);
            this.groupBoxActions.Controls.Add(this.btnRefresh);
            this.groupBoxActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxActions.Location = new System.Drawing.Point(730, 12);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(242, 90);
            this.groupBoxActions.TabIndex = 3;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRefresh.Location = new System.Drawing.Point(15, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 25);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRetryFailed
            // 
            this.btnRetryFailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRetryFailed.Location = new System.Drawing.Point(95, 25);
            this.btnRetryFailed.Name = "btnRetryFailed";
            this.btnRetryFailed.Size = new System.Drawing.Size(80, 25);
            this.btnRetryFailed.TabIndex = 1;
            this.btnRetryFailed.Text = "Retry Failed";
            this.btnRetryFailed.UseVisualStyleBackColor = true;
            this.btnRetryFailed.Click += new System.EventHandler(this.btnRetryFailed_Click);
            // 
            // btnCleanup
            // 
            this.btnCleanup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCleanup.Location = new System.Drawing.Point(185, 25);
            this.btnCleanup.Name = "btnCleanup";
            this.btnCleanup.Size = new System.Drawing.Size(50, 25);
            this.btnCleanup.TabIndex = 2;
            this.btnCleanup.Text = "Cleanup";
            this.btnCleanup.UseVisualStyleBackColor = true;
            this.btnCleanup.Click += new System.EventHandler(this.btnCleanup_Click);
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkAutoRefresh.Location = new System.Drawing.Point(15, 60);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(91, 17);
            this.chkAutoRefresh.TabIndex = 3;
            this.chkAutoRefresh.Text = "Auto Refresh";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalRecords.Location = new System.Drawing.Point(12, 520);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(108, 15);
            this.lblTotalRecords.TabIndex = 4;
            this.lblTotalRecords.Text = "Total Records: 0";
            // 
            // TransferStatusManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.dgvTransfers);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "TransferStatusManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Status Manager";
            this.Load += new System.EventHandler(this.TransferStatusManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfers)).EndInit();
            this.groupBoxStats.ResumeLayout(false);
            this.groupBoxStats.PerformLayout();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvTransfers;
        private System.Windows.Forms.GroupBox groupBoxStats;
        private System.Windows.Forms.Label lblTotalFiles;
        private System.Windows.Forms.Label lblPendingFiles;
        private System.Windows.Forms.Label lblUploadedFiles;
        private System.Windows.Forms.Label lblFailedFiles;
        private System.Windows.Forms.Label lblSuccessRate;
        private System.Windows.Forms.Label lblLastTransfer;
        private System.Windows.Forms.ProgressBar progressOverall;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnFilterStatus;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRetryFailed;
        private System.Windows.Forms.Button btnCleanup;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.Label lblTotalRecords;
    }
}