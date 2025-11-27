using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Proxy
{
    public interface IOrderService
    {
        Order CreateOrder(User user, List<OrderItem> items);
    }
}
