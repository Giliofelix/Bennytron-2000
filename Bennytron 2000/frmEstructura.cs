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
            //cmbTechoExistente.Text = _nucleo.Parametro("cmbTechoExistente.SelectedText");
            //cmbAnclaje.Text = _nucleo.Parametro("cmbAnclaje.SelectedText");
            //cmbDetalleAnclaje.Text = _nucleo.Parametro("cmbDetalleAnclaje.SelectedText");
            cmbTechoExistente.SelectedIndex = 0;
            cmbAnclaje.SelectedIndex = 0;
            cmbDetalleAnclaje.SelectedIndex = 0;

            //txtSegmentosPanel.Text = _nucleo.Parametro("txtSegmentosPanel.Text");
            txtSegmentosPanel.Text = "1";
            txtGradosInclinacion.Text = _nucleo.Parametro("txtGradosInclinacion.Text");
        }

        private void Actualizar()
        {
            lblMaterialEstructural.Text = "Material estructural para " + cmbAnclaje.Text + " con " + cmbDetalleAnclaje.Text;
            lblMaterialFerretero.Text = "Material ferretero para " + cmbAnclaje.Text + " con " + cmbDetalleAnclaje.Text;

            // Columnas:
            // Material			Cantidad	Detalles	Costo($)

            #region Llenado de grit de cableado y protección Et2

            DataTable dtEstructural = new DataTable("Estructural");

            #region define las columnas
            dtEstructural.Columns.Add("Material", System.Type.GetType("System.String"));
            dtEstructural.Columns.Add("Cantidad", System.Type.GetType("System.String"));
            dtEstructural.Columns.Add("Detalles", System.Type.GetType("System.String"));
            dtEstructural.Columns.Add("Costo($)", System.Type.GetType("System.String"));

            DataTable dtFerretero = new DataTable("Ferretero");

            dtFerretero.Columns.Add("Material", System.Type.GetType("System.String"));
            dtFerretero.Columns.Add("Cantidad", System.Type.GetType("System.String"));
            dtFerretero.Columns.Add("Detalles", System.Type.GetType("System.String"));
            dtFerretero.Columns.Add("Costo($)", System.Type.GetType("System.String"));
            #endregion

           
            dgvMaterialEstructural.DataSource = dtEstructural;
            dgvMaterialEstructural.Columns[0].Width = 110;
            dgvMaterialEstructural.AllowUserToAddRows = false;

            dgvMaterialFerretero.DataSource = dtFerretero;
            dgvMaterialFerretero.Columns[0].Width = 110;
            dgvMaterialFerretero.AllowUserToAddRows = false;
            #endregion
        }

        private void frmEstructura_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Guardar seleciones en parámetros
            if (!_nucleo.ModificarParametro("cmbTechoExistente.SelectedText", cmbTechoExistente.SelectedItem.ToString()))
                MessageBox.Show("No se modificó el parámetro cmbTechoExistente");

            if (!_nucleo.ModificarParametro("cmbAnclaje.SelectedText", cmbAnclaje.SelectedItem.ToString()))
                MessageBox.Show("No se modificó el parámetro cmbAnclaje");

            if (!_nucleo.ModificarParametro("cmbDetalleAnclaje.SelectedText", cmbDetalleAnclaje.SelectedItem.ToString()))
                MessageBox.Show("No se modificó el parámetro cmbDetalleAnclaje");

            if (!_nucleo.ModificarParametro("txtSegmentosPanel.Text", txtSegmentosPanel.Text))
                MessageBox.Show("No se modificó el parámetro txtSegmentosPanel");

            if (!_nucleo.ModificarParametro("txtGradosInclinacion.Text", txtGradosInclinacion.Text))
                MessageBox.Show("No se modificó el parámetro txtGradosInclinacion");
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
