using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ChartRepo : DataAccessLayer
    {
        public List<ChartViewModel> GetBrandHistory()
        {
            return Query<ChartViewModel>(@"select a.Brand,b.times
                                        from Product as a
                                        left join (select Product_Id,sum(Quantity) as times from BuyHistory group by Product_Id ) as b
                                        on a.Product_Id = b.Product_Id
                                        order by b.times desc").ToList();
        }
    }
}