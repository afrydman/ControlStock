using System;
using System.Windows.Forms;
using Repository.Repositories.UsuarioRepository;
using Services.UsuarioService;

namespace InstalationsUtils
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //usuario.ComputeHash(,"SHA512",)
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var usuarioService = new UsuarioService(new UsuarioRepository());
            txtResult.Text = usuarioService.getHash(txtPwd.Text);
        }
    }
}
