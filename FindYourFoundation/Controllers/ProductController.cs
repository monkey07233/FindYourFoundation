using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using FindYourFoundation.Services;
using FindYourFoundation.ViewModels;
using Jose;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class ProductController : ApiController
    {
        private ProductService _productService = new ProductService();
        private ProductRepo _productRepo = new ProductRepo();
        private string secret = "FindYourFoundation";
        // GET: Product
        [HttpGet]
        public List<ProductViewModel> GetProducts()
        {
            var product = _productRepo.GetProducts();
            if (product != null)
            {
                foreach (var p in product)
                {
                    p.Url = "/ProductPic/" + Path.GetFileNameWithoutExtension(p.Url) + Path.GetExtension(p.Url);
                }
            }
            return product;
        }
        [HttpGet]
        public List<ProductViewModel> GetProductsByAcc()
        {
            var jwtObject = GetjwtToken();
            var product = _productRepo.GetProducts();
            if (product != null)
            {
                foreach (var p in product)
                {
                    var favorite = _productRepo.CheckFavorite(jwtObject["Account"].ToString(),p.Product_Id);
                    if (favorite != null)
                    {
                        p.isFavorite = true;
                    }
                    p.Url = "/ProductPic/" + Path.GetFileNameWithoutExtension(p.Url) + Path.GetExtension(p.Url);
                }
            }
            return product;
        }
        [HttpPost]
        public ProductViewModel GetProductById(Product product)
        {
            return _productRepo.GetProductById(product.Product_Id);
        }
        [HttpPost]
        public string AddProduct()
        {
            Product product = new Product();
            product.Brand= HttpContext.Current.Request.Params["Brand"];
            product.Name = HttpContext.Current.Request.Params["Name"];
            product.Color = HttpContext.Current.Request.Params["Color"];
            product.Ticket = HttpContext.Current.Request.Params["Ticket"];
            product.Info = HttpContext.Current.Request.Params["Info"];
            product.Original_price = Convert.ToInt32(HttpContext.Current.Request.Params["Original_price"]);
            string PicResult=InsertProductPic();
            if(PicResult== "上傳圖片成功")
            {
                return _productRepo.AddProduct(product);
            }
            else
            {
                return PicResult;
            }
        }
        [HttpPost]
        public string DeleteProduct(Product product)
        {
            return _productRepo.DeleteProduct(product.Product_Id);
        }
        [HttpPost]
        public string OutProduct(Product product)
        {
            return _productRepo.OutProduct(product.Product_Id);
        }
        [HttpPost]
        public string AddFavorite(Product product)
        {
            var jwtObject = GetjwtToken();
            return _productService.AddFavorite(jwtObject["Account"].ToString(), product.Product_Id);
        }
        [HttpPost]
        public string CancelFavorite(Product product)
        {
            var jwtObject = GetjwtToken();
            return _productRepo.CancelFavorite(jwtObject["Account"].ToString(), product.Product_Id);
        }
        public string InsertProductPic()
        {
            var HttpRequest = HttpContext.Current.Request;
            if (HttpRequest.Files.Count > 0)
            {
                string result = "";
                foreach (string name in HttpRequest.Files.Keys)
                {
                    var file = HttpRequest.Files[name];
                    if ((!file.ContentType.Equals("image/gif")) && (!file.ContentType.Equals("image/png")) && (!file.ContentType.Equals("image/jpeg")))
                    {
                        result = "檔案格式請選取gif,jpg,png";
                    }
                    else
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string Url = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/ProductPic/"), filename);
                        file.SaveAs(Url);
                        new ProductPicRepo().InsertProductPic(file.FileName, Url, file.ContentLength, file.ContentType);
                        result = "上傳圖片成功";
                    }
                }
                return result;
            }
            else
            {
                return "請選擇上傳圖片";
            }
        }
        public Dictionary<string, object> GetjwtToken()
        {
            var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                    ActionContext.Request.Headers.Authorization.Parameter,
                    Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS512);
            return jwtObject;
        }
        public async void GetCheapPrice()
        {
            var ParityList = new List<Parity>();
            string[] ShopList = { "books", "cosmed", "momo", "wastons", "yahoobuy", "yahoomall" };
            var NameList = _productRepo.GetProductName();
            foreach (var Name in NameList)
            {
                foreach (var Shop in ShopList)
                {
                    var parity = new Parity();
                    var price = await new DataMiningService().GetPrice(Name,Shop);
                    parity.ProductName = Name;
                    parity.ShopName = Shop;
                    parity.Price = price;
                    ParityList.Add(parity);                    
                }
            }
            var cheapPrice =
                from parity in ParityList
                where parity.Price > 0
                group parity by parity.ProductName into parityGroup
                select new
                {
                    Name = parityGroup.Key,
                    Price = parityGroup.Min(p => p.Price),
                };
            foreach(var list in cheapPrice)
            {
                _productRepo.UpdatePrice(list.Name, list.Price);
            }
        }
        public class Parity
        {
            public string ProductName { get; set; }
            public string ShopName { get; set; }
            public int Price { get; set; }
        }
    }
}