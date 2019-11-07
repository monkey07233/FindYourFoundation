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
            }
        }
    }
}