using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class Product
    {
        public int Product_Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Ticket { get; set; }
        public string Info { get; set; }
        public int Original_price { get; set; }
        public int Cheapest_price { get; set; }
    }
}