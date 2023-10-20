using CmsShoppingCart.InfraStructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly CmsShoppingCartContext _context;

        public OrderController(CmsShoppingCartContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {

                TempData["Error"] = "The order does not exists!";
            }
            else
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The order has been deleted!";

            }
            return RedirectToAction("Index");
        }


    }

}