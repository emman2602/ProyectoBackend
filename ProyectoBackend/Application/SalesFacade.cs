using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;
using ProyectoBackend.Infrastructure;
using ProyectoBackend.Patterns.Creational.AbstractFactory;
using ProyectoBackend.Patterns.Creational.Builder;

namespace ProyectoBackend.Application
{
    // Fachada que centraliza la creación de órdenes y el armado de computadoras.
    public class SalesFacade
    {
        // Servicio Proxy encargado de manejar la creación de órdenes.
        private readonly Patterns.Structural.Proxy.IOrderService _orderService;

        public SalesFacade(Patterns.Structural.Proxy.IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Crea una orden con el servicio Proxy y la guarda en el archivo JSON.
        public Order CreateOrder(User user, List<OrderItem> items)
        {
            var o = _orderService.CreateOrder(user, items);
            return o;
        }

        // Construye una computadora usando Abstract Factory y Builder.
        // El tipo define si es gaming o de oficina.
        public Patterns.Structural.Composite.Computer BuildComputer(string type)
        {
            IComponentFactory factory =
                type == "gaming"
                ? new Patterns.Creational.AbstractFactory.GamingComponentFactory()
                : new Patterns.Creational.AbstractFactory.OfficeComponentFactory();

            IComputerBuilder builder =
                type == "gaming"
                ? new Patterns.Creational.Builder.GamingComputerBuilder(factory)
                : new Patterns.Creational.Builder.StandardComputerBuilder(factory);

            var director = new Patterns.Creational.Builder.AssemblyDirector();
            return director.Construct(builder);
        }
    }
}

