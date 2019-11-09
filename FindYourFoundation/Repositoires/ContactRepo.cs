using FindYourFoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class ContactRepo : DataAccessLayer
    {
        public string AddContact(Contact contact)
        {
            try
            {
                Execute(@"insert into Contact(Name,Email,Content,ContactTime)
                            values(@Name,@Email,@Content,@ContactTime)"
                    , new {
                        Name = contact.Name,
                        Email = contact.Email,
                        Content = contact.Content,
                        ContactTime = DateTime.Now
                    });
                return "寄送成功";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
    }
}