using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.UsuarioRepository;
using Services;
using Services.AdministracionService;
using Services.CajaService;
using Services.PersonalService;
using Services.RetiroService;
using Services.UsuarioService;


namespace SharedForms.Impositivo
{
    public partial class retiro : Form
    {
        public retiro()
        {
            InitializeComponent();
        }

        private void retiro_Load(object sender, EventArgs e)
        {
            cargarNumero();
            cargarVendedores();
            cargarTiposRetiro();
            cargarRetiros();


        }

        private void cargarNumero()
        {
            var retiroService = new RetiroService(new RetiroRepository());

            RetiroData r = retiroService.GetLast(HelperService.IDLocal, HelperService.Prefix);
            r.Numero += 1;
            lblNum.Text = r.Show;
        }

        private void cargarRetiros()
       {

           tablaRetiros.DataSource = null;
           tablaRetiros.Rows.Clear();
           var retiroService = new RetiroService(new RetiroRepository());

           List<RetiroData> retiros = retiroService.GetByRangoFecha(DateTime.Now.Date.AddDays(-1).AddHours(23).AddMinutes(59), DateTime.Now.Date.AddDays(1), HelperService.IDLocal, HelperService.Prefix,false);

           foreach (RetiroData r in retiros)
            {

                tablaRetiros.Rows.Add();
                int fila;
                fila = tablaRetiros.RowCount - 1;
                //id tipo Monto descripcion
                tablaRetiros[0, fila].Value = r.ID;
                tablaRetiros[1, fila].Value = r.Show;
                
                tablaRetiros[2, fila].Value = r.TipoRetiro.Description;
                tablaRetiros[3, fila].Value = r.Monto;
                tablaRetiros[4, fila].Value = r.Description;
                tablaRetiros[5, fila].Value = !r.Enable ? "Anulada" : "no anulada";

                if (!r.Enable)
                    tablaRetiros.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;


                tablaRetiros.ClearSelection();
            }
        }

        private void cargarTiposRetiro()
        {
            var tipoRetiroService = new TipoRetiroService(new TipoRetiroRepository());
            List<TipoRetiroData> tipos = tipoRetiroService.GetAll(true,false);
            cmbTipoRetiro.DisplayMember = "Description";
            cmbTipoRetiro.DataSource = tipos;
            cmbTipoRetiro.DisplayMember = "Description";
        }

        private void cargarVendedores()
        {
            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();
            cmbvendedores.DisplayMember = "nombrecontacto";
            cmbvendedores.DataSource = vendedores;
            cmbvendedores.DisplayMember = "nombrecontacto";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var retiroService = new RetiroService(new RetiroRepository());
            if (retiroValido())
            {

                RetiroData n = new RetiroData();
            n.Date = DateTime.Now;
            n.fechaUso = DateTime.Now;
            n.Local.ID = HelperService.IDLocal;
            
            n.Monto = HelperService.ConvertToDecimalSeguro(txtmonto.Text);
            n.Description = txtdescr.Text;
            n.TipoRetiro.ID = ((TipoRetiroData)cmbTipoRetiro.SelectedItem).ID;
            n.Personal.ID = ((PersonalData)cmbvendedores.SelectedItem).ID;
            n.Prefix = HelperService.Prefix;
            n.Numero = Convert.ToInt32(lblNum.Text.Split('-')[1]);
                n.Enable = true;
                retiroService.Insert(n);


            limpiarControles();


            MessageBox.Show("retiro realizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cargarNumero();
            cargarRetiros();
            }

            
        }

        private void limpiarControles()
        {
            txtdescr.Text = "";
            txtmonto.Text = "";
            cmbTipoRetiro.SelectedIndex = 0;
            tablaRetiros.DataSource = null;
            tablaRetiros.ClearSelection();
            tablaRetiros.Rows.Clear();
            

        }

        private bool retiroValido()
        {
            if (txtmonto.Text == "")
            {
                MessageBox.Show("Debe de ingresar un Monto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (HelperService.ConvertToDecimalSeguro(txtmonto.Text) <= 0)
            {
                MessageBox.Show("Debe de ingresar un Monto mayor a cero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            
            string resultado = "";
            bool needPass = false;
            var cajaService = new CajaService(new CajaRepository());

            if (cajaService.IsClosed(DateTime.Now.Date,HelperService.IDLocal))
            {
                needPass = true;
                helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);

            }
            var usuarioService = new UsuarioService(new UsuarioRepository());
            
            if (needPass)
            {
                if (usuarioService.VerificarPermiso(resultado))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtmonto, e);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tablaRetiros.SelectedCells.Count!=1)
            {
                MessageBox.Show("Debe seleccionar una sola casilla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { //id tipo Monto descripcion

                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

             if (dg == DialogResult.OK)
             {


                 bool needPass = false;
                 bool task = false;
                 string resultado = "";
                 var cajaService = new CajaService(new CajaRepository());
                 var retiroService = new RetiroService(new RetiroRepository());

                 if (cajaService.IsClosed(DateTime.Now,HelperService.IDLocal))
                 {
                     needPass = true;
                     helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);

                 }
                 var usuarioService = new UsuarioService(new UsuarioRepository());
            
                 if (needPass)
                 {
                     if (usuarioService.VerificarPermiso(resultado))
                     {

                         task = retiroService.Disable(new RetiroData(new Guid(tablaRetiros.Rows[tablaRetiros.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                         


                     }
                     else
                     {
                         MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);


                     }
                 }
                 else
                 {
                     task = retiroService.Disable(new RetiroData(new Guid(tablaRetiros.Rows[tablaRetiros.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                     

                 }

                 if (task)
                 {
                     MessageBox.Show("Retiro anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     limpiarControles();
                     cargarRetiros();
                 }
                 else
                 {
                     MessageBox.Show("Ocurrio un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }


                 
             }
            }
        }
        private void tablaRetiros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
