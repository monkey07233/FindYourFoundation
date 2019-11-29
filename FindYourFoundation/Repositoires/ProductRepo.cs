using FindYourFoundation.Models;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ProductRepo : DataAccessLayer
    {
        public List<ProductViewModel> GetProductsDesc()
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.Name,a.Info,a.Original_price,a.Cheapest_price,a.IsOut,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and a.IsOut = 0 order by a.Cheapest_price desc").ToList();
        }
        public List<ProductViewModel> GetProducts()
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.Name,a.Info,a.Original_price,a.Cheapest_price,a.IsOut,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and a.IsOut = 0 order by a.Cheapest_price").ToList();
        }
        public List<ProductViewModel> GetProductsHot()
        {
            return Query<ProductViewModel>(@"select a.Product_Id,a.Brand,a.[Name],a.Original_price,a.Cheapest_price,a.IsOut,a.[Url],b.times
                                            from (select p.Product_Id,p.Brand,p.[Name],p.Original_price,p.Cheapest_price,p.IsOut,ProductPic.[Url] from Product as p,ProductPic where p.Product_Id = ProductPic.Product_Id and p.IsOut = 0) as a
                                            left join (select Product_Id,sum(Quantity) as times from BuyHistory group by Product_Id ) as b
                                            on a.Product_Id = b.Product_Id
                                            order by b.times desc").ToList();
        }
        public List<ProductViewModel> GetProductsHotTop3()
        {
            return Query<ProductViewModel>(@"select top 3 a.Product_Id,a.Brand,a.[Name],a.Original_price,a.Cheapest_price,a.IsOut,a.[Url],b.times
                                            from (select p.Product_Id,p.Brand,p.[Name],p.Original_price,p.Cheapest_price,p.IsOut,ProductPic.[Url] from Product as p,ProductPic where p.Product_Id = ProductPic.Product_Id and p.IsOut = 0) as a
                                            left join (select Product_Id,sum(Quantity) as times from BuyHistory group by Product_Id ) as b
                                            on a.Product_Id = b.Product_Id
                                            order by b.times desc").ToList();
        }
        public List<ProductViewModel> SearchProduct(string search)
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.Name,a.Info,a.Original_price,a.Cheapest_price,a.IsOut,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and a.IsOut = 0 and (a.Brand like '%" + @search + "' or a.[Name] like '%" + @search + "%') order by a.Cheapest_price desc", new { search }).ToList();
        }
        public List<Product> GetProductsDescForAdmin()
        {
            return Query<Product>("select * from Product order by Cheapest_price desc").ToList();
        }
        public List<Product> GetProductsForAdmin()
        {
            return Query<Product>("select * from Product order by Cheapest_price").ToList();
        }
        public List<Product> GetProductsHotForAdmin()
        {
            return Query<Product>(@"select *
                                    from Product as a
                                    left join (select Product_Id,sum(Quantity) as times from BuyHistory group by Product_Id ) as b
                                    on a.Product_Id = b.Product_Id
                                    order by b.times desc").ToList();
        }
        public List<string> GetProductName()
        {
            return Query<string>("select Name from Product where IsOut = 0").ToList();
        }
        public ProductViewModel GetProductById(int Product_Id)
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.Name,a.Info,a.Original_price,a.Cheapest_price,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and a.IsOut = 0 and a.Product_Id = @Product_Id", new { Product_Id }).FirstOrDefault();
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
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public string OutProduct(int Product_Id)
        {
            try
            {
                Execute("update Product set IsOut=1 where Product_Id=@Product_Id", new { Product_Id });
                return "修改成功";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public string CancelOutProduct(int Product_Id)
        {
            try
            {
                Execute("update Product set IsOut=0 where Product_Id=@Product_Id", new { Product_Id });
                return "修改成功";
            }
            catch (Exception e)
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
        public string CancelFavorite(string Account, int Product_Id)
        {
            try
            {
                Execute(@"delete from Favorite where Account = @Account and Product_Id = @Product_Id",
                        new
                        {
                            Account = Account,
                            Product_Id = Product_Id
                        });
                return "移除最愛成功";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public void UpdatePrice(string name, int price)
        {
            Execute(@"update Product set Cheapest_price = @price where Name = @name", new { price, name });
        }
        public List<BuyHistoryViewModel> GetBuyHistories(string Account)
        {
            return Query<BuyHistoryViewModel>("select a.BuyHistory_Id,b.Brand,b.[Name],a.Price,a.Quantity,a.BuyTime from BuyHistory as a,Product as b where a.Product_Id = b.Product_Id and Account = @Account", new { Account }).ToList();
        }
        public List<string> GetProTicket()
        {
            return Query<string>("select Ticket from Product").ToList();
        } 
        public ProductViewModel GetProductByTicket(string ticket)
        {
            return Query<ProductViewModel>("select a.Product_Id,a.Brand,a.[Name],a.Ticket,b.[Url] from Product as a,ProductPic as b where a.Product_Id = b.Product_Id and Ticket = @ticket", new { ticket }).FirstOrDefault();
        }
    }
}