using System;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.CajaRepository;
using Services;
using Services.CajaService;


namespace SharedForms.Impositivo
{
    public partial class CerrarCajaNuevo : Form
    {
        public CerrarCajaNuevo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var CajaService = new CajaService(new CajaRepository());
            CajaService.CerrarCaja(DateTime.Now, 10, Guid.NewGuid(), HelperService.IDLocal);
        }

        private void CerrarCajaNuevo_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var CajaService = new CajaService(new CajaRepository());
            CajaData caja = CajaService.GetLast(HelperService.IDLocal, 1);
        }
    }
}
