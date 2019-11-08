using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class Coupon
    {
        public int Counpon_Id { get; set; }
        public string Name { get; set; }
        public int Coupon_price { get; set; }
        public int ValidTime { get; set; }
    }
}