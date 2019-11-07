using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class ProductService
    {
        private ProductRepo _productRepo = new ProductRepo();
        public string AddFavorite(string account,int productid)
        {
            var favorite = _productRepo.CheckFavorite(account,productid);
            if (favorite == null)
            {
                return _productRepo.AddFavorite(account, productid);
            }
            else
            {
                return "已加入至我的最愛";
            }
        }
    }
}