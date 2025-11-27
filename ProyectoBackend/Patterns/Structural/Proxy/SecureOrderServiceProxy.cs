using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Patterns.Structural.Proxy
{
    public class SecureOrderServiceProxy: IOrderService
    {
        private readonly IOrderService _real;
        private readonly bool _requireAuth;

        public SecureOrderServiceProxy(IOrderService real, bool requireAuth = true)
        {
            _real = real;
            _requireAuth = requireAuth;
        }

        public Order CreateOrder(User user, List<OrderItem> items)
        {
            if (_requireAuth && user == null) throw new UnauthorizedAccessException();
            Console.WriteLine("Proxy: authorizing order creation.");
            return _real.CreateOrder(user, items);
        }
    }
}
