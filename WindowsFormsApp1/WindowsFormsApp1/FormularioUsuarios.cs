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
    public partial class FormularioUsuarios : Form
    {
        public FormularioUsuarios()
        {
            InitializeComponent();
        }

        UsuarioDA usuarioDA = new UsuarioDA();
        Usuario user = new Usuario();
        string Operacion = string.Empty;

        private void FormularioUsuarios_Load(object sender, EventArgs e)
        {
            ListarUsuarios();
        }

        private void ListarUsuarios()
        {   //la funcion DataSource envia en este caso el listado de usuarios al DGV
            UsuariosDataGridView.DataSource= usuarioDA.ListarUsuarios();
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            Operacion = "Nuevo";
        }

        private void HabilitarControles()
        {   //textboxs
            CodigoTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            RolComboBox.Enabled = true;
            ClaveTextBox.Enabled = true;
            ActivoCheckBox.Enabled = true;
            //botones
            NuevoButton.Enabled = false;
            ModificarButton.Enabled = true;
            GuardarButton.Enabled = true;
            EliminarButton.Enabled = true;
            CancelarButton.Enabled = true;
        }

        private void DeshabilitarControles()
        {   //textboxs
            CodigoTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            RolComboBox.Enabled = false;
            ClaveTextBox.Enabled = false;
            ActivoCheckBox.Enabled = false;
            //botones
            NuevoButton.Enabled = true;
            ModificarButton.Enabled = false;
            GuardarButton.Enabled = false;
            EliminarButton.Enabled = false;
            CancelarButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Text = " ";
            NombreTextBox.Text= String.Empty;
            RolComboBox.Text= String.Empty;
            ClaveTextBox.Clear();
            ActivoCheckBox.Enabled = false;
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            user.CodigoUsuario= CodigoTextBox.Text;
            user.Nombre= NombreTextBox.Text;
            user.Rol= RolComboBox.Text; 
            user.Clave= ClaveTextBox.Text;
            user.Activo= ActivoCheckBox.Checked;

            if (Operacion == "Nuevo")
            {
                bool Insertado = usuarioDA.InsertarUsuario(user);

                if (Insertado)
                {
                    MessageBox.Show("Usuario Agregado Exitosamente.");
                    ListarUsuarios();
                    LimpiarControles();
                    DeshabilitarControles();
                }
                else
                {
                    MessageBox.Show("Usuario no pudo ser Agregado.");
                }
            }
            else if(Operacion == "Modificar")
            {
                bool Modificado = usuarioDA.ModificarUsuario(user);

                if (Modificado)
                {
                    MessageBox.Show("Usuario Modificado Exitosamente.");
                    ListarUsuarios();
                    LimpiarControles();
                    DeshabilitarControles();
                }
                else
                {
                    MessageBox.Show("Usuario no pudo ser Modificado.");
                }
            }

            
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            Operacion= "Modificar";
            //sirve para ver si se selecciono alguna fila
            if (UsuariosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString();
                NombreTextBox.Text = UsuariosDataGridView.CurrentRow.Cells["Nombre"].Value.ToString(); ;
                RolComboBox.Text = UsuariosDataGridView.CurrentRow.Cells["Rol"].Value.ToString(); ;
                ClaveTextBox.Text= UsuariosDataGridView.CurrentRow.Cells["Clave"].Value.ToString(); ;
                ActivoCheckBox.Checked = Convert.ToBoolean(UsuariosDataGridView.CurrentRow.Cells["Activo"].Value);

                HabilitarControles();
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (UsuariosDataGridView.SelectedRows.Count > 0)
            {
                bool Eliminado = usuarioDA.EliminarUsuario(UsuariosDataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                if (Eliminado)
                {
                    MessageBox.Show("Usuario Eliminado Exitosamente.");
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("Usuario no pudo ser Eliminado.");
                }

            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }
    }
}
