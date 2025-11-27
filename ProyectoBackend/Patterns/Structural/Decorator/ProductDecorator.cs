using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Structural.Decorator
{
    public class ProductDecorator: IProducts
    {
        protected readonly IProducts Decorated;
        public ProductDecorator(IProducts product) => Decorated = product;
        public virtual string GetName() => Decorated.GetName();
        public virtual double GetPrice() => Decorated.GetPrice();
    }
}
