using FindYourFoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class UserPicRepo : DataAccessLayer
    {
        public UserPic GetPicByAcc(string acc)
        {
            return Query<UserPic>("select * from UserPic where Account = @acc", new { acc }).FirstOrDefault();
        }
        public void UpdateUserPic(string Account, string FileName, string Url, int Size, string Type)
        {
            Execute(@"Update UserPic
                        set Name=@name,Url=@url,Size=@size,Type=@type,CreateTime=@createTime
                        where Account=@account"
                    , new
                    {
                        name = FileName,
                        url = Url,
                        size = Size,
                        type = Type,
                        createTime = DateTime.Now,
                        account = Account
                    });
        }
        public void InsertUserPic(string Account, string FileName, string Url, int Size, string Type)
        {
            Execute(@"insert into UserPic(Account,Name,Url,Size,Type,CreateTime)
                        values(@account,@name,@url,@size,@type,@createTime)"
                    , new
                    {
                        account = Account,
                        name = FileName,
                        url = Url,
                        size = Size,
                        type = Type,
                        createTime = DateTime.Now
                    });
        }
    }
}