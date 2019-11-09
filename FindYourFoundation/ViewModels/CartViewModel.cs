using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.ViewModels
{
    public class CartViewModel
    {
        public int Product_Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Cheapest_price { get; set; }
        public string Url { get; set; }
        public int Quantity { get; set; }

        public List<CartViewModel> cartList { get; set; }
        public int CouponRecord_Id { get; set; }
    }
}