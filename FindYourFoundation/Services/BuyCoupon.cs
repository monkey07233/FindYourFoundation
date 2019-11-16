using FindYourFoundation.Repositoires;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class BuyCoupon:IJob
    {
        private static CustomerRepo _customerRepo = new CustomerRepo();
        private static CouponRepo _couponRepo = new CouponRepo();
        private static BuyHistoryRepo _buyHistoryRepo = new BuyHistoryRepo();
        public void Execute(IJobExecutionContext context)
        {
            AutoSendBuyTimesCoupon();
        }
        public static void AutoSendBuyTimesCoupon()
        {
            var customer = _customerRepo.GetCustomers();
            foreach(var c in customer)
            {
                var times = _buyHistoryRepo.GetBuyTimes(c.Account);
                if (times.Count >=1 && _couponRepo.GetCouponByAcc(c.Account, 3) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,3);
                }
                if (times.Count >= 5 && _couponRepo.GetCouponByAcc(c.Account, 4) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,4);
                }
                if (times.Count >= 10 && _couponRepo.GetCouponByAcc(c.Account, 5) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,5);
                }
                if (times.Count >= 15 && _couponRepo.GetCouponByAcc(c.Account, 6) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,6);
                }
                if (times.Count >= 20 && _couponRepo.GetCouponByAcc(c.Account, 7) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,7);
                }
                if (times.Count >= 25 && _couponRepo.GetCouponByAcc(c.Account, 8) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,8);
                }
                if (times.Count >= 30 && _couponRepo.GetCouponByAcc(c.Account, 9) == 0)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,9);
                }
            }
        }
    }
}