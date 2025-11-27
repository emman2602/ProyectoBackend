using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Behavioral.State
{
    public class PendingState: IOrderState
    {
        public void HandleProcess(Order order)
        {
            Console.WriteLine("Order moving from Pending to Building");
            order.SetState(new BuildingState());
        }
        public void HandleCancel(Order order)
        {
            Console.WriteLine("Order cancelled (Pending).");
            order.SetState(new CancelledState());
        }
        public void HandleShip(Order order) => Console.WriteLine("Can't ship pending order.");
    }
}
