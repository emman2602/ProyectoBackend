using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.Builder
{
    public class AssemblyDirector
    {
        public Computer Construct(IComputerBuilder builder)
        {
            builder.BuildMotherboard();
            builder.BuildCPU();
            builder.BuildRAM();
            builder.BuildGPU();
            return builder.GetComputer();
        }
    }
}
