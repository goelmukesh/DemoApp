using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public static class DbInitializer
    {
        public static void Initialize(OrderManagement context)
        {
            context.Database.EnsureCreated();

            if (context.TblCustomer.Any())
            {
                return;
            }

            var customers = new Customer[]
                {
                    new Customer { CustName="Sanjay", Address="New delhi", contact=9213456780},
                    new Customer{ CustName="Amit", Address="Sector 34 Gurgaon", contact=9813456780},
                    new Customer { CustName="Mona", Address="New delhi", contact=9233366780},
                    new Customer { CustName="Harish", Address="Noida", contact=9540372905}
                };

            context.TblCustomer.AddRange(customers);
            context.SaveChanges();

            var orders = new Order[]
                 {
                     new Order { CustId=1, ord_amt=2500, Ord_date=Convert.ToDateTime("1/1/2017") },
                     new Order { CustId=2, ord_amt=3500, Ord_date=Convert.ToDateTime("1/15/2017") }
                 };

            context.TblOrder.AddRange(orders);
            context.SaveChanges();
        }
    }
}
