using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Accesos
{
    public class FacturaDA
    {
        readonly string Cadena = "Server=localhost; Port=3306; Database=Programacion3; Uid=root; Pwd=123456789;";

        MySqlConnection conexion;
        MySqlCommand comando;

        public int InsertarFactura(Factura factura)
        {
            int IdFactura = 0;
            try
            {
                string sql = "INSERT INTO Factura (Id, IdCliente, Cliente, Fecha, SubTotal, Impuesto, Total) VALUES (@Id, @IdCliente, @Cliente, @Fecha, @SubTotal, @Impuesto, @Total); SELECT LAST_INSERT_ID();";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);
                //se mandan como parametros todas las propiedades de un objeto producto
                comando.Parameters.AddWithValue("@Id", factura.Id);
                comando.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                comando.Parameters.AddWithValue("@Cliente", factura.Cliente);
                comando.Parameters.AddWithValue("@Fecha", factura.Fecha);
                comando.Parameters.AddWithValue("@SubTotal", factura.SubTotal);
                comando.Parameters.AddWithValue("@Impuesto", factura.Impuesto);
                comando.Parameters.AddWithValue("@Total", factura.Total);

                IdFactura = Convert.ToInt32(comando.ExecuteScalar());

           
                conexion.Close();
            }
            catch (Exception ex)
            {


            }

            return IdFactura;
        }

        public bool InsertarDetalleFactura(DetalleFactura detalleFactura)
        {
            bool Insertado = false;
            try
            {
                string sql = "INSERT INTO DetalleFactura (Id, IdFactura, IdProducto, Descripcion, Cantidad, Precio, Total) VALUES (@Id, @IdFactura, @IdProducto, @Descripcion, @Cantidad, @Precio, @Total)";
                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);
                //se mandan como parametros todas las propiedades de un objeto producto
                comando.Parameters.AddWithValue("@Id", detalleFactura.Id);
                comando.Parameters.AddWithValue("@IdFactura", detalleFactura.IdFactura);
                comando.Parameters.AddWithValue("@IdProducto", detalleFactura.IdProducto);
                comando.Parameters.AddWithValue("@Descripcion", detalleFactura.Descripcion);
                comando.Parameters.AddWithValue("@Cantidad", detalleFactura.Cantidad);
                comando.Parameters.AddWithValue("@Precio", detalleFactura.Precio);
                comando.Parameters.AddWithValue("@Total", detalleFactura.Total);

                comando.ExecuteNonQuery(); //ejecuta, pero no devuelve nada.

                Insertado = true;
                conexion.Close();
            }
            catch (Exception ex)
            {


            }

            return Insertado;
        }
    }
}
