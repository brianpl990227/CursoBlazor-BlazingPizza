using BlazingPizza.UI.Shared;

namespace BlazingPizza.UI.Client.Services
{
    public class OrderState
    {
        public Pizza ConfiguringPizza { get; private set; }
        public bool ShowingConfigurePizza { get; private set; }
        public Order order { get; private set; } = new Order();

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };
            ShowingConfigurePizza = true;
        }

        public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigurePizza = false;
        }

        public void ConfirmConfigurePizzaDialog()
        {
            order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
            ShowingConfigurePizza = false;
        }

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            order.Pizzas.Remove(pizza);
        }

        public void ResetOrder()
        {
            order = new Order();
        }

    }
}
