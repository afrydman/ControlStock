using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.UsuarioRepository;
using Services;
using Services.LocalService;
using Services.RemitoService;
using Services.UsuarioService;

namespace SharedForms.Stock
{
    public partial class verEnviosStock : Form
    {
        public verEnviosStock()
        {
            InitializeComponent();
        }

        private void verEnviosStock_Load(object sender, EventArgs e)
        {
            CargarLocales();
            CargarEstados();

            
        }

        private void CargarEstados()
        {

            var values = Enum.GetValues(typeof(remitoEstado));

            string[] array = Enum.GetNames(typeof(remitoEstado));
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = "-";

            cmbFiltroEstado.DataSource = array;
            cmbFiltroEstado.Text = "-";

        }

        private void CargarLocales()
        {
            LocalData aux = new LocalData();
            aux.Codigo = "-";
            aux.ID = Guid.Empty;

            var localService = new LocalService(new LocalRepository());
            List<LocalData> locales = localService.GetAll();
            List<LocalData> locales2 = localService.GetAll();
            
            
            
            locales.Add(aux);
            locales2.Add(aux);


            cmbDestino.DataSource = locales;
            cmbDestino.DisplayMember = "Codigo";


            cmbOrigen.DataSource = locales2;
            cmbOrigen.DisplayMember = "Codigo";


            cmbOrigen.Text = "-";
            cmbDestino.Text = "-";
        }

        private void cargarRemitos()
        {

            var localService = new LocalService(new LocalRepository());
            List<LocalData> locales = localService.GetAll();
            List<RemitoData> remitos = new List<RemitoData>();
            var remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());

            if (cmbOrigen.Text=="-")
            {
                foreach (LocalData l in locales)
                {
                    remitos.AddRange(remitoService.GetByLocalOrigen(l.ID,false,true));
                }
            }
            else
            {
                remitos.AddRange(remitoService.GetByLocalOrigen(((LocalData)cmbOrigen.SelectedItem).ID,false,true));
            }
            remitos.Sort(delegate(RemitoData x, RemitoData y)
            {//inverso?
                return x.Date.CompareTo(y.Date);
            });


            if (cmbDestino.Text != "-")
            {
                remitos = remitos.FindAll(data => data.LocalDestino.ID == ((LocalData) cmbDestino.SelectedItem).ID);
            }

            if (cmbFiltroEstado.Text!="-")
            {
                remitos = remitos.FindAll(data => data.estado.ToString() == cmbFiltroEstado.Text);
            }


            if (checkGenerado.Checked)
            {
                remitos =
                    remitos.FindAll(
                        data => data.Date > generadoDesde.Value && data.Date < generadoHasta.Value);
            }

            if (checkRecibo.Checked)
            {
                remitos =
                    remitos.FindAll(
                        data => data.FechaRecibo > recibidoDesde.Value && data.FechaRecibo < recibidoHastsa.Value);
            }

            int fila;
            foreach (RemitoData r in remitos)
            {
                tabla.Rows.Add();

                fila = tabla.RowCount - 1;
                
                tabla[0, fila].Value = r.ID;
                tabla[1, fila].Value = r.Show;
                tabla[2, fila].Value = r.Date;


                tabla[3, fila].Value = r.Local.Codigo;
                tabla[4, fila].Value = r.LocalDestino.Codigo;
                
                tabla[5, fila].Value = (r.estado==remitoEstado.Enviado)?"-":r.FechaRecibo.ToString();
                tabla[6, fila].Value = r.estado;
                tabla[7, fila].Value = r.CantidadTotal.ToString();

                
                tabla.ClearSelection();

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiarTabla();
            cargarRemitos();
        }

        private void limpiarTabla()
        {
           tabla.Rows.Clear();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());
            if (tabla.SelectedCells.Count > 0)
            {
                RemitoData v = remitoService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                padreBase.AbrirForm(new verEnvio(v), this.MdiParent, true);
                //mostrarCambio(v);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());
            RemitoData remito;
            if (tabla.SelectedCells.Count > 0 && tabla[5, tabla.SelectedCells[0].RowIndex].Value.ToString() != "Anulado" && tabla[5, tabla.SelectedCells[0].RowIndex].Value.ToString() != "")
            {


                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {

                    remito = remitoService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                    if (!remito.Enable)
                    {
                        MessageBox.Show("El remito ya fue anulado anteriormente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {



                        if (remito.Local.ID != HelperService.IDLocal)//si no se creo aca no se tiene q poder
                        {
                            MessageBox.Show("El remito solo puede ser eliminado en el Local donde se genero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {// se creo aca
                            if (remito.LocalDestino.ID != HelperService.IDLocal && remito.FechaRecibo > DateTime.Parse("01/01/1900"))
                            {//se recibio entonces no
                                MessageBox.Show("El remito solo puede ser eliminado si aun no ha sido recibido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {

                                string resultado = "";
                                var usuarioService = new UsuarioService(new UsuarioRepository());
            
                                helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);
                                if (usuarioService.VerificarPermiso(resultado))
                                {

                                    bool task = remitoService.Disable(remito.ID, remito.LocalDestino.ID == HelperService.IDLocal);    //es una baja de central, pq altas solo son en comprasProveedores

                                    if (task)
                                    {
                                        MessageBox.Show("Anulado Exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        tabla[6, tabla.SelectedCells[0].RowIndex].Value = "Anulado";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ocurrio un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione solo un remito para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
