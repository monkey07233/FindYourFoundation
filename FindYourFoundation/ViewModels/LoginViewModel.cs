using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindYourFoundation.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Account { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}