using FindYourFoundation.Repositoires;
using FindYourFoundation.Services;
using FindYourFoundation.ViewModels;
using Jose;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class FaceController : ApiController
    {
        private FaceService _faceService = new FaceService();
        private FaceRepo _faceRepo = new FaceRepo();
        private string secret = "FindYourFoundation";
        [HttpPost]
        public void InsertFacePic()
        {
            var jwtObject = GetjwtToken();
            var HttpRequest = HttpContext.Current.Request;
            if (HttpRequest.Files.Count > 0)
            {
                foreach (string name in HttpRequest.Files.Keys)
                {
                    var file = HttpRequest.Files[name];
                    if ((!file.ContentType.Equals("image/jpg")) && (!file.ContentType.Equals("image/png")) && (!file.ContentType.Equals("image/jpeg")))
                    {
                        //result = "檔案格式請選取jpg,png";
                    }
                    else
                    {
                        string filename = jwtObject["Account"].ToString() + "_face_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        string Url = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/FacePic/"), filename);
                        file.SaveAs(Url);
                    }
                }
            }
        }
        [HttpPost]
        public async Task<FaceViewModel> SkinDetection()
        {
            var jwtObject = GetjwtToken();
            var HttpRequest = HttpContext.Current.Request;
            FaceViewModel faceViewModel = new FaceViewModel();
            if (HttpRequest.Files.Count > 0)
            {
                foreach (string name in HttpRequest.Files.Keys)
                {
                    var file = HttpRequest.Files[name];
                    if ((!file.ContentType.Equals("image/jpg")) && (!file.ContentType.Equals("image/png")) && (!file.ContentType.Equals("image/jpeg")))
                    {
                        //result = "檔案格式請選取jpg,png";
                    }
                    else
                    {
                        string filename = jwtObject["Account"].ToString() + "_face_" + DateTime.Now.ToString("yyyyMMddHHmmss")+".jpg";
                        string Url = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/FacePic/"), filename);
                        file.SaveAs(Url);
                        faceViewModel.Account = jwtObject["Account"].ToString();
                        faceViewModel.FaceUrl = "/FacePic/" + Path.GetFileNameWithoutExtension(Url) + Path.GetExtension(Url);
                        var skin = await _faceService.GetSkin(Path.GetFileNameWithoutExtension(Url) + Path.GetExtension(Url));
                        string[] result = skin.Split(';');
                        faceViewModel.FaceColor = result[0];
                        var product = new ProductRepo().GetProductByTicket(result[1]);
                        faceViewModel.Product_Id = product.Product_Id;
                        faceViewModel.Brand = product.Brand;
                        faceViewModel.Name = product.Name;
                        faceViewModel.Color = product.Color;
                        faceViewModel.ProductUrl= "/ProductPic/" + Path.GetFileNameWithoutExtension(product.Url) + Path.GetExtension(product.Url);
                        faceViewModel.FaceDate = DateTime.Now;
                        new FaceRepo().AddFaceHistory(Url, faceViewModel);
                    }
                }               
            }
            return faceViewModel;
        }
        [HttpGet]
        public List<FaceViewModel> GetFaceHistoryByAcc()
        {
            var jwtObject = GetjwtToken();
            var history = _faceRepo.GetFaceHistoryByAcc(jwtObject["Account"].ToString());
            if (history != null)
            {
                foreach(var face in history)
                {
                    face.FaceUrl="/FacePic/"+Path.GetFileNameWithoutExtension(face.FaceUrl) + Path.GetExtension(face.FaceUrl);
                    face.ProductUrl = "/ProductPic/" + Path.GetFileNameWithoutExtension(face.ProductUrl) + Path.GetExtension(face.ProductUrl);
                }
            }
            return history;
        }
        public Dictionary<string, object> GetjwtToken()
        {
            var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                    ActionContext.Request.Headers.Authorization.Parameter,
                    Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS512);
            return jwtObject;
        }
    }
}
