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
        public List<PieChart> GetGender()
        {
            return Query<PieChart>("select count(Gender) as value,Gender as label from Customer group by Gender").ToList();
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
        public List<DateTime> GetBuyFrequency(string Account)
        {
            return Query<DateTime>("select convert(varchar, BuyTime, 120) as [Date] from BuyHistory where Account=@Account group by convert(varchar, BuyTime, 120) order by [Date]", new { Account }).ToList();
        }
        public List<PieChart> GetBuyBrand(string Account)
        {
            return Query<PieChart>("select sum(Quantity) as [value],b.Brand as [label] from BuyHistory as a,Product as b where a.Product_Id = b.Product_Id and a.Account=@Account group by b.Brand", new { Account }).ToList();
        }
    }
    public class GetBrandHistory
    {
        public string Brand { get; set; }
        public int times { get; set; }
    }
    public class PieChart
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