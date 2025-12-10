using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Behavioral.Command; 
using ProyectoBackend.Patterns.Creational.AbstractFactory;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Patterns.Creational.Builder
{
    public class StandardComputerBuilder : IComputerBuilder
    {
        private readonly IComponentFactory _factory;
        private readonly Computer _computer = new();

        // Instancia del invoker del patrón Command
        private readonly Assembler _assembler = new();

        public StandardComputerBuilder(IComponentFactory factory) { _factory = factory; }

        public void BuildCPU()
        {
            // 1. Lógica del Composite (Objeto datos)
            _computer.Add(_factory.CreateCPU());

            // 2. Lógica del Command (Acción física simulada)
            _assembler.AddStep(new InstallCPU());
        }

        public void BuildGPU()
        {
            _computer.Add(_factory.CreateGPU());
            _assembler.AddStep(new InstallGPU());
        }

        public void BuildMotherboard()
        {
            _computer.Add(_factory.CreateMotherboard());
             
        }

        public void BuildRAM()
        {
            _computer.Add(_factory.CreateRAM());
            _assembler.AddStep(new InstallRAM());
        }

        public Computer GetComputer()
        {
            Console.WriteLine("\n--- Iniciando proceso de ensamblaje físico (Command Pattern) ---");
            _assembler.ExecuteSteps(); // Ejecuta todos los comandos acumulados
            Console.WriteLine("--- Ensamblaje finalizado ---\n");

            return _computer;
        }
    }
}