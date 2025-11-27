using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Observer
{
    public class CatalogManager:IStockObserver
    {
        public void UpdateName(string productName, int newStock) => Console.WriteLine($"CatalogManager: updated stock for {productName} => {newStock}");
    }
}
