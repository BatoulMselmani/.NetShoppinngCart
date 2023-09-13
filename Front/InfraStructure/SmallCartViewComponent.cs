

using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;

using Shared.InfraStructure;

namespace Front.InfraStructure
{
    public class SmallCartViewComponent : ViewComponent
    {

       public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            SmallCartViewModel smallCartVM;
            if(cart == null || cart.Count == 0)
            {
                smallCartVM = null;
            }
            else
            {
                smallCartVM = new SmallCartViewModel
                {
                    NumerOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }
            return View(smallCartVM);
        }

    }
}
