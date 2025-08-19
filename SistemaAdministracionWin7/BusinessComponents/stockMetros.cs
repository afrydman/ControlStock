namespace BusinessComponents
{
    public class StockMetros
    {
        //public static decimal obtenerMetrosPorTalle(string productoCodigo, string colorCodigo, int talledec)
        //{


        //    productoData p = null;//producto.getProductoByCodigoInterno(productoCodigo);
        //    colorData c = null;//color.getByCodigo(colorCodigo);


        //    return TalleMetrosDataMapper.getMetros(p.ID, c.ID, talledec);
        //}

        //public static string obtenerCodigo(string productoCodigo, string colorCodigo, string metros, bool insertTalle = true)
        //{
        //    productoData p = null;//producto.getProductoByCodigoInterno(productoCodigo);
        //    colorData c = null;//color.getByCodigo(colorCodigo);


        //    string s = TalleMetrosDataMapper.getTalle(p.ID, c.ID, helper.ConvertToDecimalSeguro(metros));
        //    string nuevoTalle = s;
        //    int talledec = 0;
        //    if (s == "-" && insertTalle)
        //    {
        //        s = decTo61(TalleMetrosDataMapper.obtengoUltimo(p.ID, c.ID));

        //        nuevoTalle = aumentoValor(s, 1);
        //        talledec = Convert.ToInt32(BusinessComponents.StockMetros.from61ToDec(nuevoTalle));
        //        TalleMetrosDataMapper.nuevoTalle(p.ID, c.ID, helper.ConvertToDecimalSeguro(metros), nuevoTalle, talledec);
        //        //productoTalle.Insert(p.ID, talledec);
        //    }

        //    return productoCodigo + colorCodigo + nuevoTalle.PadLeft(2, '0');

        //}

        //public static Dictionary<string, decimal> obtenerTodoByProductoColor(Guid idProducto, Guid idColor)
        //{
        //    return TalleMetrosDataMapper.obtenerTodoByProductoColor(idProducto, idColor);
        //}

        //public static Dictionary<decimal, string> obtenerTodoByProducto(Guid idProducto)
        //{
        //    return TalleMetrosDataMapper.obtenerTodoByProducto(idProducto);
        //}


        //internal static char[] baseCh =
        //    new char[] { '0','1','2','3','4','5','6','7','8','9',//10
        //        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',//26
        //        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',//26
        //    };


        ////convert int -> Numero base 26.

        //public static string decTo61(int value, bool len2needed = false)
        //{
        //    char[] baseChars = baseCh;

        //    string result = String.Empty;
        //    int targetBase = baseChars.Length;

        //    do
        //    {
        //        result = baseChars[value % targetBase] + result;
        //        value = value / targetBase;
        //    }
        //    while (value > 0);
        //    if (len2needed)
        //    {
        //        result = result.PadLeft(2, '0');
        //    }
        //    return result;
        //}


        //public static string from61ToDec(string value)
        //{
        //    string valuestring = value.PadLeft(2, '0');
        //    int aux;

        //    string digitoMayor = valuestring.Substring(0, 1);
        //    string digitoMenor = valuestring.Substring(1, 1);


        //    aux = obtengoValorDecimaldeDigito(digitoMayor) * baseCh.Length + obtengoValorDecimaldeDigito(digitoMenor);



        //    return aux.ToString("00");

        //}


        //public static string aumentoValor(string valor, int valorAAumentar = 1)
        //{
        //    return decTo61(Convert.ToInt32(from61ToDec(valor)) + valorAAumentar);

        //}

        //private static int obtengoValorDecimaldeDigito(string caracter)
        //{
        //    int n;
        //    bool isNumeric = Int32.TryParse(caracter, out n);
        //    if (isNumeric)
        //        return n;

        //    //es una letra.

        //    byte asciiBytes = Encoding.ASCII.GetBytes(caracter)[0];


        //    if ((int)asciiBytes > 90)
        //    {//esta en el rango  97-122 ( minisculas ) //97 -> 10, 98 -> 11, etc
        //        return (int)asciiBytes - 97 + 10;

        //    }
        //    else
        //    {//esta en el rango  65-90 ( mayusculas ) //65 -> 36, 66 -> 37, etc
        //        return (int)asciiBytes - 65 + 36;


        //    }


        //}

        ////string a ="";
        ////   string aback = "";
        ////   string b = "";
        ////   string bback = "";
        ////   string c = "";
        ////   string cback = "";
        ////   string d = "";
        ////   string dback = "";
        ////   int originalA = 1;
        ////   int originalb = 11;
        ////   int originalc = 99;
        ////   int originald = 100;


        ////   a = helper.decTo61(originalA);
        ////   aback = helper.from61ToDec(a);

        ////   b = helper.decTo61(originalb);
        ////   bback = helper.from61ToDec(b);

        ////   c = helper.decTo61(originalc);
        ////   cback = helper.from61ToDec(c);

        ////   d = helper.decTo61(originald);
        ////   dback = helper.from61ToDec(d);


        ////   MessageBox.Show(originalA.ToString() + "-" +a + "=" + aback);
        ////   MessageBox.Show(originalb.ToString() + "-" +b + "=" + bback);
        ////   MessageBox.Show(originalc.ToString() + "-" +c + "=" + cback);
        ////   MessageBox.Show(originald.ToString() + "-" + d + "=" + dback);




        //public string conversorAuxiliar(string[] acambiar)
        //{
        //    int aux = 0;
        //    string[] news = new string[acambiar.Length];
        //    string sarasa = "";
        //    foreach (string s in acambiar)
        //    {
        //        if (s != "*")
        //        {
        //            sarasa += BusinessComponents.StockMetros.decTo61(Convert.ToInt32(s), true) + ",";
        //        }
        //        else
        //        {
        //            sarasa += "*,";
        //        }

        //        aux++;

        //    }

        //    return sarasa;
        //}
    }
}
