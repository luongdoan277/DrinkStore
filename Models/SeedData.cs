using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStores.Models
{
    public class SeedData
    {
        public static void DataDrink(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Drinks.Any())
            {
                context.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Caramel Brulée Latte",
                        Description = "Our signature espresso, steamed milk and rich caramel brulée sauce finished with whipped cream and a supreme topping of even more caramel brulée.",
                        Img = "Drink1.png",
                        Price = 9,
                        Discount = 10,
                        Status = "new"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
