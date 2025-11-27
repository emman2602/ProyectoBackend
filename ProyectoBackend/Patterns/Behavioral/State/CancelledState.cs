using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Behavioral.State
{
    public class CancelledState: IOrderState
    {
        public void HandleProcess(Order order) => Console.WriteLine("Cancelled — cannot process.");
        public void HandleCancel(Order order) => Console.WriteLine("Already cancelled.");
        public void HandleShip(Order order) => Console.WriteLine("Cancelled — cannot ship.");
    }
}
