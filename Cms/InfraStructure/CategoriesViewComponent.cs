using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.InfraStructure;
using Microsoft.EntityFrameworkCore;

namespace Front.InfraStructure
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly CmsShoppingCartContext context;

        public CategoriesViewComponent(CmsShoppingCartContext context)
        {
            this.context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }

        private Task<List<Category>> GetCategoriesAsync()
        {
            return context.Categories.OrderBy(x => x.Sorting).ToListAsync();
        }


    }
}
