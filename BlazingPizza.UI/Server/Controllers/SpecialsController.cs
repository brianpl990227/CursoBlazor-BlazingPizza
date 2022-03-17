using BlazingPizza.UI.Server.Data;
using BlazingPizza.UI.Server.Models;
using BlazingPizza.UI.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.UI.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private readonly PizzaStoreContext context;
        public SpecialsController(PizzaStoreContext _context)
        {
            
            context = _context;

            if(context.Specials.Count() == 0)
            {
                SeedData.Initialize(context);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
        {
            var response = await context.Specials.OrderByDescending(x => x.BasePrice).ToListAsync();
            return Ok(response);
        }

    }
}
