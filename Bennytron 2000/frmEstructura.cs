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
        Calculo _calculo;

        //Comentario
        public frmEstructura(Nucleo nucleo, Calculo calculo)
        {
            _nucleo = nucleo;
            _calculo = calculo; 

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
            /*
            DataTable dtEstructural = new DataTable("Estructural");
            DataTable dtFerretero = new DataTable("Ferretero");

            dtEstructural.Columns.Add("Material", System.Type.GetType("System.String"));
            dtEstructural.Columns.Add("Cantidad", System.Type.GetType("System.String"));
            dtEstructural.Columns.Add("Detalles", System.Type.GetType("System.String"));
            dtEstructural.Columns.Add("Costo($)", System.Type.GetType("System.String"));

            dtFerretero.Columns.Add("Material", System.Type.GetType("System.String"));
            dtFerretero.Columns.Add("Cantidad", System.Type.GetType("System.String"));
            dtFerretero.Columns.Add("Detalles", System.Type.GetType("System.String"));
            dtFerretero.Columns.Add("Costo($)", System.Type.GetType("System.String"));
            */

            DataTable dtEstructural = _nucleo.Obtener("SELECT Descripción as Material, 1 as Cantidad, Detalles, Costo as [Costo($)] from Material_estructural where utilizar; ");
            DataTable dtFerretero = _nucleo.Obtener("SELECT Descripción as Material, 1 as Cantidad, Detalles, Costo as [Costo($)] from Material_ferretero where utilizar; ");

            int segmentos = int.Parse(txtSegmentosPanel.Text);

            decimal cantidadMiniRiel = 0;

            for (int i = 0; i < dtEstructural.Rows.Count; i++)
            {
                if (dtEstructural.Rows[i][0].ToString().Trim() == "Next-Rail Mini")
                {
                    cantidadMiniRiel =(decimal)Math.Ceiling(((_calculo.NumeroDeModulos * 2) + (segmentos * 2)) * 1.05);  //REDONDEAR.MAS(((D10 * 2) + (Q11 * 2)) * 1.05, 0)
                    dtEstructural.Rows[i][1] = cantidadMiniRiel;
                    
                }
            }

            for (int i = 0; i < dtEstructural.Rows.Count; i++)
            {
                if (dtEstructural.Rows[i][0].ToString().Trim() == "Abrazadera Universal (CLAMP)")
                {
                    dtEstructural.Rows[i][1] = cantidadMiniRiel;
                }
            }

            dgvMaterialEstructural.DataSource = dtEstructural;
            dgvMaterialEstructural.Columns[0].Width = 101;
            dgvMaterialEstructural.Columns[1].Width = 101;
            dgvMaterialEstructural.Columns[2].Width = 101;
            dgvMaterialEstructural.Columns[3].Width = 101;
            dgvMaterialEstructural.AllowUserToAddRows = false;
            dgvMaterialEstructural.ReadOnly = true;



            for (int i = 0; i < dtFerretero.Rows.Count; i++)
            {
                if (dtFerretero.Rows[i][0].ToString().Trim() == "Pijabroca de 1/4\" X 1\" c/ rondana neopreno con cabeza hexagonal 3/8")
                {
                    dtFerretero.Rows[i][1] = Math.Ceiling(cantidadMiniRiel * (decimal)1.1);
                }


                if (dtFerretero.Rows[i][0].ToString().Trim() == "Pija broca 1/4 x 1\"")
                {
                    dtFerretero.Rows[i][1] = Math.Ceiling((cantidadMiniRiel / 2) * (decimal)1.15);
                }


                if (dtFerretero.Rows[i][0].ToString().Trim() == "Rondana plana 3/8\" inox")
                {
                    dtFerretero.Rows[i][1] = Math.Ceiling((cantidadMiniRiel / 2) * (decimal)1.15);
                }


                if (dtFerretero.Rows[i][0].ToString().Trim() == "Terminal de ojillo 1/4 Cal. 10 AWG")
                {
                    dtFerretero.Rows[i][1] = Math.Ceiling((cantidadMiniRiel / 2) * (decimal)1.15);
                }
            }

            /*
Pijabroca de 1/4" X 1" c/ rondana neopreno con cabeza hexagonal 3/8			
Pija broca 1/4 x 1"			
Rondana plana 3/8" inox			
Terminal de ojillo 1/4 Cal. 10 AWG			
Brocha de 4"			
             */


            dgvMaterialFerretero.DataSource = dtFerretero;
            dgvMaterialFerretero.Columns[0].Width = 101;
            dgvMaterialFerretero.Columns[1].Width = 101;
            dgvMaterialFerretero.Columns[2].Width = 101;
            dgvMaterialFerretero.Columns[3].Width = 101;
            dgvMaterialFerretero.AllowUserToAddRows = false;
            dgvMaterialFerretero.ReadOnly = true;
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
