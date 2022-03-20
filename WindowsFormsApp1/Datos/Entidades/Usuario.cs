using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{   //debe ser public
    public class Usuario
    {   //atributos de la tabla Usuario 
        public string CodigoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }

        //Creacion de constructores
        public Usuario()
        {
        }

        public Usuario(string codigoUsuario, string nombre, string rol, string clave, bool activo)
        {
            CodigoUsuario = codigoUsuario;
            Nombre = nombre;
            Rol = rol;
            Clave = clave;
            Activo = activo;
        }
    }


}
