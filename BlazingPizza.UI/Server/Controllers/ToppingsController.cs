using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazingPizza.UI.Server.Data;
using BlazingPizza.UI.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.UI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly PizzaStoreContext context;

        public ToppingsController(PizzaStoreContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetToppoings()
        {
            var result = await context.Toppings.OrderBy(x => x.Name).ToListAsync();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<string>>Test(Test x)
        {
            return x.Name;
        }


    }
}
