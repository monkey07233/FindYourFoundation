﻿using FindYourFoundation.Models;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ProductRepo:DataAccessLayer
    {
        public List<ProductViewModel> GetProducts()
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.Name,a.Info,a.Original_price,a.Cheapest_price,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and a.IsOut = 0").ToList();
        }
        public List<Product> GetProductsForAdmin()
        {
            return Query<Product>("select * from Product where IsOut = 0").ToList();
            //return Query<Product>("select * from Product where IsOut = 0 order by Cheapest_price desc").ToList();
        }
        public List<string> GetProductName()
        {
            return Query<string>("select Name from Product where IsOut = 0").ToList();
        }
        public ProductViewModel GetProductById(int Product_Id)
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.Name,a.Info,a.Original_price,a.Cheapest_price,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and a.IsOut = 0 and a.Product_Id = @Product_Id",new { Product_Id }).FirstOrDefault();
        }
        public string AddProduct(Product product)
        {
            try
            {
                Execute(@"insert into Product(Brand,Name,Color,Ticket,Info,Original_price)
                        values(@brand,@name,@color,@ticket,@info,@original_price)"
                    , new
                    {
                        brand = product.Brand,
                        name = product.Name,
                        color = product.Color,
                        ticket = product.Ticket,
                        info = product.Info,
                        original_price = product.Original_price
                    });
                return "新增成功";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public string DeleteProduct(int Product_Id)
        {
            try
            {
                Execute("delete from Product where Product_Id = @Product_Id", new { Product_Id });
                return "刪除成功";
            }catch(Exception e)
            {
                return e.ToString();
            }
        }
        public string OutProduct(int Product_Id)
        {
            try
            {
                Execute("update Product set IsOut=1 where Product_Id=@Product_Id",new { Product_Id });
                return "修改成功";
            }catch(Exception e)
            {
                return e.ToString();
            }
        }
        public Favorite CheckFavorite(string Account, int Product_Id)
        {
            return Query<Favorite>("select * from Favorite where Account = @Account and Product_Id = @Product_Id", new { Account, Product_Id }).FirstOrDefault();
        }
        public string AddFavorite(string Account, int Product_Id)
        {
            try
            {
                Execute(@"insert into Favorite(Account,Product_Id,Favorite_Time)
                        values(@account,@product_id,@favorite_time)",
                        new
                        {
                            account = Account,
                            product_id = Product_Id,
                            favorite_time = DateTime.Now
                        });
                return "新增成功";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public void UpdatePrice(string name,int price)
        {
            Execute(@"update Product set Cheapest_price = @price where Name = @name", new { price,name });
        }
    }
}