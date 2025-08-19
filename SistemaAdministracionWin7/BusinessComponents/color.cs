namespace BusinessComponents
{
    public class color

    {
        //public static colorData getColorByID(Guid idColor)
        //{

        //    return colorDataMapper.getColorByID(idColor);


        //}

        //public static List<colorData> getAll()
        //{
        //    return getAll(true);
        //}
        //public static List<colorData> getAll(bool onlyEnable,bool connLocal=true)
        //{
        //    List<colorData> cs = colorDataMapper.getAll(connLocal);

            
        //    cs.Sort(delegate(colorData x, colorData y)
        //    {//inverso?
        //        return x.descripcion.CompareTo(y.descripcion);
        //    });


        //    if (onlyEnable)
        //    {
        //        cs = cs.FindAll(delegate(colorData c)
        //        {
        //            return c.enable;
        //        });
        //    }

        //    return cs;
        //}

        //public static bool insert(colorData ncolor, bool connLocal = true)
        //{
        //    if (ncolor.ID == null || ncolor.ID == new Guid())
        //    {
        //        ncolor.ID = Guid.NewGuid();

        //    }
        //    ncolor.enable = true;
        //    return colorDataMapper.insert(ncolor, connLocal);
        //}

        //public static bool delete(Guid guid)
        //{
        //    return colorDataMapper.disable(guid);
        //}

        //public static colorData getByCodigo(string col)
        //{
        //    if (col.Length>3)
        //    {//probable q te este pasando todo el codigo de barra
        //        col = col.Substring(7, 3);
        //    }
        //    return colorDataMapper.getColorByCodigo(col);
        //}

       
    }

}
