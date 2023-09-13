
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Shared.InfraStructure;
using System;
using System.Linq;

namespace Shared.Models
{
    public class SeeData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CmsShoppingCartContext
                 (serviceProvider.GetRequiredService<DbContextOptions<CmsShoppingCartContext>>()))
            {
                if (context.Pages.Any())
                {
                    return;
                }
                context.Pages.AddRange(
                    new Page
                    {
                        Title = "Home",
                        Slug = "home",
                        Content="home page",
                        Sorting =0,
                    },
                    new Page
                    {
                        Title = "About Us",
                        Slug = "about-us",
                        Content = "This is our cute simple website that will help do shopping in an easy way.Hope it is useful!!",
                        Sorting = 100,
                    },
                     new Page
                     {
                         Title = "Services",
                         Slug = "services",
                         Content = "services page",
                         Sorting = 100,
                     },
                      new Page
                      {
                          Title = "Contact",
                          Slug = "contact",
                          Content = "contact page",
                          Sorting = 100,
                      }
                  );
                context.SaveChanges();
            }
        }
    }
}
