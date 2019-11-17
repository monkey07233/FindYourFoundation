using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Jose;

namespace FindYourFoundation.Services
{
    public class JwtAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string secret = "FindYourFoundation";
            var request = actionContext.Request;
            if (!WithoutVerifyToken(request.RequestUri.ToString()))
            {
                if (request.Headers.Authorization == null || request.Headers.Authorization.Scheme != "Bearer")
                {
                    throw new Exception("Token認證錯誤");
                }
                else
                {
                    var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                    request.Headers.Authorization.Parameter,
                    Encoding.UTF8.GetBytes(secret),JwsAlgorithm.HS512);

                    if (IsTokenExpired(jwtObject["Exp"].ToString()))
                    {
                        throw new Exception("Token已過期");
                    }
                }
            }
            base.OnActionExecuting(actionContext);
        }

        public bool WithoutVerifyToken(string requestUri)
        {
            if (requestUri.EndsWith("/Login") || requestUri.EndsWith("/Register") || requestUri.EndsWith("/GetProducts") || requestUri.EndsWith("/GetProductsDesc") || requestUri.EndsWith("/GetProductsHot") || requestUri.EndsWith("/GetProductsHotTop3") || requestUri.EndsWith("/AddContact") || requestUri.EndsWith("/GetCheapPrice") || requestUri.Contains("/SearchProduct") || requestUri.Contains("/GetBrandHistory") || requestUri.Contains("/GetGender"))
            {
                return true;
            }

            return false;
        }

        public bool IsTokenExpired(string dateTime)
        {
            return Convert.ToDateTime(dateTime) < DateTime.Now;
        }
    }
}