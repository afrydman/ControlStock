namespace BusinessComponents
{
    public static class chequera
    {


        //public static List<DTO.BusinessEntities.chequeraData> getAll(bool onlyEnable)
        //{
        //    List<DTO.BusinessEntities.chequeraData> aux = chequeraDataMapper.getAll();

        //    aux.Sort(delegate(chequeraData x, chequeraData y)
        //    {
        //        return x.codigoInterno.CompareTo(y.codigoInterno);
        //    });
            
        //    if (onlyEnable)
        //    {
        //        aux = aux.FindAll(delegate(chequeraData c) { return c.enable; });
        //    }

        //    foreach (chequeraData c in aux)
        //    {
        //        if (c.cuenta.banco.ID==Guid.Empty)
        //        {
        //            c.cuenta = BusinessComponents.cuenta.getbyId(c.cuenta.ID);
        //        }
        //        c.cuenta.banco = BusinessComponents.banco.getbyId(c.cuenta.banco.ID);
        //    }
        //    return aux;
        //}


      


        //public static string obtenerSiguienteInterno()
        //{
        //    List<chequeraData> cs = getAll(false);

        //    if (cs != null && cs.Count > 0)
        //    {//ver si es el ultimo o el primero!
        //        return (cs[cs.Count - 1].codigoInterno + 1).ToString("0000");
        //    }
        //    else
        //    {
        //        return "0001";
        //    }      
        //}

        //public static bool insert(chequeraData c)
        //{
        //    return chequeraDataMapper.insert(c);
        //}

        //public static bool disable(Guid guid)
        //{
        //    return chequeraDataMapper.disable(guid);
        //}

        //public static chequeraData getbyid(Guid id, bool completo)
        //{
        //    chequeraData c = chequeraDataMapper.getById(id);

        //    if (completo)
        //    {
        //        c.cuenta = BusinessComponents.cuenta.getbyId(c.cuenta.ID);
        //        c.cuenta.banco = BusinessComponents.banco.getbyId(c.cuenta.banco.ID);
        //    }

        //    return c;

        //}

        //public static bool existeEsteCheque(Guid idChequera,string numeroVerificar)
        //{
        //    List<chequeData> chequesChequera = BusinessComponents.cheque.getByChequera(idChequera,false);

        //    foreach (chequeData c in chequesChequera)
        //    {
        //        if (!c.anulado && c.Numero == numeroVerificar )
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public static bool SetearSiguiente(chequeraData c)
        //{
        //    int len1 = 0;
        //    int len2 = 0;
        //    len1 = c.siguiente.Length;
            
        //    string newSiguiente = (Convert.ToInt32(c.siguiente) + 1).ToString();
        //    len2 = newSiguiente.Length;

        //    string aux = "";
        //    for (int i = 0; i < len1 - len2; i++)
        //    {
        //        aux += "0";
        //    }
        //    aux += newSiguiente;
            
        //    return chequeraDataMapper.SetearSiguiente(c.ID, aux);
        //}
    }
}
