using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;


namespace Sync.Jobs
{
    class tranfieroRemitosJob : LoggeableJob,IJob
    {
        public tranfieroRemitosJob()
            : base("tranfieroRemitosJob")
        {
        }


        public static IJobDetail Get(Guid idLocal, int prefix, bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroRemitosJob>()
                .WithIdentity("remitos_" + category)
                 .UsingJobData("prefix", prefix)
                .UsingJobData("idLocal", idLocal.ToString())
             
                .UsingJobData("descargar", descargar)
                .UsingJobData("contadorReal", 0)
                .UsingJobData("status", (int)JobHelper.statusJob.sleep)
                .UsingJobData("CountEjecuccionDiferencia", CountEjecuccionDiferencia)

                .Build();

        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Log("Arrancando");

                JobDataMap dataMap = context.JobDetail.JobDataMap;
                if (JobHelper.FireNow(context))
                {
                    Log("Disparando");
                     update(dataMap.GetBoolean("descargar"), new Guid(dataMap.GetString("idLocal")), dataMap.GetInt("prefix"));
                     Log("Completo");
                }
                else
                {
                    Log("No puedo disparar ahora");
                }


            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);

            }
            finally
            {


                context.JobDetail.JobDataMap["status"] = (int)JobHelper.statusJob.sleep;

            }
            Close();
        }
       

     
        private void update(bool download, Guid idLocal,int prefix)//subo los q cree y actualizo los que confirme
        {
            bool connLocal;
            bool aux = false;
            connLocal = download;



            remitoData remito = BusinessComponents.remito.getLast(idLocal, prefix, connLocal);//busco por local Origen


            connLocal = !download;
            List<remitoData> remitosLocal = BusinessComponents.remito.getOlderThan(remito, idLocal, false, prefix, connLocal);//esto ya me trae los de origen aca
            
            if (remitosLocal!=null)
                Log("Se encontraron " + remitosLocal.Count.ToString());
            
            connLocal = download;

            foreach (remitoData r in remitosLocal)
            {
                
                aux = BusinessComponents.remito.generarNuevo(r,connLocal);
                Log("Transmitiendo remito:  " + r.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }

            Log("Fin de remitos nuevos");

            Log("Actualizando remitos recibidos");
            //ahora me queda actualizar las altas que confirme
            //son los que tienen fecha de recibo mayor a 1800                  


            connLocal = download;


            remitoData remito2 = BusinessComponents.remito.getLastLocalRecibido(idLocal, prefix, connLocal);//busco por local Origen

            connLocal = !download;

            List<remitoData> remitosLocal2 = BusinessComponents.remito.getByLocalRecibido(idLocal, false, connLocal);
            //busco id local destino

            DateTime fechaAlpha = new DateTime(2000,1,1);

            if (remitosLocal2!=null && remitosLocal2.Count>0)
            {
                fechaAlpha = remitosLocal2[0].FechaRecibo;
            }
            remitosLocal2 = remitosLocal2.FindAll(delegate(remitoData r) { return r.localOrigen.id != idLocal && r.FechaRecibo > fechaAlpha; });

            connLocal = download;

            if (remitosLocal2 != null)
                Log("Se encontraron " + remitosLocal2.Count.ToString() + " Remitos a actualizar");
            foreach (remitoData r in remitosLocal2)
            {
                
                aux = BusinessComponents.remito.confirmarRecibo(r.id, r.FechaRecibo,connLocal);
                Log("Transmitiendo la confirmacion del remito:  " + r.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }
            Log("Fin de remitos a actualizar");
        }
    }
}
