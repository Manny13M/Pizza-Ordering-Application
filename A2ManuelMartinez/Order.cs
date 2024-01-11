using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ManuelMartinez
{
    internal class Order
    {
        public List<Pizza> Pizzas { get; set; }
        public List<OtherItem> OtherItems { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<Topping> Toppings { get; set; }

        public Order(List<Pizza> pizzas, List<OtherItem> otherItems, List<Drink> drinks, List<Topping> toppings) 
        {
            Pizzas = pizzas;
            OtherItems = otherItems;
            Drinks = drinks;
            Toppings = toppings;
        }
    }
}
