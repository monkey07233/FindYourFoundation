using FindYourFoundation.Models;
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
    public class BuyHistoryController : ApiController
    {
        private BuyHistoryRepo _buyHistoryRepo = new BuyHistoryRepo();
        private string secret = "FindYourFoundation";
        // GET: BuyHistory
        [HttpGet]
        public List<BuyHistoryViewModel> GetBuyHistories()
        {
            var jwtObject = GetjwtToken();
            return _buyHistoryRepo.GetBuyHistories(jwtObject["Account"].ToString());
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