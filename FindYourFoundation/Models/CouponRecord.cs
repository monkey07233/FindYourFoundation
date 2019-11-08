using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class CouponRecord
    {
        public int CouponRecord_Id { get; set; }
        public string Account { get; set; }
        public int Type { get; set; }
        public string IsUse { get; set; }
        public DateTime CouponTime { get; set; }
    }
}