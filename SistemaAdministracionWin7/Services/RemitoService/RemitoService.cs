using System;
using System.Collections.Generic;
using System.Transactions;
using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.RemitoRepository;
using Services.AbstractService;

namespace Services.RemitoService
{
    public class RemitoService : FatherService<RemitoData, remitoDetalleData, IRemitoRepository, IRemitoDetalleRepository>
    {



        public RemitoService(IRemitoRepository repo, IRemitoDetalleRepository repoDet)
            : base(repo, repoDet)
        {

        }

        public RemitoService(bool local = true)
            : base()
        {
            _repo = new RemitoRepository(local);
            _repoDetalle = new RemitoDetalleRepository(local);
        }



        
        public override List<RemitoData> NormalizeList(List<RemitoData> list, bool onlyEnable = true)
        {

            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public override RemitoData getPropertiesInfo(RemitoData r)
        {
            try
            {
                r.Children = _repoDetalle.GetDetalles(r.ID);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(r, "Remito_getPropertiesInfo"), true, true);

                throw;

            }


            return r;
        }

        public List<RemitoData> GetByLocalDestino(Guid idLocal, bool noSync, bool onlyEnable = true)
        {
            try
            {


                List<RemitoData> rs = _repo.GetByLocalDestino(idLocal);
                return NormalizeList(rs);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "Remito_GetByLocalDestino"), true, true);

                throw;

            }
        }

        public List<RemitoData> GetByLocalOrigen(Guid idLocal, bool noSync, bool onlyEnable = true)
        {
            try
            {


                List<RemitoData> rs = _repo.GetByLocalOrigen(idLocal);
                return NormalizeList(rs);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "Remito_GetByLocalOrigen"), true, true);

                throw;

            }

        }

        public List<RemitoData> getByLocalSinRecibir(Guid idLocal, bool noSync)
        {

            var all = GetByLocalDestino(idLocal, noSync);
            var result = all.FindAll(
                                           delegate(RemitoData r)
                                           {
                                               return r.estado == remitoEstado.Enviado && r.Enable;
                                           }
                                          );


            return result;
        }

        public List<RemitoData> getByLocalRecibido(Guid idLocal, bool noSync)
        {

            return GetByLocalDestino(idLocal, noSync).FindAll(
                                            delegate(RemitoData r)
                                            {
                                                return r.estado == remitoEstado.Recibido;
                                            }
                                           );
        }
        public bool confirmarRecibo(Guid id, DateTime fecha)
        {
            try
            {


                return _repo.ConfirmarRecibo(id, fecha);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(id, "Remito_confirmarRecibo"), true, true);

                throw;

            }
        }

        public bool confirmarRecibo(Guid id)
        {

            RemitoData remito = GetByID(id);


            var stockService = new StockService.StockService();//Todo! esto la puede cagar al realizar la sync?
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                try
                {



                    foreach (var item in remito.Children)
                    {
                        IGetteableCodigoAndCantidad xxx = item as IGetteableCodigoAndCantidad;
                        if (xxx != null)
                        {
                            if (!stockService.UpdateStock(xxx.GetCodigo(), xxx.GetCantidad(), remito.Local.ID, add: true))
                                return false;
                        }
                    }

                    if (_repo.ConfirmarRecibo(id, DateTime.Now))
                        trans.Complete();
                }
                catch (Exception e)
                {


                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(id, "Remito_confirmarRecibo"), true, true);

                    throw;

                }
            }

            return true;


        }

        /// <summary>
        /// Se llama al hacer una baja de stock, se inserta y luego esto
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool confirmarBaja(RemitoData remito)
        {
            var stockService = new StockService.StockService();

            bool task = false;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                try
                {
                    foreach (var item in remito.Children)
                    {//Todo! esto la puede cagar al realizar la sync?
                        IGetteableCodigoAndCantidad xxx = item as IGetteableCodigoAndCantidad;
                        if (xxx != null)
                        {
                            if (!stockService.UpdateStock(xxx.GetCodigo(), xxx.GetCantidad() * -1, remito.Local.ID, add: true))
                                return false;
                        }
                    }
                    trans.Complete();
                }

                catch (Exception e)
                {


                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(remito, "Remito_confirmarBaja"), true, true);

                    throw;

                }
            }


            return true;

        }

        public List<RemitoData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            try
            {


                return NormalizeList(_repo.GetBiggerThan(ultimo, idLocal, prefix));
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimo, "Remito_GetBiggerThan"), true, true);

                throw;

            }

        }

        public List<RemitoData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            try
            {
                return NormalizeList(_repo.GetOlderThan(ultimo, idLocal, prefix));
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimo, "Remito_GetOlderThan"), true, true);

                throw;

            }

        }


        public List<RemitoData> getOlderThan(RemitoData ultimoRemito)
        {
            try
            {
                return GetOlderThan(ultimoRemito.Date, HelperService.IDLocal, HelperService.Prefix);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimoRemito, "Remito_getOlderThan"), true, true);

                throw;

            }

        }

        public List<RemitoData> GetAnuladosByOrigen(Guid guid)
        {
            try
            {
                return NormalizeList(_repo.GetAnulados(guid), false);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(guid, "Remito_GetAnuladosByOrigen"), true, true);

                throw;

            }

        }
    }

}
