using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Services
{
    public class UserPicService
    {
        private UserPicRepo _userPicRepo = new UserPicRepo();
        public UserPic GetPicByAcc(string acc)
        {
            var pic= _userPicRepo.GetPicByAcc(acc);
            if (pic == null)
            {
                return _userPicRepo.GetPicByAcc("Default");
            }
            else
            {
                return pic;
            }
        }
    }
}