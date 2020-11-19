using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DrinkStores.Models.ViewModels;
using DrinkStores.Models;

namespace DrinkStores.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string category,int drinkPage = 1)
        {
            return View(new DrinksListViewModel
            {
                Drinks = repository.Drinks
                .Where(d => category == null || d.Category.CategoryName == category)
                .OrderBy(d => d.Id)
                .Skip((drinkPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = drinkPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Drinks.Count()
                }
            });
        }
        //public ViewResult ListView(int CategoryID)
        //{
        //    var list = new DrinksListViewModel()
        //    {
        //        Drinks = repository.Drinks.FirstOrDefault(l => l.CategoryID = CategoryID)
        //    };
        //    return View(list);
        //}
    }
}
