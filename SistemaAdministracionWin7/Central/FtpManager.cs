using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.FtpService;

namespace Central
{
    public partial class FtpManager : Form
    {
        private CentralExportService _exportService;

        public FtpManager()
        {
            InitializeComponent();
            _exportService = new CentralExportService();
        }

        private async void btnExportGlobal_Click(object sender, EventArgs e)
        {
            try
            {
                btnExportGlobal.Enabled = false;
                btnExportGlobal.Text = "Exporting...";
                lblStatus.Text = "Exporting global data...";
                
                Application.DoEvents();

                bool success = await Task.Run(() => _exportService.ExportGlobalData());

                if (success)
                {
                    lblStatus.Text = "Global data exported successfully!";
                    MessageBox.Show("Global data exported successfully to FTP server.", "Export Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Global data export failed!";
                    MessageBox.Show("Failed to export global data. Check logs for details.", "Export Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Export error occurred!";
                MessageBox.Show($"Error during export: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExportGlobal.Enabled = true;
                btnExportGlobal.Text = "Export Global Data";
            }
        }

        private async void btnExportStore_Click(object sender, EventArgs e)
        {
            if (cmbStores.SelectedValue == null)
            {
                MessageBox.Show("Please select a store first.", "No Store Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnExportStore.Enabled = false;
                btnExportStore.Text = "Exporting...";
                lblStatus.Text = "Exporting store-specific data...";
                
                Application.DoEvents();

                Guid storeId = (Guid)cmbStores.SelectedValue;
                bool success = await Task.Run(() => _exportService.ExportStoreSpecificData(storeId));

                if (success)
                {
                    lblStatus.Text = "Store data exported successfully!";
                    MessageBox.Show("Store-specific data exported successfully.", "Export Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Store data export failed!";
                    MessageBox.Show("Failed to export store data. Check logs for details.", "Export Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Export error occurred!";
                MessageBox.Show($"Error during export: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExportStore.Enabled = true;
                btnExportStore.Text = "Export Store Data";
            }
        }

        private void FtpManager_Load(object sender, EventArgs e)
        {
            LoadStores();
            lblStatus.Text = "Ready";
        }

        private void LoadStores()
        {
            // TODO: Load stores from your LocalData/LocalService
            // For now, add sample data
            cmbStores.DisplayMember = "Name";
            cmbStores.ValueMember = "ID";
            
            var stores = new[]
            {
                new { ID = Guid.NewGuid(), Name = "Store 001 - Downtown" },
                new { ID = Guid.NewGuid(), Name = "Store 002 - Mall" },
                new { ID = Guid.NewGuid(), Name = "Store 003 - Airport" }
                // Add your actual stores here
            };
            
            cmbStores.DataSource = stores;
        }
    }
}