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

        // Guarda el stock actual de cada componente por nombre.
        private readonly Dictionary<string, int> _stock = new();

        // Lista de observadores que serán notificados cuando el stock cambie.
        private readonly List<IStockObserver> _observers = new();

        // Constructor privado que carga el inventario inicial desde components.json.
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
            // 1. Actualizar memoria (lo que ya tenías)
            _stock[productName] = quantity;
            Notify(productName, quantity);

            // 2. NUEVO: Persistir el cambio en el archivo JSON
            try
            {
                // Cargamos la lista completa de objetos (con precio, categoría, etc.)
                var allComponents = JsonDataStore.LoadComponents();

                // Buscamos el componente que cambió
                var itemToUpdate = allComponents.FirstOrDefault(c => c.Name == productName);

                if (itemToUpdate != null)
                {
                    // Como los records son inmutables en C# 9+, creamos una copia con el nuevo stock
                    // O si cambiaste ComponentDto a clase normal, solo asigna: itemToUpdate.Stock = quantity;

                    // Si ComponentDto es un 'record' (como en tu código original):
                    var updatedItem = itemToUpdate with { Stock = quantity };

                    // Reemplazamos en la lista
                    allComponents.Remove(itemToUpdate);
                    allComponents.Add(updatedItem);

                    // Guardamos en disco
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

