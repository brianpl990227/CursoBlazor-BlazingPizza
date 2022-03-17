using BlazingPizza.UI.Server.Data;
using BlazingPizza.UI.Shared;

namespace BlazingPizza.UI.Server.Models
{
    public static class SeedData
    {
        public static void Initialize(PizzaStoreContext context)
        {
            var data = new PizzaSpecial[]
            {
                new PizzaSpecial()
                {
                    Name = "Hawaiana",
                    BasePrice = 69.99m,
                    Description = "Esto es una pizza muy rica",
                    ImageURL = "img/pizzas/Foto1.jpeg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza de Atun",
                    BasePrice = 120.9m,
                    Description = "Esto es una pizza muy rica de atun",
                    ImageURL = "img/pizzas/Foto2.jpeg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza de chorizo",
                    BasePrice = 30.60m,
                    Description = "Esto es una pizza muy rica de chorizo",
                    ImageURL = "img/pizzas/Foto3.jpeg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza mixta",
                    BasePrice = 50.40m,
                    Description = "Esto es una pizza muy rica mixta",
                    ImageURL = "img/pizzas/Foto4.jpg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza Gourmet",
                    BasePrice = 200.9m,
                    Description = "Esto es una pizza muy rica gourmet",
                    ImageURL = "img/pizzas/Foto5.jpg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza Galaxy",
                    BasePrice = 117.50m,
                    Description = "Esto es una pizza para ver las estrellas",
                    ImageURL = "img/pizzas/Foto6.jpg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza Brian",
                    BasePrice = 69.7m,
                    Description = "Esto es una pizza a lo Brian",
                    ImageURL = "img/pizzas/Foto7.jpg"

                },
                new PizzaSpecial()
                {
                    Name = "Pizza Plus",
                    BasePrice = 10.5m,
                    Description = "Esto es una pizza Muy Muy rica",
                    ImageURL = "img/pizzas/Foto5.jpg"

                }
            };

            var Toppings = new Topping[]
            {
                new Topping()
                {
                    Name = "Cebolla",
                    Price = 6.50m
                },
                new Topping()
                {
                    Name = "Jamón",
                    Price = 7.50m
                },
                new Topping()
                {
                    Name = "Ajo",
                    Price = 8.50m
                },
                new Topping()
                {
                    Name = "Queso",
                    Price = 9.50m
                },
                new Topping()
                {
                    Name = "Ají",
                    Price = 10.50m
                }
            };

            context.Toppings.AddRange(Toppings);
            context.Specials.AddRange(data);
            context.SaveChanges();
        }
    }
}
