using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    public class Tablero
    {
        Nucleo _nucleo;

        string _tablero;
        int _circuitos;
        decimal _costo;
        int _corrienteMaxima;
        string _medidas;
        decimal _costoPesos;

        public Tablero(Nucleo nucleo, string tablero)
        {
            _nucleo = nucleo;
            _tablero = tablero;

            System.Data.DataTable dt = nucleo.Obtener("SELECT * FROM TABLEROS WHERE TABLERO_METALICO = '" + tablero + "'");

            if (dt.Rows.Count > 0)
            {
                _circuitos = int.Parse(dt.Rows[0]["Circuitos"].ToString());
                _costo = decimal.Parse(dt.Rows[0]["Costo"].ToString());
                _corrienteMaxima = int.Parse(dt.Rows[0]["Corriente_maxima"].ToString());
                _medidas = dt.Rows[0]["Medidas"].ToString();
                _costoPesos = decimal.Parse(dt.Rows[0]["Costo_pesos"].ToString());

            }
        }

        #region Propiedades
        public string Descripcion
        {
            get
            {
                return _tablero;
            }
        }

        public int Circuitos
        {
            get
            {
                return _circuitos;
            }
        }

        public decimal Costo
        {
            get
            {
                return _costo;
            }
        }

        public int CorrienteMaxima
        {
            get
            {
                return _corrienteMaxima;
            }
        }

        public string Medidas
        {
            get
            {
                return _medidas;
            }
        }

        public decimal CostoPesos
        {
            get
            {
                return _costoPesos;
            }
        }
        #endregion

        public static Tablero BuscarPorCorriente(Nucleo nucleo, decimal corriente)
        {
            System.Data.DataTable dt = nucleo.Obtener("SELECT Tablero_metalico FROM Tableros " 
                + " WHERE CORRIENTE_MINIMA >= " + corriente
                + " AND " + corriente + " < CORRIENTE_MAXIMA;");

            if (dt.Rows.Count < 1)
                throw new Exception("Corriente " + corriente + " excede la capacidad máxima de los tableros metálicos capturados.");

            return new Tablero(nucleo, dt.Rows[0][0].ToString());
        }
    }
}
