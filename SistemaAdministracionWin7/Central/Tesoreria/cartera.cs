using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ChequeRepository;
using Services.ChequeService;
using System.Drawing;

namespace Central.Tesoreria
{
    public partial class cartera : Form
    {
        public cartera()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chequera_Load(object sender, EventArgs e)
        {

            cargarEstados();

            cargarCarteras();

        }



        private void cargarEstados()
        {
            string[] array = Enum.GetNames(typeof(EstadoCheque));
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = "-";

            cmbFiltroEstado.DataSource = array;
            cmbFiltroEstado.Text = "-";
        }

        private void cargarCarteras()
        {

            
            var chequeService = new ChequeService(new ChequeRepository());
            List<ChequeData> cs = chequeService.GetChequesTercero(true, null,true);

            cs = AplicarFiltros(cs);

            cargarTabla(cs);

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

            if (cmbFiltroEstado.Text != "-")
            {
                cs = cs.FindAll(delegate(ChequeData c) { return c.EstadoCheque.ToString() == cmbFiltroEstado.Text; });
            }




            return cs;



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
                    tabla[1, fila].Value = c.Interno.ToString();
                    tabla[2, fila].Value = c.BancoEmisor.Description;
                    tabla[3, fila].Value = c.Numero;
                    
                    tabla[4, fila].Value = c.FechaCobro;
                    tabla[5, fila].Value = c.Monto;
                    tabla[6, fila].Value = c.EstadoCheque.ToString();


                    if (c.EstadoCheque == EstadoCheque.Anulado || c.EstadoCheque==EstadoCheque.Rechazado)
                        tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
               
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            cargarCarteras();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            var chequeService = new ChequeService(new ChequeRepository());

            if (tabla.SelectedCells.Count > 0)
            {
                ChequeData c = chequeService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                padre.AbrirForm(new Central.Tesoreria.verCheque(c), this.MdiParent, true);

            }
        }



    }
}
