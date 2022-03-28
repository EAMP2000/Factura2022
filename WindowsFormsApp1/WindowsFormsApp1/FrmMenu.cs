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
        FrmFactura FrmFactura = null;

        private void UsuariosToolStripButton1_Click(object sender, EventArgs e)
        {
            if (formularioUsuarios == null)
            {
                formularioUsuarios = new FormularioUsuarios();
                formularioUsuarios.MdiParent = this;
                formularioUsuarios.FormClosed += FormularioUsuarios_FormClosed;
                formularioUsuarios.Show();
            }
            else
            {
                formularioUsuarios.Activate();
            }

             
        }

        private void FormularioUsuarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            formularioUsuarios = null;
        }

        private void ProductosToolStripButton1_Click(object sender, EventArgs e)
        {
            if(formularioProducto == null)
            {
                formularioProducto = new FormularioProducto();
                formularioProducto.MdiParent=this;
                formularioProducto.FormClosed += FormularioProducto_FormClosed; //funcion que genera metodo para regenerar frmularios despues de cerrarlos.
                formularioProducto.Show();
            }
            else
            {
                formularioProducto.Activate();
            }
        }
        //funcion generada para abrir nuevament el formulario
        private void FormularioProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            formularioProducto = null;  
        }

        private void NuevaFacturaToolStripButton1_Click(object sender, EventArgs e)
        {
            if (FrmFactura == null)
            {
                FrmFactura = new FrmFactura();
                FrmFactura.MdiParent = this;
                FrmFactura.FormClosed += FrmFactura_FormClosed;
                FrmFactura.Show();
            }
            else
            {
                FrmFactura.Activate();
            }

        }

        private void FrmFactura_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmFactura = null; 
        }
    }
}
