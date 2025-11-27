using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;
using ProyectoBackend.Patterns.Behavioral.Strategy;

namespace ProyectoBackend.Application
{
    public static class Services
    {
        public static void PayOrder(Order o)
        {
            if (o.Payment == null) Console.WriteLine("No payment assigned.");
            else o.Payment.Pay(o.TotalPrice());
        }

        public static void ShipOrder(Order o)
        {
            if (o.Shipping == null) Console.WriteLine("No shipping assigned.");
            else o.Shipping.Ship(o);
        }
    }
}
