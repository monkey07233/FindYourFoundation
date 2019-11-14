using FindYourFoundation.Repositoires;
using FindYourFoundation.ViewModels;
using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class CouponController : ApiController
    {
        private CouponRepo _couponRepo = new CouponRepo();
        private string secret = "FindYourFoundation";
        // GET: Coupon
        [HttpGet]
        public List<CouponViewModel> GetCoupons()
        {
            var jwtObject = GetjwtToken();
            return _couponRepo.GetCoupons(jwtObject["Account"].ToString());
        }
        public Dictionary<string, object> GetjwtToken()
        {
            var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                    ActionContext.Request.Headers.Authorization.Parameter,
                    Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS512);
            return jwtObject;
        }
    }
}