using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.AbstractFactory
{
    public class UserSelectionFactory:IComponentFactory
    {
        private readonly IProducts _cpu;
        private readonly IProducts _gpu;
        private readonly IProducts _motherboard;
        private readonly IProducts _ram;

        public UserSelectionFactory(IProducts cpu, IProducts gpu, IProducts motherboard, IProducts ram)
        {
            _cpu = cpu;
            _gpu = gpu;
            _motherboard = motherboard;
            _ram = ram;
        }

        public IProducts CreateCPU() => _cpu;
        public IProducts CreateGPU() => _gpu;
        public IProducts CreateMotherboard() => _motherboard;
        public IProducts CreateRAM() => _ram;
    }
}
