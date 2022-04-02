using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.UI.Shared
{
    public class Order
    {
        public int? OrderId { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public Address? DeliveryAddress { get; set; } = new Address();
        public LatLong? DeliveryLocation { get; set; }
        public List<Pizza>? Pizzas { get; set; } = new List<Pizza>();
        public decimal GetTotalPrice() => Pizzas.Sum(x => x.GetTotalPrice());
        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
        public string GetFormattedCreatedTime() => CreatedTime.Value.ToString("D", new CultureInfo("es"));
    }
}
