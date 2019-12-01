using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.ViewModels
{
    public class FaceViewModel
    {
        public string Account { get; set; }
        public string FaceUrl { get; set; }
        public string FaceColor { get; set; }
        public int Product_Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Ticket { get; set; }
        public string ProductUrl { get; set; }
        public DateTime FaceDate { get; set; }
    }
}