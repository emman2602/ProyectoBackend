using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProyectoBackend.Patterns.Behavioral.Strategy
{
    // Template Method Pattern: Define el esqueleto del proceso de pago
    public abstract class PaymentStrategyBase : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"\nIniciando transacción por {amount:C}...");

            // Paso 1: Recolectar datos del usuario
            CollectPaymentDetails();

            // Paso 2: Validar los datos (Simulación)
            if (ValidateData())
            {
                // Paso 3: Procesar el pago
                ProcessPayment(amount);
                Console.WriteLine("Transacción completada exitosamente.");
            }
            else
            {
                Console.WriteLine("Error: Datos de pago inválidos o saldo insuficiente.");
            }
        }

        protected abstract void CollectPaymentDetails();
        protected abstract bool ValidateData();
        protected abstract void ProcessPayment(double amount);
    }
}