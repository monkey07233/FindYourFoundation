using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ChartRepo : DataAccessLayer
    {
        public List<GetBrandHistory> GetBrandHistory()
        {
            return Query<GetBrandHistory>(@"select a.Brand,b.times
                                        from Product as a
                                        left join (select Product_Id,sum(Quantity) as times from BuyHistory group by Product_Id ) as b
                                        on a.Product_Id = b.Product_Id
                                        order by b.times desc").ToList();
        }
        public List<GetGender> GetGender()
        {
            return Query<GetGender>("select count(Gender) as value,Gender as label from Customer group by Gender").ToList();
        }
    }
    public class GetBrandHistory
    {
        public string Brand { get; set; }
        public int times { get; set; }
    }
    public class GetGender
    {
        public string label { get; set; }
        public int value { get; set; }
    }
}