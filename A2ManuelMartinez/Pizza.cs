using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ManuelMartinez
{
    internal class Pizza
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Pizza(string name, double price)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = 1;
        }
    }
}
