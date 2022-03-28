using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public string IdCliente { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public Factura()
        {
        }

        public Factura(int id, string idCliente, string cliente, DateTime fecha, decimal subTotal, decimal impuesto, decimal total)
        {
            Id = id;
            IdCliente = idCliente;
            Cliente = cliente;
            Fecha = fecha;
            SubTotal = subTotal;
            Impuesto = impuesto;
            Total = total;
        }
    }
}
