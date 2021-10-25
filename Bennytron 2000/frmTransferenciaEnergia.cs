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
    public partial class frmTransferenciaEnergia : Form
    {
        Calculo _calculo;
        Nucleo _nucleo;

        public frmTransferenciaEnergia(Calculo calculo)
        {
            _calculo = calculo;

            InitializeComponent();
        }

        private void frmTransferenciaEnergia_Load(object sender, EventArgs e)
        {
            try
            {
                _nucleo = Generales.Nucleo;

                #region TRANSFERENCIA DE ENERGÍA
                
                lblN8.Text = _calculo.CapacidadTotal.ToString();
                lblO8.Text = Math.Floor(_calculo.CapacidadTotal / (decimal)1.1).ToString();
                
                lblN10.Text = _calculo.ModMicro.ToString();
                lblN12.Text = _calculo.Modulo.VoltajeV.ToString();
                lblO12.Text = _calculo.Modulo.CorrienteIscA.ToString();
                lblN18.Text = _calculo.TotalMicros.ToString();

                // Max. Micro/bus
                lblO18.Text = _calculo.MaxMicroBus.ToString();
                lblN20.Text = _calculo.CableCorrecto.Descripcion;
                lblO20.Text = _calculo.CableCorrecto.CorrienteMax75.ToString();

                lblN22.Text = _calculo.ProtecionITM;
                lblO22.Text = _calculo.ProtecionITMnecesarias.ToString();

                if (_calculo.ProtecionITMrequerida != 0)
                    lblN24.Text = _calculo.ProtecionITMrequerida.ToString();
                else
                    lblN24.Text = "";

                lblO24.Text = _calculo.CantidadITMNecesarias.ToString();
                lblN26.Text = _calculo.PolosProteccionITM.ToString();

                lblN28.Text = _calculo.CorrienteTotalSistema.ToString("#,###.000");

                if (_calculo.UsarMicroinversor)
                    lblO26.Text = _calculo.TipoInstalacion + " " + _calculo.Microinversor.VoltajeNominal.ToString() + "v"; //

                //Fin transferencia de energía
                #endregion

                #region Programación Tablero metálico

                lblN29.Text = _calculo.Encajonado;

                // Poner valor correcto en lblN30.Text
                // según corrienteTotalSistema
                // buscar tablero metalico en tabla Tableros donde corriente maxima > corrienteTotalSistema
                

                lblN30.Text = _calculo.Tablero.Descripcion;

                lblN35.Text = "Espacios a utilizar / " + _calculo.Encajonado;
                lblN37.Text = "Corriente ITM prin. + Prot. (A) / " + _calculo.Encajonado;
                lblO29.Text = "Cantidad " + _calculo.Encajonado + "(#)";
                lblO31.Text = "Corriente / " + _calculo.Encajonado + "(A)+Prot";
                lblN31.Text = "Corriente máx. (A)";

                lblO30.Text = _calculo.CantidadEncajonado.ToString();

                lblN32.Text = _calculo.Tablero.CorrienteMaxima.ToString();
                lblO32.Text = Math.Ceiling(_calculo.CorrienteTotalSistema / _calculo.CantidadEncajonado).ToString();
                lblN34.Text = ((_calculo.PolosProteccionITM == 2) ? _calculo.CantidadITMNecesarias * 2 : _calculo.CantidadITMNecesarias).ToString(); // =SI(N26=2,O24*N26,O24)

                lblN36.Text = _calculo.EspaciosUtilizar.ToString();

                //Corriente ITM prin. + Prot. (A) / Gabinete metálico
                //= SI(O26 = "Monofásico 220v", (REDONDEAR.MAS(((((I20 * O18 * N36)) / (220 * 1)) * 1.25), 0)), (REDONDEAR.MAS(((((I20 * O18 * N36)) / (220 * 1 * RAIZ(3))) * 1.25), 0)))

                lblN38.Text = _calculo.CorrienteITMPrinProtEncajonado.ToString();

                //=(N36*O30)-O24
                lblO36.Text = ((_calculo.CorrienteITMPrinProtEncajonado * _calculo.CantidadEncajonado) - _calculo.CantidadITMNecesarias).ToString();

                // ITM principal a utilizar
                

                lblO38.Text = _calculo.ITMPrincipalUtilizar.ToString();

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

                decimal buscar = _calculo.ITMPrincipalUtilizar / _calculo.TemperaturaAmbiente.FactorCorreccion;

                // De E341 hacia abajo
                DataTable dtCablesAC = _nucleo.Obtener("SELECT CABLE, CORRIENTE_MAX_75 FROM CABLES_AC WHERE NOT CORRECTO ORDER BY CORRIENTE_MAX_75 ASC;  ");


                //Cap. nominal ITM
                int capNominalITM = 0;

                string nombreCableAC = "";

                foreach (DataRow dr in dtCablesAC.Rows)
                {
                    if (buscar < int.Parse(dr[1].ToString()))
                    {
                        capNominalITM = int.Parse(dr[1].ToString());
                        nombreCableAC = dr[0].ToString();

                        break;
                    }
                }

                CableAC cableAC = new CableAC(_nucleo, nombreCableAC);

                lblN40.Text = capNominalITM.ToString();

                // CableAC
                lblN42.Text = cableAC.Descripcion;

                // Corriente Max. cable AC (A)
                lblO40.Text = cableAC.CorrienteMax75.ToString();

                // Calibre (AWG/kcmil)
                lblO42.Text = cableAC.Calibre.ToString();

                // Corriente ITM centro de carga + Prot.(A)
                // =SI(O26="Monofásico 220v",(REDONDEAR.MAS(((((I20*N18))/(220*1))*1.25),0)),(REDONDEAR.MAS(((((I20*N18))/(220*1*RAIZ(3)))*1.25),0)))
                /*decimal corrienteITMprinprotencajonado = (lblO26.Text == "Monofásico 220v") ?
                    Math.Ceiling(((_calculo.Microinversor.CapacidadMaxModulo * maxMicroBus * espaciosUtilizar)) / ((decimal)220 * (decimal)1) * (decimal)1.25) :
                    Math.Ceiling(((_calculo.Microinversor.CapacidadMaxModulo * maxMicroBus * espaciosUtilizar)) / ((decimal)220 * (decimal)1 * (decimal)Math.Sqrt((double)3)) * (decimal)1.25);*/
                decimal corrienteITMcentrocargaprot = (lblO26.Text == "Monofásico 220v") ?
                    Math.Ceiling(((_calculo.Microinversor.PotenciaSalida * _calculo.MaxMicroBus)) / ((decimal)220 * (decimal)1) * (decimal)1.25) :
                    Math.Ceiling(((_calculo.Microinversor.PotenciaSalida * _calculo.MaxMicroBus)) / ((decimal)220 * (decimal)1 * (decimal)Math.Sqrt((double)3)) * (decimal)1.25);

                lblN44.Text = corrienteITMcentrocargaprot.ToString();

                // ITM centro de carga a utilizar 
                /*
                 =SI(N44<='BD y criterio'!I303,'BD y criterio'!I303,
                  SI(N44<='BD y criterio'!I304,'BD y criterio'!I304,
                  SI(N44<='BD y criterio'!I305,'BD y criterio'!I305,
                  SI(N44<='BD y criterio'!I306,'BD y criterio'!I306,
                  SI(N44<='BD y criterio'!I307,'BD y criterio'!I307,
                  SI(N44<='BD y criterio'!I308,'BD y criterio'!I308,
                  SI(N44<='BD y criterio'!I309,'BD y criterio'!I309,
                  SI(N44<='BD y criterio'!I310,'BD y criterio'!I310,
                  SI(N44<='BD y criterio'!I311,'BD y criterio'!I311,
                  SI(N44<='BD y criterio'!I312,'BD y criterio'!I312,
                  SI(N44<='BD y criterio'!I313,'BD y criterio'!I313,
                  SI(N44<='BD y criterio'!I314,'BD y criterio'!I314,
                  SI(N44<='BD y criterio'!I315,'BD y criterio'!I315,
                  SI(N44<='BD y criterio'!I316,'BD y criterio'!I316,
                  SI(N44<='BD y criterio'!I317,'BD y criterio'!I317,
                  SI(N44<='BD y criterio'!I318,'BD y criterio'!I318,
                  SI(N44<='BD y criterio'!I319,'BD y criterio'!I319,
                  SI(N44<='BD y criterio'!I320,'BD y criterio'!I320,
                  SI(N44<='BD y criterio'!I321,'BD y criterio'!I321,
                  SI(N44<='BD y criterio'!I322,'BD y criterio'!I322,
                  SI(N44<='BD y criterio'!I323,'BD y criterio'!I323,'BD y criterio'!I323)))))))))))))))))))))
                 */

                CentroCarga ITMcentrocargautilizar = null;

                int amperajeCentroCarga = 0;

                DataTable dtCemtrosCarga = _nucleo.Obtener("SELECT AMPERAJE FROM CENTROS_CARGA ORDER BY AMPERAJE ASC;  ");

                foreach (DataRow dr in dtCemtrosCarga.Rows)
                {
                    if (corrienteITMcentrocargaprot < int.Parse(dr[0].ToString()))
                    {
                        amperajeCentroCarga = int.Parse(dr[0].ToString());
                        break;
                    }
                }

                ITMcentrocargautilizar = new CentroCarga(_nucleo, amperajeCentroCarga);

                lblO44.Text = "";
                if (ITMcentrocargautilizar != null)
                    lblO44.Text = ITMcentrocargautilizar.Amperaje.ToString();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ((Generales.MostrarStackTrace) ? ex.StackTrace : ""));
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
