using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Bridge
{
    public abstract class ShippingType
    {
        protected IShippingPlatform Platform;
        public ShippingType(IShippingPlatform platform) { Platform = platform; }
        public abstract void Ship(Order order);
    }
}
