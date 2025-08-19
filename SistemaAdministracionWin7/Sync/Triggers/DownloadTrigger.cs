using System;
using System.Configuration;
using Quartz;

namespace Sync.Jobs
{
    //class DownloadTrigger
    //{

    //    public static ITrigger Get(int priority=1,int minutesInterval = 60)
    //    {
            
    //        return TriggerBuilder.Create()
    //                .WithDescription("DownloadTrigger")
                    
    //                .StartAt(DateBuilder.FutureDate(10, IntervalUnit.Second))
    //                .WithSimpleSchedule(x => x
                        
    //                  .WithRepeatCount(10)
    //                    .WithMisfireHandlingInstructionIgnoreMisfires()            
    //                    .WithIntervalInMinutes(minutesInterval))
    //                .WithPriority(priority)//download baja prioridad
    //                .Build();
    //    }
    //}
}
