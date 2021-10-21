using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bennytron_2000
{
    public partial class frmEstructura : Form
    {
        Nucleo _nucleo;

        //Comentario
        public frmEstructura(Nucleo nucleo)
        {
            _nucleo = nucleo;

            InitializeComponent();
        }

        private void frmEstructura_Load(object sender, EventArgs e)
        {
            // Cargar parámetros guardados;
            cmbTechoExistente.Text = _nucleo.Parametro("cmbTechoExistente.SelectedText");
            cmbAnclaje.Text = _nucleo.Parametro("cmbAnclaje.SelectedText");
            cmbDetalleAnclaje.Text = _nucleo.Parametro("cmbDetalleAnclaje.SelectedText");

            txtSegmentosPanel.Text = _nucleo.Parametro("txtSegmentosPanel.Text");
        }

        private void Actualizar()
        {
            lblMaterialEstructural.Text = "Material estructural para " + cmbAnclaje.Text + " con " + cmbDetalleAnclaje.Text;
            lblMaterialFerretero.Text = "Material ferretero para " + cmbAnclaje.Text + " con " + cmbDetalleAnclaje.Text;


        }

        private void frmEstructura_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Guardar seleciones en parámetros
            _nucleo.ModificarParametro("cmbTechoExistente.SelectedText", cmbTechoExistente.SelectedText);
            _nucleo.ModificarParametro("cmbAnclaje.SelectedText", cmbAnclaje.SelectedText);
            _nucleo.ModificarParametro("cmbDetalleAnclaje.SelectedText", cmbDetalleAnclaje.SelectedText);
            _nucleo.ModificarParametro("txtSegmentosPanel.Text", txtSegmentosPanel.Text);

            Actualizar();
        }

        private void cmbTechoExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void txtSegmentosPanel_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void cmbAnclaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void cmbDetalleAnclaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            Actualizar();
        }
    }
}
