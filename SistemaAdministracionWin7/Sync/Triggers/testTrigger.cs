using System;
using System.Configuration;
using Quartz;

namespace Sync.Jobs
{
    internal class defaultUploadTrigger
    {
        public static ITrigger Get( string id , int minutesInterval = 60, int whenstart = 5)
        {
            
            return TriggerBuilder.Create()
                      .WithIdentity(id,"Upload")
                       .WithSimpleSchedule(x => x
                            .WithIntervalInMinutes(minutesInterval)
                            
                            .RepeatForever())
                        .WithPriority(10)//higger fire first
                        .StartAt(DateBuilder.FutureDate(whenstart, IntervalUnit.Second))
                      .Build();
            
        }
    }
    internal class defaultDownloadTrigger
    {
        public static ITrigger Get(string id, int minutesInterval = 60, int whenstart = 20)
        {
            return TriggerBuilder.Create()
                      .WithIdentity(id,"Download")
                       .WithSimpleSchedule(x => x
                            .WithIntervalInMinutes(minutesInterval)

                            .RepeatForever())
                        .WithPriority(1)//higger fire first
                        .StartAt(DateBuilder.FutureDate(whenstart, IntervalUnit.Second))
                      .Build();

        }
    }

}
