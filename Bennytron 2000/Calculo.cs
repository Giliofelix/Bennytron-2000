using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Data;

namespace Bennytron_2000
{
    public class Calculo
    {
        private Nucleo _nucleo;
        private Modulo _modulo;
        private int _numeroModulos;
        private Microinversor _microInversor;
        private Inversor _inversor;

        private bool _usarMicroinversor;

        private string _tipoInstalacion;
        private string _encajonado;
        private Temperatura _temperaturaAmbiente;
        private string _transformador;

        public Calculo(string modulo, int cantidadModulos, bool usarMicroinversor, 
            string microInversor, string inversor, string tipoInstalacion, string encajonado, string temperaturaAmbiente, string transformador)
        {
            _nucleo = Generales.Nucleo;

            // Obtener modulos

            _modulo = new Modulo(_nucleo, modulo);
            _numeroModulos = cantidadModulos;

            _usarMicroinversor = usarMicroinversor;
            if (_usarMicroinversor)
            {
                _microInversor = new Microinversor(_nucleo, microInversor);
                if (_microInversor == null)
                    throw new Exception("El microinversor es nulo.");
                _inversor = null;
            }
            else
            {
                _microInversor = null;
                _inversor = new Inversor(_nucleo, inversor);
                if (_inversor == null)
                    throw new Exception("El inversor es nulo.");
            }

            _tipoInstalacion = tipoInstalacion;
            _encajonado = encajonado;
            _temperaturaAmbiente = new Temperatura(_nucleo, temperaturaAmbiente);
            _transformador = transformador;

            // TODO: continuar...
        }

        #region Estáticos
        public static string Clasificacion(int capacidadKw)
        {
            int capacidadw = capacidadKw;

            int C173 = 30, C174 = 250, C175 = 600;

            string A173 = "Casa y microempresa",
                A174 = "Pequeña empresa",
                A175 = "Mediana empresa",
                A176 = "Corporativo";

            if (capacidadw <= C173)
                return A173;
            else
            {
                if (capacidadw <= C174)
                    return A174;
                else
                    if (capacidadw <= C175)
                    return A175;
            }

            return A176;
        }

        public static int Potencia(string modulo)
        {
            switch (modulo)
            {
                case "JASOLAR 445":
                    return 445;
                case "JASOLAR 450":
                    return 450;
                case "JASOLAR 540":
                    return 540;
            }

            return 0;
        }

        public static int NumeroModulos(double capacidadKw, double portenciaw)
        {
            return (int)Math.Round(capacidadKw / (portenciaw / 1000), 0);
        }

        public static decimal PrecioProteccionITM(string itm)
        {
            return 0;
        }
        #endregion

        #region propiedades
        public Modulo Modulo
        {
            get
            {
                return _modulo;
            }
        }

        public bool UsarMicroinversor
        {
            get
            {
                return _usarMicroinversor;
            }
        }

        public Microinversor Microinversor
        {
            get
            {
                return _microInversor;
            }
        }

        public Inversor Inversor
        {
            get
            {
                return _inversor;
            }
        }

        public int NumeroDeModulos
        {
            get
            {
                return _numeroModulos;
            }
        }

        public string TipoInstalacion
        {
            get
            {
                return _tipoInstalacion;
            }
        }

        public string Encajonado
        {
            get
            {
                return _encajonado;
            }
        }

        public Temperatura TemperaturaAmbiente
        {
            get
            {
                return _temperaturaAmbiente;
            }
        }
        #endregion
    }
}