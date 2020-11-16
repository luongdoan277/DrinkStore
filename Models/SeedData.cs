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
            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Category
                    {
                        CategoryName = "Do uong dong chai"
                    },
                    new Category
                    { 
                        CategoryName = "Tra sua"
                    }
                    );
            }
            if (!context.Drinks.Any())
            {
                context.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Cocacola",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://www.phunu8.vn/PhuNu8DuLieu/PhuNu8HinhAnh/users/5016/19-05-2018/bi-mat-cua-coca-cola-hinh-anh_1.jpg",
                        Price = 23000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 1
                    },
                    new Drink
                    {
                        Name = "Pepsi",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://nhahanghuongduyen.com/wp-content/uploads/2016/12/pepsi-1-1024x738.jpg",
                        Price = 20000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 1
                    },
                    new Drink
                    {
                        Name = "Pepsi chanh",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://cdn.tgdd.vn/Files/2020/09/04/1286939/pepsi-ra-mat-dong-san-pham-pepsi-vi-chanh-moi-khong-calo-thich-hop-cho-nguoi-an-kieng-202009041447246769.jpg",
                        Price = 25000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 1
                    },
                    new Drink
                    {
                        Name = "Sting do",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://lh3.googleusercontent.com/proxy/OTiuNMA-8mfDiu6Er0JQYldA5RUI201wKONaRs9KIJ3NXypqlqiYepXw-FlIcehrO57Bo1FDEKoQHNa-k6VHCFZ5RAdeozGXZXfw-Pz6cSIoP5VqNmfDj_QRpXtcxFcco4OyJgobDt6p6iZxzkpqRqtybPAShYGrwPcpa5JmCN8",
                        Price = 16000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 1
                    },
                    new Drink
                    {
                        Name = "Sting vang",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://cdn.tgdd.vn/products/Images/3226/91595/bhx/nuoc-tang-luc-sting-vang-lon-cao-330ml-1511201815591.jpg",
                        Price = 16000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 1
                    },
                    new Drink
                    {
                        Name = "Tra thai vi cam",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://cachlamchuan.com/wp-content/uploads/2019/07/cach-lam-tra-sua-thai-do.jpg",
                        Price = 20000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 2

                    },
                    new Drink
                    {
                        Name = "Tra thai vi xoai",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://cachlamchuan.com/wp-content/uploads/2019/07/cach-lam-tra-sua-thai-do.jpg",
                        Price = 20000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 2

                    },
                    new Drink
                    {
                        Name = "Tra thai dua hau",
                        Description = "Et in lobortis consetetur accusam ut ea diam ipsum consequat amet",
                        Img = "https://media.cooky.vn/images/blog-2016/7-cach-nau-tra-sua-vua-ngon-vua-dam-bao-an-toan-suc-khoe-tai-nha-cho-chi-em-1.jpg",
                        Price = 20000,
                        Discount = 0,
                        Status = "Done",
                        CategoryID = 2
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
