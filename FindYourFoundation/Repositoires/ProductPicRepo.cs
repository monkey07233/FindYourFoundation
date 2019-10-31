using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ProductPicRepo : DataAccessLayer
    {
        public void InsertProductPic(string FileName, string Url, int Size, string Type)
        {
            Execute(@"insert into ProductPic(Name,Url,Size,Type,CreateTime)
                        values(@name,@url,@size,@type,@createTime)"
                    , new
                    {
                        name = FileName,
                        url = Url,
                        size = Size,
                        type = Type,
                        createTime = DateTime.Now
                    });
        }
    }
}