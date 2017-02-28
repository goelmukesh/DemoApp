using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoApp.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DemoApp.Controllers
{
    public class CustomerController : Controller
    {
        private OrderManagement context;
        private IMemoryCache _memorycache;
        // GET: /<controller>/

        public CustomerController(OrderManagement _context, IMemoryCache memorycache)
        {
            context = _context;
            _memorycache = memorycache;
        }
        public IActionResult Index()
        {
            var customers = GetCustomers();
            return View("Index",customers);
        }

        [HttpGet]
        public IActionResult NewCustomer()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult NewCustomer(Customer c)
        {
            this.AddCustomer(c);
            return RedirectToAction("Index");
        }
        public Customer GetCustomerById(int id)
        {
            Customer cust = context.TblCustomer.Where(c => c.CustId == id).FirstOrDefault();
            return cust;
        }


        [NonAction]
        public List<Customer> GetCustomers()
        {
            var key = "custcache";
            List<Customer> custs;

            if (!_memorycache.TryGetValue(key, out custs))
            {
                var opts = new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                custs = context.TblCustomer.ToList();

                _memorycache.Set(key, custs, opts);
            }
            return custs;
        }

        [NonAction]
        public int AddCustomer(Customer cust)
        {
            Customer c = new Customer { CustName=cust.CustName, contact=cust.contact, Address=cust.Address };
            context.TblCustomer.Add(c);
            context.SaveChanges();
            return c.CustId;
        }
    }
}
