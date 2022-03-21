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
    public partial class FormularioProducto : Form
    {
        public FormularioProducto()
        {
            InitializeComponent();
        }

        string Operacion = "";
        ProductoDA productoDA = new ProductoDA();

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Operacion = "Nuevo";
            HabilitarControles();
        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled=true;
            DescripcionTextBox.Enabled=true;
            PrecioTextBox.Enabled=true; 
            ExistenciasTextBox.Enabled=true;
            ExaminarImagenButton.Enabled=true;

            GuardarButton.Enabled=true;
            CancelarButton.Enabled=true;
            NuevoButton.Enabled=false;
            ModificarButton.Enabled=false;  
        }

        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            ExistenciasTextBox.Enabled = false;
            ExaminarImagenButton.Enabled = false;

            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            NuevoButton.Enabled = true;
            ModificarButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            ExistenciasTextBox.Clear();
            ImagenPictureBox.Image=null;

            
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese el Codigo del producto.");
                    CodigoTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionTextBox, "Ingrese la descripcion del producto.");
                    DescripcionTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(PrecioTextBox.Text))
                {
                    errorProvider1.SetError(PrecioTextBox, "Ingrese el Precio del producto.");
                    PrecioTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ExistenciasTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese la cantidad de existencias del producto.");
                    ExistenciasTextBox.Focus();
                    return;
                }

                Producto producto = new Producto();
                producto.Codigo = CodigoTextBox.Text;
                producto.Descripcion = DescripcionTextBox.Text;
                producto.Precio = Convert.ToDecimal(PrecioTextBox.Text);
                producto.Existencia = Convert.ToInt32(ExistenciasTextBox.Text);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();  
                ImagenPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                producto.Imagen = ms.GetBuffer();

                if (Operacion == "Nuevo")
                {
                    bool Insertado = productoDA.InsertarProducto(producto);

                    if (Insertado)
                    {
                        DeshabilitarControles();
                        LimpiarControles();
                        ListarProductos();
                        MessageBox.Show("Producto Agregado Exitosamente.");

                    }
                }
                else if(Operacion =="Modificar")
                {
                   bool Modificar= productoDA.ModificarProducto(producto);
                    if (Modificar)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        ListarProductos();
                        MessageBox.Show("Producto a sido Modificado");

                    }
                }
            }
            catch (Exception ex)
            {

                
            }
        }
        //abrir imagen para seleccionar
        private void ExaminarImagenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog(); //abre cuadro de dialogo para pasar la imagen
            DialogResult Result = dialog.ShowDialog();

            if(Result == DialogResult.OK)
            {
                ImagenPictureBox.Image = Image.FromFile(dialog.FileName);  
            }
        }

        private void FormularioProducto_Load(object sender, EventArgs e)
        {
            ListarProductos();    
        }

        private void ListarProductos()
        {
            ProductosDataGridView.DataSource = productoDA.ListarProductos();
        }


        //en este metodo se validara el ingreso unicamente de numeros
        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;   
            }

            //valida que no se ingrese mas de un punto.
            if((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') >-1)
            {
                e.Handled=true;
            }
        }

        private void ExistenciasTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            Operacion = "Modificar";

            if (ProductosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                DescripcionTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Descripcion"].Value.ToString(); ;
                PrecioTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Precio"].Value.ToString(); ;
                ExistenciasTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Existencia"].Value.ToString(); ;

                var temporal = productoDA.SeleccionarImagen(CodigoTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                if (temporal.Length > 0)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(temporal);
                    ImagenPictureBox.Image = System.Drawing.Image.FromStream(ms);
                }
                else
                {
                    ImagenPictureBox.Image = null;
                }

                HabilitarControles();
                CodigoTextBox.Focus();
            }
            else
            {
                MessageBox.Show("Seleccione el producto");
            }
        }
    }
}
