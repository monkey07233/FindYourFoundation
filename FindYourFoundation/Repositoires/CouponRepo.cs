using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class CouponRepo : DataAccessLayer
    {
        public void AddBirthdayCounpon(string account)
        {
            Execute(@"insert into CouponRecord(Account,Type,CouponTime)
                    values(@Account,@Type,@CouponTime)",
                    new {
                        Account=account,
                        Type=1,
                        CouponTime=DateTime.Now
                    });
        }
    }
}