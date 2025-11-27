using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Bridge
{
    public class DHLPlatform:IShippingPlatform
    {
        public string GenerateGuide(Order order) => $"DHL-{order.Id.ToString().Substring(0, 8)}";
        public void SendPackage(string guide, Order order) => Console.WriteLine($"DHL: sending {order.Id} with guide {guide}");
    }
}
