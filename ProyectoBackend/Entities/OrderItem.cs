using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Structural.Composite;

namespace ProyectoBackend.Entities
{
    // Representa un artículo dentro de una orden de compra.
    // Guarda información del producto y la cantidad solicitada.
    public class OrderItem
    {
        // Nombre del producto asociado a este artículo.
        // Se inicializa con una cadena vacía para evitar valores nulos.
        public string ProductName { get; set; } = "";

        // Precio unitario del producto.
        public double Price { get; set; }

        // Cantidad del producto que se agregará a la orden.
        // Por defecto se asume que se agrega al menos una unidad.
        public int Quantity { get; set; } = 1;

        // Referencia opcional al objeto del producto.
        // Se marca para que no se incluya al convertir la orden a JSON,
        // ya que solo se necesita durante la ejecución, no para almacenamiento.
        [System.Text.Json.Serialization.JsonIgnore]
        public IProducts? ProductObj { get; set; }

        // Constructor vacío necesario para procesos como serialización o deserialización.
        public OrderItem() { }

        // Constructor que permite crear un OrderItem a partir de un objeto de producto
        // y la cantidad que se desea agregar.
        public OrderItem(IProducts p, int q)
        {
            ProductName = p.GetName();  // Se obtiene el nombre directamente del producto.
            Price = p.GetPrice();        // Se obtiene el precio del producto.
            Quantity = q;                // Se asigna la cantidad deseada.
            ProductObj = p;              // Se guarda la referencia al producto original para uso interno.
        }
    }
}
