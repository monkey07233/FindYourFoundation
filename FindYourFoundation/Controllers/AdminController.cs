using FindYourFoundation.Models;
using FindYourFoundation.Repositoires;
using FindYourFoundation.ViewModels;
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
        public List<Product> GetProductsDescForAdmin()
        {
            return _productRepo.GetProductsDescForAdmin();
        }
        [HttpGet]
        public List<Product> GetProductsForAdmin()
        {
            return _productRepo.GetProductsForAdmin();
        }
        [HttpGet]
        public List<Product> GetProductsHotForAdmin()
        {
            return _productRepo.GetProductsHotForAdmin();
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
        [HttpPost]
        public List<Customer> SearchCustomer(string search)
        {
            return _customerRepo.SearchCustomer(search);
        }
        [HttpPost]
        public string AddBlackList(Customer customer)
        {
            return _customerRepo.AddBlackList(customer);
        }
        [HttpPost]
        public string DeleteBlackList(Customer customer)
        {
            return _customerRepo.DeleteBlackList(customer);
        }
        [HttpGet]
        public List<BuyHistoryViewModel> GetBuyHistoryByAcc(Customer customer)
        {
            return new BuyHistoryRepo().GetBuyHistories(customer.Account);
        }
    }
}