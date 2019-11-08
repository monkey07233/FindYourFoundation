using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FindYourFoundation.Controllers
{
    public class AdminController : ApiController
    {
        private ProductRepo _productRepo = new ProductRepo();
        private CustomerRepo _customerRepo = new CustomerRepo();
        // GET: Admin
        [HttpGet]
        public List<Product> GetProductsForAdmin()
        {
            return _productRepo.GetProductsForAdmin();
        }
        [HttpGet]
        public List<Customer> GetCustomers()
        {
            return _customerRepo.GetCustomers();
        }
        [HttpGet]
        public List<Customer> GetCustomersDesc()
        {
            return _customerRepo.GetCustomersDesc();
        }
    }
}