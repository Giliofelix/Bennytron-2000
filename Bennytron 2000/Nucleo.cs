using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Bennytron_2000
{
    public class Nucleo
    {
        OleDbConnection _conexion;

        public Nucleo(string cadenaconeccion)
        {
            _conexion = new OleDbConnection(cadenaconeccion);
        }

        public DataTable Obtener(string query)
        {
            if (_conexion.State == ConnectionState.Closed)
                _conexion.Open();

            OleDbCommand cmd = new OleDbCommand(query, _conexion);
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);

            //_conexion.Close();

            return dt;
        }

        public object EjecutarEscalar(string query)
        {
            if (_conexion.State == ConnectionState.Closed)
                _conexion.Open();

            OleDbCommand cmd = new OleDbCommand(query, _conexion);
            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteScalar();
        }

        public int EjecutarSentencia(string nonquery)
        {
            if (_conexion.State == ConnectionState.Closed)
                _conexion.Open();

            OleDbCommand cmd = new OleDbCommand(nonquery, _conexion);
            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteNonQuery();
        }

        public Modulo[] ObtenerModulos()
        {
            DataTable dtModulos = this.Obtener("SELECT * FROM MODULOS;");

            Modulo[] Modulos = new Modulo[dtModulos.Rows.Count];

            int i = 0;
            foreach (DataRow dr in dtModulos.Rows)
            {
                Modulos[i] = new Modulo(this, dr["MODULO"].ToString());
                i++;
            }

            return Modulos;
        }
        public Microinversor[] ObtenerMicroinversores()
        {
            DataTable dtMicroinversor = this.Obtener("SELECT * FROM Microinversores;");

            Microinversor[] Microinversores = new Microinversor[dtMicroinversor.Rows.Count];

            int i = 0;
            foreach (DataRow dr in dtMicroinversor.Rows)
            {
                Microinversores[i] = new Microinversor(this, dr["Microinversor"].ToString());
                i++;
            }

            return Microinversores;
        }

        /// <summary>
        /// Obtiene el valor del parámetro en la tabla de parámetros
        /// </summary>
        /// <param name="clave">Identificador</param>
        /// <returns>Valor actual del parámetro</returns>
        public string Parametro(string clave)
        {
            DataTable dt = this.Obtener("SELECT VALOR FROM PARAMETROS WHERE CLAVE LIKE '" + clave + "'");

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["VALOR"].ToString();
            else
                throw new Exception("No existe el parámetro con identificador: '" + clave + "'");
        }

        public bool ModificarParametro(string clave, string valor)
        {
            return this.EjecutarSentencia("UPDATE PARAMETROS SET VALOR = '" + valor + "' WHERE CLAVE = '" + clave + "'") > 0;
        }

        #region Propiedades

        public OleDbConnection Conexion
        {
            get
            {
                return _conexion;
            }
        }

        #endregion
    }
}
