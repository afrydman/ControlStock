using System;
using System.Windows.Forms;
//using ClienteFacturaElectronica;
//using ClienteFacturaElectronica.Helpers;
using DTO.BusinessEntities;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.NotaService;
using Services.VentaService;

namespace Central
{
    public partial class TestPrinter : Form
    {
        public TestPrinter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

        }

        private void TestPrinter_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //
            NotaService notaService = new NotaService(new NotaDebitoClienteRepository(),new NotaDebitoClienteDetalleRepository(),new TributoNotaDebitoClientesRepository() );
            

            NotaData anota = notaService.GetByID(new Guid(txtDebitoCliente.Text));


            PrintNotaForm(anota, notaService);
        }

        private void PrintNotaForm(NotaData anota, NotaService notaService)
        {
            //ClienteFacturaElectronica.Helpers.Printer aPrinter = new Printer();
            string cae = "67425023950555";
            DateTime caeVto = new DateTime(2017, 11, 01);
            anota.CAE = cae;
            anota.CAEVto = caeVto;

            string barcodeFile = string.Format(
                @"C:\Users\afrydman\Documents\sistemacompleto\Fuente\SistemaAdministracion\Central\Barcodes\{0}.png",
                DateTime.Now.ToString("yyMMdd") + "_" + DateTime.Now.ToString("hmmss"));



            try
            {
                notaService.PrepareBeforePrint(anota);


              //  aPrinter.PrintNota(anota,  Convert.ToInt32(cantidad.Text));
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
    //ClienteFacturaElectronica.Helpers.Printer aPrinter = new Printer();
            //
            VentaService aVentaService = new VentaService();
            //
            VentaData aVenta = aVentaService.GetByID(new Guid(idVenta.Text));
            //

            string cae = "67425023950555";
            DateTime caeVto = new DateTime(2017, 11, 01);
            aVenta.CAE = cae;
            aVenta.CAEVto = caeVto;

            

            try
            {
                aVentaService.PrepareBeforePrint(aVenta);
                //aPrinter.PrintFactura(aVenta,  Convert.ToInt32(cantidad.Text));
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NotaService notaService = new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository());


            NotaData anota = notaService.GetByID(new Guid(txtDebitoProveedor.Text));


            PrintNotaForm(anota, notaService);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NotaService notaService = new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository());


            NotaData anota = notaService.GetByID(new Guid(txtCreditoCliente.Text));


            PrintNotaForm(anota, notaService);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NotaService notaService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository());


            NotaData anota = notaService.GetByID(new Guid(txtCreditoProveedor.Text));


            PrintNotaForm(anota, notaService);
        }
    }
}
