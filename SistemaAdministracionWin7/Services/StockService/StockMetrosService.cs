using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ColoresRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.TalleMetrosRepository;
using Services.ProductoService;

namespace Services.StockService
{
    public class StockMetrosService
    {

        protected readonly ITalleMetrosRepository _repo;

        public StockMetrosService(ITalleMetrosRepository repo)
        {
            _repo = repo;
        }

        public StockMetrosService(bool local = true)
        {
            _repo = new TalleMetrosRepository(local);

        }
        public decimal obtenerMetrosPorTalle(string productoCodigo, string colorCodigo, int talledec)
        {

            try
            {
                var colorService = new ColorService.ColorService(new ColorRepository());
                var productoService = new ProductoService.ProductoService(new ProductoRepository());
                ProductoData p = productoService.GetProductoByCodigoInterno(productoCodigo, false).FirstOrDefault();
                ColorData c = colorService.GetByCodigo(colorCodigo);


                return _repo.GetMetros(p.ID, c.ID, talledec);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(productoCodigo, "StockMetros_obtenerMetrosPorTalle"), true, true);

                throw;

            }

        }

        public string obtenerCodigo(string productoCodigo, string colorCodigo, string metros, bool insertTalle = true)
        {

            try
            {


                var colorService = new ColorService.ColorService(new ColorRepository());
                var productoService = new ProductoService.ProductoService(new ProductoRepository());
                var productoTalleService = new ProductoTalleService();

                ProductoData p =
                    productoService.GetProductoByCodigoInterno(productoCodigo, false, true).FirstOrDefault();
                ColorData c = colorService.GetByCodigo(colorCodigo);

                string s = _repo.GetTalle(p.ID, c.ID, HelperService.ConvertToDecimalSeguro(metros));
                string nuevoTalle = s ?? "0";
                int talledec = 0;
                if (s == null && insertTalle)
                {
                    s = decTo61(_repo.ObtengoUltimoTalle(p.ID, c.ID));

                    nuevoTalle = aumentoValor(s, 1); //desperdicio el 0 :(

                    talledec = Convert.ToInt32(from61ToDec(nuevoTalle));

                    _repo.InsertTalleMetros(p.ID, c.ID, HelperService.ConvertToDecimalSeguro(metros), nuevoTalle,
                        talledec);

                    ProductoTalleData pt = new ProductoTalleData();
                    pt.IDProducto = p.ID;
                    pt.Talle = talledec;

                    productoTalleService.Insert(pt);
                }

                return productoCodigo + colorCodigo + nuevoTalle.PadLeft(2, '0');
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(productoCodigo, "StockMetros_obtenerCodigo"), true, true);

                throw;

            }
        }

        public Dictionary<string, decimal> obtenerTodoByProductoColor(Guid idProducto, Guid idColor)
        {
            try
            {


                return _repo.ObtenerTodoByProductoColor(idProducto, idColor);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idProducto, "StockMetros_obtenerTodoByProductoColor"), true, true);

                throw;

            }
        }

        public Dictionary<decimal, string> obtenerTodoByProducto(Guid idProducto)
        {
            try
            {


                return _repo.ObtenerTodoByProducto(idProducto);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idProducto, "StockMetros_obtenerTodoByProducto"), true, true);

                throw;

            }
        }


        internal char[] baseCh =
            new char[] { '0','1','2','3','4','5','6','7','8','9',//10
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',//26
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',//26
            };


        //convert int -> Numero base 26.

        public string decTo61(int value, bool len2needed = false)
        {

            try
            {


                char[] baseChars = baseCh;

                string result = String.Empty;
                int targetBase = baseChars.Length;

                do
                {
                    result = baseChars[value % targetBase] + result;
                    value = value / targetBase;
                } while (value > 0);
                if (len2needed)
                {
                    result = result.PadLeft(2, '0');
                }
                return result;
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(value, "StockMetros_decTo61"), true, true);

                throw;

            }
        }


        public string from61ToDec(string value)
        {

            try
            {


                string valuestring = value.PadLeft(2, '0');
                int aux;

                string digitoMayor = valuestring.Substring(0, 1);
                string digitoMenor = valuestring.Substring(1, 1);


                aux = obtengoValorDecimaldeDigito(digitoMayor) * baseCh.Length + obtengoValorDecimaldeDigito(digitoMenor);



                return aux.ToString("00");
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(value, "StockMetros_from61ToDec"), true, true);

                throw;

            }
        }


        public string aumentoValor(string valor, int valorAAumentar = 1)
        {
            return decTo61(Convert.ToInt32(from61ToDec(valor)) + valorAAumentar);

        }

        private int obtengoValorDecimaldeDigito(string caracter)
        {
            try
            {



                int n;
                bool isNumeric = Int32.TryParse(caracter, out n);
                if (isNumeric)
                    return n;

                //es una letra.

                byte asciiBytes = Encoding.ASCII.GetBytes(caracter)[0];


                if ((int)asciiBytes > 90)
                {
                    //esta en el rango  97-122 ( minisculas ) //97 -> 10, 98 -> 11, etc
                    return (int)asciiBytes - 97 + 10;

                }
                else
                {
                    //esta en el rango  65-90 ( mayusculas ) //65 -> 36, 66 -> 37, etc
                    return (int)asciiBytes - 65 + 36;


                }
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(caracter, "StockMetros_obtengoValorDecimaldeDigito"), true, true);

                throw;

            }


        }

        //string a ="";
        //   string aback = "";
        //   string b = "";
        //   string bback = "";
        //   string c = "";
        //   string cback = "";
        //   string d = "";
        //   string dback = "";
        //   int originalA = 1;
        //   int originalb = 11;
        //   int originalc = 99;
        //   int originald = 100;


        //   a = HelperService.decTo61(originalA);
        //   aback = HelperService.from61ToDec(a);

        //   b = HelperService.decTo61(originalb);
        //   bback = HelperService.from61ToDec(b);

        //   c = HelperService.decTo61(originalc);
        //   cback = HelperService.from61ToDec(c);

        //   d = HelperService.decTo61(originald);
        //   dback = HelperService.from61ToDec(d);


        //   MessageBox.Show(originalA.ToString() + "-" +a + "=" + aback);
        //   MessageBox.Show(originalb.ToString() + "-" +b + "=" + bback);
        //   MessageBox.Show(originalc.ToString() + "-" +c + "=" + cback);
        //   MessageBox.Show(originald.ToString() + "-" + d + "=" + dback);




        public string conversorAuxiliar(string[] acambiar)
        {
            int aux = 0;
            string[] news = new string[acambiar.Length];
            string sarasa = "";
            foreach (string s in acambiar)
            {
                if (s != "*")
                {
                    sarasa += decTo61(Convert.ToInt32(s), true) + ",";
                }
                else
                {
                    sarasa += "*,";
                }

                aux++;

            }

            return sarasa;
        }

    }
}
