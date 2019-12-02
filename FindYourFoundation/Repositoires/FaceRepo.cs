using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class FaceRepo : DataAccessLayer
    {
        public void AddFaceHistory(string Url,FaceViewModel faceViewModel)
        {
            Execute(@"insert into FaceHistory(Account,Url,FaceColor,Product_Id,FaceDate)
                        values(@account,@url,@faceColor,@product_Id,@faceDate)",
                        new
                        {
                            account = faceViewModel.Account,
                            url = Url,
                            faceColor=faceViewModel.FaceColor,
                            product_id = faceViewModel.Product_Id,
                            faceDate = DateTime.Now
                        });
        }
        public List<FaceViewModel> GetFaceHistoryByAcc(string Account)
        {
            return Query<FaceViewModel>("select a.Account,a.[Url] as FaceUrl,a.FaceColor,a.Product_Id,b.Brand,b.[Name],b.Color,c.[Url] as ProductUrl,a.FaceDate from FaceHistory as a,Product as b,ProductPic as c where a.Product_Id = b.Product_Id and b.Product_Id = c.Product_Id and a.Account = @Account", new { Account }).ToList();
        }
    }
}