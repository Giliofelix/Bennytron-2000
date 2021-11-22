using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bennytron_2000
{
    class Proyecto
    {
        Nucleo _nucleo;
        string _nombre;

        int _capacidad;
        string _ubicacion;
        Modulo _modulo;
        Microinversor _microinversor;
        Inversor _inversor;
        bool _usarMicro;
        string _techoExistente;
        int _segmentos;
        string _anclaje;
        string _detallesAnclaje;
        int _gradosInclinacion;
        bool _existe;

        public Proyecto(Nucleo nucleo, string nombre)
        {
            _nucleo = nucleo;
            _nombre = nombre;

            DataTable dt = nucleo.Obtener("SELECT * FROM PROYECTOS WHERE NOMBRE = '" + _nombre + "'");

            if (dt.Rows.Count > 0)
            {
                _existe = true;

                _capacidad = int.Parse(dt.Rows[0]["CAPACIDAD"].ToString());
                _ubicacion = dt.Rows[0]["UBICACION"].ToString();
                _modulo = new Modulo(_nucleo, dt.Rows[0]["MODULO"].ToString());
                _usarMicro = (dt.Rows[0]["usar_micro"].ToString().ToLower() == "true");
                _microinversor = new Microinversor(_nucleo, dt.Rows[0]["microinversor"].ToString());
                _inversor = new Inversor(_nucleo, dt.Rows[0]["inversor"].ToString());
                _techoExistente = dt.Rows[0]["UBICACION"].ToString();
                _segmentos = int.Parse(dt.Rows[0]["Segmentos"].ToString());
                _anclaje = dt.Rows[0]["UBICACION"].ToString();
                _detallesAnclaje = dt.Rows[0]["UBICACION"].ToString();
                _gradosInclinacion = int.Parse(dt.Rows[0]["Grados_Inclinacion"].ToString());
            }
            else
            {
                _existe = false;
            }
        }

        public bool Guardar()
        {
            if (_nucleo.Conexion.State == ConnectionState.Closed)
                _nucleo.Conexion.Open();

            int r = 0;

            #region validaciones
            if (this.Nombre.Trim().Length == 0)
                throw new Exception("Falta el nombre del proyecto.");

            if (_capacidad <= 0)
                throw new Exception("Falta la capacidad de kilowatts del proyecto.");

            if (_segmentos <= 0)
                throw new Exception("El número de segmentos no es válido.");
            #endregion

            if (!_existe)
            {
                //Si no existe insertar
                string strSQL = "INSERT INTO PROYECTOS (NOMBRE, CAPACIDAD, UBICACION, MODULO, USAR_MICRO, MICROINVERSOR, INVERSOR, TECHO_EXISTENTE, SEGMENTOS, ANCLAJE, DETALLES_ANCLAJE, GRADOS_INCLINACION) "
                    + " VALUES("
                    + "'" + this.Nombre.Trim().Replace('\'', ' ') + "',"
                    + this.Capacidad + ","
                    + "'" + this.Ubicacion + "',"
                    + "'" + this.Modulo.Descripcion + "'," 
                    + (this.UsarMicro ? "1" : "0") + "," 
                    + "'" + this.Microinversor.Descripcion + "'," 
                    + "'" + this.Inversor.Descripcion + "', " 
                    + "'" + this.TechoExistente + "', " 
                    + this.Segmentos + "," 
                    + "'" + this.Anclaje + "', " 
                    + "'" + this.DetalleAnclaje + "', " 
                    + this.Inclinacion + ")"; 

                r = _nucleo.EjecutarSentencia(strSQL);
            }
            else
            {
                //si existe modificar
                string strSQL = "UPDATE PROYECTOS "
                    + " SET "
                    + " CAPACIDAD = " + this.Capacidad 
                    + " UBICACION = '" + this.Ubicacion + "'"
                    + " MODULO = '" + this.Modulo.Descripcion + "'"
                    + " USAR_MICRO = " + (this.UsarMicro ? "1" : "0")
                    + " MICROINVERSOR = '" + this.Microinversor.Descripcion + "'"
                    + " INVERSOR = '" + this.Inversor.Descripcion + "'"
                    + " TECHO_EXISTENTE = '" + this.TechoExistente + "'"
                    + " SEGMENTOS = " + this.Segmentos
                    + " ANCLAJE = '" + this.Anclaje + "'"
                    + " DETALLES_ANCLAJE = '" + this.DetalleAnclaje + "'"
                    + " GRADOS_INCLINACION = " + this.Inclinacion
                    + " WHERE NOMBRE = " + this.Nombre.Trim().Replace('\'', ' ') + ";";

                r = _nucleo.EjecutarSentencia(strSQL);
            }

            _nucleo.Conexion.Close();

            _existe = (r > 0);
            return _existe;
        }

        #region Propiedades

        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }

        public int Capacidad
        {
            get
            {
                return _capacidad;
            }
            set
            {
                _capacidad = value;
            }
        }

        public string Ubicacion
        {
            get
            {
                return _ubicacion;
            }
            set
            {
                _ubicacion = value;
            }
        }


        public Modulo Modulo
        {
            get
            {
                return _modulo;
            }
            set
            {
                _modulo = value;
            }
        }

        public bool UsarMicro
        {
            get
            {
                return _usarMicro;
            }
            set
            {
                _usarMicro = value;
            }
        }

        public Microinversor Microinversor
        {
            get
            {
                return _microinversor;
            }
            set
            {
                _microinversor = value;
            }
        }

        public Inversor Inversor
        {
            get
            {
                return _inversor;
            }
            set
            {
                _inversor = value;
            }
        }

        public string TechoExistente
        {
            get
            {
                return _techoExistente;
            }
            set
            {
                _techoExistente = value;
            }
        }

        public int Segmentos
        {
            get
            {
                return _segmentos;
            }
            set
            {
                _segmentos = value;
            }
        }

        public string Anclaje
        {
            get
            {
                return _anclaje;
            }
            set
            {
                _anclaje = value;
            }
        }

        public string DetalleAnclaje
        {
            get
            {
                return _detallesAnclaje;
            }
        }

        public int Inclinacion
        {
            get
            {
                return _gradosInclinacion;
            }
        }


        #endregion
    }
}
