using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class CartService
    {
        private CartRepo _cartRepo = new CartRepo();
        public List<CartViewModel> GetCartByAcc(string acc)
        {
            return _cartRepo.GetCartByAcc(acc);
        }
        public void DeleteCart(string acc)
        {
            _cartRepo.DeleteCart(acc);
        }
        public void DeleteCartById(string acc,int productId)
        {
            _cartRepo.DeleteCartById(acc, productId);
        }
        public string AddToCart(string acc, int productId)
        {
            var cart = _cartRepo.GetCartByAcc(acc);
            bool isHave = true;
            foreach(var c in cart)
            {
                if (c.Product_Id == productId) { isHave = false; }
            }
            if (isHave)
            {
                _cartRepo.AddToCart(acc, productId);
                return "新增成功";
            }
            else
            {
                return "已在購物車";
            }            
        }
        public string BuyProducts(CartViewModel cart,string Account)
        {
            if (cart.cartList != null)
            {
                foreach(var c in cart.cartList)
                {
                    _cartRepo.AddBuyProduct(c,Account);                    
                }
                if (cart.CouponRecord_Id != -1)
                {
                    new CouponRepo().UseCoupon(cart.CouponRecord_Id);
                }                
                return "購買成功";
            }
            else
            {
                return "購買失敗";
            }
        }
    }
}