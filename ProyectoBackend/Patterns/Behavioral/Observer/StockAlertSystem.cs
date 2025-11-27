using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Observer
{
    public class StockAlertSystem: IStockObserver
    {
        public void UpdateName(string productName, int newStock)
        {
            if (newStock <= 2) Console.WriteLine($"ALERT: Low stock for {productName}: {newStock}");
        }
    }
}
