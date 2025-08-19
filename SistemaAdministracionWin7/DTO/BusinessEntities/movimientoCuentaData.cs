using System;

namespace DTO.BusinessEntities
{
    public class MovimientoCuentaData : MovimientoEnCajaData
    {
        public enum TipoMovimientoCuenta
        {
            SinDefinir = 0,
            RetiroCajaChica = 1,//banco -> caja chicha
            IngresoCajaChica = 2,//caja chica -> banco
            CobroCheque = 3,// cheque - > banco
            PagoCheque = 4//banco -> outside
        }


        public TipoMovimientoCuenta TipoMovimiento
        {
            get
            {


                if (cheque == null || cheque.ID == Guid.Empty)
                {// es retiro, deposito o sindf
                    if (cuentaDestino != null && cuentaDestino.ID != Guid.Empty)
                    {
                        if ((cuentaDestino.TipoCuenta == TipoCuenta.Otra) && (cuentaOrigen.TipoCuenta == TipoCuenta.Banco))
                        {
                            return TipoMovimientoCuenta.IngresoCajaChica;
                        }
                        else if ((cuentaDestino.TipoCuenta == TipoCuenta.Banco) && (cuentaOrigen.TipoCuenta == TipoCuenta.Otra))
                        {
                            return TipoMovimientoCuenta.RetiroCajaChica;

                        }
                        else
                        {
                            return TipoMovimientoCuenta.SinDefinir;
                        }
                    }
                    else
                    {
                        return TipoMovimientoCuenta.SinDefinir;
                    }


                }
                else
                {// es cobro o pago de cheque 
                    if ((cuentaDestino == null || cuentaDestino.ID == Guid.Empty) && (cuentaOrigen != null && cuentaOrigen.ID != Guid.Empty))
                    {//cobro cheque
                        return TipoMovimientoCuenta.PagoCheque;
                    }
                    else if ((cuentaOrigen == null || cuentaOrigen.ID == Guid.Empty) && (cuentaDestino != null && cuentaDestino.ID != Guid.Empty))
                    {//pago cheque
                        return TipoMovimientoCuenta.CobroCheque;

                    }
                    else
                    {
                        return TipoMovimientoCuenta.SinDefinir;

                    }


                }


            }
        }

        public CuentaData cuentaOrigen { get; set; }
        public CuentaData cuentaDestino { get; set; }

        public ChequeData cheque { get; set; }
        public MovimientoCuentaData()
        {

            cuentaDestino = new CuentaData();
            cuentaOrigen = new CuentaData();
            cheque = new ChequeData();


        }
    }
}
