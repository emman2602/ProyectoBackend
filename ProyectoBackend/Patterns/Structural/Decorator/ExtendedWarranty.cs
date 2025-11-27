using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Structural.Decorator
{
    public class ExtendedWarranty:ProductDecorator
    {
        public ExtendedWarranty(IProducts p) : base(p) { }
        public override string GetName() => Decorated.GetName() + " + Extended Warranty";
        public override double GetPrice() => Decorated.GetPrice() + 50.0;
    }
}
