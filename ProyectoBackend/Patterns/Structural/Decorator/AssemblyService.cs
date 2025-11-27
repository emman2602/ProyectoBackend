using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Structural.Decorator
{
    public class AssemblyService:ProductDecorator
    {
        public AssemblyService(IProducts p) : base(p) { }
        public override string GetName() => Decorated.GetName() + " + Assembly Service";
        public override double GetPrice() => Decorated.GetPrice() + 30.0;
    }
}
