using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Services.FtpService;

namespace Central
{
    public partial class TransferStatusManager : Form
    {
        private CentralExportService _exportService;
        private Timer _refreshTimer;

        public TransferStatusManager()
        {
            InitializeComponent();
            _exportService = new CentralExportService();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 30000; // Refresh every 30 seconds
            _refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void TransferStatusManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadTransferData();
            LoadStatistics();
        }

        private void SetupDataGridView()
        {
            dgvTransfers.AutoGenerateColumns = false;
            dgvTransfers.Columns.Clear();

            // Add columns
            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileName",
                HeaderText = "File Name",
                DataPropertyName = "FileName",
                Width = 200
            });

            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileType",
                HeaderText = "Type",
                DataPropertyName = "FileType",
                Width = 80
            });

            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Destination",
                HeaderText = "Destination",
                DataPropertyName = "DestinationDisplay",
                Width = 150
            });

            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "StatusDisplay",
                Width = 100
            });

            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                HeaderText = "Created",
                DataPropertyName = "CreatedDate",
                Width = 120,
                DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm" }
            });

            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileSize",
                HeaderText = "Size",
                DataPropertyName = "FileSize",
                Width = 80,
                DefaultCellStyle = { Format = "N0" }
            });

            dgvTransfers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RetryCount",
                HeaderText = "Retries",
                DataPropertyName = "RetryCount",
                Width = 60
            });

            dgvTransfers.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Action",
                HeaderText = "Action",
                Text = "Retry",
                UseColumnTextForButtonValue = true,
                Width = 80
            });

            // Set row colors based on status
            dgvTransfers.CellFormatting += DgvTransfers_CellFormatting;
            dgvTransfers.CellClick += DgvTransfers_CellClick;
        }

        private void DgvTransfers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var transfer = dgvTransfers.Rows[e.RowIndex].DataBoundItem as TransferFileData;
            if (transfer == null) return;

            var row = dgvTransfers.Rows[e.RowIndex];

            switch (transfer.Status)
            {
                case TransferStatus.Failed:
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    break;
                case TransferStatus.Uploaded:
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    break;
                case TransferStatus.Pending:
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    break;
                case TransferStatus.Uploading:
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    break;
                case TransferStatus.Processed:
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    break;
            }
        }

        private void DgvTransfers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvTransfers.Columns["Action"].Index && e.RowIndex >= 0)
            {
                var transfer = dgvTransfers.Rows[e.RowIndex].DataBoundItem as TransferFileData;
                if (transfer != null && transfer.CanRetry)
                {
                    RetryTransfer(transfer.ID);
                }
            }
        }

        private void LoadTransferData()
        {
            try
            {
                int days = (int)nudDays.Value;
                var transfers = _exportService.GetRecentTransfers(days);
                
                // Sort by creation date descending
                var sortedTransfers = transfers.OrderByDescending(t => t.CreatedDate).ToList();
                
                dgvTransfers.DataSource = sortedTransfers;
                lblTotalRecords.Text = $"Total Records: {transfers.Count}";
                
                UpdateActionButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transfer data: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                var stats = _exportService.GetTransferStatistics();
                
                lblTotalFiles.Text = $"Total: {stats.TotalFiles}";
                lblPendingFiles.Text = $"Pending: {stats.PendingFiles}";
                lblUploadedFiles.Text = $"Uploaded: {stats.UploadedFiles}";
                lblFailedFiles.Text = $"Failed: {stats.FailedFiles}";
                lblSuccessRate.Text = $"Success Rate: {stats.SuccessRate:F1}%";
                lblLastTransfer.Text = $"Last Transfer: {stats.LastTransfer:yyyy-MM-dd HH:mm}";
                
                // Update progress bar
                if (stats.TotalFiles > 0)
                {
                    int successCount = stats.UploadedFiles + stats.ProcessedFiles;
                    progressOverall.Value = Math.Min(100, (successCount * 100) / stats.TotalFiles);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading statistics: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateActionButtonStates()
        {
            var transfers = dgvTransfers.DataSource as System.Collections.Generic.List<TransferFileData>;
            if (transfers == null) return;

            btnRetryFailed.Enabled = transfers.Any(t => t.CanRetry);
            btnCleanup.Enabled = transfers.Any(t => t.Status == TransferStatus.Processed || t.Status == TransferStatus.Expired);
        }

        private void RetryTransfer(Guid transferId)
        {
            try
            {
                bool success = _exportService.RetryFailedTransfer(transferId);
                if (success)
                {
                    MessageBox.Show("Transfer retry initiated successfully.", "Retry Started", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTransferData();
                    LoadStatistics();
                }
                else
                {
                    MessageBox.Show("Failed to retry transfer.", "Retry Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrying transfer: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTransferData();
            LoadStatistics();
        }

        private void btnRetryFailed_Click(object sender, EventArgs e)
        {
            var transfers = dgvTransfers.DataSource as System.Collections.Generic.List<TransferFileData>;
            if (transfers == null) return;

            var failedTransfers = transfers.Where(t => t.CanRetry).ToList();
            if (failedTransfers.Count == 0)
            {
                MessageBox.Show("No failed transfers available for retry.", "No Failed Transfers", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Retry {failedTransfers.Count} failed transfers?", "Confirm Retry", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int successCount = 0;
                foreach (var transfer in failedTransfers)
                {
                    if (_exportService.RetryFailedTransfer(transfer.ID))
                        successCount++;
                }

                MessageBox.Show($"Initiated retry for {successCount} of {failedTransfers.Count} transfers.", 
                    "Retry Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LoadTransferData();
                LoadStatistics();
            }
        }

        private void btnCleanup_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Clean up old processed transfers (30+ days)?", "Confirm Cleanup", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int cleanedCount = _exportService.CleanupOldTransfers(30);
                    MessageBox.Show($"Cleaned up {cleanedCount} old transfers.", "Cleanup Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadTransferData();
                    LoadStatistics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during cleanup: {ex.Message}", "Cleanup Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void nudDays_ValueChanged(object sender, EventArgs e)
        {
            LoadTransferData();
        }

        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            _refreshTimer.Enabled = chkAutoRefresh.Checked;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadTransferData();
            LoadStatistics();
        }

        private void btnFilterStatus_Click(object sender, EventArgs e)
        {
            var transfers = dgvTransfers.DataSource as System.Collections.Generic.List<TransferFileData>;
            if (transfers == null) return;

            var selectedStatus = cmbStatusFilter.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "All")
            {
                LoadTransferData(); // Reload all data
                return;
            }

            if (Enum.TryParse<TransferStatus>(selectedStatus, out TransferStatus status))
            {
                var filteredTransfers = transfers.Where(t => t.Status == status).ToList();
                dgvTransfers.DataSource = filteredTransfers;
                lblTotalRecords.Text = $"Filtered Records: {filteredTransfers.Count}";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}