using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Creational.AbstractFactory;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.Builder
{
    public class GamingComputerBuilder: IComputerBuilder    
    {
        private readonly IComponentFactory _factory;
        private readonly Computer _computer = new();

        public GamingComputerBuilder(IComponentFactory factory) { _factory = factory; }

        public void BuildCPU() => _computer.Add(_factory.CreateCPU());
        public void BuildGPU() => _computer.Add(_factory.CreateGPU());
        public void BuildMotherboard() => _computer.Add(_factory.CreateMotherboard());
        public void BuildRAM() => _computer.Add(_factory.CreateRAM());
        public Computer GetComputer() => _computer;
    }
}
