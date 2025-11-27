using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.AbstractFactory
{
    public class OfficeComponentFactory: IComponentFactory
    {
        public IProducts CreateCPU() => new BaseComponent("Office CPU (i5)", 180);
        public IProducts CreateGPU() => new BaseComponent("Office GPU (integrated)", 40);
        public IProducts CreateMotherboard() => new BaseComponent("Office Motherboard", 110);
        public IProducts CreateRAM() => new BaseComponent("8GB DDR4", 40);
    }
}
