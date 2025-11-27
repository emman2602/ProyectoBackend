using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Bridge
{
    public interface IShippingPlatform
    {
        string GenerateGuide(Order order);
        void SendPackage(string guide, Order order);
    }
}
