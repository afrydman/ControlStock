using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.CuentaRepository;
using Services;
using Services.CajaService;
using Services.CuentaService;

namespace Central.Tesoreria
{
    public partial class cuentas : Form
    {
        public cuentas()
        {
            InitializeComponent();
        }

        private void cuentas_Load(object sender, EventArgs e)
        {
            cargarCuentas();
        }

        private void cargarCuentas()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cuentas = cuentaService.GetAll(false);
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.Red;
            style.ForeColor = Color.Black;

            var cajaService = new CajaService(new CajaRepository());

            foreach (CuentaData c in cuentas)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = c.ID;
                tabla[1, fila].Value = c.TipoCuenta.ToString();
                tabla[2, fila].Value = c.Description;
                if(c.Banco!=null)
                    tabla[3, fila].Value = c.Banco.Description;

                if (c.TipoCuenta == TipoCuenta.Otra && ! cajaService.IsClosed(DateTime.Now.Date,HelperService.IDLocal))
                {
                    tabla[4, fila].Style = style;
                }
                tabla[4, fila].Value = c.Saldo.ToString();
                tabla[5, fila].Value = c.Descubierto.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            
            if (tabla.SelectedCells.Count > 0)
            {
                CuentaData c = cuentaService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                mostrarCuenta(c);
            }
        }

        private void mostrarCuenta(CuentaData c)
        {
            padre.AbrirForm(new Tesoreria.verCuenta(c), this.MdiParent, true);
        }
    }
}
