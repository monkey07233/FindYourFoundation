using FindYourFoundation.Models;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class CartRepo : DataAccessLayer
    {
        public List<CartViewModel> GetCartByAcc(string acc)
        {
            return Query<CartViewModel>(@"select a.Product_Id,a.Brand,a.[Name],a.Cheapest_price,c.[Url]
                                from Product as a,Cart as b,ProductPic as c
                                where a.Product_Id = b.Product_Id and c.Product_Id = a.Product_Id and b.Account = @acc and a.Isout = 0"
                                , new
                                {
                                    acc
                                }).ToList();
        }
        public void DeleteCart(string acc)
        {
            Execute("delete from Cart where Account = @acc", new { acc });
        }
        public void DeleteCartById(string acc,int productId)
        {
            Execute("delete from Cart where Account = @acc and Product_Id = @productId"
                , new {
                    acc = acc,
                    productId = productId
                });
        }
        public void AddToCart(string acc, int productId)
        {
            Execute(@"insert into Cart(Account,Product_Id)
                        values(@acc, @productId)"
                , new
                {
                    acc = acc,
                    productId = productId
                });
        }
        public void AddBuyProduct(CartViewModel cart,string Account)
        {
            Execute(@"insert into BuyHistory(Account,Product_Id,Price,Quantity,BuyTime)
                        values(@account,@product_Id,@price,@quantity,@buyTime)"
                , new
                {
                    account = Account,
                    product_Id = cart.Product_Id,
                    price = cart.Cheapest_price,
                    quantity = cart.Quantity,
                    buyTime = DateTime.Now
                });
        }
    }
}