using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Structural.Composite
{
    public class Computer:IProducts
    {
        private readonly List<IProducts> _components = new();
        public void Add(IProducts p) => _components.Add(p);
        public string GetName() => "Custom Computer: " + string.Join(", ", _components.Select(c => c.GetName()));
        public double GetPrice() => _components.Sum(c => c.GetPrice());
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Computer:");
            foreach (var c in _components) sb.AppendLine($" - {c.GetName()} : {c.GetPrice():C}");
            sb.AppendLine($"Total: {GetPrice():C}");
            return sb.ToString();
        }
    }
}
