using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Command
{
    public class Assembler
    {
        private readonly List<IAssemblyStep> _steps = new();
        public void AddStep(IAssemblyStep s) => _steps.Add(s);
        public void ExecuteSteps()
        {
            foreach (var s in _steps) s.Execute();
            Console.WriteLine("Assembly finished.");
        }
    }
}
