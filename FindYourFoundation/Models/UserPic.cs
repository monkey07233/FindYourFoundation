using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Models
{
    public class UserPic
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public DateTime CreateTime { get; set; }
    }
}