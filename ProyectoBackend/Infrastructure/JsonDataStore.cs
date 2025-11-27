using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProyectoBackend.Entities;

namespace ProyectoBackend.Infrastructure
{
    // Maneja la carga y guardado de datos en archivos JSON del proyecto.
    public static class JsonDataStore
    {
        // Rutas de los archivos donde se guardan usuarios, componentes, productos y órdenes.
        private static readonly string BasePath = Path.Combine(AppContext.BaseDirectory, "Data");
        private static readonly string UsersFile = Path.Combine(BasePath, "users.json");
        private static readonly string ComponentsFile = Path.Combine(BasePath, "components.json");
        private static readonly string ProductsFile = Path.Combine(BasePath, "products.json");
        private static readonly string OrdersFile = Path.Combine(BasePath, "orders.json");

        // Configuración del serializador JSON.
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,               // Guarda el JSON con formato legible.
            PropertyNameCaseInsensitive = true  // Permite ignorar mayúsculas/minúsculas en propiedades.
        };

        // Inicializa las carpetas y archivos si no existen.
        static JsonDataStore()
        {
            if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);
            if (!File.Exists(UsersFile)) File.WriteAllText(UsersFile, "[]");
            if (!File.Exists(ComponentsFile)) File.WriteAllText(ComponentsFile, "[]");
            if (!File.Exists(ProductsFile)) File.WriteAllText(ProductsFile, "[]");
            if (!File.Exists(OrdersFile)) File.WriteAllText(OrdersFile, "[]");
        }

        // ----- Users -----

        // Carga la lista de usuarios desde users.json.
        public static List<User> LoadUsers()
        {
            var s = File.ReadAllText(UsersFile);
            return JsonSerializer.Deserialize<List<User>>(s, Options) ?? new();

        }

        // Guarda la lista de usuarios en users.json.
        public static void SaveUsers(List<User> users) =>
            File.WriteAllText(UsersFile, JsonSerializer.Serialize(users, Options));

        // ----- Components -----

        // Representa un componente básico como un pequeño DTO.
        public record ComponentDto(string Category, string Name, double Price, int Stock);

        // Carga los componentes desde components.json.
        public static List<ComponentDto> LoadComponents()
        {
            var s = File.ReadAllText(ComponentsFile);
            return JsonSerializer.Deserialize<List<ComponentDto>>(s, Options) ?? new();
        }

        // Guarda los componentes en components.json.
        public static void SaveComponents(IEnumerable<ComponentDto> comps) =>
            File.WriteAllText(ComponentsFile, JsonSerializer.Serialize(comps, Options));

        // ----- Products -----

        // DTO para productos pre-armados con sus componentes.
        public record ProductDto(string Type, string Name, double Price, List<string> Components);

        // Carga la lista de productos desde products.json.
        public static List<ProductDto> LoadProductDtos()
        {
            var s = File.ReadAllText(ProductsFile);
            return JsonSerializer.Deserialize<List<ProductDto>>(s, Options) ?? new();
        }

        // Guarda los productos en products.json.
        public static void SaveProductDtos(IEnumerable<ProductDto> prods) =>
            File.WriteAllText(ProductsFile, JsonSerializer.Serialize(prods, Options));

        // ----- Orders -----

        // Carga la lista de órdenes desde orders.json.
        public static List<Order> LoadOrders()
        {
            var s = File.ReadAllText(OrdersFile);
            var list = JsonSerializer.Deserialize<List<Order>>(s, Options) ?? new();

            // Restaurar el objeto State para cada orden
            foreach (var o in list) o.RestoreState();

            return list;
        }

        // Guarda las órdenes en orders.json.
        public static void SaveOrders(IEnumerable<Order> orders) =>
            File.WriteAllText(OrdersFile, JsonSerializer.Serialize(orders, Options));
    }
}
