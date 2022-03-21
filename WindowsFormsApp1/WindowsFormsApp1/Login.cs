using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//se add los usings de las carpetas para poder trabajar con ellas en los formularios
using Datos;
using Datos.Entidades;
using Datos.Accesos;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            UsuarioDA usuarioDA = new UsuarioDA();
            Usuario usuario = new Usuario();

            usuario = usuarioDA.Login(UsuariotextBox.Text, ClaveTextBox.Text);

            if(usuario == null)
            {
                MessageBox.Show("Usuario desconocido.");
                return; //el return detiene la ejecucion de las demas lineas del programa
            }
            else if (!usuario.Activo)
            {
                MessageBox.Show("Usuario Inactivo.");
                return;
            }
            
            //se instancia el formulario de usuarios para ser llamado en la ejecucion y acceder a el.
            FrmMenu frmMenu = new FrmMenu();
            frmMenu.Show();
            this.Hide();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
