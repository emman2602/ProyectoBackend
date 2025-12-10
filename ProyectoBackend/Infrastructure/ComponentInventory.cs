using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Patterns.Behavioral.Observer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoBackend.Infrastructure
{
    // Maneja el inventario de componentes utilizando el patrón Singleton.
    // Permite consultar y actualizar existencias, y notifica a observadores cuando hay cambios.
    public class ComponentInventory
    {
        // Instancia única del inventario (Singleton).
        private static ComponentInventory? _instance;

       
        private readonly Dictionary<string, int> _stock = new();

       
        private readonly List<IStockObserver> _observers = new();

        
        private ComponentInventory()
        {
            var comps = JsonDataStore.LoadComponents();
            foreach (var c in comps) _stock[c.Name] = c.Stock;
        }

        // Devuelve la instancia única del inventario o la crea si no existe.
        public static ComponentInventory Instance => _instance ??= new ComponentInventory();

        // Agrega un observador para recibir notificaciones.
        public void Attach(IStockObserver o) => _observers.Add(o);

        // Elimina un observador.
        public void Detach(IStockObserver o) => _observers.Remove(o);

        // Actualiza el stock de un componente y notifica a los observadores.
        public void SetStock(string productName, int quantity)
        {
     
            _stock[productName] = quantity;
            Notify(productName, quantity);

            try
            {
                
                var allComponents = JsonDataStore.LoadComponents();

         
                var itemToUpdate = allComponents.FirstOrDefault(c => c.Name == productName);

                if (itemToUpdate != null)
                {
                    

                    var updatedItem = itemToUpdate with { Stock = quantity };

                    allComponents.Remove(itemToUpdate);
                    allComponents.Add(updatedItem);

                   
                    JsonDataStore.SaveComponents(allComponents);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando inventario: {ex.Message}");
            }
        }

        // Devuelve el stock actual del componente solicitado.
        public int GetStock(string productName) =>
            _stock.TryGetValue(productName, out var q) ? q : 0;

        // Notifica a todos los observadores sobre un cambio de stock.
        private void Notify(string name, int quantity)
        {
            foreach (var obs in _observers) obs.UpdateName(name, quantity);
        }
    }
}

