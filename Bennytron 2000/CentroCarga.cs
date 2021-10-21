using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    class CentroCarga
    {
        Nucleo _nucleo;

        int _amperaje;
        string _itmcentrocarga;
        decimal _costo;

        public CentroCarga(Nucleo nucleo, int amperaje)
        {
            _nucleo = nucleo;
            _amperaje = amperaje;

            System.Data.DataTable dt = nucleo.Obtener("SELECT * FROM CENTROS_CARGA WHERE Amperaje = " + amperaje);

            if (dt.Rows.Count > 0)
            {
                _itmcentrocarga = dt.Rows[0]["Decripcion"].ToString();
                _costo = decimal.Parse(dt.Rows[0]["Costo_pesos"].ToString());

            }
        }

        #region Propiedades

        public int Amperaje
        {
            get
            {
                return _amperaje;
            }
        }

        public string ITM
        {
            get
            {
                return _itmcentrocarga;
            }
        }

        /// <summary>
        /// Costo en pesos
        /// </summary>
        public decimal CostoPesos
        {
            get
            {
                return _costo;
            }
        }
        #endregion
    }
}
