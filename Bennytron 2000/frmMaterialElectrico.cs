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
        

        private void frmMaterialElectrico_Load(object sender, EventArgs e)
        {
            try
            {
                // Configurar encabezados de los grids...

                // 
                //Encabezados:
                //Capacidad (w) Voltaje (v) Corriente (A)   Eficiencia (%)  Costo unitario ($)
                dgvModulosEt1.DataSource = _nucleo.Obtener("SELECT Modulos.Modulo as [Módulo], "
                    + "Modulos.Capacidad as [Capacidad (w)], "
                    + "Modulos.Voltaje as [Voltaje (v)], "
                    + "Modulos.Eficiencia as [Eficiencia (%)], "
                    + " Modulos.Precio as [Cost unit($)] "
                    + "FROM Modulos WHERE Modulos.Modulo = '" + _calculo.Modulo.Descripcion + "'");

                dgvModulosEt1.AllowUserToAddRows = false;

                //
                if (_calculo.UsarMicroinversor)
                {
                    dgvsSubEt1.Visible = false;

                    lblB13.Text = "Microinversor " + _calculo.Microinversor.Descripcion;

                    dgvEt2.DataSource = _nucleo.Obtener("SELECT Microinversor, potencia_salida as [Capacidad(W)], corriente_entrada_total as [I ent max (A)],  "
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
                lblB13.Text = _calculo.UsarMicroinversor ? "Microinversor" : "Inversor";

                // Cable correcto



                #region Llenado de grit de cableado y protección Et2

                DataTable dtEt2 = new DataTable("CableadoYProteccionesEt2");
                DataTable dtEt3 = new DataTable("CableadoYProteccionesEt3");

                #region define las columnas
                dtEt2.Columns.Add("Descripción", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Máx (v)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Máx I (A)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cant (pzas)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cant (mts)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cost unit ($)", System.Type.GetType("System.String"));

                dtEt3.Columns.Add("Descripción", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Máx (v)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Máx I (A)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cant (pzas)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cant (mts)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cost unit ($)", System.Type.GetType("System.String"));
                #endregion

                //0,            1,      2,      3,          4,             5,
                //Descripción, Max V, Max I, Cantidad pz, cantidad mts, Precio.
                //CableAC cableCorrecto = new CableAC(_nucleo, true);

                DataRow drEt2 = dtEt2.NewRow();
                drEt2[0] = _calculo.CableCorrecto.Descripcion;
                drEt2[1] = _calculo.CableCorrecto.MaxV.ToString(); // 3 campo, H22 (nombre del cable), en bd A339 I340 // cableCorrecto.MaxV // I23 
                drEt2[2] = _calculo.CableCorrecto.CorrienteMax75.ToString(); // 5 campo
                drEt2[3] = "";
                drEt2[4] = "";
                drEt2[5] = _calculo.CableCorrecto.Costo.ToString("N"); // J339 BD y criterio
                dtEt2.Rows.Add(drEt2);

                // TODO: protecciones ITM cantidad por precio
                

                drEt2 = dtEt2.NewRow();
                drEt2[0] = "Protección ITM " + _calculo.ProtecionITM.ToString(); // "Protección ITM (A)"; 
                drEt2[1] = "";
                drEt2[2] = _calculo.ProtecionITMrequerida.ToString(); // lblN24.Text; // Proteccion ITM requerida (A)
                drEt2[3] = _calculo.CantidadITMNecesarias.ToString(); // lblO24.Text;
                drEt2[4] = "";
                drEt2[5] = _calculo.ImporteProtecionesITM.ToString("N"); // 
                dtEt2.Rows.Add(drEt2);


                //Tablero tableroMetalico = new Tablero(_nucleo, lblN30.Text);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = _calculo.Tablero.Descripcion;  // N30 "Tablero metalico 225"; 
                drEt2[1] = "0";
                drEt2[2] = _calculo.Tablero.CorrienteMaxima;
                drEt2[3] = _calculo.CantidadEncajonado.ToString(); // lblO30.Text; // llenar region Programación Tablero metálico primero
                drEt2[4] = "";
                drEt2[5] = _calculo.Tablero.Costo.ToString("N");
                dtEt2.Rows.Add(drEt2);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = "ITM principal " + _calculo.ITMPrincipalUtilizar; // lblO38.Text;  //"ITM principal 15"; 
                drEt2[1] = "";
                drEt2[2] = _calculo.ITMPrincipalUtilizar; //  lblO38.Text;
                drEt2[3] = _calculo.CantidadEncajonado.ToString(); // lblO30.Text;
                drEt2[4] = "0";
                drEt2[5] = "0";
                drEt2[5] = Calculo.PrecioProteccionITM(_nucleo, _calculo.ITMPrincipalUtilizar).ToString("N");
                dtEt2.Rows.Add(drEt2);

                dgvSubEt2.DataSource = dtEt2;
                dgvSubEt2.Columns[0].Width = 110;
                dgvSubEt2.AllowUserToAddRows = false;
                #endregion

                #region Calculo costos
                // costo total = electrico + feretero + estructural0
                lblP8.Text = _calculo.CostoModulos.ToString("N");
                lblP19.Text = _calculo.CostoMicroinversores.ToString("N");
                lblP25.Text = (_calculo.ImporteProtecionesITM + _calculo.ImporteTablero).ToString("N");
                lblP45.Text = _calculo.CostoElectrico.ToString("N");
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }

        private void btnTransferenciaEnergia_Click(object sender, EventArgs e)
        {
            frmTransferenciaEnergia frm = new frmTransferenciaEnergia(_calculo);
            frm.ShowDialog();
        }
    }
}
