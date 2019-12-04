using FindYourFoundation.Repositoires;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class BuyTimeCoupon : IJob
    {
        private static CouponRepo _couponRepo = new CouponRepo();
        public void Execute(IJobExecutionContext context)
        {
            AutoSendBuyTimeCoupon();
        }
        private static void AutoSendBuyTimeCoupon()
        {
            var customer = new CustomerRepo().GetCustomers();
            foreach (var c in customer)
            {
                var days = 0;
                var buyTime = new BuyHistoryRepo().GetBuyTimesDesc(c.Account);
                foreach (var time in buyTime)
                {
                    DateTime temp = new DateTime();
                    if (temp != null)
                    {
                        TimeSpan timeSpan = DateTime.Parse(time) - temp;

                    }
                    else
                    {
                        temp = DateTime.Parse(time);
                    }
                }

            }
        }
    }
}