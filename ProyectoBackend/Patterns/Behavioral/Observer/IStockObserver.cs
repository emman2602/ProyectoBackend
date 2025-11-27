using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBackend.Patterns.Behavioral.Observer
{
    public interface IStockObserver
    {
        void UpdateName(string productName, int newStock);
    }
}
