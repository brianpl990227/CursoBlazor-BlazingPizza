using BlazingPizza.UI.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.UI.Server.Data
{
    public class PizzaStoreContext : DbContext
    {
        public DbSet<PizzaSpecial> Specials { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaTopping>()
                .HasKey(x => new { x.PizzaId, x.ToppingId });

            modelBuilder.Entity<PizzaTopping>()
                 .HasOne<Pizza>().WithMany(x => x.Toppings);

             modelBuilder.Entity<PizzaTopping>()
                 .HasOne(x => x.Topping).WithMany();

            modelBuilder.Entity<Order>()
                .OwnsOne(x => x.DeliveryLocation);
        }
        

    }
}
