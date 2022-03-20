using BlazingPizza.UI.Server.Data;
using BlazingPizza.UI.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.UI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaStoreContext context;

        public OrdersController(PizzaStoreContext _context)
        {
            context = _context;
        }


        [HttpPost("test")]
        public ActionResult Test(int x)
        {
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
             order.CreatedTime = DateTime.UtcNow;
             order.DeliveryLocation = new LatLong(19.043679206924864, -98.19811254438645);

             foreach(var pizza in order.Pizzas)
             {
                 pizza.SpecialId = pizza.Special.Id;
                 pizza.Special = null;
                 foreach(var topping in pizza.Toppings)
                 {
                     topping.ToppingId = topping.Topping.Id;
                     topping.Topping = null;
                 }
             }

             context.Orders.Attach(order);
             await context.SaveChangesAsync();

             return order.OrderId;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders ()
        {
            var orders = await context.Orders
                .Include(x => x.DeliveryLocation)
                .Include(x => x.Pizzas).ThenInclude(x => x.Special)
                .Include(x => x.Pizzas).ThenInclude(x => x.Toppings)
                .ThenInclude(x => x.Topping)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            return orders.Select(x => OrderWithStatus.FromOrder(x)).ToList(); 
        }


    }
}
