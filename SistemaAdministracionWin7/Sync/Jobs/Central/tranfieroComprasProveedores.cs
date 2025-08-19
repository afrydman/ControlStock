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
    class tranfieroComprasProveedores : IJob
    {



        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroClientesJob>()
                .WithIdentity("clientes_" + category)
                .UsingJobData("descargar", descargar)
                .UsingJobData("contadorReal", 0)
                .UsingJobData("CountEjecuccionDiferencia", CountEjecuccionDiferencia)

                .Build();

        }

        public void Execute(IJobExecutionContext context)
        {
          
          
            try
            {

               JobDataMap dataMap = context.JobDetail.JobDataMap;
               if(JobHelper.FireNow(context))
                   update(dataMap.GetBoolean("descargar")); ;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                context.JobDetail.JobDataMap["completo"] = true;

            }

        }

        private void update(bool isdownload)
        {
            return;
        }

        private void update2(bool isdownload)
        {

            if (isdownload)
            {
                conexion.getRemoteConection();
            }
            else
            {
                conexion.getLocalConection();
            }

            List<clienteData> clienteRemoto = BusinessComponents.cliente.getAll();


            if (!isdownload)
            {
                conexion.getRemoteConection();
            }
            else
            {
                conexion.getLocalConection();
            }
            List<clienteData> clienteLocal = BusinessComponents.cliente.getAll();


            foreach (clienteData p in clienteRemoto)
            {

                if (clienteLocal.Find(delegate(clienteData pp) { return pp.id == p.id; }) == null)
                {
                    //nuevo
                    BusinessComponents.cliente.insert(p);
                }
                else
                {
                    //update
                    BusinessComponents.cliente.update(p);
                }


            }

        }

    }
}
