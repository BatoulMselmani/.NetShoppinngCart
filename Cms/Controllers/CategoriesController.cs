using Shared.InfraStructure;
using Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Controllers
{
   // [Authorize(Roles = "admin")]
    //[Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CmsShoppingCartContext context;
        public CategoriesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await context.Categories.OrderBy(x => x.Sorting).ToListAsync());
        }
        //GET /admin/categories/create/id=5
        public IActionResult Create() => View();


        //POST /admin/categories/create
        [HttpPost] //bt5aleha tbayen bara bas 23mela create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-"); //replace space with -
                category.Sorting = 100;

                var slug = await context.Categories.FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists");
                    return View(category);
                }
                context.Add(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been added";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET /admin/categories/edit/id=5
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST /admin/category/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Id == 1 ? "home" : category.Name.ToLower().Replace(" ", "-");


                var slug = await context.Pages.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists");
                    return View(category);
                }
                context.Update(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been edited!";

                return RedirectToAction("Edit", new { id = category.Id });
            }
            return View(category);
        }

        //GET /admin/category/delete/id=5
        public async Task<IActionResult> Delete(int id)
        {
            Category category  = await context.Categories.FindAsync(id);
            if (category == null)
            {

                TempData["Error"] = "The category does not exists!";
            }
            else
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been deleted!";

            }
            return RedirectToAction("Index");
        }

        //POST /admin/categories/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;
            foreach (var categoryId in id)
            {
                Category category = await context.Categories.FindAsync(categoryId);
                category.Sorting = count;
                context.Update(category);
                await context.SaveChangesAsync();
                count++;
            }
            return Ok();
        }
    }
}
