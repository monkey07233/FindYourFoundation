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
        public string Color { get; set; }
        public string Ticket { get; set; }
        public string Info { get; set; }
    }
}