using System.Collections.Generic;
using Services.AdministracionService;

namespace Services
{
    public static class ServiceHandler
    {

        private static List<object> Services = new List<object>()
        {
            new BancoService.BancoService(),
            new TipoIngresoService(),
            new TipoRetiroService()
        };

        public static List<object> GetAll()
        {

            return Services;
        } 
    }
}
