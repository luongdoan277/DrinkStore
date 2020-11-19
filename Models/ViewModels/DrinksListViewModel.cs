using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkStores.Models.ViewModels
{
    public class DrinksListViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }
        
        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}
