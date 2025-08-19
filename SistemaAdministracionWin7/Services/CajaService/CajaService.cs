using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.CajaRepository;

namespace Services.CajaService
{
    public class CajaService 
    {

           protected readonly ICajaRepository _cajaRepository;
           public CajaService(ICajaRepository cajaRepository)
        {
            _cajaRepository = cajaRepository;
        }
           public CajaService(bool local=true)
           {
               _cajaRepository = new CajaRepository(local);
           }


        public bool CerrarCaja(decimal final)
        {
            return CerrarCaja(DateTime.Now.Date, final, Guid.NewGuid(), HelperService.IDLocal);
        }

        public bool CerrarCaja(DateTime fecha, decimal final, Guid idCaja, Guid idLocal)
        {
            if (idLocal==Guid.Empty)
                idLocal = HelperService.IDLocal;//no se puede pasar en la firma 
            
            if (idCaja==Guid.Empty)
               idCaja = Guid.NewGuid();
            
            CajaData caja = new CajaData();
            caja.Local.ID = idLocal;
            caja.Date = fecha;
            caja.Monto = final;
            caja.ID = idCaja;

          
            try
            {
                return _cajaRepository.Insert(caja);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(caja, "Caja_cerrarCaja"), true, true);
            }
            return false;
        }



        public CajaData GetCajaInicial()
        {
            return GetCajaInicial(DateTime.Now.Date, Guid.Empty);

        }

        public CajaData GetCajaInicial(DateTime fecha, Guid idLocal)
        {
            if (idLocal==Guid.Empty)
                idLocal = HelperService.IDLocal;//no se puede pasar en la firma 

        

            try
            {
                return _cajaRepository.GetCajaInicial(idLocal, fecha);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);
                HelperService.writeLog(ObjectDumperExtensions.DumpToString(idLocal, "Caja_GetCajaInicial"), true, true);
              
            }
            return null;
        }


        public CajaData GetLast()
        {
            return GetLast(HelperService.IDLocal, HelperService.Prefix);
        }

        public CajaData GetLast(Guid idLocal,int first)
        {
            
            
            try
            {
                return _cajaRepository.GetLast(idLocal, first);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);
                HelperService.writeLog(ObjectDumperExtensions.DumpToString(idLocal, "Caja_GetLast"), true, true);

            }

            return null;
        }

        public bool IsClosed(DateTime fecha, Guid idLocal)
        {
           
            try
            {
                CajaData c = _cajaRepository.GetCajabyFecha(idLocal, fecha);
                return c != null && _cajaRepository.GetCajabyFecha(idLocal, fecha).ID != Guid.Empty;
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(fecha, "CajaService_IsClosed"), true, true);

                throw;

            }
        }

        public List<CajaData> GetByRangoFechas2(DateTime from, DateTime to, Guid idLocal, int prefix)
        {

            try
            {

                return NormalizeList(_cajaRepository.GetByRangoFecha2(from, to, idLocal, prefix));
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString("", "CajaService_GetByrangoFecha"), true, true);

                throw;

            }

        }
        public List<CajaData> GetByrangoFecha(DateTime from, DateTime to, Guid IdLocal, int prefix)
        {

            try
            {

                return NormalizeList(_cajaRepository.GetByRangoFecha(from, to, IdLocal, prefix));
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString("", "CajaService_GetByrangoFecha"), true, true);

                throw;

            }

        }
        public List<CajaData> GetOlderThan(DateTime ultimaCaja, Guid idLocal)
        {
            try
            {
                return NormalizeList(_cajaRepository.GetOlderThan(ultimaCaja, idLocal, HelperService.Prefix));
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimaCaja, "CajaService_GetOlderThan"), true, true);

                throw;

            }
          
        }

        //public List<CajaData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid Local)
        //{
        //    return NormalizeList(_cajaRepository.GetByRangoFecha(fecha1.Date, fecha2.Date, idLocal, Prefix));
        //}


        public List<CajaData> NormalizeList(List<CajaData> list, bool onlyEnable = true)
        {

            //if (onlyEnable)
            //    list = list.FindAll(data => data.Enable);

            //list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }
    }
}
