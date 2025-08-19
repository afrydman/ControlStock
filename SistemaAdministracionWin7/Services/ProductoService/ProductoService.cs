using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.ProductoRepository;
using Services.AbstractService;
using Services.Interfaces;

namespace Services.ProductoService
{
    public class ProductoService : ObjectService<ProductoData, IProductoRepository>, IDefaultable<ProductoData>
    {
        
        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }
        public ProductoService(bool local = true)
         {
             _repo = new ProductoRepository(local);
             
         }

        public override bool Insert(ProductoData theObject)
        {
            return Insert(theObject, true);
        }

        public bool Insert(ProductoData theObject, bool insertTalle = true)
        {
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());

            if (theObject.ID == null || theObject.ID == new Guid())
            {
                theObject.ID = Guid.NewGuid();
            }
            if (string.IsNullOrEmpty(theObject.CodigoInterno))
            {
                theObject.CodigoInterno = GenerarCodigoInterno(theObject.Proveedor.Codigo);
            }

            if (insertTalle)
            {
                ProductoTalleData ptalle;
                int aux = HelperService.TallesPorProductoDefault ?? 50;

                

                if (HelperService.talleUnico ||HelperService.haymts)
                { 
                    aux = 0;
                }
             

                var opts = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                };
                Guid idPadre;
                using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
                {
                    try
                    {
                        for (int i = 0; i <= aux; i++)
                        {
                            ptalle = new ProductoTalleData();
                            ptalle.IDProducto = theObject.ID;
                            ptalle.Talle = i;
                            ptalle.ID = Guid.NewGuid();
                            if(!productoTalleService.Insert(ptalle))
                                return false;
                        }

                        if (_repo.Insert(theObject))
                            trans.Complete();
                        return true;
                    }
                    catch (Exception e)
                    {

                        HelperService.WriteException(e);

                        HelperService.writeLog(
                                        ObjectDumperExtensions.DumpToString(theObject, "Producto_insert"), true, true);

                        throw;

                    }
                }

            }
            
            return _repo.Insert(theObject);
        }
        public  string GenerarCodigoInterno(string codigoProveedor)
        {
            try
            {
                string lastcode = "";
                string newCode = codigoProveedor;
                List<ProductoData> ps = _repo.GetProductoByCodigoInterno(codigoProveedor, true);


                if (ps.Count > 0)
                {
                    ps.Sort((x, y) => x.CodigoInterno.CompareTo(y.CodigoInterno));

                    lastcode = ps[ps.Count - 1].CodigoInterno;
                }


                int aux = 0;
                if (lastcode != "")
                {

                    aux = Convert.ToInt32(lastcode.Substring(4, 3));
                    aux++;
                    newCode += aux.ToString("000");
                }
                else
                {
                    newCode += "000";
                }


                //return codigoProveedor + aux.ToString("000");
                return newCode;
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(codigoProveedor, "ProductoService_GenerarCodigoInterno"), true, true);

                throw;

            }
           
        }

        public  List<ProductoData> GetProductoByCodigoInterno(string codigo,bool useLike,bool onlyEnable=true)
        {
            List<ProductoData> p = new List<ProductoData>();
            if (HelperService.consultarArticulo && !string.IsNullOrEmpty(codigo) && ((codigo.Length == 4) || (codigo.Length == 7) || (codigo.Length == 10) || (codigo.Length == 12)))
            {
                try
                {
                    p = _repo.GetProductoByCodigoInterno(codigo.Substring(0, 7), useLike);
                }
                catch (Exception e)
                {

                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(codigo, "Producto_GetProductoByCodigoInterno"), true, true);

                    throw;

                }
               

            }
            return NormalizeList(p, onlyEnable);
        }

        public  List<ProductoData> GetbyProveedor(Guid idProveedor, bool onlyEnable = true)
        {
            try
            {


                List<ProductoData> ps = _repo.GetProductosByProveedor(idProveedor);

                return NormalizeList(ps, onlyEnable);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idProveedor, "Producto_GetbyProveedor"), true, true);

                throw;

            }

        }

        public List<ProductoData> Search(List<ProductoData> todos, string p, bool onlyEnable = true)
        {
            List<ProductoData> aux = todos.FindAll(
               pp =>
                   pp.Description.ToLower().Contains(p.ToLower()) ||
                   pp.Proveedor.RazonSocial.ToLower().Contains(p.ToLower()) ||
                   pp.CodigoProveedor.ToLower().Contains(p.ToLower())
               );


            return NormalizeList(aux, onlyEnable);


        }

        public override ProductoData getPropertiesInfo(ProductoData p)
        {
            if (IsEmpty(p))
                p = GetDefault();

            return p;
        }

       

        public override List<ProductoData> NormalizeList(List<ProductoData> ps, bool onlyEnable = true)
        {
            if (onlyEnable)
                ps = ps.FindAll(data => data.Enable);

            ps.ForEach(n => n = getPropertiesInfo(n));


            ps.Sort((x, y) => System.String.Compare(x.CodigoProveedor, y.CodigoProveedor, System.StringComparison.Ordinal));

            return ps;
        }


        public ProductoData GetDefault()
        {
            ProductoData p = new ProductoData();

            p.Description = "Sin Informacion";
            p.Enable = true;
            p.Proveedor.RazonSocial = "Sin Informacion";
            p.Linea.Description = "Sin Informacion";
            p.Temporada.Description = "Sin Informacion";

            return p;
        }
    }
}
