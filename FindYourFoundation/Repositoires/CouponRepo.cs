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
            Execute(@"insert into CouponRecord(Account,Type,CouponTime)
                    values(@Account,@Type,@CouponTime)",
                    new {
                        Account=account,
                        Type=1,
                        CouponTime=DateTime.Now
                    });
        }
        public void AddAnniversaryCoupon(string account)
        {
            Execute(@"insert into CouponRecord(Account,Type,CouponTime)
                    values(@Account,@Type,@CouponTime)",
                    new
                    {
                        Account = account,
                        Type = 2,
                        CouponTime = DateTime.Now
                    });
        }
        public void UseCoupon(int CouponRecord_Id)
        {
            Execute("update CouponRecord set IsUse = 1 where CouponRecord_Id = @CouponRecord_Id", new { CouponRecord_Id });
        }
    }
}