using System;
using System.Collections.Generic;
using System.Transactions;
using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.PuntoControlStockRepository;
using Repository.Repositories.RemitoRepository;
using Services.AbstractService;

namespace Services.PuntoControlService
{
    public class PuntoControlService : FatherService<PuntoControlStockData, PuntoControlStockDetalleData, IPuntoControlStockRepository, IPuntoControlStockDetalleRepository>
    {


        public PuntoControlService(IPuntoControlStockRepository repo, IPuntoControlStockDetalleRepository repoDet)
            : base(repo, repoDet)
        {

        }



        public PuntoControlService(bool local = true)
            : base()
        {
            _repo = new PuntoControlStockRepository(local);
            _repoDetalle = new PuntoControlStockDetalleRepository(local);
        }


        public override bool Disable(Guid id)
        {
            try
            {
                return _repo.Disable(id);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(id, "PC_Disable"), true, true);
            }
            return false;
        }


        public override bool Disable(Guid id,bool UpdateStock)
        {
            return Disable(id);
        }

        public override List<PuntoControlStockData> NormalizeList(List<PuntoControlStockData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public override PuntoControlStockData getPropertiesInfo(PuntoControlStockData theObject)
        {
            try
            {
                theObject.Children = _repoDetalle.GetDetalles(theObject.ID);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(theObject, "PuntoControlService_getPropertiesInfo"), true, true);

                throw;

            }


            return theObject;
        }


        public override PuntoControlStockData GetLast(Guid idLocal, int first)
        {

            var aux = new PuntoControlStockData();

            try
            {
                aux = _repo.GetLast(idLocal, first);
                aux = getPropertiesInfo(aux);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(aux, "PuntoControlStockData_GetLast"), true, true);
                throw;

            }

            return aux;
        }

        public List<PuntoControlStockData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            try
            {


                return NormalizeList(_repo.GetBiggerThan(ultimo, idLocal, prefix));
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimo, "PuntoControlService_GetBiggerThan"), true, true);

                throw;

            }

        }

        public List<PuntoControlStockData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            try
            {
                return NormalizeList(_repo.GetOlderThan(ultimo, idLocal, prefix));
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimo, "PuntoControlService_GetOlderThan"), true, true);

                throw;

            }

        }


        public List<PuntoControlStockData> getOlderThan(PuntoControlStockData ultimoRemito)
        {
            try
            {
                return GetOlderThan(ultimoRemito.Date, HelperService.IDLocal, HelperService.Prefix);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimoRemito, "PuntoControlService_getOlderThan"), true, true);

                throw;

            }

        }
    }

}
