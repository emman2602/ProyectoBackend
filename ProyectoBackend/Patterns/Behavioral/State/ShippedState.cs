using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Behavioral.State
{
    public class ShippedState:IOrderState
    {
        public void HandleProcess(Order order) => Console.WriteLine("Already shipped.");
        public void HandleCancel(Order order) => Console.WriteLine("Can't cancel shipped order.");
        public void HandleShip(Order order) => Console.WriteLine("Already shipped.");
    }
}

