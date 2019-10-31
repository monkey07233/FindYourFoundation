using FindYourFoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ProductRepo:DataAccessLayer
    {
        public List<Product> GetProducts()
        {
            return Query<Product>("select * from Product").ToList();
        }
        public List<string> GetProductName()
        {
            return Query<string>("select Name from Product").ToList();
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
        public string DeleteProduct(string Product_Id)
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
        public Product GetProductById(string Product_Id)
        {
            return Query<Product>("select * from Product where Product_Id = @Product_Id", new { Product_Id }).FirstOrDefault();
        }
    }
}