using FindYourFoundation.Repositoires;
using FindYourFoundation.Services;
using FindYourFoundation.ViewModels;
using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class ChartController : ApiController
    {
        private ChartRepo _chartRepo = new ChartRepo();
        private string secret = "FindYourFoundation";
        // GET: Chart
        [HttpGet]
        public List<Bar> GetBrandHistory()
        {
            List<Bar> brandHistories = new List<Bar>();
            var history = _chartRepo.GetBrandHistory();
            var list = history.GroupBy(h => h.Brand).Select(g => new { Brand = g.Key, times = g.Sum(q => q.times) });
            foreach (var b_history in list)
            {
                Bar brandHistory = new Bar();
                brandHistory.genre = b_history.Brand;
                brandHistory.sold = b_history.times;
                brandHistories.Add(brandHistory);
            }
            return brandHistories;
        }
        [HttpGet]
        public Web GetBrandHistoryForWeb()
        {
            Web brandHistories = new Web();
            brandHistories.labels = new List<string>();
            brandHistories.data = new List<int>();
            var history = _chartRepo.GetBrandHistory();
            var list = history.GroupBy(h => h.Brand).Select(g => new { Brand = g.Key, times = g.Sum(q => q.times) });
            foreach (var b_history in list)
            {
                brandHistories.labels.Add(b_history.Brand);
                brandHistories.data.Add(b_history.times);
            }
            return brandHistories;
        }
        [HttpGet]
        public List<PieChart> GetGender()
        {
            return _chartRepo.GetGender();
        }
        [HttpGet]
        public List<Bar> GetAge()
        {
            var age = _chartRepo.GetAge();
            List<Bar> ageList = new List<Bar>();
            for (int i = 0; i <= 4; i++)
            {
                Bar getAge = new Bar();
                if (i == 0)
                {
                    getAge.sold = age.underTwenty;
                    getAge.genre = "20歲以下";
                }
                if (i == 1)
                {
                    getAge.sold = age.twoTothree;
                    getAge.genre = "21歲-30歲";
                }
                if (i == 2)
                {
                    getAge.sold = age.threeTofour;
                    getAge.genre = "31歲-40歲";
                }
                if (i == 3)
                {
                    getAge.sold = age.fourTofive;
                    getAge.genre = "41歲-50歲";
                }
                if (i == 4)
                {
                    getAge.sold = age.overFive;
                    getAge.genre = "50歲以上";
                }
                ageList.Add(getAge);
            }
            return ageList;
        }
        [HttpGet]
        public List<Bar> GetBuyFrequency()
        {
            var jwtObject = GetjwtToken();
            var frequency = _chartRepo.GetBuyFrequency(jwtObject["Account"].ToString());
            var year = DateTime.Now.Year;
            int[] month = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<Bar> dateList = new List<Bar>();
            foreach (var date in frequency)
            {
                if (date.Year == year)
                {
                    if (date.Month == 1)
                    {
                        month[0]++;
                    }
                    else if (date.Month == 2)
                    {
                        month[1]++;
                    }
                    else if (date.Month == 3)
                    {
                        month[2]++;
                    }
                    else if (date.Month == 4)
                    {
                        month[3]++;
                    }
                    else if (date.Month == 5)
                    {
                        month[4]++;
                    }
                    else if (date.Month == 6)
                    {
                        month[5]++;
                    }
                    else if (date.Month == 7)
                    {
                        month[6]++;
                    }
                    else if (date.Month == 8)
                    {
                        month[7]++;
                    }
                    else if (date.Month == 9)
                    {
                        month[8]++;
                    }
                    else if (date.Month == 10)
                    {
                        month[9]++;
                    }
                    else if (date.Month == 11)
                    {
                        month[10]++;
                    }
                    else if (date.Month == 12)
                    {
                        month[11]++;
                    }
                }
            }
            Dictionary<string, int> getBuyDic = new Dictionary<string, int>();
            getBuyDic.Add("一月", month[0]);
            getBuyDic.Add("二月", month[1]);
            getBuyDic.Add("三月", month[2]);
            getBuyDic.Add("四月", month[3]);
            getBuyDic.Add("五月", month[4]);
            getBuyDic.Add("六月", month[5]);
            getBuyDic.Add("七月", month[6]);
            getBuyDic.Add("八月", month[7]);
            getBuyDic.Add("九月", month[8]);
            getBuyDic.Add("十月", month[9]);
            getBuyDic.Add("十一月", month[10]);
            getBuyDic.Add("十二月", month[11]);
            foreach (var buy in getBuyDic)
            {
                Bar getbuy = new Bar();
                getbuy.genre = buy.Key;
                getbuy.sold = buy.Value;
                dateList.Add(getbuy);
            }
            return dateList;
        }
        [HttpGet]
        public List<PieChart> GetBuyBrand()
        {
            var jwtObject = GetjwtToken();
            return _chartRepo.GetBuyBrand(jwtObject["Account"].ToString());
        }
        public Dictionary<string, object> GetjwtToken()
        {
            var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                    ActionContext.Request.Headers.Authorization.Parameter,
                    Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS512);
            return jwtObject;
        }
        public class Bar
        {
            public string genre { get; set; }
            public int sold { get; set; }
        }
        public class Web
        {
            public List<string> labels { get; set; }
            public List<int> data { get; set; }
        }
    }
}