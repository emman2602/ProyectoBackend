using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Strategy
{
    public class CreditCardPayment: IPaymentStrategy
    {
        public void Pay(double amount) => Console.WriteLine($"Paid {amount:C} with Credit Card.");
    }
}
