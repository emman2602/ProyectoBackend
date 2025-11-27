using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.AbstractFactory
{
    public interface IComponentFactory
    {
        IProducts CreateCPU();
        IProducts CreateGPU();
        IProducts CreateMotherboard();
        IProducts CreateRAM();

    }
}
