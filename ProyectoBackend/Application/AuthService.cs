using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProyectoBackend.Entities;
using ProyectoBackend.Infrastructure;

namespace ProyectoBackend.Application
{
    // Servicio que maneja todo lo relacionado con usuarios:
    // registro, inicio de sesión y guardado en archivo JSON.
    // Es estático porque no hace falta crear instancias; solo se necesita un punto de acceso global.
    public static class AuthService
    {
        // Lista de usuarios cargada desde users.json al iniciar la aplicación
        private static List<User> _users = JsonDataStore.LoadUsers();

        // Se expone solo para lectura, evitando que se modifique desde fuera.
        public static IEnumerable<User> Users => _users;

        // Guarda el estado actual de la lista en el archivo JSON
        public static void Save()
        {
            JsonDataStore.SaveUsers(_users);
        }

        // Convierte una contraseña en un hash usando SHA256
        public static string Hash(string plain)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        // Registra un usuario nuevo siempre y cuando el nombre no exista.
        // Si ya está registrado, devuelve null.
        public static User? Register(string username, string password, string name)
        {
            // Verifica si el usuario ya está tomado.
            if (_users.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return null;

            // Crea el usuario, hashea la contraseña y lo guarda.
            var user = new User(username, Hash(password), name);
            _users.Add(user);

            Save(); // Persistencia

            return user;
        }

        // Intenta iniciar sesión comparando usuario y contraseña.
        // Si coinciden, devuelve el usuario; si no, retorna null.
        public static User? Login(string username, string password)
        {
            var h = Hash(password);

            return _users.FirstOrDefault(u =>
                u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.PasswordHash == h
            );
        }
    }
}

