using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl.Matchers;

namespace Sync.Jobs
{
    public static class JobHelper
    {
        public  enum statusJob
        {
            sleep = 0,
            run = 1,

        }

        public static bool RunForrest(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            bool isdownload = dataMap.GetBoolean("descargar");
            int CountEjecuccionDiferencia = dataMap.GetIntValue("CountEjecuccionDiferencia");
            string whoIam = context.JobDetail.Key.Name;
            string whoIsMyTwin = whoIam.Split('_')[0] + "_" + !isdownload;
            int my_counter = dataMap.GetIntValue("contadorReal");

            var executingJobs = context.Scheduler.GetCurrentlyExecutingJobs();
            foreach (var job in executingJobs)
            {//busco si esta el hmno opuesto ejecutandose, si es asi, retorno sin hacer nada dado que no pueden 
             //estar ambos a la vez
                if (job.JobDetail.Key.ToString().StartsWith(context.JobDetail.Key.ToString().Split('_')[0]))
                {

                    if (job.JobDetail.JobDataMap.GetBoolean("descargar") != isdownload)
                    {//encontre al hmno, entonces veo si se esta ejecutando o esta en tiempo muerto.
                        if (job.JobDetail.JobDataMap.GetIntValue("status")==(int)statusJob.run)
                        {//se esta ejecutando asi q no te puedo ejecutar a vos 
                            return false;     
                        }
                        else
                        {//lo encontre pero sin ejecutarse, de todas formas voy a ver si el ratio permite que lo corra
                            int twin_counter = job.JobDetail.JobDataMap.GetIntValue("contadorReal");
                            if ((my_counter - twin_counter) > CountEjecuccionDiferencia)
                            {
                                return false;
                            }
                        }

                    }
                    else
                    {//soy yo mismo asi q todo bien.
                        

                    }
                    
                }
            }

            return true;

        }

        internal static bool FireNow(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            if (!JobHelper.RunForrest(context))
                return false;

            int my_counter = dataMap.GetIntValue("contadorReal");
            my_counter++;
            context.JobDetail.JobDataMap["contadorReal"] = my_counter;
            context.JobDetail.JobDataMap["status"] = (int)statusJob.run;

            return true;

        }
    }
}
