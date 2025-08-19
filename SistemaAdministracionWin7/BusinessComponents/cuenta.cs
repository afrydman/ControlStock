namespace BusinessComponents
{
    public static class cuenta
    {
        //public static cuentaData getbyId(Guid id)
        //{
        //    cuentaData c =  cuentaDataMapper.getbyid(id);
            
            
        //    c.banco = BusinessComponents.banco.getbyId(c.banco.ID);

        //    return c;
        //}
        //public static List<cuentaData> getAll()
        //{
        //    return getAll(false, false);
        //}
        //public static List<cuentaData> getAll(bool onlyEnable,bool loadBanco)
        //{
        //    List<cuentaData> aux = cuentaDataMapper.getAll();



        //    if (onlyEnable)
        //    {
        //        aux = aux.FindAll(delegate(cuentaData c) { return c.enable; });
        //    }
        //    if (loadBanco)
        //    {
        //        foreach (cuentaData c in aux)
        //        {
        //            c.banco = banco.getbyId(c.banco.ID);
        //        }

        //    }

        //    return aux;
        //}
        //public static bool insert(cuentaData c)
        //{
        //    if (c.ID==null || c.ID  == Guid.Empty)
        //    {
        //        c.ID = Guid.NewGuid();
        //    }
        //    return cuentaDataMapper.insert(c);
        //}

        //public static bool disable(cuentaData c)
        //{
        //    return cuentaDataMapper.disable(c.ID);
        //}




        //public static List<cuentaData> getcuentasCorrientes()
        //{
        //    List<cuentaData> aux = getAll(true, true);

        //    aux = aux.FindAll(delegate(cuentaData c) { return c.esCuentaCorriente; });

        //    return aux;
        //}

        //public static List<cuentaData> getOtras()
        //{

        //    List<cuentaData> aux = getAll(true, true);

        //    aux = aux.FindAll(delegate(cuentaData c) { return c.tipoCuenta == TipoCuenta.Otra; });

        //    return aux;
            
        //}

        //public static List<cuentaData> getCuentasBancarias()
        //{
        //    List<cuentaData> aux = getAll(true, true);

        //    aux = aux.FindAll(delegate(cuentaData c) { return c.tipoCuenta == TipoCuenta.Banco; });

        //    return aux;
        //}
        //public static bool updateSaldo(Guid idcuenta, decimal saldomodificar)
        //{
        //    return updateSaldo(idcuenta, saldomodificar, true);
        //}
        //public static bool updateSaldo(Guid idcuenta, decimal saldomodificar, bool agregar)
        //{
        //    decimal saldoActual = 0;
        //    if (agregar)
        //        saldoActual = getbyId(idcuenta).saldo;

        //    return cuentaDataMapper.updateSaldo(idcuenta, saldoActual + saldomodificar);
        //}
    }
}
