using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoApp.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoApp.Controllers
{
    public class OrderController : Controller
    {
        private OrderManagement context;

        public OrderController(OrderManagement _context)
        {
            context = _context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowOrders()
        {
            List<Order> orders = context.TblOrder.ToList();
            ViewBag.OrderCount = orders.Count;
            return View("ShowOrders",orders);
        }

        public IActionResult GetOrderByCustomerId(int id)
        {
            List<Order> orders = this.GetOrdersByCustId(id);
            if (orders!=null)
            {
                ViewBag.OrderCount = orders.Count;
            }
            else
            {
                ViewBag.OrderCount = 0;
            }
            return PartialView("_ShowOrders", this.GetOrdersByCustId(id));
        }

        [NonAction]
        public List<Order> GetOrdersByCustId(int id)
        {
            return context.TblOrder.Where(o => o.CustId == id).ToList();
        }


    }
}
