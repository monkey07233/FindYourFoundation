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

                IJobDetail monthCouponJob = JobBuilder.Create<MonthCoupon>().WithIdentity("MonthCoupon").Build();

                ITrigger monthCouponTrigger = TriggerBuilder.Create()
                                .WithCronSchedule("0 0 12 1 * ?")
                                .WithIdentity("monthCouponTrigger")
                                .Build();

                IJobDetail buyCouponJob = JobBuilder.Create<BuyCoupon>().WithIdentity("BuyCoupon").Build();

                ITrigger buyCouponTrigger = TriggerBuilder.Create()
                                .WithCronSchedule("0 0/5 * * * ?")
                                .WithIdentity("buyCouponTrigger")
                                .Build();

                scheduler.ScheduleJob(monthCouponJob, monthCouponTrigger);
                scheduler.ScheduleJob(buyCouponJob, buyCouponTrigger);
                scheduler.Start();
            }
        }
    }
}