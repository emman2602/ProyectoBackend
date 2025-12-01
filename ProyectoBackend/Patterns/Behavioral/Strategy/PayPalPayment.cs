using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Strategy
{
    public class PayPalPayment : PaymentStrategyBase
    {
        private string email = "";
        private string password = "";

        protected override void CollectPaymentDetails()
        {
            Console.Write("Ingrese email de PayPal: ");
            email = Console.ReadLine() ?? "";
            Console.Write("Ingrese contraseña: ");
            // Simulación de lectura de password oculta
            password = Console.ReadLine() ?? "";
        }

        protected override bool ValidateData()
        {
            if (email.Contains("@") && !string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Autenticando con PayPal... OK.");
                return true;
            }
            Console.WriteLine("Email inválido o contraseña vacía.");
            return false;
        }

        protected override void ProcessPayment(double amount)
        {
            Console.WriteLine($"Enviando {amount:C} desde la cuenta {email}...");
        }
    }
}