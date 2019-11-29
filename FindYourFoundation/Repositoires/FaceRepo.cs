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
    }
}