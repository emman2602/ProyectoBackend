using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ProyectoBackend.Entities
{
    // Esta clase representa a un usuario dentro del sistema.
    // Contiene información básica necesaria para identificarlo y autenticarlo.
    public class User
    {
        // Identificador único del usuario.
        // Se genera automáticamente cada vez que se crea una nueva instancia.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nombre de usuario que la persona utilizará para iniciar sesión.
        public string UserName { get; set; }

        // Hash de la contraseña. Aquí no se guarda la contraseña real,
        // sino su versión encriptada por motivos de seguridad.
        public string PasswordHash { get; set; }

        // Nombre real del usuario o el nombre que se quiera mostrar.
        public string Name { get; set; }

        // Constructor vacío utilizado por el sistema de deserialización JSON.
        // Permite que se pueda reconstruir un objeto User desde un archivo o una cadena JSON.
        [JsonConstructor]
        public User() { }

        // Constructor que permite crear un nuevo usuario proporcionando
        // su nombre de usuario, su contraseña en forma de hash y su nombre real.
        public User(string userName, string passwordHash, string name)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            Name = name;
        }
    }
}
