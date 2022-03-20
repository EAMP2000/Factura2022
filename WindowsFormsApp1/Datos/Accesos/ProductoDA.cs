using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Accesos
{
    public class ProductoDA
    {
        readonly string Cadena = "Server=localhost; Port=3306; Database=Programacion3; Uid=root; Pwd=123456789;";

        MySqlConnection conexion;
        MySqlCommand comando;
        //Un Data table permite traer un listado de una consulta de una BD,se importa el using

        public DataTable ListarProductos()
        {
            DataTable ListaProductosDT = new DataTable();

            try
            {
                string sql = "SELECT * FROM Producto;"; //sql sentense

                conexion = new MySqlConnection(Cadena); 
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);

                MySqlDataReader reader = comando.ExecuteReader(); 
                ListaProductosDT.Load(reader);
            }
            catch (Exception ex)
            {


            }
            return ListaProductosDT;
        }

        public bool InsertarProducto(Producto producto)
        {
            bool Insertado = false;
            try
            {
                string sql = "INSERT INTO Producto VALUES (@Codigo, @Descripcion, @Precio, @Existencia)";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);
                //se mandan como parametros todas las propiedades de un objeto producto
                comando.Parameters.AddWithValue("@Codigo", producto.Codigo);
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@Existencia", producto.Existencia);
              
                comando.ExecuteNonQuery(); //ejecuta, pero no devuelve nada.

                Insertado = true;
                conexion.Close();
            }
            catch (Exception ex)
            {


            }

            return Insertado;
        }

        public bool ModificarProducto(Producto producto)
        {
            bool Modificado = false;
            try
            {
                string sql = "UPDATE Producto SET Codigo= @Codigo, Nombre= @Nombre, Descripcion= @Descripcion, Precio= @Precio, Existencia= @Existencia WHERE Codigo= @Codigo";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@Codigo", producto.Codigo);
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@Existencia", producto.Existencia);

                comando.ExecuteNonQuery();

                Modificado = true;
                conexion.Close();
            }
            catch (Exception ex)
            {


            }

            return Modificado;
        }

        public bool EliminarUsuario(string Codigo)
        {
            bool Eliminado = false;

            try
            {
                string sql = "DELETE FROM Producto  WHERE Codigo= @Codigo";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@Codigo", Codigo);

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
