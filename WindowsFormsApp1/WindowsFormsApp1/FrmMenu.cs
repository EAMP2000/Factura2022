using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmMenu : Syncfusion.Windows.Forms.Office2010Form
    {
        public FrmMenu()
        {
            InitializeComponent(); 
        }

        FormularioUsuarios formularioUsuarios = null;
        FormularioProducto formularioProducto= null;

        private void UsuariosToolStripButton1_Click(object sender, EventArgs e)
        {
            if (formularioUsuarios == null)
            {
                formularioUsuarios = new FormularioUsuarios();
                formularioUsuarios.MdiParent = this;
                formularioUsuarios.Show();
            }
            else
            {
                formularioUsuarios.Activate();
            }

             
        }

        private void ProductosToolStripButton1_Click(object sender, EventArgs e)
        {
            if(formularioProducto == null)
            {
                formularioProducto = new FormularioProducto();
                formularioProducto.MdiParent=this;
                formularioProducto.Show();
            }
            else
            {
                formularioProducto.Activate();
            }
        }
    }
}
