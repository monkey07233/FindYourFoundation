using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.ViewModels
{
    public class BuyHistoryViewModel
    {
        public int BuyHistory_Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime BuyTime { get; set; }
    }
}