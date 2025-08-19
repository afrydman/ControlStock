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
using Repository.Repositories.ValeRepository;
using Services;
using Services.AdministracionService;
using Services.CajaService;
using Services.IngresoService;
using Services.PersonalService;
using Services.UsuarioService;
using Services.ValeService;


namespace SharedForms.Impositivo
{
    public partial class ingreso : Form
    {
        public ingreso()
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
            var ingresoService = new IngresoService(new IngresoRepository());

            IngresoData r = ingresoService.GetLast(HelperService.IDLocal, HelperService.Prefix);
            r.Numero += 1;
            lblNum.Text = r.Show;
        }

        private void cargarRetiros()
       {

           tablaRetiros.DataSource = null;
           tablaRetiros.Rows.Clear();
           var ingresoService = new IngresoService(new IngresoRepository());

           List<IngresoData> retiros = ingresoService.GetByRangoFecha(DateTime.Now.Date.AddDays(-1).AddHours(23).AddMinutes(59), DateTime.Now.Date.AddDays(1), HelperService.IDLocal, HelperService.Prefix,false);

           foreach (IngresoData r in retiros)
            {

                tablaRetiros.Rows.Add();
                int fila;
                fila = tablaRetiros.RowCount - 1;
                //id tipo Monto descripcion
                tablaRetiros[0, fila].Value = r.ID;
                tablaRetiros[1, fila].Value = r.Show;
                
                tablaRetiros[2, fila].Value = r.TipoIngreso.Description;
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
            var tipoIngresoService = new TipoIngresoService(new TipoIngresoRepository());
            List<TipoIngresoData> tipos = tipoIngresoService.GetAll(true, false);
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
            
            
            if (retiroValido())
            {

                IngresoData n = new IngresoData();
            n.Date = DateTime.Now;
            n.fechaUso = DateTime.Now;
            n.Local.ID = HelperService.IDLocal;
                
            n.Monto = HelperService.ConvertToDecimalSeguro(txtmonto.Text);
            n.Description = txtdescr.Text;
            n.TipoIngreso.ID = ((TipoIngresoData)cmbTipoRetiro.SelectedItem).ID;
            n.Personal.ID = ((PersonalData)cmbvendedores.SelectedItem).ID;
            n.Prefix = HelperService.Prefix;
            n.Numero = Convert.ToInt32(lblNum.Text.Split('-')[1]);
            n.Enable = true;
            var ingresoService = new IngresoService(new IngresoRepository());

            ingresoService.Insert(n);

                int task = -1;
            if (n.TipoIngreso.ID == HelperService.idVale)
            {
                var valeService = new ValeService(new ValeRepository());



              

                task = valeService.GenerarNuevo(n);
                if (task>0)
                {
                    MessageBox.Show("Vale correctamente generado \r\n Por El Monto de " + n.Monto.ToString() + "\r\n Numero de vale: " + task.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);    
                }
                else
                {
                    MessageBox.Show("Error al generar el vale " , "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                
            }

            limpiarControles();


            MessageBox.Show("Ingreso realizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                 var ingresoService = new IngresoService(new IngresoRepository());

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

                         task = ingresoService.Disable(new IngresoData(new Guid(tablaRetiros.Rows[tablaRetiros.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                         
                     }
                     else
                     {
                         MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);


                     }
                 }
                 else
                 {
                     task = ingresoService.Disable(new IngresoData(new Guid(tablaRetiros.Rows[tablaRetiros.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                     
                 }

                 if (task)
                 {
                     MessageBox.Show("Ingreso anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         limpiarControles();
                         cargarRetiros();
                 }
                 else
                 {
                     MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                 








                 
             }
            }
        }
        private void tablaRetiros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
