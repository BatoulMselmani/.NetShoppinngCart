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

                // You can send a confirmation email here

                return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
            }

            return View("/Cart/Index"); // Return to the checkout page if there's an error
        }
        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId );
          //  var name = _context.Orders.FirstOrDefault(o => o.CustomerName == username);
            return View(order);
        }
    }
}

