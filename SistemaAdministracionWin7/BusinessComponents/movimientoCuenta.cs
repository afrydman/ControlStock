namespace BusinessComponents
{
    public static class movimientoCuenta
    {
        //public static bool insert(DTO.BusinessEntities.MovimientoCuentaData mov)
        //{

            
        //    double aux = 0;
        //    if (mov.ID==Guid.Empty)
        //    {
        //        mov.ID = Guid.NewGuid();
        //    }
        //    if (mov.cheque.ID!=Guid.Empty)
        //    {

        //        //o es un deposito  de un cheque de otro a mi cuenta o estoy pagando un cheque mio.
        //        chequeData c = BusinessComponents.cheque.getbyId(mov.cheque.ID);

        //        if (c.chequera.ID==Guid.Empty)
        //        {//es de tercero y lo estoy cobrando en una cuenta bancaria
        //            c.estado = estadoCheque.Depositado;
                    
        //            BusinessComponents.cuenta.updateSaldo(mov.cuentaDestino.ID, mov.Monto);
        //        }
        //        else
        //        {//es mio  y lo estoy pagando desde mi cuenta bancaria
        //            c.estado = estadoCheque.Acreditado;
        //            BusinessComponents.cuenta.updateSaldo(mov.cuentaOrigen.ID, -1*(mov.Monto));
        //        } 
        //        BusinessComponents.cheque.update(c);




        //    }
        //    else
        //    {
        //        //es una extraccion o deposito de efectivo
        //        retiroData n = new retiroData();
        //        n.fecha = DateTime.Now;
        //        n.fechaUso = DateTime.Now;
        //        n.Local.ID = helper.IDLocal;

        //        n.Monto = mov.Monto;
        //        n.ID = mov.ID;

        //        n.Personal.ID = Guid.Empty;
        //        n.Prefix = helper.firstNum;
        //        retiroData rr = null;

        //        mov.cuentaOrigen = BusinessComponents.cuenta.getbyId(mov.cuentaOrigen.ID);
        //        mov.cuentaDestino = BusinessComponents.cuenta.getbyId(mov.cuentaDestino.ID);

        //        if (mov.cuentaDestino.ID!=mov.cuentaOrigen.ID)
        //        {// no creacion
        //            cuentaData auxCuenta = null;
                    
        //            if (mov.cuentaDestino.tipoCuenta == TipoCuenta.Otra)
        //            {//banco -> caja chica
        //                rr =BusinessComponents.ingreso.getLast(helper.IDLocal, helper.firstNum);
        //                auxCuenta = BusinessComponents.cuenta.getbyId(mov.cuentaOrigen.ID);
        //                BusinessComponents.cuenta.updateSaldo(mov.cuentaOrigen.ID, -1 * (mov.Monto));
        //                n.Numero = ++rr.Numero;
        //                n.desc = "Movimiento de: " + auxCuenta.Show;
        //                n.tipoRetiro.ID = helper.IDextraccioCuenta;
        //                BusinessComponents.ingreso.insertarRetiro(n);
        //            }
        //            else
        //            {//caja chica -> banco
        //                rr = BusinessComponents.retiro.getLast(helper.IDLocal, helper.firstNum);
        //                BusinessComponents.cuenta.updateSaldo(mov.cuentaDestino.ID,  (mov.Monto));
        //                auxCuenta = BusinessComponents.cuenta.getbyId(mov.cuentaDestino.ID);
        //                n.desc = "Movimiento a: " + auxCuenta.Show;
        //                n.tipoRetiro.ID = helper.IDdepositoCuenta;
        //                n.Numero = ++rr.Numero;
        //                BusinessComponents.retiro.insertarRetiro(n);
        //            }
                    
                    
        //          }

        //    }

            


        //    return movimientoCuentaDataMapper.insert(mov);
        //}

        //public static string getNro(bool completo)
        //{
        //    MovimientoCuentaData aux = getLast(helper.firstNum, helper.IDLocal);
        //    aux.Numero++;
        //    if (completo)
        //    {
        //        return aux.Show;
        //    }
        //    else
        //    {
        //        return aux.Numero.ToString();
        //    }
            
        //}

        //private static MovimientoCuentaData getLast(int Prefix, Guid idlocal)
        //{
        //    return movimientoCuentaDataMapper.getLast(Prefix, idlocal);
        //}

        //public static List<MovimientoCuentaData> getbyCajaDestino(Guid guid,bool complete)
        //{
        //    List<MovimientoCuentaData>  aux = movimientoCuentaDataMapper.getbyCajaDestino(guid);
        //    if (complete)
        //    {
        //        foreach (MovimientoCuentaData m in aux)
        //        {
        //            m.cuentaDestino = BusinessComponents.cuenta.getbyId(m.cuentaDestino.ID);
                    
        //            m.cuentaOrigen = BusinessComponents.cuenta.getbyId(m.cuentaOrigen.ID);
                   
                    
        //            m.cheque = BusinessComponents.cheque.getbyId(m.cheque.ID, true);
                    
        //            m.Local = BusinessComponents.Local.getbyID(m.Local.ID);


        //        }
        //    }
            
        //    return aux;
        //}

        //public static List<MovimientoCuentaData> getbyCajaOrigen(Guid guid, bool complete)
        //{
        //    List<MovimientoCuentaData> aux = movimientoCuentaDataMapper.getbyCajaOrigen(guid);
        //    if (complete)
        //    {
        //        foreach (MovimientoCuentaData m in aux)
        //        {
        //            m.cuentaDestino = BusinessComponents.cuenta.getbyId(m.cuentaDestino.ID);

        //            m.cuentaOrigen = BusinessComponents.cuenta.getbyId(m.cuentaOrigen.ID);


        //            m.cheque = BusinessComponents.cheque.getbyId(m.cheque.ID, true);

        //            m.Local = BusinessComponents.Local.getbyID(m.Local.ID);


        //        }
        //    }
        //    return aux;
        //}


        //public static MovimientoCuentaData getById(Guid guid) { return getById(guid, false); }

        //public static MovimientoCuentaData getById(Guid guid,bool completo)
        //{
        //    MovimientoCuentaData m =movimientoCuentaDataMapper.getbyid(guid);

        //    if (completo)
        //    {
        //        m.cuentaDestino = BusinessComponents.cuenta.getbyId(m.cuentaDestino.ID);
        //        m.cuentaOrigen = BusinessComponents.cuenta.getbyId(m.cuentaOrigen.ID);
        //        m.Local = BusinessComponents.Local.getbyID(m.Local.ID);
        //    }
        //    return m;
        //}

        //public static bool anular(Guid guid)
        //{
        //    MovimientoCuentaData mov = getById(guid, true);
        //    //

        //    if (mov.cuentaDestino.tipoCuenta == TipoCuenta.Otra)
        //    {//banco -> caja chica
        //        BusinessComponents.cuenta.updateSaldo(mov.cuentaOrigen.ID,  (mov.Monto));
        //    }
        //    else
        //    {//caja chica -> banco
        //        BusinessComponents.cuenta.updateSaldo(mov.cuentaDestino.ID, -1*(mov.Monto));
        //    }
                    
            
        //    return movimientoCuentaDataMapper.disable(guid);
        //}

        //public static MovimientoCuentaData getbyCheque(Guid guid)
        //{
        //    MovimientoCuentaData aux = movimientoCuentaDataMapper.getbyCheque(guid);
            

        //    aux.cuentaDestino = BusinessComponents.cuenta.getbyId(aux.cuentaDestino.ID);

        //    return aux;
        //}
    }
}
