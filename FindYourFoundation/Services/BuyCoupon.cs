﻿using FindYourFoundation.Repositoires;
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
                if (times>=1 && _couponRepo.GetCouponByAcc(c.Account, 3) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,3);
                }
                if (times >= 5 && _couponRepo.GetCouponByAcc(c.Account, 4) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,4);
                }
                if (times >= 10 && _couponRepo.GetCouponByAcc(c.Account, 5) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,5);
                }
                if (times >= 15 && _couponRepo.GetCouponByAcc(c.Account, 6) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,6);
                }
                if (times >= 20 && _couponRepo.GetCouponByAcc(c.Account, 7) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,7);
                }
                if (times >= 25 && _couponRepo.GetCouponByAcc(c.Account, 8) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,8);
                }
                if (times >= 30 && _couponRepo.GetCouponByAcc(c.Account, 9) == null)
                {
                    _couponRepo.AddBuyTimesCoupon(c.Account,9);
                }
            }
        }
    }
}