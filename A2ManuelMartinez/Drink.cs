using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ManuelMartinez
{
    internal class Drink
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Drink(string name, int quantity, double price)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}
