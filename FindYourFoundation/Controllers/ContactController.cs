﻿using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class ContactController : ApiController
    {
        private ContactRepo _contactRepo = new ContactRepo();
        // GET: Contact
        [HttpGet]
        public List<Contact> GetContacts()
        {
            return _contactRepo.GetContacts();
        }
        [HttpPost]
        public string AddContact(Contact contact)
        {
            return _contactRepo.AddContact(contact);
        }
    }
}