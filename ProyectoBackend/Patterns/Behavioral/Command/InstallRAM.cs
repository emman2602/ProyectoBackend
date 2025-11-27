using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Command
{
    public class InstallRAM:IAssemblyStep
    {
        public void Execute() => Console.WriteLine("Installing RAM...");
    }
}
