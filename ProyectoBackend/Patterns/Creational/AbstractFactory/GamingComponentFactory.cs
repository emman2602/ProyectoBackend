using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.AbstractFactory
{
    public class GamingComponentFactory: IComponentFactory
    {
        public IProducts CreateCPU() => new BaseComponent("Gaming CPU (Ryzen 7)", 300);
        public IProducts CreateGPU() => new BaseComponent("Gaming GPU (RTX 4060)", 350);
        public IProducts CreateMotherboard() => new BaseComponent("Gaming Motherboard", 150);
        public IProducts CreateRAM() => new BaseComponent("16GB DDR5", 90);
    }
}
