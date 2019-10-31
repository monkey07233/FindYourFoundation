using FindYourFoundation.Models;
using FindYourFoundation.Services;
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
    public class CartController : ApiController
    {
        private CartService _cartService = new CartService();
        private string secret = "FindYourFoundation";
        // GET: Cart
        [HttpGet]
        public List<CartViewModel> GetCartByAcc()
        {
            var jwtObject = GetjwtToken();
            var cart = _cartService.GetCartByAcc(jwtObject["Account"].ToString());
            if (cart != null)
            {
                return cart;
            }
            return null;
        }
        [HttpPost]
        public string AddToCart(CartViewModel cart)
        {
            var jwtObject = GetjwtToken();
            _cartService.AddToCart(jwtObject["Account"].ToString(), cart.Product_Id);
            return "新增成功";
        }
        [HttpPost]
        public string DeleteCartById(CartViewModel cart)
        {
            var jwtObject = GetjwtToken();
            _cartService.DeleteCartById(jwtObject["Account"].ToString(), cart.Product_Id);
            return "刪除成功";
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