using System.Collections.Generic;
using DemoApp.Controllers;
using DemoApp.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.Extensions.Caching.Memory;

namespace DemoTest
{
    [TestFixture]
    public class DemoTestClass
    {
        private CustomerController custcontroller;
        private OrderController ordercontroller;

        public static IConfigurationRoot Config { get; set; }

        private static void SetupSimpleConfiguration()
        {
            Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }
        public DemoTestClass()
        {
            SetupSimpleConfiguration();
            string constr = Config["DefaultConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<OrderManagement>();
            optionsBuilder.UseSqlServer(constr);
            var context = new OrderManagement(optionsBuilder.Options);

            var cache = new MemoryCache(new MemoryCacheOptions());
            custcontroller = new CustomerController(context,cache);
            ordercontroller = new OrderController(context);
        }

        [Test, Property("Topic","Working with Data")]
        public void Customer_GetCustomerbyid()
        {
            string name = "Sanjay";
            Customer cust= custcontroller.GetCustomerById(1);
            Assert.AreEqual(name, cust.CustName);
        }

        [Test,Property("Topic","Working with Data")]
        public void Customer_AddCustomer()
        {
            Customer c = new Customer { CustName="AAAA", contact=1234567890, Address="Delhi" };
            int id = custcontroller.AddCustomer(c);
            Assert.IsTrue(id > 0);
        }

        [Test, Property("Topic","Caching")]
        public void Customer_GetCustomers()
        {
            List<Customer> customers1 = custcontroller.GetCustomers();

            Customer c = new Customer { CustName = "AAAA", contact = 1234567890, Address = "Delhi" };
            int id = custcontroller.AddCustomer(c);

            List<Customer> customers2 = custcontroller.GetCustomers();

            Assert.AreEqual(customers1.Count, customers2.Count);
        }
    }
}
