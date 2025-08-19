using System;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Services;
using SharedForms.Admin;

namespace SharedForms
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("Acerca de ");
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Versión {0}", AssemblyVersion);
            
            this.labelVersion.Text = String.Format("Fecha Licencia {0}", ConfigurationManager.AppSettings["UpdateNet"]);
         
        }

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void labelVersion_Click(object sender, EventArgs e)
        {


            padreBase.AbrirFormAdmin(new verificandoStock(), this, true);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            StringBuilder st = new StringBuilder();
            st.AppendLine("");
            st.AppendLine("Conexion: ");
            st.AppendLine(ConfigurationManager.ConnectionStrings["Local"].ConnectionString);
            st.AppendLine("");
            st.AppendLine("Usuario: ");
            st.AppendLine(((GrupoCliente)HelperService.usuarioActual.Cliente).ToString());
            st.AppendLine("");

            st.AppendLine("Configuracion Mts: ");
            st.AppendLine(HelperService.haymts.ToString());

            textBoxDescription.Text = st.ToString();
        }
    }
}
