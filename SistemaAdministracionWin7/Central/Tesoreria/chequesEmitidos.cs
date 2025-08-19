using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Services.ChequeraService;
using Services.ChequeService;

namespace Central.Tesoreria
{
    public partial class chequesEmitidos : Form
    {
        public chequesEmitidos()
        {
            InitializeComponent();
        }

        private void chequesEmitidos_Load(object sender, EventArgs e)
        {
            cargarEstados();
            cargarChequesEmitidos();

        }

        private void cargarEstados()
        {
            string[] array = Enum.GetNames(typeof(EstadoCheque));
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = "-";

            cmbFiltroEstado.DataSource = array;
            cmbFiltroEstado.Text = "-";
        }
        private List<ChequeData> AplicarFiltros(List<ChequeData> cs)
        {

            if (checkCobro.Checked)
            {
                cs = cs.FindAll(delegate(ChequeData c) { return c.FechaCobro <= cobroHasta.Value & c.FechaCobro >= cobroDesde.Value; });
            }
            if (checkEmision.Checked)
            {
                cs = cs.FindAll(delegate(ChequeData c) { return c.FechaEmision <= emisionHastsa.Value & c.FechaEmision >= emisionDesde.Value; });
            }
            if (checkGenerado.Checked)
            {
                cs = cs.FindAll(delegate(ChequeData c) { return c.Date <= generadoHasta.Value & c.FechaEmision >= generadoDesde.Value; });
            }

            if (cmbFiltroEstado.SelectedItem != null && cmbFiltroEstado.SelectedItem.ToString() != "-")
            {
                cs = cs.FindAll(delegate(ChequeData c) { return c.EstadoCheque.ToString() == cmbFiltroEstado.SelectedItem.ToString(); });
            }

            return cs;

        }

        private void cargarChequesEmitidos()
        {
            var chequeraService = new ChequeraService(new ChequeraRepository());
            var chequeService = new ChequeService(new ChequeRepository());

            List<ChequeraData> chequerasPropias = chequeraService.GetAll(false);

            List<ChequeData> cheques = new List<ChequeData>();
            foreach (ChequeraData chequera in chequerasPropias)
	        {
                cheques.AddRange(chequeService.GetByChequera(chequera.ID, false));
	        }


            cheques = AplicarFiltros(cheques);
            cargarTabla(cheques);
           



        }

        private void cargarTabla(List<ChequeData> cheques)
        {
            tabla.Rows.Clear();


            foreach (ChequeData c in cheques)
            {


                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //id nombre Codigo
                tabla[0, fila].Value = c.ID;
                tabla[1, fila].Value = c.Numero.ToString();
                tabla[2, fila].Value = c.Chequera.Show;
                tabla[3, fila].Value = c.BancoEmisor.Description ;
                tabla[4, fila].Value = c.FechaEmision;
                tabla[5, fila].Value = c.FechaCobro;
                tabla[6, fila].Value = c.Monto;
                tabla[7, fila].Value = c.EstadoCheque.ToString();


                if (!c.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;




            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            var chequeService = new ChequeService(new ChequeRepository());
            if (tabla.SelectedCells.Count > 0)
            {
                ChequeData c = chequeService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                padre.AbrirForm(new Central.Tesoreria.verCheque(c), this.MdiParent, true);
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cargarChequesEmitidos();
        }
    }
}
