using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Application;
using ProyectoBackend.Entities;
using ProyectoBackend.Infrastructure;
using ProyectoBackend.Patterns.Behavioral.Strategy;
using ProyectoBackend.Patterns.Structural.Bridge;
using ProyectoBackend.Patterns.Structural.Composite;
using ProyectoBackend.Patterns.Structural.Decorator;
using ProyectoBackend.Patterns.Structural.Proxy;

namespace ProyectoBackend.UI
{
    public class Menu
    {
        private User? _current;
        private readonly List<OrderItem> _cart = new();
        private readonly SalesFacade _facade;
        public Menu()
        {
            // create order service via proxy over real
            var real = new Patterns.Structural.Proxy.RealOrderService();
            var proxy = new SecureOrderServiceProxy(real, true);
            _facade = new SalesFacade(proxy);

            // attach observers
            ComponentInventory.Instance.Attach(new Patterns.Behavioral.Observer.StockAlertSystem());
            ComponentInventory.Instance.Attach(new Patterns.Behavioral.Observer.CatalogManager());
        }

        public void Run()
        {
            EnsureInitialData();
            while (true)
            {
                if (_current == null) ShowAuthMenu();
                else ShowMainMenu();
            }
        }

        private void EnsureInitialData()
        {
            var prods = JsonDataStore.LoadProductDtos();
            var comps = JsonDataStore.LoadComponents();
            if (!prods.Any() && !comps.Any())
            {
                // If both empty, create defaults (but Data JSON provided already)
            }
        }

        private void ShowAuthMenu()
        {
            Console.WriteLine("\n=== Computer Store ===");
            Console.WriteLine("1) Login");
            Console.WriteLine("2) Register");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
            var c = Console.ReadLine();
            switch (c)
            {
                case "1": Login(); break;
                case "2": Register(); break;
                case "0": Environment.Exit(0); break;
                default: Console.WriteLine("Invalid"); break;
            }
        }

        private void Login()
        {
            Console.Write("User: "); var u = Console.ReadLine() ?? "";
            Console.Write("Password: "); var p = ReadPassword();
            var usr = AuthService.Login(u, p);
            if (usr == null) Console.WriteLine("Invalid credentials.");
            else { _current = usr; Console.WriteLine($"Welcome {_current.Name}!"); }
        }

        private void Register()
        {
            Console.Write("User: "); var u = Console.ReadLine() ?? "";
            Console.Write("Name: "); var n = Console.ReadLine() ?? "";
            Console.Write("Password: "); var p = ReadPassword();
            var newUser = AuthService.Register(u, p, n);
            if (newUser == null) Console.WriteLine("User already exists.");
            else Console.WriteLine("Registered successfully. Login now.");
        }

        private void ShowMainMenu()
        {
            Console.WriteLine($"\n== Main menu ({_current?.UserName}) ==");
            Console.WriteLine("1) Ver componentes");
            Console.WriteLine("2) Comprar prearmada");
            Console.WriteLine("3) Armar PC");
            Console.WriteLine("4) Ver carrito");
            Console.WriteLine("5) Checkout");
            Console.WriteLine("6) Mis órdenes");
            Console.WriteLine("7) Logout");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
            var c = Console.ReadLine();
            switch (c)
            {
                case "1": ShowComponents(); break;
                case "2": BuyPrebuilt(); break;
                case "3": BuildPC(); break;
                case "4": ViewCart(); break;
                case "5": Checkout(); break;
                case "6": MyOrders(); break;
                case "7": _current = null; _cart.Clear(); break;
                case "0": Environment.Exit(0); break;
                default: Console.WriteLine("Invalid"); break;
            }
        }

        private void ShowComponents()
        {
            var comps = JsonDataStore.LoadComponents();
            Console.WriteLine("\n-- Components --");
            for (int i = 0; i < comps.Count; i++)
            {
                var c = comps[i];
                Console.WriteLine($"{i + 1}) [{c.Category}] {c.Name} - {c.Price:C} (stock {c.Stock})");
            }
        }

        private void BuyPrebuilt()
        {
            var prods = JsonDataStore.LoadProductDtos();
            if (!prods.Any()) { Console.WriteLine("No prebuilt products."); return; }
            for (int i = 0; i < prods.Count; i++)
            {
                var p = prods[i];
                Console.WriteLine($"{i + 1}) {p.Name} - {p.Price:C} (Components: {string.Join(", ", p.Components)})");
            }
            Console.Write("Choose number: ");
            if (int.TryParse(Console.ReadLine(), out var idx) && idx >= 1 && idx <= prods.Count)
            {
                var chosen = prods[idx - 1];
                _cart.Add(new OrderItem { ProductName = chosen.Name, Price = chosen.Price, Quantity = 1 });
                Console.WriteLine("Added to cart.");
            }
        }

