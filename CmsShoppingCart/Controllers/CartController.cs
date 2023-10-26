using CmsShoppingCart.InfraStructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
   [Authorize]
    public class CartController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public CartController(CmsShoppingCartContext context)
        {
            this.context = context;

        }
        //GET /cart
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };

            return View(cartVM);
        }

        //GET /cart/add/id
        public async Task<IActionResult> Add(int id)
        {
            Product product = await context.Products.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if( cartItem == null)
            {
                cart.Add(new CartItem(product));

            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            
                return RedirectToAction("Index");
               
            

            return ViewComponent("SmallCart");

           
        }
        [HttpPost]
        public JsonResult Discount( decimal productPrice, string discountCode)
        {
            // Check if a discount code is provided
            decimal discount = 0;

            // Apply a discount if a valid code is provided (you can implement a discount code validation logic here)
            if (discountCode == "DISCOUNT")
            {
                discount = 10; // Example: 10% discount
            }

            // Calculate the final price after applying the discount
            decimal finalPrice = productPrice - (productPrice * discount / 100);

            // Add the product to the cart with the final price
            // You should implement your own cart logic here

            return Json(new { success = true, message = "Product added to cart.", finalPrice });
        }

        //GET /cart/decrease/id
        public IActionResult Decrease(int id)
        {
            
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ;
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;

            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

          

            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        //GET /cart/remove/id
        public IActionResult Remove(int id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            

                cart.RemoveAll(x => x.ProductId == id);
            
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

           
            return RedirectToAction("Index");
        }

        //GET /cart/clear/id
        public IActionResult Clear()
        {

            HttpContext.Session.Remove("Cart");

            // return RedirectToAction("Page","Pages"); 

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Redirect(Request.Headers["Referer"].ToString());

            return Ok();
        }

        
        [HttpPost]
        public IActionResult RateContent( int rating)
        {
           

            // Save the rating to the database
            var newRate = new Rate
            {
               
                RatingValue = rating
            };

            context.Rates.Add(newRate);
            context.SaveChanges();
            return RedirectToAction("Index", TempData["Success"] = "Thanks for your feedback!");
           
        }




    }
}
