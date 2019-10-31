using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class PriceList
    {
        public List<int> Wastons { get; set; }
        public List<int> Books { get; set; }
        public List<int> Cosmed { get; set; }
        public List<int> Momo { get; set; }
        public List<int> YahooBuy { get; set; }
        public List<int> YahooMall { get; set; }
    }
}