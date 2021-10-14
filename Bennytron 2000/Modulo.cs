using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bennytron_2000
{
    public class Modulo
    {
        Nucleo _nucleo;
        string _descripcion;

        int _capacidadw;
        decimal _voltajevocv;
        decimal _corrienteiscA;
        decimal _eficiendia;
        Cable _cable;
        int _calibrecable;
        int _maxvcable;
        string _maxI;
        decimal _tempoperacionc;
        string _acdc;
        decimal _precio;

        public Modulo(Nucleo nucleo, string descripcion)
        {
            _nucleo = nucleo;
            _descripcion = descripcion;

            DataTable dt = nucleo.Obtener("SELECT * FROM MODULOS WHERE MODULO = '" + descripcion + "'");

            if (dt.Rows.Count > 0)
            {
                _descripcion = dt.Rows[0]["MODULO"].ToString();
                _capacidadw = int.Parse(dt.Rows[0]["CAPACIDAD"].ToString());
                _voltajevocv = decimal.Parse(dt.Rows[0]["voltaje"].ToString());
                _corrienteiscA = decimal.Parse(dt.Rows[0]["Corriente_Isc"].ToString());
                _eficiendia = decimal.Parse(dt.Rows[0]["Eficiencia"].ToString());
                _cable = new Cable(dt.Rows[0]["Cable"].ToString(), decimal.Parse(dt.Rows[0]["Calibre"].ToString()), int.Parse(dt.Rows[0]["Max_v"].ToString()));
                _calibrecable = int.Parse(dt.Rows[0]["Calibre"].ToString());
                _maxvcable = int.Parse(dt.Rows[0]["Max_v"].ToString());
                _maxI = dt.Rows[0]["Max_I"].ToString();
                _tempoperacionc = decimal.Parse(dt.Rows[0]["Temp_operacion"].ToString());
                _acdc = dt.Rows[0]["AC_DC"].ToString();
                _precio = decimal.Parse(dt.Rows[0]["Precio"].ToString());
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

        public int CapacidadW
        {
            get
            {
                return _capacidadw;
            }
        }

        public decimal VoltajeV
        {
            get
            {
                return _voltajevocv;
            }
        }

        public decimal CorrienteIscA
        {
            get
            {
                return _corrienteiscA;
            }
        }

        public decimal Eficiencia
        {
            get
            {
                return _eficiendia;
            }
        }

        public Cable Cable
        {
            get
            {
                return _cable;
            }
        }

        public string MaxI
        {
            get
            {
                return _maxI;
            }
        }

        public decimal TemperaturaOperacion
        {
            get
            {
                return _tempoperacionc;
            }
        }

        public string ACDC
        {
            get
            {
                return _acdc;
            }
        }

        public decimal Precio
        {
            get
            {
                return _precio;
            }
        }
        #endregion
    }
}