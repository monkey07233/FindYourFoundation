using FindYourFoundation.Models;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class CustomerRepo : DataAccessLayer
    {
        public Customer CheckLoginUser(LoginViewModel login)
        {
            return Query<Customer>("select * from Customer where Account = @acc and Password = @pwd", new { acc = login.Account, pwd = login.Password }).FirstOrDefault();
        }
        public Customer GetUserByAcc(string acc)
        {
            return Query<Customer>("select * from Customer where Account = @acc", new { acc }).FirstOrDefault();
        }
        public Customer GetUserByPwd(string pwd)
        {
            return Query<Customer>("select * from Customer where Password = @pwd", new { pwd }).FirstOrDefault();
        }
        public string RegisterUser(Customer customer)
        {
            try
            {
                Execute(@"insert into Customer(Account,Password,Name,Gender,Birthday,Email,Phone,Address,RegisterTime)
                        values(@account,@password,@name,@gender,@birthday,@email,@phone,@address,@registerTime)"
                    , new
                    {
                        account = customer.Account,
                        password = customer.Password,
                        name = customer.Name,
                        gender = customer.Gender,
                        birthday = customer.Birthday,
                        email = customer.Email,
                        phone = customer.Phone,
                        address = customer.Address,
                        registerTime = DateTime.Now
                    });
                return "註冊成功";
            }
            catch(Exception e)
            {
                return e.ToString();
            }            
        }
        public void UpdatePassword(string acc, string pwd)
        {
            Execute("update Customer set Password = @pwd where Account = @acc", new { pwd = pwd, acc = acc });
        }
        public void UpdateUser(Customer customer)
        {
            Execute(@"update Customer
                        set Name=@name,Gender=@gender,Birthday=@birthday,Email=@email,Phone=@phone,Address=@address"
                    , new
                    {
                        name = customer.Name,
                        gender = customer.Gender,
                        birthday = customer.Birthday,
                        email = customer.Email,
                        phone = customer.Phone,
                        address = customer.Address
                    });
        }        
    }
}