using FindYourFoundation.Models;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class BuyHistoryRepo : DataAccessLayer
    {
        public List<BuyHistoryViewModel> GetBuyHistories(string Account)
        {
            return Query<BuyHistoryViewModel>("select a.BuyHistory_Id,a.Product_Id,b.Brand,b.Name,a.Price,a.Quantity,a.BuyTime from BuyHistory as a,Product as b where a.Product_Id = b.Product_Id and a.Account = @Account", new { Account }).ToList();
        }
        public List<string> GetBuyTimes(string Account)
        {
            return Query<string>("select convert(varchar,BuyTime,111) as times from BuyHistory where Account=@Account group by convert(varchar,BuyTime,111)", new { Account }).ToList();           
        }
        public List<string> GetBuyTimesDesc(string Account)
        {
            return Query<string>("select convert(varchar,BuyTime,111) as times from BuyHistory where Account=@Account group by convert(varchar,BuyTime,111)  order by times desc", new { Account }).ToList();
        }
    }
}