using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.ViewModels
{
    public class CouponViewModel
    {
        public int CouponRecord_Id { get; set; }
        public string Name { get; set; }
        public int Coupon_price { get; set; }
        public string IsUse { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}