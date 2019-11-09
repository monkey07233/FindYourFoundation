using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class BuyHistory
    {
        public int BuyHistory_Id { get; set; }
        public string Account { get; set; }
        public int Product_Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime BuyTime { get; set; }
    }
}