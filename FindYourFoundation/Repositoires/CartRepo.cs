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
            return Query<CartViewModel>(@"select a.Brand,a.[Name],a.Color,a.Ticket,a.Info
                                from Product as a,Cart as b
                                where a.Product_Id = b.Product_Id and b.Account = @acc"
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
            Execute("delete from Cart where Account = @acc and productId = @productId"
                , new {
                    acc = acc,
                    productId = productId
                });
        }
        public void AddToCart(string acc, string productId)
        {
            Execute(@"insert into Cart(Account,Product_Id)
                        values(@acc, @productId)"
                , new
                {
                    acc = acc,
                    productId = Convert.ToInt32(productId)
                });
        }
    }
}