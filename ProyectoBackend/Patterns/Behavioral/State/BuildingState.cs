using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Behavioral.State
{
    public class BuildingState: IOrderState
    {
        public void HandleProcess(Order order)
        {
            Console.WriteLine("Order already processing (Building).");
        }
        public void HandleCancel(Order order)
        {
            Console.WriteLine("Cancelling while building...");
            order.SetState(new CancelledState());
        }
        public void HandleShip(Order order)
        {
            Console.WriteLine("Built — shipping now.");
            order.SetState(new ShippedState());
        }
    }
}
