using Shared.InfraStructure;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
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

        /* [HttpPost]
         public IActionResult RateContent(int contentId, int rating)
         {
             var userId = @ViewBag.id.AppUser; // Implement a way to get the current user's ID
             var existingRating = context.Ratings.FirstOrDefault(x => x.ContentId == contentId);

             if (existingRating != null)
             {
                 existingRating.RatingValue = rating;
             }
             else
             {
                 var newRating = new Rating
                 {
                     UserId = userId,
                     ContentId = contentId,
                     RatingValue = rating
                 };
                 context.Ratings.Add(newRating);
             }

             context.SaveChanges();

             return RedirectToAction("Index");
         }*/
        [HttpPost]
        public IActionResult RateContent( int rating)
        {
            // Implement authentication to get the current user's ID
            //var userId = @ViewBag().AppUser;

            // Save the rating to the database
            var newRate = new Rate
            {
                //ContentId = contentId,
               // UserId = userId,
                RatingValue = rating
            };

            context.Rates.Add(newRate);
            context.SaveChanges();
            return RedirectToAction("Index", TempData["Success"] = "Thanks for your feedback!");
            // Calculate the new average rating and number of ratings for the content
            //var averageRating = context.Ratings.Where(r => r.ContentId == contentId).Average(r => r.RatingValue);
            //var numberOfRatings = context.Ratings.Count(r => r.ContentId == contentId);

            // Return the updated data (e.g., as JSON)
            //return Json(new { averageRating, numberOfRatings });
        }




    }
}
