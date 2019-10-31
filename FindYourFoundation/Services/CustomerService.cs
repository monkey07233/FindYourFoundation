using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using FindYourFoundation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class CustomerService
    {
        private CustomerRepo _customerRepo = new CustomerRepo();
        public Customer CheckLogin(LoginViewModel login)
        {
            return _customerRepo.CheckLoginUser(login);
        }
        public string Register(Customer customer)
        {
            var user = _customerRepo.GetUserByAcc(customer.Account);
            if (user == null)
            {
                return _customerRepo.RegisterUser(customer);              
            }
            else
            {
                return "帳號已經註冊過了";
            }            
        }
        public string ModifyPassword(string oldPwd,string newPwd)
        {
            var user = _customerRepo.GetUserByPwd(oldPwd);
            if (user == null)
            {
                return "無此帳號";
            }
            else
            {
                _customerRepo.UpdatePassword(user.Account,newPwd);
                return "修改密碼成功";
            }
        }
        public Customer GetUserByAcc(string acc)
        {
            return _customerRepo.GetUserByAcc(acc);
        }
        public void UpdateUser(Customer customer)
        {
            _customerRepo.UpdateUser(customer);
        }        
    }
}