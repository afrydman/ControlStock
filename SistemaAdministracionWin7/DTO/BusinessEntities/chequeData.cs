using System;
using System.Text;

namespace DTO.BusinessEntities
{


    public enum EstadoCheque
    {
       EnCartera=0,
       Anulado = 1,
       Depositado = 2,
       Entregado = 3,
       Rechazado = 4,
       Acreditado = 5,
       Creado = 6,
       EntregadoSinOpago = 7

    }
    public class ChequeData  : GenericObject
    {

        public ChequeData() { 
    
         FechaEmision = Convert.ToDateTime(HelperDTO.BEGINNING_OF_TIME);
         FechaCobro = Convert.ToDateTime(HelperDTO.BEGINNING_OF_TIME);
         Date = Convert.ToDateTime(HelperDTO.BEGINNING_OF_TIME);
         FechaAnuladooRechazado = Convert.ToDateTime(HelperDTO.BEGINNING_OF_TIME);
         BancoEmisor = new BancoData();
         Local = new LocalData();
         EstadoCheque = EstadoCheque.Creado;
         Chequera = new ChequeraData();
        }

        public DateTime Date { get; set; }

        public BancoData BancoEmisor { get; set; }
        public int Interno { get; set; }
        public string Numero { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaCobro { get; set; }
        
        public string Titular { get; set; }

        public decimal Monto { get; set; }
        public EstadoCheque EstadoCheque { get; set; }
       
        public ChequeraData Chequera { get; set; }
        
        public LocalData Local { get; set; }
        public DateTime FechaAnuladooRechazado { get; set; }

        public string Show
        {
            get
            {
                StringBuilder toshow = new StringBuilder();

                toshow.Append(Interno);
                toshow.Append(" - ");
                toshow.Append(Numero);
                toshow.Append(" - ");
                if (BancoEmisor != null)
                    toshow.Append(BancoEmisor.Description);

                return toshow.ToString();
            }
        }
    }
}
