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
        public void AddToCart(string acc, int productId)
        {
            _cartRepo.AddToCart(acc, productId);
        }
    }
}