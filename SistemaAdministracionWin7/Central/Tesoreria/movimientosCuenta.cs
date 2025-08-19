using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.MovimientoRepository;
using Services;
using Services.CuentaService;
using Services.MovimientoCuentaService;
using System.Drawing;

namespace Central.Tesoreria
{
    public partial class movimientosCuenta : Form
    {
        public movimientosCuenta()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbCuentaDestino.SelectedIndex>0)
            {
                

                refresh2();
            }
            else
            {
                MessageBox.Show("Seleccione una cuenta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public List<MovimientoCuentaData> DedupCollection(List<MovimientoCuentaData> input)
        {
            
            List<MovimientoCuentaData> aux = new List<MovimientoCuentaData>();
            List<Guid> ids = new List<Guid>();

            // Relatively simple dupe check alg used as example
            foreach (MovimientoCuentaData item in input)
                if (!ids.Contains(item.ID))
                {
                    ids.Add(item.ID);
                    aux.Add(item);
                }
                    

            return aux;
        }
        public  void refresh2()
        {
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());
            List<MovimientoCuentaData> movimientos = movimientoCuentaService.GetbyCajaDestino(((CuentaData)cmbCuentaDestino.SelectedItem).ID);
            movimientos.AddRange(movimientoCuentaService.GetbyCajaOrigen(((CuentaData)cmbCuentaDestino.SelectedItem).ID));

            movimientos = DedupCollection(movimientos);
            List<string> aux = new List<string>();

            
            movimientos.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            
            
            
            cargoTabla(movimientos, ((CuentaData)cmbCuentaDestino.SelectedItem).ID);
        }


        decimal total = 0;
        private void cargoTabla(List<MovimientoCuentaData> movimientos,Guid idcuenta)
        {

            tabla.Rows.Clear();
            total = 0;
            int fila;
            foreach (MovimientoCuentaData m in movimientos)
            {
                tabla.Rows.Add();
              

                
                fila = tabla.RowCount - 1;
               
                tabla[0, fila].Value = m.ID;
                tabla[1, fila].Value = HelperService.convertToFechaHoraConFormato(m.Date);
                tabla[2, fila].Value = m.Show;
                if (m.cuentaDestino!=null && idcuenta == m.cuentaDestino.ID)
                {//es un deposito
                    if (m.cuentaOrigen.ID!=Guid.Empty)
	                {
		                 tabla[3, fila].Value = "Deposito proveniente de Cuenta: "+m.cuentaOrigen.Show;
	                }else
	                {
                        tabla[3, fila].Value = "Deposito proveniente de Cheque: " + m.cheque.Show;
	                }
                    
                    tabla[4, fila].Value = m.Monto;
                    tabla[6, fila].Value = !m.Enable ? "Si" : "No";
                    if (m.Enable)
                    {
                        total += m.Monto;

                    }
                    tabla[5, fila].Value = total;
                }
                else
                {//es una extraccion
                    if (m.cuentaDestino!= null && m.cuentaDestino.ID != Guid.Empty)
                    {
                        tabla[3, fila].Value = "Extraccion a  " + m.cuentaDestino.Show;
                    }
                    else
                    {
                        tabla[3, fila].Value = "Pago de cheque" + m.cheque.Show;
                    }

                    tabla[4, fila].Value = m.Monto;
                    tabla[6, fila].Value = !m.Enable ? "Si" : "No";
                    if (m.Enable)
                    {
                        total -= m.Monto;
                            
                    }
                    tabla[5, fila].Value = total;
                    



                }

                if (!m.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
            }


        }

        private void movimientosCuenta_Load(object sender, EventArgs e)
        {
            cargarCuentas();//solo bancaris?
        }

        private void cargarCuentas()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cuentasBanc = cuentaService.GetCuentasByTipo(TipoCuenta.Banco, true);

            

            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una cuenta";
            cuentasBanc.Insert(0, aux);
            cmbCuentaDestino.DataSource = cuentasBanc;
            cmbCuentaDestino.DisplayMember = "Show";
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());

            if (tabla.SelectedCells.Count > 0)
            {
                MovimientoCuentaData v = movimientoCuentaService.GetById(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                padre.AbrirForm(new verDeposito(v), this.MdiParent, true);
                //mostrarCambio(v);
            }
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
