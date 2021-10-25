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
    public partial class Form1 : Form
    {
        public Nucleo _nucleo;
        public Calculo _calculo;

        public Form1()
        {
            InitializeComponent();
        }

        private void RecalcularCostos()
        {
            // Actualizar número de módulos y clasificación
            try
            {

                lblClasificacion.Text = "";

                if (txtCapacidad.Text != "")
                    lblClasificacion.Text = Calculo.Clasificacion(int.Parse(txtCapacidad.Text));

                // Actualizar potencia
                //lblPotencia.Text = Calculo.Potencia(cmbModulo.Text).ToString();

                Modulo modulo = new Modulo(_nucleo, cmbModulo.Text);
                lblPotencia.Text = modulo.CapacidadW.ToString();

                int numeroModulos = 0;

                double potencia = (lblPotencia.Text != "")? double.Parse(lblPotencia.Text) : 0;

                if (txtCapacidad.Text != "" && potencia > 0)
                {
                    numeroModulos = Calculo.NumeroModulos(double.Parse(txtCapacidad.Text), potencia);
                    lblNoModulos.Text = numeroModulos.ToString();
                }

                decimal costoTotalMaterialElectrico = 0;

                //costoTotalMaterialElectrico = modulo.Precio * numeroModulos;

                //Calculo calculo; usar el _calculo de la clase

                if (cmbUsarMicroinversor.SelectedIndex == 0 && cmbMicroinversor.SelectedIndex > -1)
                {
                    //Microinversor
                    //MessageBox.Show("Indice selecionado en comboUsarMicro 0");
                    /*

                    Microinversor micro = new Microinversor(_nucleo, cmbMicroinversor.SelectedItem.ToString());

                    decimal moduloMicro = (modulo.CapacidadW > 410) ? 3 : 4;

                    decimal capacidadTotal = modulo.CapacidadW * numeroModulos;

                    decimal totalMicros = capacidadTotal / (modulo.CapacidadW * moduloMicro);

                    switch (cmbMicroinversor.SelectedItem.ToString())
                    {
                        case "APS YC600W":
                            costoTotalMaterialElectrico += 161.91 * totalMicros;
                            break;
                        case "APS 1200W":
                            costoTotalMaterialElectrico += 223.65 * totalMicros;
                            break;
                        case "APS 1500W":
                            costoTotalMaterialElectrico += 250.74 * totalMicros;
                            break;
                        default:
                            throw new Exception("Microninversor in
                    : " + cmbMicroinversor.SelectedItem.ToString());
                    }

                    costoTotalMaterialElectrico += micro.Precio * totalMicros;
                    */

                    _calculo = new Calculo(modulo.Descripcion, numeroModulos, (cmbUsarMicroinversor.SelectedIndex == 0),
                        cmbMicroinversor.SelectedItem.ToString(),
                        "", cmbTipoInstalacion.Text, cmbEncajonado.Text, cmbTemperatura.Text, cmbTransformador.Text);

                    costoTotalMaterialElectrico = _calculo.CostoElectrico;
                }

                lblMaterialElectrico.Text = costoTotalMaterialElectrico.ToString("#,0.00");

                decimal granTotal = costoTotalMaterialElectrico; // Sumar ferretero y estructural

                lblGranTotal.Text = granTotal.ToString("#,0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }

        #region métodos
        #endregion 

        #region eventos
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Instancias nucleo
                _nucleo = Generales.Nucleo;

                // Cargar modulos.

                cmbModulo.Items.Clear();
                //cmbModulo.Items.
                //cmbModulo.Items.Add("Seleccione una opción...");

                foreach (Modulo modulo in _nucleo.ObtenerModulos())
                {
                    cmbModulo.Items.Add(modulo.Descripcion);
                }

                cmbMicroinversor.Items.Clear();
                
                foreach (Microinversor micro in _nucleo.ObtenerMicroinversores())
                {
                    cmbMicroinversor.Items.Add(micro.Descripcion);
                }

                /*
                cmbMicroinversor.Items.Add("APS YC600W");
                cmbMicroinversor.Items.Add("APS 1200W");
                cmbMicroinversor.Items.Add("APS 1500W");
                */

                /*
                foreach (DataRow dr in _nucleo.Obtener("SELECT MICROINVERSOR FROM MICROINVESORES").Rows)
                {
                    cmbMicroinversor.Items.Add(dr[0].ToString());
                }
                */

                cmbTemperatura.Items.Clear();

                foreach (DataRow dr in Temperatura.Temperaturas(_nucleo).Rows)
                {
                    cmbTemperatura.Items.Add(dr[1].ToString());
                }

                /*
                cmbEncajonado.SelectedIndex = 0;
                if (cmbMicroinversor.Items.Count > 0)
                    cmbMicroinversor.SelectedIndex = 0;
                cmbModulo.SelectedIndex = 0;
                cmbTemperatura.SelectedIndex = 0;
                cmbTipoInstalacion.SelectedIndex = 0;
                cmbTransformador.SelectedIndex = 0;
                cmbUsarMicroinversor.SelectedIndex = 0;*/

                // Pre cargar selecciones último uso:
                txtNombreProyecto.Text = _nucleo.Parametro("txtNombreProyecto.Text");
                txtCapacidad.Text = _nucleo.Parametro("txtCapacidad.Text");
                txtUbicacion.Text = _nucleo.Parametro("txtUbicacion.Text");
                cmbModulo.Text = _nucleo.Parametro("cmbModulo.SelectedText");
                cmbUsarMicroinversor.Text = _nucleo.Parametro("cmbUsarMicroinversor.SelectedText");
                if (cmbUsarMicroinversor.Text == "Sí")
                    cmbUsarMicroinversor.SelectedIndex = 0;
                cmbMicroinversor.Text = _nucleo.Parametro("cmbMicroinversor.SelectedText");
                cmbTipoInstalacion.Text = _nucleo.Parametro("cmbTipoInstalacion.SelectedText");
                cmbEncajonado.Text = _nucleo.Parametro("cmbEncajonado.SelectedText");
                cmbTemperatura.Text = _nucleo.Parametro("cmbTemperatura.SelectedText");
                cmbTransformador.Text = _nucleo.Parametro("cmbTransformador.SelectedText");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void txtCapacidad_TextChanged(object sender, EventArgs e)
        {
            // Actualizar número de módulos y clasificación
            try
            {
                RecalcularCostos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RecalcularCostos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbUsarMicroinversor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularCostos();
        }

        private void cmbTipoInstalacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularCostos();
        }

        private void cmbEncajonado_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularCostos();
        }

        private void cmbTemperatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularCostos();
        }

        private void cmbTransformador_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularCostos();
        }

        private void btnMaterialElectrico_Click(object sender, EventArgs e)
        {
            try
            {
                RecalcularCostos();

                if (cmbMicroinversor.SelectedIndex < 0)
                    throw new Exception("No ha selecionado un microinversor.");

                frmMaterialElectrico frm = new frmMaterialElectrico(_nucleo, _calculo);
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }

        private void btnMaterialEstructural_Click(object sender, EventArgs e)
        {
            frmEstructura frm = new frmEstructura(_nucleo, _calculo);
            frm.ShowDialog();
            frm.Dispose();

        }

        private void btnMaterialFerretero_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGranTotal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desarrollo del detalle en proceso.");
        }

        private void cmbMicroinversor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalcularCostos();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _nucleo.ModificarParametro("txtNombreProyecto.Text", txtNombreProyecto.Text);

                if (!_nucleo.ModificarParametro("txtCapacidad.Text", txtCapacidad.Text))
                    MessageBox.Show("El parámatro Capacidad no fue guardado.");

                _nucleo.ModificarParametro("txtUbicacion.Text", txtUbicacion.Text);

                _nucleo.ModificarParametro("cmbModulo.SelectedText", cmbModulo.SelectedItem.ToString());
                _nucleo.ModificarParametro("cmbUsarMicroinversor.SelectedText", cmbUsarMicroinversor.SelectedItem.ToString());
                _nucleo.ModificarParametro("cmbMicroinversor.SelectedText", cmbMicroinversor.SelectedItem.ToString());
                _nucleo.ModificarParametro("cmbTipoInstalacion.SelectedText", cmbTipoInstalacion.SelectedItem.ToString());
                _nucleo.ModificarParametro("cmbEncajonado.SelectedText", cmbEncajonado.SelectedItem.ToString());
                _nucleo.ModificarParametro("cmbTemperatura.SelectedText", cmbTemperatura.SelectedItem.ToString());
                _nucleo.ModificarParametro("cmbTransformador.SelectedText", cmbTransformador.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
