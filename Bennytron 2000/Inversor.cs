using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bennytron_2000
{
    public class Inversor
    {
        Nucleo _nucleo;
        string _descripcion;

        int _capacidad;
        decimal _inputDC;
        decimal _voltajeMaxSistema;
        decimal _corrienteEntradaMaxima;
        string _inputCurrentPerTracker;
        decimal _mppTracker;
        decimal _stringPerMppInput;
        string _inputCurrtentPerStringInput;
        decimal _precio;
        decimal _outputAC;
        decimal _ACVoltajeNominal;
        decimal _corrienteSalidaMaxima;
        decimal _outputphases;
        decimal _lineConnection;
        decimal _poderAparente;
        string _configTrifasica;

        public Inversor(Nucleo nucleo, string descripcion)
        {
            _nucleo = nucleo;
            _descripcion = descripcion;

            DataTable dt = nucleo.Obtener("SELECT * FROM INVERSORES WHERE INVERSOR = '" + descripcion + "'");

            if (dt.Rows.Count > 0)
            {
                _descripcion = dt.Rows[0]["Inversor"].ToString();

                _capacidad = int.Parse(dt.Rows[0]["Capacidad"].ToString());

                _inputDC = decimal.Parse(dt.Rows[0]["Input_DC"].ToString());
                _voltajeMaxSistema = decimal.Parse(dt.Rows[0]["Voltaje_max_sistema"].ToString());
                _corrienteEntradaMaxima = decimal.Parse(dt.Rows[0]["Corriente_entrada_maxima"].ToString());

                _inputCurrentPerTracker = dt.Rows[0]["Input_current_per_tracker"].ToString();

                _mppTracker = decimal.Parse(dt.Rows[0]["Mpp_tracker"].ToString());
                _stringPerMppInput = decimal.Parse(dt.Rows[0]["String_per_MPP_input"].ToString());

                _inputCurrtentPerStringInput = dt.Rows[0]["Input_current_per_string_input"].ToString();

                _precio = decimal.Parse(dt.Rows[0]["Precio"].ToString());
                _outputAC = decimal.Parse(dt.Rows[0]["Output_AC"].ToString());
                _ACVoltajeNominal = decimal.Parse(dt.Rows[0]["AC_voltaje_nominal"].ToString());
                _corrienteSalidaMaxima = decimal.Parse(dt.Rows[0]["Corriente_salida_maxima"].ToString());
                _outputphases = decimal.Parse(dt.Rows[0]["Output_phases"].ToString());
                _lineConnection = decimal.Parse(dt.Rows[0]["Line_connections"].ToString());
                _poderAparente = decimal.Parse(dt.Rows[0]["Poder_aparente"].ToString());

                _configTrifasica = dt.Rows[0]["Config_trifasica"].ToString();

            }
        }

        #region Propiedades

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
        }

        public int Capacidad
        {
            get
            {
                return _capacidad;
            }
        }

        public decimal InputDC
        {
            get
            {
                return _inputDC;
            }
        }

        public decimal VoltajeMaxSistema
        {
            get
            {
                return _voltajeMaxSistema;
            }
        }

        public decimal CorrienteEntradaMaxima
        {
            get
            {
                return _corrienteEntradaMaxima;
            }
        }

        public string InputCurrentPerTracker
        {
            get
            {
                return _inputCurrentPerTracker;
            }
        }

        public decimal MppTracker
        {
            get
            {
                return _mppTracker;
            }
        }

        public decimal StringPerMPPInput
        {
            get
            {
                return _stringPerMppInput;
            }
        }

        public string InputCurrentPerStringInput
        {
            get
            {
                return _inputCurrtentPerStringInput;
            }
        }

        public decimal Precio
        {
            get
            {
                return _precio;
            }

        }

        public decimal OutputAC
        {
            get
            {
                return _outputAC;
            }
        }

        public decimal ACVoltajeNominal
        {
            get
            {
                return _ACVoltajeNominal;
            }
        }

        public decimal CorrienteSalidaMaxima
        {
            get
            {
                return _corrienteSalidaMaxima;
            }
        }

        public decimal OutputPhases
        {
            get
            {
                return _outputphases;
            }
        }

        public decimal LineConnections
        {
            get
            {
                return _lineConnection;
            }
        }

        public decimal PoderAparente
        {
            get
            {
                return _poderAparente;
            }
        }

        public string ConfiguracionTrifasica
        {
            get
            {
                return _configTrifasica;
            }
        }
        #endregion
    }
}
