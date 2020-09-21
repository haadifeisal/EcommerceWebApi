using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Repository.Ecommerce
{
    public static class SeedDB
    {
        
        public static void Populate(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<EcommerceContext>());
            }
        }

        public static void SeedData(EcommerceContext context)
        {
            System.Console.WriteLine("\n\nApplying Migrations ...\n\n");

            context.Database.Migrate();

            if (!context.Product.Any())
            {
                System.Console.WriteLine("\n\nAdding data - Seeding ...\n\n");

                var category = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Games"
                };
                context.Category.Add(category);

                var product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "Shadow Company makes their entrance in Season Five, the newest season for Call of Duty®: Modern Warfare® and Warzone™. A new faction means new Operators and a new arsenal—and they’ve already completely changed the landscape of Warzone",
                    Image = "https://assets.mmsrg.com/isr/166325/c1/-/pixelboxx-mss-81422861/fee_786_587_png/Call-of-Duty%3A-Modern-Warfare-PlayStation-4-",
                    Price = 500,
                    Stock = 10,
                    Title = "Call of duty: Modern Warfare"
                };
                context.Product.Add(product);

                context.SaveChanges();

                System.Console.WriteLine("\n\nMigrations worked !! ...\n\n");
            }
            else
            {
                System.Console.WriteLine("Already have data - not seeding");
            }
        }

    }
}
