using System;
using System.Collections.Generic;
using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.VentaRepository;
using Services.AdministracionService;

namespace Services.ProveedorService
{
    public class ProveedorService : PersonaService<ProveedorData, IProveedorRepository>
    {
     
        public ProveedorService(IProveedorRepository repo)
            : base(repo)
        {
            _repo = repo;
        }

        public ProveedorService(bool local = true)
         {
             _repo = new ProveedorRepository(local);
             
         }

        public override ProveedorData getPropertiesInfo(ProveedorData n)
        {
            return n;
        }

        public ProveedorData GetByCodigo(string cod)
        {
            try
            {
                return _repo.GetByCodigo(cod);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(cod, "ProveedorService_GetByCodigo"), true, true);


                throw;

            }
            
        }

        public  int NextAvailableCode()
        {
            List<ProveedorData> ps = GetAll(false);
           if(ps!=null && ps.Count> 0) { 
            ps.Sort(
                delegate(ProveedorData x, ProveedorData y)
                {
                    return Convert.ToInt32(x.Codigo).CompareTo(Convert.ToInt32(y.Codigo));
                });
            }
            int maxcodigo = 0000;

            if (ps != null && ps.Count > 0)
            {
             
                maxcodigo = Convert.ToInt32(ps[ps.Count - 1].Codigo) + 1;
            }
            return maxcodigo;
        }

        public override void GetCC(PersonaData persona, out DateTime maxDatePago, out DateTime maxDateCompra, out decimal subt)
        {

            try
            {


                subt = 0;
                var notaDebitoProveedoresService = new NotaService.NotaService(new NotaDebitoProveedoresRepository(),
                    new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository());
                var notaCreditoProveedoresService = new NotaService.NotaService(new NotaCreditoProveedoresRepository(),
                    new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository());

                var comprasProveedoresService =
                    new ComprasProveedorService.ComprasProveedorService(new ComprasProveedoresRepository(),
                        new CompraProveedoresDetalleRepository());

                var OrdenPagoService = new OrdenPagoService.OrdenPagoService(new OrdenPagoRepository(),
                    new OrdenPagoDetalleRepository());



                maxDatePago = HelperDTO.BEGINNING_OF_TIME_DATE;
                maxDateCompra = HelperDTO.BEGINNING_OF_TIME_DATE;

                List<ComprasProveedoresData> compras = comprasProveedoresService.GetbyProveedor(persona.ID);
                List<NotaData> notasCredito = notaCreditoProveedoresService.GetByTercero(persona.ID, false);
                List<NotaData> notasDebito = notaDebitoProveedoresService.GetByTercero(persona.ID, false);
                List<OrdenPagoData> opagos = OrdenPagoService.GetbyProveedor(persona.ID);



                foreach (ComprasProveedoresData v in compras)
                {

                    if (v.Enable && v.Date > maxDateCompra)
                    {
                        maxDateCompra = v.Date;

                    }
                }

                foreach (OrdenPagoData v in opagos)
                {

                    if (v.Enable && v.Date > maxDatePago)
                    {
                        maxDatePago = v.Date;

                    }
                }



                foreach (ComprasProveedoresData v in compras)
                {
                    if (v.Enable)
                    {
                        subt -= v.Monto;
                    }
                }
                foreach (OrdenPagoData v in opagos)
                {

                    if (v.Enable)
                    {
                        subt += v.Monto;
                    }
                }
                foreach (NotaData v in notasCredito)
                {
                    if (v.Enable)
                    {
                        subt += v.Monto;
                    }
                }

                foreach (NotaData v in notasDebito)
                {
                    if (v.Enable)
                    {
                        subt -= v.Monto;
                    }
                }
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(persona, "ProveedorService_getCC"), true, true);


                throw;

            }

        }
    }
}
