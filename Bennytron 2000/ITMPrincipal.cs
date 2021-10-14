using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    class ITMPrincipal
    {
        Nucleo _nucleo;

        int _amperaje;
        string _itmprincipal;
        decimal _costo; 

        public ITMPrincipal(Nucleo nucleo, int amperaje)
        {
            _nucleo = nucleo;
            _amperaje = amperaje;

            System.Data.DataTable dt = nucleo.Obtener("SELECT * FROM ITM_Principales WHERE Amperaje = " + amperaje);

            if (dt.Rows.Count > 0)
            {
                _itmprincipal = dt.Rows[0]["itm_principales"].ToString();
                _costo = decimal.Parse(dt.Rows[0]["Costo"].ToString());

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
                return _itmprincipal;
            }
        }

        /// <summary>
        /// Costo en pesos
        /// </summary>
        public decimal Costo
        {
            get
            {
                return _costo;
            }
        }
        #endregion
    }
}
