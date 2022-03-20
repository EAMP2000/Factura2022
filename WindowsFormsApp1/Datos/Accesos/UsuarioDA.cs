using Datos.Entidades;
using MySql.Data.MySqlClient; //se importo esta libreria al declarar los objetos conexion y comando
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Accesos
{   //Acceso de Datos del Usuario
    public class UsuarioDA
    {   //CADENA DE CONEXION        nombre seridor  puertoMysql     nombreBD        nombreUsuario   contraseña 
        readonly string Cadena = "Server=localhost; Port=3306; Database=Programacion3; Uid=root; Pwd=123456789;";

        MySqlConnection conexion;
        MySqlCommand comando;

        public Usuario Login(string CodigoUsuario, string Clave)
        {
            Usuario user = null;

            //dentro del try se escribe todo el codigo
            try
            {
                //Consulta a MySQL
                string sql = "SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario AND Clave=@Clave";

                //instanciacion del objeto de coneccion recibiendo como parametro la cadena de conexion
                conexion = new MySqlConnection(Cadena);
                conexion.Open(); //abriendo la conexion

                comando = new MySqlCommand(sql, conexion);  //manda a ejecutar a la BD por medio de comando 
                comando.Parameters.AddWithValue("@CodigoUsuario", CodigoUsuario);
                comando.Parameters.AddWithValue("@Clave", Clave);

                MySqlDataReader reader = comando.ExecuteReader(); //almacena en reader lo que traiga la consulta sql

                while (reader.Read())
                {
                    user = new Usuario();
                    user.CodigoUsuario = reader[0].ToString();
                    user.Nombre = reader[1].ToString();
                    user.Rol = reader[2].ToString();
                    user.Clave= reader[3].ToString();
                    user.Activo = Convert.ToBoolean(reader[4]);
                }
                reader.Close();
                conexion.Close();

            }
            catch (Exception ex) //dentroo del catch se capturan los errores
            {

                
            }

            return user;
        }
         //Un Data table permite traer un listado de una consulta de una BD,se importa el using

        public DataTable ListarUsuarios()
        {
            DataTable ListaUsuariosDT = new DataTable();

            try
            {
                string sql = "SELECT * FROM usuario;"; //sql sentense

                conexion = new MySqlConnection(Cadena); //se inicializa y se abre nuevamente la conexion y comando
                conexion.Open(); 
                comando = new MySqlCommand(sql, conexion);

                MySqlDataReader reader = comando.ExecuteReader(); //lee toda la sentencia que se envia a la conexion
                ListaUsuariosDT.Load(reader); 
            }
            catch (Exception ex)
            {

                
            }
            return ListaUsuariosDT;
        }
         
        public bool InsertarUsuario(Usuario usuario)
        {
            bool Insertado = false;
            try
            {
                string sql = "INSERT INTO usuario VALUES (@CodigoUsuario, @Nombre, @Rol, @Clave, @Activo)";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);
                //se mandan como parametros todas las propiedades de un objeto usuario
                comando.Parameters.AddWithValue("@CodigoUsuario", usuario.CodigoUsuario);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Rol", usuario.Rol);
                comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                comando.Parameters.AddWithValue("@Activo", usuario.Activo);

                comando.ExecuteNonQuery(); //ejecuta, pero no devuelve nada.

                Insertado = true;
                conexion.Close();
            }
            catch (Exception ex)
            {

                
            }

            return Insertado;
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            bool Modificado = false;
            try
            {
                string sql = "UPDATE usuario SET CodigoUsuario= @CodigoUsuario, Nombre= @Nombre, Rol= @Rol, Clave= @Clave, Activo= @Activo WHERE CodigoUsuario= @CodigoUsuario";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@CodigoUsuario", usuario.CodigoUsuario);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Rol", usuario.Rol);
                comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                comando.Parameters.AddWithValue("@Activo", usuario.Activo);

                comando.ExecuteNonQuery();

                Modificado = true;
                conexion.Close();
            }
            catch (Exception ex)
            {


            }

            return Modificado;
        }

        public bool EliminarUsuario(string CodigoUsuario)
        {
            bool Eliminado = false;

            try
            {
                string sql = "DELETE FROM usuario  WHERE CodigoUsuario= @CodigoUsuario";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@CodigoUsuario",CodigoUsuario);
             
                comando.ExecuteNonQuery();

                Eliminado = true;
                conexion.Close();
            }
            catch (Exception ex)
            {


            }

            return Eliminado;
        }
    }
}
