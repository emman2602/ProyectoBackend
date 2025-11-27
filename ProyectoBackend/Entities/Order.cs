using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Behavioral.State;
using ProyectoBackend.Patterns.Behavioral.Strategy;
using ProyectoBackend.Patterns.Structural.Bridge;

namespace ProyectoBackend.Entities
{
    // Representa una orden realizada por un usuario.
    // Contiene información sobre su estado, los artículos incluidos
    // y los métodos de pago y envío seleccionados.
    public class Order
    {
        // Identificador único de la orden. Se genera automáticamente.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nombre del tipo de estado actual de la orden.
        // Se guarda como texto para poder serializarlo fácilmente.
        public string StateType { get; set; } =
            nameof(ProyectoBackend.Patterns.Behavioral.State.PendingState);

        // Objeto que representa el estado actual de la orden usando el patrón State.
        // No se serializa porque se volverá a reconstruir desde StateType.
        [System.Text.Json.Serialization.JsonIgnore]
        public IOrderState State { get; set; }

        // Tipo de pago seleccionado por el usuario (por ejemplo: tarjeta, PayPal, etc.).
        public string? PaymentType { get; set; }

        // Tipo de envío elegido (por ejemplo: estándar, exprés).
        public string? ShippingType { get; set; }

        // Lista de artículos incluidos en la orden.
        public List<OrderItem> Items { get; set; } = new();

        // Id del usuario que creó la orden.
        public Guid UserId { get; set; }

        // Estrategia de pago usada en tiempo de ejecución
        // No se serializa porque se vuelve a generar según PaymentType.
        [System.Text.Json.Serialization.JsonIgnore]
        public IPaymentStrategy? Payment { get; set; }

        // Estrategia o tipo de envío seleccionada. Igual que Payment, se usa solo en ejecución.
        [System.Text.Json.Serialization.JsonIgnore]
        public ShippingType? Shipping { get; set; }

        // Constructor por defecto.
        // Inicializa la orden con su estado por defecto: pendiente.
        public Order()
        {
            State = new ProyectoBackend.Patterns.Behavioral.State.PendingState();
            StateType = State.GetType().Name; // Guarda el nombre del estado para serialización.
        }

        // Calcula el precio total de la orden sumando el precio de cada producto
        // multiplicado por su cantidad.
        public double TotalPrice() => Items.Sum(i => i.Price * i.Quantity);

        // Cambia el estado actual de la orden y actualiza el nombre almacenado.
        public void SetState(IOrderState s)
        {
            State = s;
            StateType = s.GetType().Name;
        }

        // Ejecuta la acción de procesar la orden
        // Ejecuta la acción de procesar la orden delegando al Estado actual
        public void ProcessOrder()
        {
            State.HandleProcess(this);
        }

        // Ejecuta la acción de enviar la orden delegando al Estado actual
        public void ShipOrder()
        {
            State.HandleShip(this);
        }

        public void RestoreState()
        {
            switch (StateType)
            {
                case nameof(ProyectoBackend.Patterns.Behavioral.State.PendingState):
                    State = new ProyectoBackend.Patterns.Behavioral.State.PendingState();
                    break;
                case nameof(ProyectoBackend.Patterns.Behavioral.State.BuildingState):
                    State = new ProyectoBackend.Patterns.Behavioral.State.BuildingState();
                    break;
                case nameof(ProyectoBackend.Patterns.Behavioral.State.ShippedState):
                    State = new ProyectoBackend.Patterns.Behavioral.State.ShippedState();
                    break;
                case nameof(ProyectoBackend.Patterns.Behavioral.State.CancelledState):
                    State = new ProyectoBackend.Patterns.Behavioral.State.CancelledState();
                    break;
                default:
                    State = new ProyectoBackend.Patterns.Behavioral.State.PendingState();
                    break;
            }
        }
    } // Cierre de la clase
} // Cierre del namespace