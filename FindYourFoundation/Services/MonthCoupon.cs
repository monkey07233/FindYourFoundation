using FindYourFoundation.Repositoires;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class MonthCoupon:IJob
    {
        private static CouponRepo _couponRepo = new CouponRepo();
        public void Execute(IJobExecutionContext context)
        {
            AutoSendBirthdayCoupon();
        }
        private static void AutoSendBirthdayCoupon()
        {
            var customer = new CustomerRepo().GetCustomers();
            var NowMonth = DateTime.Now.Month;
            foreach(var c in customer)
            {
                if (c.Birthday.Month == NowMonth)
                {
                    _couponRepo.AddBirthdayCoupon(c.Account);
                }
            }
        }
        private static void AutoSendAnniversaryCoupon()
        {
            var customer = new CustomerRepo().GetCustomers();
            if (DateTime.Now.Month == 12)
            {
                foreach (var c in customer)
                {
                    _couponRepo.AddAnniversaryCoupon(c.Account);
                }
            }           
        }
    }
}