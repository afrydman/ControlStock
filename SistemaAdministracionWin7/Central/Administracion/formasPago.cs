using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.FormaPagoRepository;
using Services;
using Services.FormaPagoService;

namespace Central
{
    public partial class formasPago : Form
    {
        public formasPago()
        {
            InitializeComponent();
        }


        private const string ACTUALIZAR = "Actualizar";
        private const string AGREGAR = "Agregar";
        private void formasPago_Load(object sender, EventArgs e)
        {
            tunearTabla();
            limpiarControles();
            cargarFormasPago();
            
        }

        private void limpiarControles(bool limpioTabla)
        {
            if (limpioTabla)
            {
                tabla.Rows.Clear();
                tabla.ClearSelection();    
            }
            
            txtNombre.Text = "";
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
            txt9.Text = "";
            txt10.Text = "";
            txt11.Text = "";
            txt12.Text = ""; 
        }
        private void limpiarControles()
        {

            limpiarControles(true);

        }

        private void cargarFormasPago()
        {
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            List<FormaPagoData> fps = formaPagoService.GetAll();

            foreach (FormaPagoData f in fps)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //id nombre Codigo
                tabla[0, fila].Value = f.ID;
                tabla[1, fila].Value = f.Description;
                tabla[2, fila].Value = f.Credito;

                tabla[3, fila].Value = !f.Enable? "Anulado" : "No Anulado";

                

                if (f.Cuotas != null && f.Cuotas.Count > 0)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        tabla[4 + i, fila].Value = f.Cuotas[i].Aumento;
                    }
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        tabla[4 + i, fila].Value = "-";
                    }
                }

                
                
                tabla.ClearSelection();
            }
        }

        private void tunearTabla()
        {
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                column.Width = 50;
            }
            tabla.Columns[1].Width = 150;
            tabla.Columns[2].Width = 100;
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormaPagoData f = new FormaPagoData();
            f.ID= new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            f.Description = tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            List<decimal> l = new List<decimal>();

            FormaPagoCuotaData fcuota;
            List<FormaPagoCuotaData> cuotas = new List<FormaPagoCuotaData>();
            for (int i = 0; i < 12; i++)
            {

                fcuota = new FormaPagoCuotaData();
                fcuota.Aumento =
                    HelperService.ConvertToDecimalSeguro(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[i + 4].Value);
                fcuota.Cuota = i + 1;
                fcuota.FatherID = f.ID;

                cuotas.Add(fcuota);
            }
            f.Credito = Convert.ToBoolean(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[2].Value);
            f.Cuotas = cuotas;
            idF = f.ID;
            cargarControl(f);
            button1.Text = ACTUALIZAR;

        }


        Guid idF = new Guid();
        private void cargarControl(FormaPagoData f)
        {
            txtNombre.Text = f.Description;
            checkTarjeta.Checked = f.Credito;
            txt1.Text=f.Cuotas[0].Aumento.ToString();
            txt2.Text = f.Cuotas[1].Aumento.ToString();
            txt3.Text = f.Cuotas[2].Aumento.ToString();
            txt4.Text = f.Cuotas[3].Aumento.ToString();
            txt5.Text = f.Cuotas[4].Aumento.ToString();
            txt6.Text = f.Cuotas[5].Aumento.ToString();
            txt7.Text = f.Cuotas[6].Aumento.ToString();
            txt8.Text = f.Cuotas[7].Aumento.ToString();
            txt9.Text = f.Cuotas[8].Aumento.ToString();
            txt10.Text = f.Cuotas[9].Aumento.ToString();
            txt11.Text = f.Cuotas[10].Aumento.ToString();
            txt12.Text = f.Cuotas[11].Aumento.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                if (tabla.SelectedCells.Count != 1)
                {
                    MessageBox.Show("Debe seleccionar una sola casilla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString().ToLower().StartsWith("efe"))
                    {
                        MessageBox.Show("No puede anular el efectivo!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        anularFp(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        MessageBox.Show("Actualizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarControles();
                        cargarFormasPago();
                        button1.Text = AGREGAR;
                        idF = new Guid();        
                    }

                    
                }
                
            }
            
        }

        private void anularFp(Guid idfp)
        {
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(),new FormaPagoCuotasRepository());
            formaPagoService.Disable(new FormaPagoData(idfp));
        }


        
        private void button2_Click(object sender, EventArgs e)
        {
            limpiarControles(false);
            button1.Text = AGREGAR;
            idF = new Guid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validoParaInsertar())
            {
                FormaPagoData f = new FormaPagoData();
                f.Description = txtNombre.Text;
                f.Credito = checkTarjeta.Checked;
                f.Enable = true;

                List<decimal> aumentos = new List<decimal>();
                FormaPagoCuotaData fcuota;
                List<FormaPagoCuotaData> cuotas = new List<FormaPagoCuotaData>();
                if (f.Credito)
                {



                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt1.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt2.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt3.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt4.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt5.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt6.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt7.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt8.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt9.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt10.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt11.Text));
                    aumentos.Add(HelperService.ConvertToDecimalSeguro(txt12.Text));

                    for (int i = 0; i < 12; i++)
                    {

                        fcuota = new FormaPagoCuotaData();
                        fcuota.Aumento = aumentos[i];
                        fcuota.Cuota = i + 1;
                        fcuota.FatherID = f.ID;

                        cuotas.Add(fcuota);
                    }

                    f.Cuotas = cuotas;
                    
                }






                bool task = false;
                var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
                if (button1.Text ==AGREGAR)
                {
                    
                    task = formaPagoService.Insert(f);
                }
                else
                {
                    f.ID = idF;
                    task = formaPagoService.Update(f);
                }
                
                if(task)
                    MessageBox.Show("Actualizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiarControles();
                cargarFormasPago();
                button1.Text = AGREGAR;
            }
            else
            {
                MessageBox.Show("Corrija los datos para confirmar la operacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private bool validoParaInsertar()
        {
            bool valido = true;

            if (txt1.Text=="")
            {
                txt1.Text = "0";
            }
            if (txt2.Text == "")
            {
                txt2.Text = "0";
            }
            if (txt3.Text == "")
            {
                txt3.Text = "0";
            }
            if (txt4.Text == "")
            {
                txt4.Text = "0";
            }
            if (txt5.Text == "")
            {
                txt5.Text = "0";
            }
            if (txt6.Text == "")
            {
                txt6.Text = "0";
            }
            if (txt7.Text == "")
            {
                txt7.Text = "0";
            }
            if (txt8.Text == "")
            {
                txt8.Text = "0";
            }
            if (txt9.Text == "")
            {
                txt9.Text = "0";
            }
            if (txt10.Text == "")
            {
                txt10.Text = "0";
            }
            if (txt11.Text == "")
            {
                txt11.Text = "0";
            }
            if (txt12.Text == "")
            {
                txt12.Text = "0";
            }



            if (HelperService.ConvertToDecimalSeguro(txt1.Text) < 0)
            {
                valido = false;    
            }
            if (HelperService.ConvertToDecimalSeguro(txt2.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt3.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt4.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt5.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt6.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt7.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt8.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt9.Text) <0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt10.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt11.Text) < 0)
            {
                valido = false;
            }
            if (HelperService.ConvertToDecimalSeguro(txt12.Text) < 0)
            {
                valido = false;
            }



            if (HelperService.IsProtectedPayment(idF))
            {
                MessageBox.Show("Esta forma de pago esta protegida, contactese con un Administrador.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                valido = false;
            }


            return valido;

        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt1,e);
        }

        private void validamoloqueponemo(TextBox txt1, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txt1,e);
            //if ((e.KeyChar == '.' && (txt1.Text.IndexOf(".") > -1 || txt1.Text.IndexOf(",") > -1)) || (e.KeyChar == ',' && (txt1.Text.IndexOf(",") > -1 || txt1.Text.IndexOf(".") > -1)) || (char.IsLetter(e.KeyChar)))
            //{
            //    e.Handled = true;
            //}
        }

        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt2, e);
        }

        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt3, e);
        }

        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt4, e);
        }

        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt5, e);
        }

       

        private void txt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt6, e);
        }

        private void txt7_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt7, e);
        }

        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt8, e);
        }

        private void txt9_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt9, e);
        }

        private void txt10_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt10, e);
        }

        private void txt11_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt11, e);
        }

        private void txt12_KeyPress(object sender, KeyPressEventArgs e)
        {
            validamoloqueponemo(txt12, e);
        }

        private void checkTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            pongoEnableLosText(checkTarjeta.Checked);
        }

        private void pongoEnableLosText(bool p)
        {
            txt1.Enabled = p;
            txt2.Enabled = p;
            txt3.Enabled = p;
            txt4.Enabled = p;
            txt5.Enabled = p;
            txt6.Enabled = p;
            txt7.Enabled = p;
            txt8.Enabled = p;
            txt9.Enabled = p;
            txt10.Enabled = p;
            txt11.Enabled = p;
            txt12.Enabled = p;
            
        }
    }
}
