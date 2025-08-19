using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ColoresRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.TalleMetrosRepository;

namespace Services.StockService
{
    public class StockService : IGenericService<StockData>
    {
        protected readonly IStockRepository _repo;

        public StockService(IStockRepository repo)
        {
            _repo = repo;

        }

        public StockService(bool local = true)
        {
            _repo = new StockRepository(local);

        }
        public bool Insert(StockData theObject, decimal newStock, Guid idlocal)
        {
            try
            {
                return _repo.InsertStock(theObject.Producto.ID, theObject.Color.ID, theObject.Talle, idlocal, newStock);

            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "Stock_Insert"), true, true);

                throw;

            }

        }

        public bool Insert(StockData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Update(StockData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(StockData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Enable(StockData theObject)
        {
            throw new NotImplementedException();
        }

        public List<StockData> GetAll(bool onlyEnable = true)
        {
            throw new NotImplementedException();
        }

        public List<StockData> GetAll(bool onlyEnable = true, Guid idLocal = new Guid())
        {

            try
            {
                List<StockData> list = _repo.GetAll(idLocal);

                return NormalizeList(list);

            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(onlyEnable, "Stock_GetAll"), true, true);

                throw;

            }
        }

        public StockData GetByID(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public StockData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public List<StockData> NormalizeList(List<StockData> list, bool onlyEnable = true)
        {
            list.ForEach(n => n = getPropertiesInfo(n));

            if (onlyEnable)
                list = list.FindAll(data => data.Producto.Enable);

            list.Sort((x, y) => System.String.CompareOrdinal(x.Codigo, y.Codigo));

            return list;
        }

        public List<detalleStockData> NormalizeList(List<detalleStockData> list, bool onlyEnable = true)
        {
            list.ForEach(n => n = getPropertiesInfo(n));

            if (onlyEnable)
                list = list.FindAll(data => data.producto.Enable);

            list.Sort((x, y) => System.DateTime.Compare(x.fecha, y.fecha));

            return list;


        }

        public StockData getPropertiesInfo(StockData s)
        {
            var proveedorService = new ProveedorService.ProveedorService();
            try
            {
                s.Producto.Proveedor = proveedorService.GetByID(s.Producto.Proveedor.ID);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(s, "StockMetros_getPropertiesInfo"), true, true);

                throw;
            }

            return s;
        }

        public Type GetTypeRepo()
        {
            return _repo.GetType();
        }

        public detalleStockData getPropertiesInfo(detalleStockData s)
        {
            var productoService = new ProductoService.ProductoService(new ProductoRepository());
            var colorService = new ColorService.ColorService(new ColorRepository());
            var proveedorService = new ProveedorService.ProveedorService(new ProveedorRepository());


            try
            {
                if (!string.IsNullOrEmpty(s.codigo))
                {
                    s.color = colorService.GetByCodigo(s.codigo.Substring(7, 3));

                    s.producto = productoService.GetProductoByCodigoInterno(s.codigo.Substring(0, 7), false, false).FirstOrDefault();

                    if (s.producto != null)
                        s.producto.Proveedor = proveedorService.GetByID(s.producto.Proveedor.ID);
                    else
                    {
                        s.producto = productoService.GetDefault();
                    }
                }
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(s, "Stock_getPropertiesInfo"), true, true);

                throw;

            }





            return s;
        }
        public List<StockData> Search(List<StockData> list, string filtro)
        {
            return list.FindAll(
                data =>
                    data.Producto.Description.ToLower().Contains(filtro.ToLower()) ||
                    data.Producto.Proveedor.RazonSocial.ToLower().Contains(filtro.ToLower()) ||
                    data.Producto.CodigoProveedor.ToLower().Contains(filtro.ToLower()));
        }


        public List<detalleStockData> GetDetalleStock(string codigo, Guid idlocal, PuntoControlStockData puntoControl = null)
        {
            string codAux = codigo;
            if (codigo.Length == 4 || codigo.Length == 7 || codigo.Length == 10 || codigo.Length == 12)
            {
                codAux = codigo + "%";
            }
            List<detalleStockData> aux = new List<detalleStockData>();
            try
            {
                aux = _repo.GetDetalleStock(codAux, idlocal);

                if (puntoControl != null && puntoControl.Date != HelperDTO.BEGINNING_OF_TIME_DATE)
                    aux.RemoveAll(data => data.fecha < puntoControl.Date);

                return NormalizeList(aux);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(codigo, "Stock_GetDetalleStock"), true, true);

                throw;
            }


        }

        public bool UpdateStock(StockData stock, decimal cantidad, Guid idLocal = new Guid(), bool add = true)
        {
            if (idLocal == Guid.Empty)
                idLocal = HelperService.IDLocal;


            try
            {
                if (HelperService.validarCodigo(stock.Codigo))
                {
                    if (stock.NotInDB)
                        return Insert(stock, cantidad, idLocal);

                    //como tiene stock en base, entonces actualizo...

                    //verifico si tengo que modificar lo que hay o solo trabajar con el valor ( para el caso de seteo dinamico)
                    cantidad = add ? stock.Stock + cantidad : cantidad;


                    return _repo.UpdateStock(stock.Producto.ID, stock.Color.ID, stock.Talle, idLocal, cantidad);
                }
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(stock, "Stock_UpdateStock"), true, true);

                throw;

            }



            return false;
        }

        public bool UpdateStock(string codigoBarra, decimal cantidad, Guid idLocal = new Guid(), bool add = true)
        {

            StockData stock = obtenerProducto(codigoBarra, idLocal);
            return UpdateStock(stock, cantidad, idLocal, add);
        }



        public decimal GetStockTotal(Guid idProducto, Guid idColor, int talle, Guid idLocal = new Guid())
        {


            try
            {
                if (idLocal == Guid.Empty)
                    idLocal = HelperService.IDLocal;

                StockData aux = _repo.GetStock(idProducto, idColor, talle, idLocal);


                //    return STOCK_MINIMO_INVALIDO;

                return HelperService.ConvertToDecimalSeguro(aux.Stock);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idProducto, "Stock_GetStockTotal"), true, true);

                throw;

            }
        }

        public decimal GetStockTotal(string codigoBarra, Guid idLocal = new Guid())
        {
            StockData aux = obtenerProducto(codigoBarra);
            if (idLocal == Guid.Empty)
                idLocal = HelperService.IDLocal;

            return GetStockTotal(aux.Producto.ID, aux.Color.ID, aux.Talle, idLocal);
        }

        public string GetCodigoBarraDinamico(ProductoData productoData, ColorData colorData, string talle)
        {


            try
            {
                var stockMetrosService = new StockMetrosService(new TalleMetrosRepository());
                if (talle == "")
                {
                    talle = "0";
                }
                if (HelperService.haymts)
                {
                    return stockMetrosService.obtenerCodigo(productoData.CodigoInterno, colorData.Codigo, talle);
                    //le mando los metros

                }

                return productoData.CodigoInterno + colorData.Codigo + talle.PadLeft(2, '0');

            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(productoData, "Stock_GetCodigoBarraDinamico"), true, true);

                throw;

            }

        }


        public List<StockData> getAllbyLocalAndProducto(Guid idlocal, Guid idproducto, bool onlyEnable = true)
        {

            try
            {
                List<StockData> list = _repo.GetAllbyLocalAndProducto(idlocal, idproducto);

                return NormalizeList(list, onlyEnable);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idproducto, "Stock_getAllbyLocalAndProducto"), true, true);

                throw;

            }


        }



        public StockData obtenerProducto(string codigoBarras, Guid idLocal = default(Guid))
        {


            try
            {
                if (idLocal == Guid.Empty)
                    idLocal = HelperService.IDLocal;

                var productoService = new ProductoService.ProductoService(new ProductoRepository());
                var colorService = new ColorService.ColorService(new ColorRepository());
                var stockMetrosService = new StockMetrosService(new TalleMetrosRepository());


                if (HelperService.talleUnico || codigoBarras.Length == 10)
                {
                    codigoBarras = codigoBarras.PadRight(12, '0');
                }

                ProductoData produc = new ProductoData();
                StockData articulo = new StockData();
                articulo.Local = new LocalService.LocalService().GetByID(idLocal);

                string art = codigoBarras.Substring(0, 7);
                string col = codigoBarras.Substring(7, 3);
                string tal = codigoBarras.Substring(10, 2);

                produc = productoService.GetProductoByCodigoInterno(art, false, false).FirstOrDefault();


                articulo.Producto = produc;
                articulo.Color = colorService.GetByCodigo(col);
                if (HelperService.haymts)
                {
                    articulo.Talle = Convert.ToInt32(stockMetrosService.from61ToDec(tal));
                    articulo.Talle61 = tal;
                    articulo.Metros = stockMetrosService.obtenerMetrosPorTalle(art, col, articulo.Talle);
                }
                else
                {
                    articulo.Talle = Convert.ToInt32(tal);
                }


                if (articulo.Producto != null)
                {

                    articulo.Stock = GetStockTotal(articulo.Producto.ID, articulo.Color.ID, articulo.Talle, idLocal);
                }

                if (articulo.Producto == null || articulo.Color == null)
                    return new stockDummyData(codigoBarras);

                return articulo;
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(codigoBarras, "Stock_obtenerProducto"), true, true);

                throw;

            }

        }

        public List<StockData> getAlldistintctProducts(Guid idproducto, bool onlyEnable = true)
        {

            try
            {
                List<StockData> list = _repo.GetAllbyProducto(idproducto);


                return NormalizeList(list, onlyEnable);
            }
            catch (Exception e)
            {


                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idproducto, "Stock_getAlldistintctProducts"), true, true);

                throw;

            }

        }

        public bool setDinamicallyStock(string codigo, Guid idLocal, bool onlyEnable = true)
        {

            string codAux = codigo;
            if (codigo.Length == 4 || codigo.Length == 7 || codigo.Length == 10 || codigo.Length == 12)
            {
                codAux = codigo + "%";
            }

            List<detalleStockData> aux = _repo.GetDetalleStock(codAux, idLocal);

            if (aux.Count > 0)
            {
                aux = NormalizeList(aux, onlyEnable);

                int auxTalle;
                string auxColor = "";
                string auxCodI = "";
                decimal auxStock = 0;


                auxColor = aux[0].color.Codigo;
                auxTalle = Convert.ToInt32(aux[0].codigo.Substring(10, 2));
                auxCodI = aux[0].codigo.Substring(0, 7);
                foreach (detalleStockData d in aux)
                {
                    if (d.color.Codigo == auxColor && d.codigo.Substring(10, 2) == auxTalle.ToString("00"))
                    {
                        auxStock += d.cantidad;

                    }
                    else
                    {// cambia, -> subo datos y recolecto nuevos
                        return UpdateStock(d.codigo.Substring(0, 7) + auxColor + auxTalle.ToString("00"), auxStock, idLocal, false);

                        auxColor = d.color.Codigo;
                        auxTalle = Convert.ToInt32(d.codigo.Substring(10, 2));
                        auxStock = d.cantidad;
                        auxCodI = d.codigo.Substring(0, 7);
                    }
                }
                return UpdateStock(auxCodI + auxColor + auxTalle.ToString("00"), auxStock, idLocal, false);
            }
            return true;//todo! probar este metodo demoniaco

        }



    }
}
