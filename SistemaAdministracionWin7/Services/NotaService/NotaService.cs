using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.ClienteRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.VentaRepository;
using Services.AbstractService;
using Services.TributoService;

namespace Services.NotaService
{
    public class NotaService : FatherService<NotaData, NotaDetalleData, INotaRepository, INotaDetalleRepository>
    {

        public NotaService(INotaRepository repo, INotaDetalleRepository repoDetalle, IGenericChildRepository<TributoNexoData> repoTributos)
            : base(repo, repoDetalle)
        {
            _repoTributo = repoTributos;
        }

        private IGenericChildRepository<TributoNexoData> _repoTributo;


        public override bool Insert(NotaData theObject)
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            Guid idPadre;
            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                try
                {
                    var TributoVentaService = new TributoNexoService(_repoTributo);

                     

                    foreach (TributoNexoData f in theObject.Tributos)
                    {

                        f.FatherID = theObject.ID;
                        if (!TributoVentaService.InsertDetalle(f)) return false;
                    }


                    if (!base.Insert(theObject))
                        return false;



                    trans.Complete();
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(theObject, "Nota_insert"), true, true);
                    throw;
                }



            }

            return true;
        }

        public override bool Update(NotaData theObject)
        {
            throw new NotImplementedException();
        }

        public override bool Disable(NotaData theObject)
        {
            try
            {


                return _repo.Disable(theObject.ID);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "Nota_Disable"), true, true);

                throw;

            }
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

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(id, "Nota_Disable"), true, true);
            }
            return false;
        }


        public override bool Disable(Guid id, bool UpdateStock)
        {
            return Disable(id);
        }


        public override bool Enable(NotaData theObject)
        {
            throw new NotImplementedException();
        }

        public override NotaData GetLast(Guid idLocal, int first)
        {

            var aux = new NotaData();

            try
            {
                aux = _repo.GetLast(idLocal, first);
                aux = getPropertiesInfo(aux);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(aux, "Nota_GetLast"), true, true);
                throw;

            }

            return aux;
        }

        public override List<NotaData> NormalizeList(List<NotaData> aux, bool onlyEnable = true)
        {
            if (onlyEnable)
                aux = aux.FindAll(n => n.Enable);



            aux.ForEach(n => n = getPropertiesInfo(n));

            aux.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            return aux;

        }

        public override NotaData getPropertiesInfo(NotaData aux)
        {
            var clienteService = new ClienteService.ClienteService(new ClienteRepository());
            var proveedorService = new ProveedorService.ProveedorService(new ProveedorRepository());

            try
            {


                aux.Children = _repoDetalle.GetDetalles(aux.ID);
                aux.Tributos = _repoTributo.GetDetalles(aux.ID);

                if (aux.tipo == tipoNota.CreditoCliente || aux.tipo == tipoNota.DebitoCliente)
                {
                    aux.tercero = clienteService.GetByID(aux.tercero.ID);
                }
                else
                {
                    aux.tercero = proveedorService.GetByID(aux.tercero.ID);
                }
                return aux;
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                    ObjectDumperExtensions.DumpToString(aux, "NotaService_getPropertiesInfo"), true, true);

                throw;
            }
        }

        public List<NotaData> GetByTercero(Guid idCliente, bool completo, bool onlyEnable = true)
        {
            List<NotaData> aux = new List<NotaData>();


            try
            {
                aux = _repo.GetbyTercero(idCliente);

            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(aux, "Nota_GetByTercero"), true, true);
                throw;

            }


            return NormalizeList(aux, onlyEnable);
        }



        public void PrepareBeforePrint(NotaData aNota)
        {
            foreach (NotaDetalleData detalle in aNota.Children)
            {
                detalle.SetSubtotal(CalcularSubtotalDetalle(detalle));//para el print
                detalle.SetSubtotalConIva(CalcularSubtotalIVADetalle(detalle));//para el print
                //detalle.SetDescription(astock.Producto.Description + "-" + astock.Color.Description);//para el print    
            }
            
            aNota.Tributos = TributoNexoService.setDescriptions(aNota.Tributos);
        }

        private decimal CalcularSubtotalIVADetalle(NotaDetalleData detalle)
        {
            decimal subtotalBonificacion = CalcularSubtotalDetalle(detalle);
            return ((((detalle.Alicuota * subtotalBonificacion) / 100)) + subtotalBonificacion);
        }

        private decimal CalcularSubtotalDetalle(NotaDetalleData detalle)
        {
            decimal subtotal = detalle.PrecioUnidad * detalle.Cantidad;

            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((subtotal * detalle.Bonificacion) / 100);
            return (subtotal - Bonificacion);


        }



    }
}
