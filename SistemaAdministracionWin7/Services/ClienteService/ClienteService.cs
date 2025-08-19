using System;
using System.Collections.Generic;
using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ClienteRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.ReciboRepository;
using Repository.Repositories.VentaRepository;
using Services.AdministracionService;

namespace Services.ClienteService
{
    public class ClienteService : PersonaService<ClienteData, IClienteRepository>
    {

        public ClienteService(IClienteRepository clienteRepository)
            : base(clienteRepository)
        {
            _repo = clienteRepository;
        }
        public ClienteService(bool local = true)
        {
            _repo = new ClienteRepository(local);
        }

        public override ClienteData getPropertiesInfo(ClienteData n)
        {
            return n;
        }


        public override void GetCC(PersonaData Cliente, out DateTime maxDateRecibo, out DateTime maxDateVenta,
            out decimal subt)
        {

            try
            {


                var notaCreditoClienteService = new NotaService.NotaService(new NotaCreditoClienteRepository(),
                    new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository());
                var notaDebitoClienteService = new NotaService.NotaService(new NotaDebitoClienteRepository(),
                    new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository());

                var ventaService = new VentaService.VentaService(new VentaRepository(), new VentaDetalleRepository());
                var reciboService = new ReciboService.ReciboService(new ReciboRepository(),
                    new ReciboDetalleRepository());


                subt = 0;


                maxDateRecibo = HelperDTO.BEGINNING_OF_TIME_DATE;
                maxDateVenta = HelperDTO.BEGINNING_OF_TIME_DATE;

                List<VentaData> ventas = ventaService.getCuentaCorrientebyCliente(Cliente.ID);


                List<NotaData> notasCredito = notaCreditoClienteService.GetByTercero(Cliente.ID, false);


                List<NotaData> notasDebito = notaDebitoClienteService.GetByTercero(Cliente.ID, false);


                List<ReciboData> recibos = reciboService.GetbyCliente(Cliente.ID);


                foreach (VentaData v in ventas)
                {
                    if (v.Enable && v.Date > maxDateVenta)
                    {
                        maxDateVenta = v.Date;
                    }
                }
                foreach (ReciboData v in recibos)
                {
                    if (v.Enable && v.Date > maxDateRecibo)
                    {
                        maxDateRecibo = v.Date;
                    }
                }


                foreach (VentaData v in ventas)
                {
                    if (v.Enable)
                    {
                        subt -= v.Monto;
                    }
                }


                foreach (NotaData v in notasDebito)
                {
                    if (v.Enable)
                    {
                        subt -= v.Monto;
                    }
                }

                foreach (ReciboData v in recibos)
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

            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                    ObjectDumperExtensions.DumpToString(Cliente, "ClienteService_GetCC"), true, true);

                throw;

            }
        }

    }
}
