using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.FtpService;

namespace PuntoVenta
{
    public partial class FtpImporter : Form
    {
        private PuntoVentaImportService _importService;

        public FtpImporter()
        {
            InitializeComponent();
            _importService = new PuntoVentaImportService();
        }

        private async void btnImportAll_Click(object sender, EventArgs e)
        {
            try
            {
                btnImportAll.Enabled = false;
                btnImportAll.Text = "Importing...";
                lblStatus.Text = "Importing data from FTP server...";
                
                Application.DoEvents();

                bool success = await Task.Run(() => _importService.ImportAllData());

                if (success)
                {
                    lblStatus.Text = "Data imported successfully!";
                    MessageBox.Show("Data imported successfully from FTP server.", "Import Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Data import failed!";
                    MessageBox.Show("Failed to import data. Check logs for details.", "Import Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Import error occurred!";
                MessageBox.Show($"Error during import: {ex.Message}", "Import Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnImportAll.Enabled = true;
                btnImportAll.Text = "Import All Data";
            }
        }

        private async void btnImportGlobal_Click(object sender, EventArgs e)
        {
            try
            {
                btnImportGlobal.Enabled = false;
                btnImportGlobal.Text = "Importing...";
                lblStatus.Text = "Importing global data...";
                
                Application.DoEvents();

                bool success = await Task.Run(() => _importService.ImportGlobalData());

                if (success)
                {
                    lblStatus.Text = "Global data imported successfully!";
                    MessageBox.Show("Global data imported successfully.", "Import Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Global data import failed!";
                    MessageBox.Show("Failed to import global data. Check logs for details.", "Import Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Import error occurred!";
                MessageBox.Show($"Error during import: {ex.Message}", "Import Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnImportGlobal.Enabled = true;
                btnImportGlobal.Text = "Import Global Data";
            }
        }

        private async void btnImportStore_Click(object sender, EventArgs e)
        {
            try
            {
                btnImportStore.Enabled = false;
                btnImportStore.Text = "Importing...";
                lblStatus.Text = "Importing store-specific data...";
                
                Application.DoEvents();

                bool success = await Task.Run(() => _importService.ImportStoreSpecificData());

                if (success)
                {
                    lblStatus.Text = "Store data imported successfully!";
                    MessageBox.Show("Store-specific data imported successfully.", "Import Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Store data import failed!";
                    MessageBox.Show("Failed to import store data. Check logs for details.", "Import Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Import error occurred!";
                MessageBox.Show($"Error during import: {ex.Message}", "Import Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnImportStore.Enabled = true;
                btnImportStore.Text = "Import Store Data";
            }
        }

        private void FtpImporter_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Ready";
            
            // Show store ID from config
            var storeId = System.Configuration.ConfigurationManager.AppSettings["idLocal"];
            this.Text = $"FTP Data Import - Store ID: {storeId}";
        }
    }
}