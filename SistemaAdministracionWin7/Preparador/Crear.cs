using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using Persistence;

namespace Preparador
{
    public partial class Crear : Form
    {
        public Crear()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {

            lstBases.DataSource = Conexion.listAllDb();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection c = null;
            if (CreateDB(txtdb.Text))
            {
                
                SqlConnectionStringBuilder conStrbuilder = new SqlConnectionStringBuilder();
                try
                {


                    conStrbuilder.DataSource = ConfigurationManager.AppSettings["DataSource"];
                    conStrbuilder.InitialCatalog = txtdb.Text;
                    conStrbuilder.UserID = ConfigurationManager.AppSettings["User"];
                    conStrbuilder.Password = ConfigurationManager.AppSettings["Password"];
                    conStrbuilder.TrustServerCertificate = true;
                    c = new SqlConnection(conStrbuilder.ConnectionString);
                    c.Open();
                }
                catch (Exception ee)
                {

                }


                executeCodeInDB(txtdb.Text, c);
            }
        }

        private bool CreateDB(string newDB)
        {
            bool task = Conexion.ExecuteNonQuery("Create database " + newDB , null, false);

            if (task)
            {
                MessageBox.Show("Db Creada correctamente");
            }
            else
            {
                MessageBox.Show("Error en crear Db");

            }
            return task;
        }

        private void executeCodeInDB(string db,SqlConnection c)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                // Open the selected file to read.


                try
                {
                    string text = System.IO.File.ReadAllText(@openFileDialog1.FileName);

                    
               
                            Conexion.ExcuteText(text, null, c);
                
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }


            }
        }
    }
}
