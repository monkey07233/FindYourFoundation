using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using FindYourFoundation.Services;
using FindYourFoundation.ViewModels;
using Jose;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        [HttpGet]
        public Product GetProductById(Product product)
        {
            return _productRepo.GetProductById(product.Product_Id);
        }
        [HttpPost]
        public string ModifyProduct(Product product)
        {
            return _productRepo.ModifyProduct(product);
        }
        [HttpPost]
        public string AddFavorite(Product product)
        {
            var jwtObject = GetjwtToken();
            return _productService.AddFavorite(jwtObject["Account"].ToString(), product.Product_Id);
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




        //public int test()
        //{
        //    return new DataMiningService().GetPrice();
        //}
        static int price = 0;
        public async Task<int> test()
        {
            string FileName = @"wastons.py";
            await RunPython(FileName, "-u", "夢幻奇蹟無瑕粉底液");
            return price;
        }
        public static async Task RunPython(string name, string args, string pro)
        {
            await Task.Run(() => {
                Process process = new Process();
                //string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + name;
                string path = @"D:/Python/" + name;
                process.StartInfo.FileName = @"D:/Anaconda3/python.exe";
                path += " " + pro;
                process.StartInfo.Arguments = path;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                
                process.OutputDataReceived += new DataReceivedEventHandler(OutputData);
                using (process)
                {
                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                    process.Close();
                }
            });  
        }
        static void OutputData(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                if (e.Data.ToString() == "None")
                {

                }
                price = Convert.ToInt32(e.Data);
                Console.WriteLine(e.Data);
            }
        }
    }
}