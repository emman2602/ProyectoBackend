using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Structural.Composite
{
    public class BaseComponent:IProducts
    {
        private readonly string _name;
        private readonly double _price;

        public BaseComponent(string name, double price) { _name = name; _price = price; }
        public string GetName() => _name;
        public double GetPrice() => _price;
        public override string ToString() => $"{_name} ({_price:C})";
    }
}
