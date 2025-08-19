using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;
using Sync.Jobs.Clientes;

namespace Sync.Jobs
{
    class tranfieroProveedoresJob : LoggeableJob, IJob
    {
        public tranfieroProveedoresJob()
            : base("tranfieroProveedoresJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroProveedoresJob>()
                .WithIdentity("proveedores_" + category)
                .UsingJobData("descargar", descargar)
                .UsingJobData("contadorReal", 0)
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

        private void update(bool donwload)
        {

            bool connLocal;
            bool aux = false;
            connLocal = !donwload;


            
            List<proveedorData> proveedoresRemoto = BusinessComponents.proveedor.getAll(false,connLocal);
            if (proveedoresRemoto!=null)
                Log("Se encontraron " + proveedoresRemoto.Count.ToString() + " en DB_local:" + connLocal.ToString());


            connLocal = donwload;

            List<proveedorData> proveedoresLocal = BusinessComponents.proveedor.getAll(false,connLocal);
            if (proveedoresLocal != null)
                Log("Se encontraron " + proveedoresLocal.Count.ToString() + " en DB_local" + connLocal.ToString());

            foreach (proveedorData p in proveedoresRemoto)
            {

                if (proveedoresLocal.Find(delegate(proveedorData pp) { return pp.id == p.id; }) == null)
                {//nuevo
                    
                    aux = BusinessComponents.proveedor.insert(p, connLocal);
                    Log("Insertando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());

                }
                else
                {//update
                    aux = BusinessComponents.proveedor.update(p, connLocal);
                    Log("Updateando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }


            }
        }

    }
}
