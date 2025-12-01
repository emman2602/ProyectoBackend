using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Strategy
{
    public class CreditCardPayment : PaymentStrategyBase
    {
        private string cardNumber = "";
        private string cvv = "";

        protected override void CollectPaymentDetails()
        {
            Console.Write("Ingrese número de tarjeta (16 dígitos): ");
            cardNumber = Console.ReadLine() ?? "";
            Console.Write("Ingrese CVV (3 dígitos): ");
            cvv = Console.ReadLine() ?? "";
        }

        protected override bool ValidateData()
        {
            // Validación simple: longitud correcta
            if (cardNumber.Length == 16 && cvv.Length == 3 && long.TryParse(cardNumber, out _))
            {
                Console.WriteLine("Validando tarjeta con el banco... OK.");
                return true;
            }
            Console.WriteLine("Validación fallida: Formato de tarjeta incorrecto.");
            return false;
        }

        protected override void ProcessPayment(double amount)
        {
            Console.WriteLine($"Cargando {amount:C} a la tarjeta terminada en {cardNumber.Substring(12)}...");
        }
    }
}
