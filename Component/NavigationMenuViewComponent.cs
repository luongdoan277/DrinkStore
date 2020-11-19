using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkStores.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkStores.Component
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(repository.Drinks
                .Select(x => x.Category.CategoryName)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
