using CmsShoppingCart.InfraStructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CmsShoppingCart.Controllers
{    
    public class OrderController : Controller
    {
        private readonly CmsShoppingCartContext _context;

        public OrderController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();


               // TempData["Success"] = "Your Order has been saved !";
                return RedirectToAction("OrderConfirmation", new {name = order.UserName});
            }

            return View("Index"); 
        }
        public IActionResult OrderConfirmation(string username)
        {
            

            var name = _context.Orders.FirstOrDefault(o => o.UserName == username);
            //TempData["Success"] = "Your Order has been saved !";
            return View(name);
            TempData["Success"] = "Your Order has been saved !";
        }
        
    }
}

