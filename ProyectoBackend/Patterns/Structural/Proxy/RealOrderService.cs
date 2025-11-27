using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Proxy
{
    public class RealOrderService: IOrderService
    {
        public Order CreateOrder(Entities.User user, List<OrderItem> items)
        {
            var o = new Order { UserId = user.Id };
            o.Items.AddRange(items);
            Console.WriteLine("RealOrderService: Order created.");
            return o;
        }
    }
}
