using DTO.BusinessEntities;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.LocalService;
using Services.ProductoService;
using Services.RemitoService;
using Services.VentaService;
using SharedForms.Stock;
using SharedForms.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharedForms.TransferFiles
{
    public partial class Transfers: Form
    {
        public Transfers()
        {
            InitializeComponent();
        }
        private List<LocalData> locales = null;
        private RemitoService remitoService = null;
        private void Transfers_Load(object sender, EventArgs e)
        {
           
            var localesService = new LocalService();
            remitoService = new RemitoService();
            locales = localesService.GetAll();
            CargarFromDB();

           
        }
    private void CargarFromDB()
        {
            var service = new TransferService();

           
            var data = service.GetAll();
            RemitoData r = null;
            foreach (FileTransferData item in data)
            {

                r = remitoService.GetByID(item.remitoID);
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                //Codigo
                tabla[0, fila].Value = item.remitoID;


                var localFrom = locales.Where(x => x.ID.ToString() == item.FromLocalID).FirstOrDefault();

                var localTo = locales.Where(x => x.ID.ToString() == item.ToLocalID).FirstOrDefault();


                tabla[1, fila].Value = r.Description;
                tabla[2, fila].Value = item.Date.ToShortDateString();
                tabla[3, fila].Value = r.estado;
                tabla[4, fila].Value = item.LocalFileName;
                tabla[5, fila].Value = r.Local.Description;
                tabla[6, fila].Value = r.LocalDestino.Description;


            }




            tabla.ClearSelection();
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count > 0)
            {
                var remitoService = new RemitoService();
                RemitoData r = remitoService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));


                mostrarRemito(r);
            }
        }

        private void mostrarRemito(RemitoData r)
        {
            padreBase.AbrirForm(new verEnvio(r), this.MdiParent, true);
        }
    }
}
