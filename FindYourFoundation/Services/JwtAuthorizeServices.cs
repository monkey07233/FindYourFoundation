using FindYourFoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FindYourFoundation.Services
{
    public class JwtAuthorizeServices
    {
        public string CreateToken(Customer customer)
        {
            string secret = "FindYourFoundation";
            Dictionary<string, object> user = new Dictionary<string, object>();
            user.Add("Account", customer.Account);
            user.Add("Exp", DateTime.Now.AddMinutes(3).ToString());
            //user.Add("Exp", DateTime.Now.AddHours(1).ToString());
            var payload = user;
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), Jose.JwsAlgorithm.HS512);
            return token;
        }
    }
}