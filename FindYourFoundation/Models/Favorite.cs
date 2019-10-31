using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class Favorite
    {
        public int Favorite_Id { get; set; }
        public string Account { get; set; }
        public int Product_Id { get; set; }
        public DateTime Favorite_Time { get; set; }
    }
}