using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    public class Temperatura
    {
        Nucleo _nucleo;
        string _rango;
        decimal _factorCorreccion;

        public Temperatura(Nucleo nucleo, string rango)
        {
            _nucleo = nucleo;
            _rango = rango;

            System.Data.DataTable dt = _nucleo.Obtener("SELECT * FROM TEMPERATURAS WHERE TEMPERATURA_AMBIENTE = '" + _rango + "'");

            if (dt.Rows.Count > 0)
            {
                _factorCorreccion = decimal.Parse(dt.Rows[0]["FACTOR_CORRECCION"].ToString());
            }
        }

        public string TemperaturaAmbiente
        {
            get
            {
                return _rango;
            }
        }

        public decimal FactorCorreccion
        {
            get
            {
                return _factorCorreccion;
            }
        }

        public static System.Data.DataTable Temperaturas(Nucleo nucleo)
        {
            System.Data.DataTable dt = nucleo.Obtener("SELECT * FROM Temperaturas; ");

            return dt;
        }
    }
}
