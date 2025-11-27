using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Bridge
{
    public class ExpressShipping:ShippingType
    {
        public ExpressShipping(IShippingPlatform platform) : base(platform) { }
        public override void Ship(Order order)
        {
            var guide = Platform.GenerateGuide(order);
            Platform.SendPackage(guide, order);
            Console.WriteLine("Express shipping executed.");
        }
    }
}
