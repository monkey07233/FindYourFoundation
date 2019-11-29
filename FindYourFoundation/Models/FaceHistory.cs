using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class FaceHistory
    {
        public int Face_Id { get; set; }
        public string Account { get; set; }
        public string Url { get; set; }
        public string FaceColor { get; set; }
        public int Product_Id { get; set; }
        public DateTime FaceDate { get; set; }
    }
}