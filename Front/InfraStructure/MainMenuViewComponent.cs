
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shared.InfraStructure;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.InfraStructure
{
    public class MainMenuViewComponent : ViewComponent
    {


        private readonly CmsShoppingCartContext context;

        public MainMenuViewComponent(CmsShoppingCartContext context)
        {
            this.context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        { var pages = await GetPagesAsync();
            return View(pages);
        }

        private Task<List<Shared.Models.Page>> GetPagesAsync()
        {
            return context.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}
