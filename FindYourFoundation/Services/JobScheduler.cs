using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FindYourFoundation.Services
{
    public class JobScheduler
    {
        public static void Start(bool toggle)
        {
            if (toggle)
            {
                IScheduler scheduler = new StdSchedulerFactory().GetScheduler();

                //IJobDetail autoSendCoupon = JobBuilder.Create<AutoSendCoupon>()
                //    .WithIdentity("AutoSendCoupon")
                //    .Build();
                //ITrigger trigger= TriggerBuilder.Create()
                //                .WithCronSchedule("0 0/1 * * * ?")
                //                .WithIdentity("SendMailTrigger")
                //                .Build();
                //scheduler.ScheduleJob(autoSendCoupon, trigger);
            }
        }
    }
}