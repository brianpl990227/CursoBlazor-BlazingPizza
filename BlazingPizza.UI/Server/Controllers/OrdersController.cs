using BlazingPizza.UI.Server.Data;
using BlazingPizza.UI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazingPizza.UI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaStoreContext context;

        public OrdersController(PizzaStoreContext _context)
        {
            context = _context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
             order.CreatedTime = DateTime.UtcNow;
             order.DeliveryLocation = new LatLong(19.043679206924864, -98.19811254438645);
             order.UserId = GetUserId();
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
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
        {
            var orders = await context.Orders
                .Where(x => x.UserId == GetUserId())
                .Include(x => x.DeliveryLocation)
                .Include(x => x.Pizzas).ThenInclude(x => x.Special)
                .Include(x => x.Pizzas).ThenInclude(x => x.Toppings)
                .ThenInclude(x => x.Topping)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            return orders.Select(x => OrderWithStatus.FromOrder(x)).ToList(); 
        }

        [HttpGet("{orderID}")]
        public async Task<ActionResult<OrderWithStatus>> GetOrderWithStatus(int orderID)
        {
            var order = await context.Orders
                .Where(x=> x.UserId == GetUserId())
                .Where(x => x.OrderId == orderID).Include(x => x.DeliveryLocation)
                .Include(x => x.Pizzas).ThenInclude(x => x.Special)
                .Include(x=> x.Pizzas).ThenInclude(x => x.Toppings).ThenInclude(x => x.Topping)
                .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();
            else
                return OrderWithStatus.FromOrder(order);

        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }


    }
}
