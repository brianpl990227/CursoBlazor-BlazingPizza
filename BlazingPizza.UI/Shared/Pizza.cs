using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.UI.Shared
{
    public class Pizza
    {
        public const int DefaultSize = 30;
        public const int MinimunSize = 20;
        public const int MaximunSize = 40;
        public const int IncrementSize = 2;

        public int Id { get; set; }
        public int OrderId { get; set; }
        public PizzaSpecial Special { get; set; }
        public int SpecialId { get; set; }
        public int Size { get; set; }
        public List<PizzaTopping> Toppings { get; set; }

        public decimal GetBasePrice() => ((decimal)Size / (decimal)DefaultSize) * Special.BasePrice;

        public decimal GetTotalPrice() => GetBasePrice() + Toppings.Sum(x => x.Topping.Price);

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");

    }
}