        private void BuildPC()
        {
            Console.WriteLine("\n--- Armado de PC Personalizado ---");

            // 1. El usuario elige cada componente paso a paso
            var motherboard = SelectComponent("Motherboard");
            if (motherboard == null) return; // Cancelado

            var cpu = SelectComponent("CPU");
            if (cpu == null) return;

            var ram = SelectComponent("RAM");
            if (ram == null) return;

            var gpu = SelectComponent("GPU");
            if (gpu == null) return;

            // 2. Creamos nuestra fábrica con las elecciones del usuario
            var customFactory = new ProyectoBackend.Patterns.Creational.AbstractFactory.UserSelectionFactory(
                cpu, gpu, motherboard, ram
            );

            // 3. Usamos el Builder estándar pero con nuestra fábrica personalizada
            var builder = new ProyectoBackend.Patterns.Creational.Builder.StandardComputerBuilder(customFactory);
            var director = new ProyectoBackend.Patterns.Creational.Builder.AssemblyDirector();

            // 4. Construimos la PC
            var comp = director.Construct(builder);

            Console.WriteLine("\nPC Armada Exitosamente:");
            Console.WriteLine(comp.ToString());

            // 5. Preguntar por garantía extendida (reutilizando tu lógica existente)
            Console.Write("Add Extended Warranty? (y/n): ");
            var w = Console.ReadLine();
            IProducts final = comp;
            if (w?.ToLower() == "y") final = new ExtendedWarranty(final);

            // 6. Agregar al carrito
            // Nota: Guardamos como BaseComponent para simplificar
            _cart.Add(new OrderItem(new BaseComponent(final.GetName(), final.GetPrice()), 1));
            Console.WriteLine("Custom PC added to cart.");
        }

        // Método auxiliar para mostrar lista y devolver la selección como un IProducts
        private IProducts? SelectComponent(string category)
        {
            Console.WriteLine($"\nSeleccione {category}:");

            // Cargamos componentes y filtramos por categoría
            var allComponents = JsonDataStore.LoadComponents();
            var available = allComponents.Where(c => c.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!available.Any())
            {
                Console.WriteLine($"No hay stock de {category}.");
                return null;
            }

            for (int i = 0; i < available.Count; i++)
            {
                var c = available[i];
                Console.WriteLine($"{i + 1}) {c.Name} - {c.Price:C} (Stock: {c.Stock})");
            }

            Console.Write("Opción: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= available.Count)
            {
                var chosen = available[index - 1];
                // Convertimos el DTO (datos) a un objeto del patrón Composite (BaseComponent)
                return new BaseComponent(chosen.Name, chosen.Price);
            }

            Console.WriteLine("Selección inválida.");
            return null;
        }
        private void ViewCart()
        {
            if (!_cart.Any()) { Console.WriteLine("Cart empty."); return; }
            Console.WriteLine("\n-- Cart --");
            for (int i = 0; i < _cart.Count; i++)
            {
                var it = _cart[i];
                Console.WriteLine($"{i + 1}) {it.ProductName} x{it.Quantity} - {it.Price:C} each");
            }
            Console.WriteLine($"Total: {_cart.Sum(i => i.Price * i.Quantity):C}");
            Console.Write("Remove item number or press enter: ");
            var s = Console.ReadLine();
            if (int.TryParse(s, out var r) && r >= 1 && r <= _cart.Count) { _cart.RemoveAt(r - 1); Console.WriteLine("Removed."); }
        }

        private void Checkout()
        {
            if (!_cart.Any()) { Console.WriteLine("Cart empty."); return; }
            var order = new Order { UserId = _current!.Id };
            order.Items.AddRange(_cart);

            Console.WriteLine("Choose Payment: 1) Credit Card  2) PayPal");
            var p = Console.ReadLine();
            order.Payment = p == "1" ? (IPaymentStrategy)new ProyectoBackend.Patterns.Behavioral.Strategy.CreditCardPayment() : new ProyectoBackend.Patterns.Behavioral.Strategy.PayPalPayment();
            order.PaymentType = order.Payment.GetType().Name;

            Console.WriteLine("Choose Shipping: 1) Standard (FedEx) 2) Express (DHL)");
            var s = Console.ReadLine();
            if (s == "2") order.Shipping = new ExpressShipping(new DHLPlatform());
            else order.Shipping = new StandardShipping(new FedExPlatform());
            order.ShippingType = order.Shipping.GetType().Name;

            // create via facade (proxy checks authentication)
            var created = _facade.CreateOrder(_current, order.Items);
            created.Payment = order.Payment;
            created.Shipping = order.Shipping;
            created.UserId = _current.Id;

            // Save and process
            var all = JsonDataStore.LoadOrders();
            all.Add(created);
            JsonDataStore.SaveOrders(all);

            Services.PayOrder(created);
            created.ProcessOrder();
            Services.ShipOrder(created);
            created.ShipOrder();

            Console.WriteLine($"Order placed: {created.Id}");
            _cart.Clear();
        }

        private void MyOrders()
        {
            var orders = JsonDataStore.LoadOrders().Where(o => o.UserId == _current!.Id).ToList();
            if (!orders.Any()) { Console.WriteLine("No orders found."); return; }
            foreach (var o in orders)
            {
                Console.WriteLine($"Order {o.Id} - Items: {o.Items.Count} - State: {o.StateType} - Total: {o.TotalPrice():C}");
            }
        }

        private static string ReadPassword()
        {
            var sb = new System.Text.StringBuilder();
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && sb.Length > 0) { sb.Remove(sb.Length - 1, 1); Console.Write("\b \b"); }
                else if (!char.IsControl(key.KeyChar)) { sb.Append(key.KeyChar); Console.Write("*"); }
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}
