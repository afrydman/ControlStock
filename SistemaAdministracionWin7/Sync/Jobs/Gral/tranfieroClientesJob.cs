using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;
using SharedForms.ConsoleWriterService;
using Quartz.Impl.Matchers;

namespace Sync.Jobs.Clientes
{
    class tranfieroClientesJob :LoggeableJob, IJob
    {
        public tranfieroClientesJob()
            : base("tranfieroClientesJob")
        {
        }

        public static IJobDetail Get(bool descargar, int CountEjecuccionDiferencia = 20)
        {
            ;
            return JobBuilder.Create<tranfieroClientesJob>()
               .WithIdentity("cliente_" + descargar.ToString())
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
                    update(dataMap.GetBoolean("descargar"));
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

       
        private void update(bool isdownload)
        {
            bool connLocal;

            connLocal = !isdownload;


            List<clienteData> clienteRemoto = BusinessComponents.cliente.getAll(false, connLocal);
            if (clienteRemoto!=null)
                Log("Se encontraron "  + clienteRemoto.Count + " en la conexion local:" + connLocal.ToString());

            connLocal = isdownload;
            List<clienteData> clienteLocal = BusinessComponents.cliente.getAll(false, connLocal);
            if (clienteLocal != null)
                Log("Se encontraron " + clienteLocal.Count + " en la conexion local:" + connLocal.ToString());

            bool aux;
            foreach (clienteData p in clienteRemoto)
            {

                if (clienteLocal.Find(delegate(clienteData pp) { return pp.id == p.id; }) == null)
                {
                    //nuevo
                    aux = BusinessComponents.cliente.insert(p, connLocal);
                    
                    Log("Insertando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {
                    //update
                    aux = BusinessComponents.cliente.update(p, connLocal);
                    
                    Log("Update " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }



            }

        }

    }
}
