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
        private ChartService _chartService = new ChartService();
        private ChartRepo _chartRepo = new ChartRepo();
        // GET: Chart
        [HttpGet]
        public List<BrandHistory> GetBrandHistory()
        {
            List<BrandHistory> brandHistories = new List<BrandHistory>();
            var history = _chartRepo.GetBrandHistory();
            var list= history.GroupBy(h => h.Brand).Select(g => new { Brand = g.Key, times = g.Sum(q => q.times) });
            foreach(var b_history in list)
            {
                BrandHistory brandHistory = new BrandHistory();
                brandHistory.genre = b_history.Brand;
                brandHistory.sold = b_history.times;
                brandHistories.Add(brandHistory);
            }
            return brandHistories;
        }

        public class BrandHistory
        {
            public string genre { get; set; }
            public int sold { get; set; }
        }
    }
}