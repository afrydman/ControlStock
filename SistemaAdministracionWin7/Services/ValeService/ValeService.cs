using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.AdministracionRepository;
using Repository.Repositories.ValeRepository;
using Services.AbstractService;


namespace Services.ValeService
{
    public class ValeService : ObjectGetterService<valeData, IValeRepository>
    {
        
         public ValeService(IValeRepository repo)
        {
            _repo = repo;
        }

        public ValeService(bool local = true)
         {
             _repo = new ValeRepository(local);
             
         }

        public override List<valeData> NormalizeList(List<valeData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(aux => aux.Enable);

            list.Sort((x, y) => x.Numero.CompareTo(y.Numero));

            return list;
        }

        public override valeData getPropertiesInfo(valeData n)
        {
            return n;
        }

        public valeData GetbyVenta(Guid idVenta)
        {
            try
            {
                return _repo.GetbyVenta(idVenta);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idVenta, "Vales_GetbyVenta"), true, true);

                throw;

            }
           
        }



        public int GenerarNuevo(IngresoData n)
        {
            int numero = -1;
            numero = GetLast(HelperService.IDLocal, HelperService.Prefix).Numero + 1;
            valeData v = new valeData();
            v.ID = Guid.NewGuid();
            v.Enable = true;
            v.EsCambio = false;
            v.Date = n.Date;
            v.idAsoc = n.ID;
            v.Local.ID = n.Local.ID;
            v.Monto = n.Monto;
            v.Numero = numero;
            v.Prefix = n.Prefix;
            v.Personal = n.Personal;
            return Insert(v) ? v.Numero : -1;
             

        }
        public int GenerarNuevo(VentaData venta) 
        {
            int numero = -1;
            numero = GetLast(HelperService.IDLocal, HelperService.Prefix).Numero + 1;
          
            valeData v = new valeData();
            v.ID = Guid.NewGuid();
            v.Enable = true;
            v.EsCambio = true;
            v.Date = venta.Date;
            v.idAsoc = venta.ID;
            v.Local.ID = venta.Local.ID;
            v.Monto = venta.Monto * -1;//pq es negativa la venta
            v.Numero = numero;
            v.Prefix = venta.Prefix;
            v.Personal = venta.Vendedor;

            return Insert(v) ? v.Numero : -1;
        }
    }
}
