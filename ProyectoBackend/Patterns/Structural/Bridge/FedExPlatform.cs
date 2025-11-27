using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Bridge
{
    public class FedExPlatform:IShippingPlatform
    {
        public string GenerateGuide(Order order) => $"FDX-{order.Id.ToString().Substring(0, 8)}";
        public void SendPackage(string guide, Order order) => Console.WriteLine($"FedEx: sending {order.Id} with guide {guide}");
    }
}
