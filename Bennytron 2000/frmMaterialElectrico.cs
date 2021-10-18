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
    public partial class frmMaterialElectrico : Form
    {
        Nucleo _nucleo;
        Calculo _calculo;

        public frmMaterialElectrico(Nucleo nucleo, Calculo calculo)
        {
            InitializeComponent();

            _nucleo = nucleo;
            _calculo = calculo;
        }
        // ...

        private void frmMaterialElectrico_Load(object sender, EventArgs e)
        {
            try
            {
                // Configurar encabezados de los grids...

                // 
                //Encabezados:
                //Capacidad (w) Voltaje (v) Corriente (A)   Eficiencia (%)  Costo unitario ($)
                dgvModulosEt1.DataSource = _nucleo.Obtener("SELECT Modulos.Modulo, "
                    + "Modulos.Capacidad as [Capacidad (w)], "
                    + "Modulos.Voltaje as [Voltaje (v)], "
                    + "Modulos.Eficiencia as [Eficiencia (%)], "
                    + "Modulos.Precio as [Cost unit($)] "
                    + "FROM Modulos WHERE Modulos.Modulo = '" + _calculo.Modulo.Descripcion + "'");

                dgvModulosEt1.AllowUserToAddRows = false;

                //
                if (_calculo.UsarMicroinversor)
                {
                    dgvsSubEt1.Visible = false;

                    lblB13.Text = "Microinversor " + _calculo.Microinversor.Descripcion;

                    dgvEt2.DataSource = _nucleo.Obtener("SELECT potencia_salida as [Capacidad(W)], corriente_entrada_total as [I ent max (A)],  "
                        + " corriente_salida as [I salida (A)], voltaje_nominal as [Volt salida (V)], precio_dlls as [Costo unit ($)] "
                        + " FROM Microinversores "
                        + " WHERE Microinversor = '" + _calculo.Microinversor.Descripcion + "'");

                    dgvEt2.AllowUserToAddRows = false;
                }
                else
                {
                    //No usar microinversores
                }
                //

                DataTable dtEt2 = new DataTable("CableadoYProteccionesEt2");
                DataTable dtEt3 = new DataTable("CableadoYProteccionesEt3");

                #region define las columnas
                dtEt2.Columns.Add("", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Max5 (v)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Max I (A)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cantidad (pzas)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cantidad (metros)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Costo unit ($)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("", System.Type.GetType("System.String"));

                dtEt3.Columns.Add("Max5 (v)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Max I (A)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cantidad (pzas)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cantidad (metros)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Costo unit ($)", System.Type.GetType("System.String"));
                #endregion

                lblB13.Text = _calculo.UsarMicroinversor ? "Microinversor" : "Inversor";

                // Cable correcto


                #region TRANSFERENCIA DE ENERGÍA
                decimal capacidadTotal = (_calculo.NumeroDeModulos * _calculo.Modulo.CapacidadW);
                lblN8.Text = capacidadTotal.ToString();
                lblO8.Text = Math.Floor(capacidadTotal * (decimal)1.1).ToString();
                decimal modMicro = ((_calculo.Modulo.CapacidadW > 410) ? 3 : 4); // 4 > 540 no viable
                lblN10.Text = modMicro.ToString();
                lblN12.Text = _calculo.Modulo.VoltajeV.ToString();
                lblO12.Text = _calculo.Modulo.CorrienteIscA.ToString();

                int totalMicros = 0;
                if (_calculo.Modulo.CapacidadW != 0)
                    totalMicros = (int)Math.Ceiling(capacidadTotal / (_calculo.Modulo.CapacidadW * modMicro));

                lblN18.Text = totalMicros.ToString();
                lblO18.Text = "3"; //PREGUNTAR porque 3?

                string nombreCableCorrecto = "Cable boost wire APSystem CA 12\""; // TODO: Obtener cable de cables _AD donde Correcto = true

                CableAC cableCorrecto = new CableAC(_nucleo, nombreCableCorrecto);

                lblN20.Text = nombreCableCorrecto;
                lblO20.Text = cableCorrecto.MaxV.ToString();

                decimal ProtecionITM = 0; 

                if (_calculo.UsarMicroinversor) 
                    ProtecionITM = ((decimal)_calculo.Microinversor.CorrienteSalida * (decimal)3);

                lblN22.Text = ProtecionITM.ToString();
                decimal protecionITMnecesarias = (ProtecionITM * (decimal)1.25);
                lblO22.Text = protecionITMnecesarias.ToString();

                int[] amperajes = new int[21] { 15, 20, 25, 30, 40, 50, 60, 70, 100, 125, 150, 175, 200, 225, 250, 300, 400, 500, 600, 800, 1000 };

                int protecionITMrequerida = 0;

                for (int i = 0; i < 21; i++)
                {
                    if (protecionITMnecesarias < amperajes[i])
                    {
                        protecionITMrequerida = amperajes[i];
                        break; 
                    }
                }

                if (protecionITMrequerida != 0)
                    lblN24.Text = protecionITMrequerida.ToString();
                else
                    lblN24.Text = "";

                int cantidadITMnecesarias = (int)Math.Ceiling((_calculo.NumeroDeModulos / modMicro) / int.Parse(lblO18.Text));

                lblO24.Text = cantidadITMnecesarias.ToString();

                int polosProteccionITM = 0;
                if (_calculo.UsarMicroinversor)
                    polosProteccionITM = (_calculo.Microinversor.VoltajeNominal == 440) ? 3 : ((_calculo.Microinversor.VoltajeNominal == 220) ? 2 : 1);

                lblN26.Text = polosProteccionITM.ToString();

                decimal corrienteTotalSistema = protecionITMnecesarias * (totalMicros / 3);

                lblN28.Text = corrienteTotalSistema.ToString();

                if (_calculo.UsarMicroinversor)
                    lblO26.Text = _calculo.TipoInstalacion + " " + _calculo.Microinversor.VoltajeNominal.ToString() + "v";

                //Fin transferencia de energía
                #endregion

                #region Programación Tablero metálico

                #endregion

                #region Llenado de grit de cableado y protección Et2
                //Descripción, Max V, Max I, Cantidad pz, cantidad mts, Precio.
                //CableAC cableCorrecto = new CableAC(_nucleo, true);

                DataRow drEt2 = dtEt2.NewRow();
                drEt2[0] = cableCorrecto.Descripcion;
                drEt2[1] = cableCorrecto.MaxV.ToString(); // 3 campo, H22 (nombre del cable), en bd A339 I340 // cableCorrecto.MaxV
                drEt2[2] = cableCorrecto.CorrienteMax75.ToString(); // 5 campo
                drEt2[3] = "0";
                drEt2[4] = "0";
                drEt2[5] = cableCorrecto.Costo.ToString("$ 0.00"); // J339 BD y criterio
                dtEt2.Rows.Add(drEt2);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = lblN21.Text; // "Protección ITM (A)"; 
                drEt2[1] = "";
                drEt2[2] = lblN24.Text;
                drEt2[3] = lblO24.Text;
                drEt2[4] = "";
                drEt2[5] = Calculo.PrecioProteccionITM(lblN21.Text);
                dtEt2.Rows.Add(drEt2);

                Tablero tableroMetalico = new Tablero(_nucleo, lblN30.Text);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = lblN30.Text;  // "Tablero metalico 225"; 
                drEt2[1] = "0";
                drEt2[2] = tableroMetalico.CorrienteMaxima;
                drEt2[3] = lblO30.Text;
                drEt2[4] = "";
                drEt2[5] = tableroMetalico.Costo;
                dtEt2.Rows.Add(drEt2); drEt2 = dtEt2.NewRow();

                drEt2[0] = "ITM principal " + lblO38.Text;  //"ITM principal 15"; 
                drEt2[1] = "";
                drEt2[2] = lblO38.Text;
                drEt2[3] = lblO30.Text;
                drEt2[4] = "0";
                drEt2[5] = "0";
                drEt2[5] = Calculo.PrecioProteccionITM("ITM principal " + lblO38.Text);

                dgvEt2.DataSource = dtEt2;
                #endregion

                #region Calculo costos

                decimal costoModulos = _calculo.NumeroDeModulos * (decimal)_calculo.Modulo.Precio;

                decimal costoMicroinversores
                if (_calculo.UsarMicroinversor)
                    decimal costoMicroinversores = totalMicros * (decimal)_calculo.Microinversor.Precio;

                // TODO: metros de calbe correcto por precio del cable correcto

                // TODO: protecciones IMT cantidad por precio

                // TODO: tablero cantidad por precio

                // ITM principal cantidad por precio

                decimal costoTotal = costoModulos + costoMicroinversores;

                lblP45.Text = costoTotal.ToString("0.00");

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }
    }
}
