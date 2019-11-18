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
        public GetAge GetAge()
        {
            return Query<GetAge>(@"select sum(case when year(getdate())-year(Birthday) <= 20 then 1 else 0 end) as underTwenty,
                                    sum(case when year(getdate())-year(Birthday) between 21 and 30 then 1 else 0 end) as twoTothree,
                                    sum(case when year(getdate())-year(Birthday) between 31 and 40 then 1 else 0 end) as threeTofour,
                                    sum(case when year(getdate())-year(Birthday) between 41 and 50 then 1 else 0 end) as fourTofive,
                                    sum(case when year(getdate())-year(Birthday) >50 then 1 else 0 end) as overFive
                                    from Customer").FirstOrDefault();
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
    public class GetAge
    {
        public int underTwenty { get; set; }
        public int twoTothree { get; set; }
        public int threeTofour { get; set; }
        public int fourTofive { get; set; }
        public int overFive { get; set; }
    }
}