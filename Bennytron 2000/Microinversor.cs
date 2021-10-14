using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bennytron_2000
{
    public class Microinversor
    {
        Nucleo _nucleo;
        string _descripcion;

        int _capacidadw;
        decimal _voltajeentradaw;
        decimal _corrienteEntrada;
        decimal _corrienteEntradaTotal;
        decimal _potenciaSalida;
        decimal _corrienteSalida;
        int _unidadMaxXRamal;
        decimal _precio;
        decimal _voltajeNominal;
        string _detalles;

        public Microinversor(Nucleo nucleo, string descripcion)
        {
            _nucleo = nucleo;
            _descripcion = descripcion;

            DataTable dt = nucleo.Obtener("SELECT * FROM Microinversores WHERE MICROINVERSOR = '" + descripcion + "'");

            if (dt.Rows.Count > 0)
            {
                _descripcion = dt.Rows[0]["Microinversor"].ToString();

                _capacidadw = int.Parse(dt.Rows[0]["Capacidad_max_modulo"].ToString());
                _voltajeentradaw = decimal.Parse(dt.Rows[0]["Voltaje_entrada"].ToString());
                _corrienteEntrada = decimal.Parse(dt.Rows[0]["Corriente_entrada"].ToString());
                _corrienteEntradaTotal = decimal.Parse(dt.Rows[0]["Corriente_entrada_total"].ToString());
                _potenciaSalida = decimal.Parse(dt.Rows[0]["potencia_salida"].ToString());
                _corrienteSalida = decimal.Parse(dt.Rows[0]["corriente_salida"].ToString());
                _unidadMaxXRamal = int.Parse(dt.Rows[0]["unidad_max_x_ramal"].ToString());
                _precio = decimal.Parse(dt.Rows[0]["Precio_dlls"].ToString());
                _voltajeNominal = decimal.Parse(dt.Rows[0]["voltaje_nominal"].ToString());
                _detalles = dt.Rows[0]["detalles"].ToString();

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

        public int CapacidadMaxModulo
        {
            get
            {
                return _capacidadw;
            }
        }

        public decimal VoltajeEntrada
        {
            get
            {
                return _voltajeentradaw;
            }
        }

        public decimal CorrienteEntrada
        {
            get
            {
                return _corrienteEntrada;
            }
        }

        public decimal CorrienteEntradaTotal
        {
            get
            {
                return _corrienteEntradaTotal;
            }
        }

        /// <summary>
        /// Capacidad (W) campo (potencia_salida)
        /// </summary>
        public decimal PotenciaSalida
        {
            get
            {
                return _potenciaSalida;
            }
        }

        /// <summary>
        /// I salida (A) campo corriente_salida
        /// </summary>
        public decimal CorrienteSalida
        {
            get
            {
                return _corrienteSalida;
            }
        }

        public decimal UnidadMaxXRamal
        {
            get
            {
                return _unidadMaxXRamal;
            }
        }

        public decimal Precio
        {
            get
            {
                return _precio;
            }
        }

        /// <summary>
        /// Voltaje salida (V)  campo voltaje_nominal
        /// </summary>
        public decimal VoltajeNominal
        {
            get
            {
                return _voltajeNominal;
            }
        }

        public string Detalles
        {
            get
            {
                return _detalles;
            }
        }
        #endregion
    }
}
