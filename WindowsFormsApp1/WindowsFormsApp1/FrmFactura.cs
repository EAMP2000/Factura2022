using Datos.Accesos;
using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmFactura : Form
    {
        public FrmFactura()
        {
            InitializeComponent();
        }

        Factura factura = new Factura();
        Producto producto;
        ProductoDA productoDA = new ProductoDA();
        FacturaDA facturaDA = new FacturaDA();

        List<DetalleFactura> DetalleFacturaLista = new List<DetalleFactura>();

        decimal Subtotal = 0;
        decimal ISV = 0;
        decimal TotalaPagar = decimal.Zero;

        private void FrmFactura_Load(object sender, EventArgs e)
        {
            DetalleFacturaDataGridView1.DataSource = DetalleFacturaLista;
        }

        //este metodo busca un producto
        private void CodigTtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                producto = new Producto();
                producto = productoDA.ObtenerProductoPorCodigo(CodigoTextBox.Text);

                DescripcionTextBox.Text = producto.Descripcion;
                CantidadTextBox.Focus();
            }
            else
            {
                producto = null;
                DescripcionTextBox.Clear();
                CantidadTextBox.Clear();
             
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                DetalleFactura detalleFactura = new DetalleFactura();
                detalleFactura.IdProducto = Convert.ToInt32(producto.Codigo);
                detalleFactura.Descripcion = producto.Descripcion;
                detalleFactura.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detalleFactura.Precio = producto.Precio;
                detalleFactura.Total = (producto.Precio * Convert.ToInt32(CantidadTextBox.Text));

                Subtotal += detalleFactura.Total;
                ISV = Subtotal * 0.15M;
                TotalaPagar = Subtotal + ISV;

                SubTotalTextBox.Text = Subtotal.ToString();
                ISVTextBox.Text = ISV.ToString();
                TotalAPagarTextBox.Text = TotalaPagar.ToString();

                DetalleFacturaLista.Add(detalleFactura);
                DetalleFacturaDataGridView1.DataSource = null;
                DetalleFacturaDataGridView1.DataSource = DetalleFacturaLista;
            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            factura.IdCliente = IdentidadMaskedTextBox1.Text;
            factura.Cliente = NombreTextBox.Text;
            factura.Fecha= FechaDateTimePicker1.Value;
            factura.SubTotal = Subtotal;
            factura.Impuesto = ISV;
            factura.Total = TotalaPagar;

            int idFactura = 0;

            idFactura= facturaDA.InsertarFactura(factura);

            if(idFactura != 0)
            {
                foreach (var item in DetalleFacturaLista)
                {
                    item.Id = idFactura;
                    facturaDA.InsertarDetalleFactura(item);
                }
            }

        }
    }
}
