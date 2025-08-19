using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ReciboRepository;
using Services.ChequeraService;
using Services.ChequeService;
using Services.ReciboService;

namespace Central.Tesoreria
{
    public partial class avisosCheques : Form
    {
        public avisosCheques()
        {
            InitializeComponent();
        }

        private void avisosCheques_Load(object sender, EventArgs e)
        {
            refresh2();
        }

        public void refresh2() {

            limpioTablas();
            cargarEmitidos();
            cargarRecibidos();
        
        }

        private void limpioTablas()
        {
            tablaE.Rows.Clear();
            tablaR.Rows.Clear();
        }
        private void cargarRecibidos()
        {
            List<EstadoCheque> e = new List<EstadoCheque>();
            e.Add(EstadoCheque.EnCartera);
            var chequeService = new ChequeService(new ChequeRepository());

            List<ChequeData> cs = chequeService.GetChequesTercero(true, e, true);

            cs.Sort(delegate(ChequeData x, ChequeData y)
            {
                return DateTime.Compare(x.FechaCobro, y.FechaCobro);

            });
            
            cargarTabla(cs,tablaR,true);
        }


        private void cargarTabla(List<ChequeData> cheques, DataGridView tabla,bool recibidos)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.Red;
            style.ForeColor = Color.Black;

            var  reciboService = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());
            var OrdenPagoService = new Services.OrdenPagoService.OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());
            foreach (ChequeData c in cheques)
            {


                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                
                tabla[0, fila].Value = c.ID;
                

                if (c.Chequera.ID!=Guid.Empty && c.EstadoCheque==EstadoCheque.Entregado)
                {
                    
                }
                if (recibidos)
                {
                    tabla[1, fila].Value = c.Interno.ToString();
                    ReciboData recibo = reciboService.GetReciboDeCheque(c.ID);
                    if (recibo!=null)
                    {
                        tabla[2, fila].Value = recibo.tercero.RazonSocial;        
                    }
                    
                    
                    tabla[3, fila].Value = c.BancoEmisor.Description;
                }
                else
                {
                    tabla[1, fila].Value = c.Numero;
                    OrdenPagoData opago = OrdenPagoService.GetOrdenQueEntregoCheque(c.ID);
                    if (opago!=null)
                    {
                        tabla[2, fila].Value = opago.Tercero.RazonSocial;    
                    }
                    
                    tabla[3, fila].Value = c.Chequera.Show;
                }
               
                tabla[4, fila].Value = c.FechaEmision;
                
                tabla[5, fila].Value = c.FechaCobro;
                tabla[6, fila].Value = c.Monto;
                tabla[7, fila].Value = c.EstadoCheque.ToString();

              

                if (c.FechaCobro.Date<=DateTime.Now.Date)
                {
                    tabla[5, fila].Style = style;
                }
            }
        }

        private void cargarEmitidos()
        {
            var chequeService = new ChequeService(new ChequeRepository());
            var chequeraService = new ChequeraService(new ChequeraRepository());


            List<ChequeraData> chequerasPropias = chequeraService.GetAll(true);

            List<ChequeData> cheques = new List<ChequeData>();
            foreach (ChequeraData chequera in chequerasPropias)
            {
                cheques.AddRange(chequeService.GetByChequera(chequera.ID, true));
            }

            cheques.Sort(delegate(ChequeData x, ChequeData y)
            {
                return DateTime.Compare(x.FechaCobro, y.FechaCobro);

            });
            cargarTabla(cheques,tablaE,false);
        }
    }
}
