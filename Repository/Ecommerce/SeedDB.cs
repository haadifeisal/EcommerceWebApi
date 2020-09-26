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

                var category2 = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Phones"
                };
                context.Category.Add(category2);

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("bond007", out passwordHash, out passwordSalt);
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Username = "bond007",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                context.User.Add(user);

                var cart = new Cart
                {
                    CartId = Guid.NewGuid(),
                    UserId = user.UserId,
                    CreatedDate = DateTime.Now
                };

                context.Cart.Add(cart);

                var customer = new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    FullName = "James Bond",
                    Address = "Kungsholmen 7",
                    City = "Stockholm",
                    PersonalIdentityNumber = "196107168177",
                    Zipcode = "16451",
                    UserId = user.UserId
                };
                context.Customer.Add(customer);

                var product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "Shadow Company makes their entrance in Season Five, the newest season for Call of Duty®: Modern Warfare® and Warzone™. A new faction means new Operators and a new arsenal—and they’ve already completely changed the landscape of Warzone",
                    Image = "https://assets.mmsrg.com/isr/166325/c1/-/pixelboxx-mss-81422861/fee_786_587_png/Call-of-Duty%3A-Modern-Warfare-PlayStation-4-",
                    Price = 500,
                    Stock = 5,
                    Title = "Call of duty: Modern Warfare"
                };
                context.Product.Add(product);

                var product2 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "FIFA 20 is a football simulation video game published by Electronic Arts as part of the FIFA series. It is the 27th installment in the FIFA series, and was released on 27 September 2019 for Microsoft Windows, PlayStation 4, Xbox One, and Nintendo Switch",
                    Image = "https://cdn.cdon.com/media-dynamic/images/product/game/playstation4/image2/fifa_20-47874750-frntl.jpg",
                    Price = 400,
                    Stock = 5,
                    Title = "FIFA 20"
                };
                context.Product.Add(product2);

                var product3 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "Battlefield V is a first-person shooter video game developed by EA DICE and published by Electronic Arts. Battlefield V is the sixteenth installment in the Battlefield series",
                    Image = "https://upload.wikimedia.org/wikipedia/en/f/f0/Battlefield_V_standard_edition_box_art.jpg",
                    Price = 350,
                    Stock = 5,
                    Title = "Battlefield V"
                };
                context.Product.Add(product3);

                var product4 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "Tekken 7 represents the final chapter of the 20-year-long Mishima feud. Unveil the epic ending to the emotionally charged family warfare between the members of the Mishima Clan as they struggle to settle old scores and wrestle for control of a global empire, putting the balance of the world in peril",
                    Image = "https://fanatical.imgix.net/product/original/753730bd-7ba2-479c-bfdf-bac31b72ebea.jpeg?auto=compress,format&w=400&fit=max",
                    Price = 350,
                    Stock = 5,
                    Title = "Tekken 7"
                };
                context.Product.Add(product4);

                var product5 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "Fortnite is a survival game where 100 players fight against each other in player versus player combat to be the last one standing. It is a fast-paced, action-packed game, not unlike The Hunger Games, where strategic thinking is a must in order to survive.",
                    Image = "https://i.pinimg.com/736x/52/41/19/524119df67d3a7720939860ea9b6b38d.jpg",
                    Price = 100,
                    Stock = 5,
                    Title = "Fortnite"
                };
                context.Product.Add(product5);

                var product6 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category.CategoryId,
                    Description = "Introduced in the 1996 video game Crash Bandicoot, Crash is a mutant eastern barred bandicoot who was genetically enhanced by the series' main antagonist Doctor Neo Cortex and soon escaped from Cortex's castle after a failed experiment in the Cortex Vortex",
                    Image = "https://cdn.cdon.com/media-dynamic/images/product/game/playstation4/image4/crash_bandicoot_n_sane_trilogy-38061091-frntl.jpg",
                    Price = 500,
                    Stock = 5,
                    Title = "Crash Bandicoot"
                };
                context.Product.Add(product6);

                var product7 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category2.CategoryId,
                    Description = "The phone comes with a 5.80-inch touchscreen display with a resolution of 1125x2436 pixels at a pixel density of 458 pixels per inch (ppi). iPhone 11 Pro is powered by a hexa-core Apple A13 Bionic processor. It comes with 4GB of RAM. The iPhone 11 Pro runs iOS 13 and is powered by a 3046mAh non-removable battery.",
                    Image = "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/iphone-11-pro-max-gold-select-2019_GEO_EMEA?wid=834&hei=1000&fmt=jpeg&qlt=95&op_usm=0.5,0.5&.v=1567808544078",
                    Price = 12999,
                    Stock = 5,
                    Title = "Iphone 11 Pro"
                };
                context.Product.Add(product7);

                var product8 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category2.CategoryId,
                    Description = "Samsung already makes three different kinds of Galaxy S20 phones: a regular, a Plus, and an Ultra. Now you can add a fourth to that mix, the Galaxy S20 FE — the FE stands for “Fan Edition.” It’s an S20 with some of the more expensive parts removed but keeping some of the key features that matter.",
                    Image = "https://www.elgiganten.se/image/dv_web_D180001002526559/213259/samsung-galaxy-s20-fe-4g-smartphone-6128gb-cloud-navy.jpg?$prod_all4one$",
                    Price = 7999,
                    Stock = 5,
                    Title = "Samsung Galaxy S20 FE"
                };
                context.Product.Add(product8);

                var product9 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category2.CategoryId,
                    Description = "Lightning-fast 5G on the motorola edge+ is capable of over 4 Gbps‡—speeds never seen before on a smartphone. Whether browsing, streaming, or gaming, you demand performance. No lag time, no interruptions.",
                    Image = "https://www.elgiganten.se/image/dv_web_D180001002449302/173289/motorola-edge-plus-5g-smartphone-12256gb-thunder-grey.jpg?$prod_all4one$",
                    Price = 9999,
                    Stock = 5,
                    Title = "Motorola Edge Plus 5G"
                };
                context.Product.Add(product9);

                var product10 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = category2.CategoryId,
                    Description = "The OnePlus 8 Pro is an IP68 rated phone that is resistant to dust and water. It comes with an impressive 6.78-inch AMOLED panel that has a resolution of 1440x3168 pixels with high color accuracy and HDR10+ support. Also, it can run at 120Hz at the QHD+ resolution",
                    Image = "https://www.elgiganten.se/image/dv_web_D180001002438127/162399/oneplus-8-pro-smartphone-12256gb-glacial-green.jpg?$prod_all4one$",
                    Price = 10990,
                    Stock = 5,
                    Title = "OnePlus 8 Pro"
                };
                context.Product.Add(product10);

                context.SaveChanges();

                System.Console.WriteLine("\n\nMigrations worked !! ...\n\n");
            }
            else
            {
                System.Console.WriteLine("Already have data - not seeding");
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
