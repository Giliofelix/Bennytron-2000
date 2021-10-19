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
                dtEt2.Columns.Add("Descripción", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Max5 (v)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Max I (A)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cant (pzas)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cant (mts)", System.Type.GetType("System.String"));
                dtEt2.Columns.Add("Cost unit ($)", System.Type.GetType("System.String"));

                dtEt3.Columns.Add("Descripción", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Max5 (v)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Max I (A)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cant (pzas)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cant (mts)", System.Type.GetType("System.String"));
                dtEt3.Columns.Add("Cost unit ($)", System.Type.GetType("System.String"));
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

                // Max. Micro/bus
                decimal maxMicroBus = 3; //PREGUNTAR porque 3? 
                lblO18.Text = maxMicroBus.ToString();


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

                decimal corrienteTotalSistema = protecionITMnecesarias * (totalMicros / maxMicroBus);

                lblN28.Text = corrienteTotalSistema.ToString();

                if (_calculo.UsarMicroinversor)
                    lblO26.Text = _calculo.TipoInstalacion + " " + _calculo.Microinversor.VoltajeNominal.ToString() + "v";

                //Fin transferencia de energía
                #endregion

                #region Programación Tablero metálico

                lblN29.Text = _calculo.Encajonado;

                // Poner valor correcto en lblN30.Text
                // según corrienteTotalSistema
                // buscar tablero metalico en tabla Tableros donde corriente maxima > corrienteTotalSistema
                Tablero tablero = Tablero.BuscarPorCorriente(_nucleo, corrienteTotalSistema);

                lblN30.Text = tablero.Descripcion;

                lblN35.Text = "Espacios a utilizar / " + _calculo.Encajonado;
                lblN37.Text = "Corriente ITM prin. + Prot. (A) / " + _calculo.Encajonado;
                lblO29.Text = "Cantidad " + _calculo.Encajonado + "(#)";
                lblO31.Text = "Corriente / " + _calculo.Encajonado + "(A)+Prot";
                lblN31.Text = "Corriente máx. (A)";

                //lblN28.Text = corrienteTotalSistema 

                int cantidadEncajonado = (int)((Math.Ceiling(corrienteTotalSistema / tablero.CorrienteMaxima) == 0) ? 0 : Math.Ceiling(corrienteTotalSistema / tablero.CorrienteMaxima));
                lblO30.Text = cantidadEncajonado.ToString();

                lblN32.Text = tablero.CorrienteMaxima.ToString();
                lblO32.Text = Math.Ceiling(corrienteTotalSistema / cantidadEncajonado).ToString();
                lblN34.Text = ((polosProteccionITM == 2) ? cantidadITMnecesarias * 2 : cantidadITMnecesarias).ToString(); // =SI(N26=2,O24*N26,O24)

                int espaciosUtilizar = (int)Math.Round((decimal)cantidadITMnecesarias / (decimal)cantidadEncajonado);

                if (espaciosUtilizar == 0)
                    espaciosUtilizar = 2;

                if (espaciosUtilizar % 2 != 0)
                    espaciosUtilizar++;

                lblN36.Text = espaciosUtilizar.ToString();

                //Corriente ITM prin. + Prot. (A) / Gabinete metálico
                //= SI(O26 = "Monofásico 220v", (REDONDEAR.MAS(((((I20 * O18 * N36)) / (220 * 1)) * 1.25), 0)), (REDONDEAR.MAS(((((I20 * O18 * N36)) / (220 * 1 * RAIZ(3))) * 1.25), 0)))

                decimal corrienteITMprinprotencajonado = (lblO26.Text == "Monofásico 220v") ?
                    Math.Ceiling(((_calculo.Microinversor.CapacidadMaxModulo * maxMicroBus * espaciosUtilizar)) / ((decimal)220 * (decimal)1) * (decimal)1.25) :
                    Math.Ceiling(((_calculo.Microinversor.CapacidadMaxModulo * maxMicroBus * espaciosUtilizar)) / ((decimal)220 * (decimal)1 * (decimal)Math.Sqrt((double)3)) * (decimal)1.25);
                lblN38.Text = corrienteITMprinprotencajonado.ToString();

                //=(N36*O30)-O24
                lblO36.Text = ((corrienteITMprinprotencajonado * cantidadEncajonado) - cantidadITMnecesarias).ToString();

                // ITM principal a utilizar
                int ITMprincipalautilizar = 0;

                for (int i = 0; i < 21; i++)
                {
                    if (corrienteITMprinprotencajonado < amperajes[i])
                    {
                        ITMprincipalautilizar = amperajes[i];
                        break;
                    }
                }

                lblO38.Text = ITMprincipalautilizar.ToString();

                // Cap. nominal ITM x Factor Temp. (A)
                /*
                 =SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E341, 'BD y criterio'!E341,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E342, 'BD y criterio'!E342,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E343, 'BD y criterio'!E343,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E344, 'BD y criterio'!E344,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E345, 'BD y criterio'!E345,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E346, 'BD y criterio'!E346,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E347, 'BD y criterio'!E347,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E348, 'BD y criterio'!E348,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E349, 'BD y criterio'!E349,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E350, 'BD y criterio'!E350,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E351, 'BD y criterio'!E351,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E352, 'BD y criterio'!E352,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E353, 'BD y criterio'!E353,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E354, 'BD y criterio'!E354,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E355, 'BD y criterio'!E355,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E356, 'BD y criterio'!E356,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E357, 'BD y criterio'!E357,
                  SI( O38/ (BUSCARV(C17,'BD y criterio'!A362:B371,2,FALSO)) <= 'BD y criterio'!E358, 'BD y criterio'!E358))))))))))))))))))
                 */

                decimal buscar = ITMprincipalautilizar / _calculo.TemperaturaAmbiente.FactorCorreccion;

                // De E341 hacia abajo
                DataTable dtCablesAC = _nucleo.Obtener("SELECT CABLE, CORRIENTE_MAX_75 FROM CABLES_AC WHERE NOT CORRECTO ORDER BY CORRIENTE_MAX_75 ASC;  ");


                //Cap. nominal ITM
                int capNominalITM = 0;

                string cableAC = "";

                foreach(DataRow dr in dtCablesAC.Rows)
                {
                    if (buscar < int.Parse(dr[1].ToString()))
                    {
                        capNominalITM = int.Parse(dr[1].ToString());
                        cableAC = dr[0].ToString();
                        break;
                    }
                }

                lblN40.Text = capNominalITM.ToString();

                // CableAC
                lblN42.Text = cableAC;

                // Corriente Max. cable AC (A)
                lblO40.Text = "";

                // Calibre (AWG/kcmil)
                lblO42.Text = "";
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
                drEt2[5] = cableCorrecto.Costo.ToString("N"); // J339 BD y criterio
                dtEt2.Rows.Add(drEt2);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = lblN21.Text; // "Protección ITM (A)"; 
                drEt2[1] = "";
                drEt2[2] = lblN24.Text;
                drEt2[3] = lblO24.Text;
                drEt2[4] = "";
                drEt2[5] = Calculo.PrecioProteccionITM(lblN21.Text).ToString("N");
                dtEt2.Rows.Add(drEt2);


                //Tablero tableroMetalico = new Tablero(_nucleo, lblN30.Text);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = tablero.Descripcion;  // N30 "Tablero metalico 225"; 
                drEt2[1] = "0";
                drEt2[2] = tablero.CorrienteMaxima;
                drEt2[3] = lblO30.Text; // ? llenar region Programación Tablero metálico primero
                drEt2[4] = "";
                drEt2[5] = tablero.Costo.ToString("N");
                dtEt2.Rows.Add(drEt2);

                drEt2 = dtEt2.NewRow();
                drEt2[0] = "ITM principal " + lblO38.Text;  //"ITM principal 15"; 
                drEt2[1] = "";
                drEt2[2] = lblO38.Text;
                drEt2[3] = lblO30.Text;
                drEt2[4] = "0";
                drEt2[5] = "0";
                drEt2[5] = Calculo.PrecioProteccionITM("ITM principal " + lblO38.Text).ToString("N");
                dtEt2.Rows.Add(drEt2);

                dgvSubEt2.DataSource = dtEt2;
                dgvSubEt2.AllowUserToAddRows = false;
                #endregion

                #region Calculo costos

                decimal costoModulos = _calculo.NumeroDeModulos * _calculo.Modulo.Precio;

                decimal costoMicroinversores = 0;
                if (_calculo.UsarMicroinversor)
                    costoMicroinversores = totalMicros * (decimal)_calculo.Microinversor.Precio;

                // TODO: metros de calbe correcto por precio del cable correcto

                // TODO: protecciones IMT cantidad por precio

                // TODO: tablero cantidad por precio

                // ITM principal cantidad por precio

                // costoElectrico = costoModulos + costoMicroinversores;

                decimal costoTotal = costoModulos + costoMicroinversores;

                // osto total = electrico + feretero + estructural

                lblP8.Text = costoModulos.ToString("N");
                lblP19.Text = costoMicroinversores.ToString("N");
                lblP25.Text = (0).ToString("N");
                lblP45.Text = costoTotal.ToString("N");

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }
    }
}
