using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.Repositories.BancosRepository;
using Services.BancoService;

namespace Central.Tesoreria
{
    public partial class banco : Form
    {
        public banco()
        {
            InitializeComponent();
        }

        private void banco_Load(object sender, EventArgs e)
        {
            cargarBancos();
        }

        private void cargarBancos()
        {
            txtBancoEmisor.Text = "";
            cargarBancos("");
        }
        private void cargarBancos(string search)
        {
            limparTabla();
            var bancoService = new BancoService(new BancoRepository());
            List<BancoData> bs = bancoService.GetAll(true);

            if (bs!=null && bs.Count > 0)
            {
                bs = bs.FindAll(delegate(BancoData b) { return b.Description.ToLower().StartsWith(search.ToLower()); });
                bs.Sort(delegate(BancoData x, BancoData y)
                {
                    return x.Description.CompareTo(y.Description);
                });

                foreach (BancoData c in bs)
                {
                    tabla.Rows.Add();
                    int fila;
                    fila = tabla.RowCount - 1;
                    //id nombre codigo
                    tabla[0, fila].Value = c.ID;
                    tabla[1, fila].Value = c.Description;

                    tabla.ClearSelection();
                }
            }
            
        }

        private void limparTabla()
        {
            tabla.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valido())
            {
                BancoData b = new BancoData();
                b.Description = txtBancoEmisor.Text;
                b.Enable = true;
                var bancoService = new BancoService(new BancoRepository());


                bool task = bancoService.Insert(b);
                if (task)
                {
                    MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarBancos();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            else
            {
                MessageBox.Show("Verifique los datos ingresados", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool valido()
        {
            if (txtBancoEmisor.Text=="")
            {
                return false;
            }

            return true;
        }

        private void txtBancoEmisor_TextChanged(object sender, EventArgs e)
        {
            cargarBancos(txtBancoEmisor.Text);
        }
    }
}
