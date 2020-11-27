using System.Collections.Generic;
using System.Linq;

namespace DrinkStores.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public void AddItem(Drink drink, int quantity)
        {
            CartLine line = Lines
                .Where(d => d.Drinks.Id == drink.Id)
                .FirstOrDefault();
            if(line == null)
            {
                Lines.Add(new CartLine
                {
                    Drinks = drink,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Drink drink)
            => Lines.RemoveAll(l => l.Drinks.Id == drink.Id);

        public decimal ComputeTotalValue()
            => Lines.Sum(e => e.Drinks.Price * e.Quantity);

        public void Clear()
            => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Drink Drinks { get; set; }
        public int Quantity { get; set; }
    }
}
