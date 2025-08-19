using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroProductosJob : LoggeableJob, IJob
    {
        public tranfieroProductosJob()
            : base("tranfieroProductosJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroProductosJob>()
                .WithIdentity("productos_" + category)
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
        private void update(bool download)
        {
            List<productoData> productosRemoto;
            List<productoData> productosLocal;
            List<Guid> newProductsId = new List<Guid>();



            bool connLocal;
            bool aux = false;
            connLocal = !download;



            productosRemoto = BusinessComponents.producto.getAll(false,connLocal);
            if (productosRemoto != null)
                Log("Se encontraron " + productosRemoto.Count + " en DB_local: " + connLocal.ToString());
           
            connLocal = download;

            productosLocal = BusinessComponents.producto.getAll(false, connLocal);
            if (productosLocal != null)
                Log("Se encontraron " + productosLocal.Count + " en DB_local: " + connLocal.ToString());
           

            foreach (productoData p in productosRemoto)
            {
                if ((productosLocal.Find(delegate(productoData pp)
                {
                    return pp.id == p.id;
                })) != null)
                {
                    aux = BusinessComponents.producto.update(p, connLocal);
                    Log("Insertando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {
                    aux = BusinessComponents.producto.insert(p, false, connLocal);
                    newProductsId.Add(p.id);
                    Log("Insertando " + p.id.ToString() + "en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
            }

          List<Guid> newProducts   =newProductsId;



            List<productoTalleData> productoTalleRemoto;
            
            connLocal =!download;

            productoTalleRemoto = BusinessComponents.productoTalle.getAll(connLocal);


            List<productoTalleData> auxProductoTalle = productoTalleRemoto.FindAll(
                                                            delegate(productoTalleData p)
                                                            { return newProducts.Contains(p.idproducto); });
            connLocal = download;
            Log("Los talles..");
            foreach (productoTalleData p in auxProductoTalle)//agrego solo de los nuevos
            {
                aux = productoTalle.Insert(p,connLocal);
                Log("Insertando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());

            }
        }
    }
}
