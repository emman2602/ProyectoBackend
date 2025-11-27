using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Behavioral.State
{
    public interface IOrderState
    {
        void HandleProcess(Order order);
        void HandleCancel(Order order);
        void HandleShip(Order order);
    }
}
