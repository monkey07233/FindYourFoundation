using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.ViewModels
{
    public class ProductViewModel
    {
        public int Product_Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Color { get; set; }
        public int Original_price { get; set; }
        public int Cheapest_price { get; set; }
        public string Url { get; set; }
        public bool isFavorite { get; set; }
    }
}