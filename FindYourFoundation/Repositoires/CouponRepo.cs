using FindYourFoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class CouponRepo : DataAccessLayer
    {
        public void AddBirthdayCoupon(string account)
        {
            Execute(@"insert into CouponRecord(Account,Type,CouponTime,ExpiryTime)
                    values(@Account,@Type,@CouponTime)",
                    new {
                        Account=account,
                        Type=1,
                        CouponTime=DateTime.Now,
                        ExpiryTime=DateTime.Now.AddMonths(2)
                    });
        }
        public void AddAnniversaryCoupon(string account)
        {
            Execute(@"insert into CouponRecord(Account,Type,CouponTime,ExpiryTime)
                    values(@Account,@Type,@CouponTime)",
                    new
                    {
                        Account = account,
                        Type = 2,
                        CouponTime = DateTime.Now,
                        ExpiryTime= DateTime.Now.AddMonths(1)
                    });
        }
        public int GetCouponByAcc(string acc,int type)
        {
            return Query<int>("select CouponRecord_Id from CouponRecord where Account = @Account and Type = @Type", new { Account = acc, Type = type }).FirstOrDefault();
        }
        public void UseCoupon(int CouponRecord_Id)
        {
            Execute("update CouponRecord set IsUse = 1 where CouponRecord_Id = @CouponRecord_Id", new { CouponRecord_Id });
        }
    }
}