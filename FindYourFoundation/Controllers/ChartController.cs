using FindYourFoundation.Repositoires;
using FindYourFoundation.Services;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class ChartController : ApiController
    {
        private ChartRepo _chartRepo = new ChartRepo();
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
        public List<GetGender> GetGender()
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
        public class Bar
        {
            public string genre { get; set; }
            public int sold { get; set; }
        }
    }
}