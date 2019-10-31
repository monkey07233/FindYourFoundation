using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using FindYourFoundation.Services;
using FindYourFoundation.ViewModels;
using Jose;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace FindYourFoundation.Controllers
{
    public class CustomerController : ApiController
    {
        private CustomerService _customerService = new CustomerService();
        private UserPicService _userPicService = new UserPicService();
        private JwtAuthorizeServices _jwtAuthorizeServices = new JwtAuthorizeServices();
        private string secret = "FindYourFoundation";
        // GET: Customer
        [HttpPost]
        public JArray Login(LoginViewModel login)
        {
            Customer customer = _customerService.CheckLogin(login);
            var result = new JArray();
            if (customer != null)
            {
                string jwtToken = _jwtAuthorizeServices.CreateToken(customer);
                if (jwtToken != null)
                {
                    result.Add(new JObject
                    {
                        {"status",true },
                        {"message","登入成功" },
                        {"token",jwtToken },
                    });
                }
            }
            else
            {
                result.Add(new JObject
                {
                    {"status",false },
                    {"message","帳號或密碼錯誤" },
                    {"token",null },
                });
            }
            return result;
        }
        [HttpPost]
        public string Register(Customer customer)
        {
            return _customerService.Register(customer);
        }
        [HttpPost]
        public string ModifyPassword(PasswordViewModel password)
        {
            return _customerService.ModifyPassword(password.oldPassword, password.newPassword);
        }
        [HttpGet]
        public Customer UserInformation()
        {
            var jwtObject = GetjwtToken();
            return _customerService.GetUserByAcc(jwtObject["Account"].ToString());
        }
        [HttpPost]
        public string UserInformation(Customer customer)
        {
            try
            {
                var jwtObject = GetjwtToken();
                var getcustomer = _customerService.GetUserByAcc(jwtObject["Account"].ToString());
                if (getcustomer != null)
                {
                    _customerService.UpdateUser(customer);
                    return "儲存成功";
                }
                else
                {
                    return "儲存失敗";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        [HttpGet]
        public string GetUserPic()
        {
            var jwtObject = GetjwtToken();
            UserPic userPic = _userPicService.GetPicByAcc(jwtObject["Account"].ToString());
            if (userPic != null)
            {
                System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
                return urlHelper.Content("~/UserPic/" + userPic.Name);
            }
            return null;
        }
        [HttpPost]
        public string UploadUserPic()
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
                        var jwtObject = GetjwtToken();
                        UserPic userPic = _userPicService.GetPicByAcc(jwtObject["Account"].ToString());
                        string filename = Path.GetFileName(file.FileName);
                        string Url = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/UserPic/"), filename);
                        file.SaveAs(Url);
                        if (userPic != null)
                        {
                            new UserPicRepo().UpdateUserPic(jwtObject["Account"].ToString(), file.FileName, Url, file.ContentLength, file.ContentType);
                        }
                        else
                        {
                            new UserPicRepo().InsertUserPic(jwtObject["Account"].ToString(), file.FileName, Url, file.ContentLength, file.ContentType);
                        }
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
    }
}