using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.Builder
{
    public interface IComputerBuilder
    {
        void BuildCPU();
        void BuildGPU();
        void BuildMotherboard();
        void BuildRAM();
        Computer GetComputer();
    }
}
