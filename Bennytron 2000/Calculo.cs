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
        private CableAC _cableACCorrecto;

        string _protecionITMetiqueta;
        decimal _protecionITMvalor;
        decimal _protecionITMnecesarias;
        int _protecionITMrequerida;
        int _cantidadITMnecesarias;

        decimal _corrienteTotalSistema;
        Tablero _tablero;
        int _totalMicros;
        decimal _capacidadTotal;
        decimal _modMicro;
        decimal _maxMicroBus;
        int _ITMprincipalautilizar;

        int _cantidadEncajonado;
        int _espaciosUtilizar;
        int _polosProteccionITM;
        decimal _corrienteITMprinprotencajonado;
        decimal _importeTablero;
        decimal _costoModulos;
        decimal _costoMicroinversores;
        decimal _costoInversores;
        decimal _costoElectrico;
        decimal _importeProtecionesITM;

        public Calculo(string modulo, int cantidadModulos, bool usarMicroinversor, 
            string microInversor, string inversor, string tipoInstalacion, string encajonado, string temperaturaAmbiente, string transformador)
        {
            _nucleo = Generales.Nucleo;

            // Obtener modulos

            _modulo = new Modulo(_nucleo, modulo);
            _numeroModulos = cantidadModulos;

            _usarMicroinversor = usarMicroinversor;
            if (this.UsarMicroinversor)
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

            _cableACCorrecto = new CableAC(_nucleo, true);

            if (this.UsarMicroinversor)
                _protecionITMvalor = ((decimal)_microInversor.CorrienteSalida * (decimal)3);
            else
                _protecionITMvalor = ((decimal)_inversor.Capacidad * (decimal)3);

            _protecionITMetiqueta = "Protecion ITM (A)";

            _protecionITMnecesarias = (_protecionITMvalor * (decimal)1.25);
            _protecionITMrequerida = 0;

            int[] amperajes = new int[21] { 15, 20, 25, 30, 40, 50, 60, 70, 100, 125, 150, 175, 200, 225, 250, 300, 400, 500, 600, 800, 1000 };


            for (int i = 0; i < 21; i++)
            {
                if (_protecionITMnecesarias < amperajes[i])
                {
                    _protecionITMrequerida = amperajes[i];
                    break;
                }
            }

            _modMicro = ((Modulo.CapacidadW > 410) ? 3 : 4); // 4 > 540 no viable
            _maxMicroBus = 3; //PREGUNTAR porque 3? 
            _cantidadITMnecesarias = (int)Math.Ceiling((_numeroModulos / _modMicro) / _maxMicroBus);

            _capacidadTotal = (NumeroDeModulos * _modulo.CapacidadW);

            _totalMicros = 0;
            if (_modulo.CapacidadW != 0)
                _totalMicros = (int)Math.Ceiling(_capacidadTotal / (_modulo.CapacidadW * _modMicro));

            _corrienteTotalSistema = _protecionITMnecesarias * (_totalMicros / _maxMicroBus);
            _tablero = Tablero.BuscarPorCorriente(_nucleo, _corrienteTotalSistema);

            _cantidadEncajonado = (int)((Math.Ceiling(_corrienteTotalSistema / _tablero.CorrienteMaxima) == 0) ? 1 : Math.Ceiling(_corrienteTotalSistema / _tablero.CorrienteMaxima));

            _espaciosUtilizar = (int)Math.Round((decimal)_cantidadITMnecesarias / (decimal)_cantidadEncajonado);

            if (_espaciosUtilizar == 0)
                _espaciosUtilizar = 2;

            if (_espaciosUtilizar % 2 != 0)
                _espaciosUtilizar++;

            if (usarMicroinversor)
            _corrienteITMprinprotencajonado = (TipoInstalacion == "Monofásico" && Microinversor.VoltajeNominal == 220) ?
                    Math.Ceiling(((Microinversor.CapacidadMaxModulo * _maxMicroBus * _espaciosUtilizar)) / ((decimal)220 * (decimal)1) * (decimal)1.25) :
                    Math.Ceiling(((Microinversor.CapacidadMaxModulo * _maxMicroBus * _espaciosUtilizar)) / ((decimal)220 * (decimal)1 * (decimal)Math.Sqrt((double)3)) * (decimal)1.25);
            else
                _corrienteITMprinprotencajonado = (TipoInstalacion == "Monofásico") ?
                    Math.Ceiling(((_inversor.Capacidad * _maxMicroBus * _espaciosUtilizar)) / ((decimal)220 * (decimal)1) * (decimal)1.25) :
                    Math.Ceiling(((_inversor.Capacidad * _maxMicroBus * _espaciosUtilizar)) / ((decimal)220 * (decimal)1 * (decimal)Math.Sqrt((double)3)) * (decimal)1.25);

            _ITMprincipalautilizar = 0;

            for (int i = 0; i < amperajes.Length; i++)
            {
                if (_corrienteITMprinprotencajonado < amperajes[i])
                {
                    _ITMprincipalautilizar = amperajes[i];
                    break;
                }
            }

            if (this.UsarMicroinversor)
                _polosProteccionITM = (Microinversor.VoltajeNominal == 440) ? 3 : ((Microinversor.VoltajeNominal == 220) ? 2 : 1);
            else
                _polosProteccionITM = (Inversor.ACVoltajeNominal == 440) ? 3 : ((Inversor.ACVoltajeNominal == 220) ? 2 : 1);

            _importeTablero = _cantidadEncajonado * _tablero.Costo;

            _costoModulos = _numeroModulos * _modulo.Precio;

            if (this.UsarMicroinversor)
                _costoMicroinversores = _totalMicros * (decimal)_microInversor.Precio;
            else
                _costoInversores = _inversor.Precio;

            // metros de calbe correcto por precio del cable correcto tampoco está en bennytron de excel

            // ITM principal cantidad por precio
            _importeProtecionesITM = _cantidadITMnecesarias * Calculo.PrecioProteccionITM(_nucleo, _protecionITMrequerida);

            _costoElectrico = _costoModulos + _costoMicroinversores + _importeProtecionesITM + _importeTablero;

        }

        #region Estáticos
        public static string Clasificacion(decimal capacidadKw)
        {
            decimal capacidadw = capacidadKw;

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

        public static decimal PrecioProteccionITM(Nucleo nucleo, int amperajeitmprincipal)
        {
            object result = nucleo.EjecutarEscalar("SELECT Costo FROM ITM_Principales "
                + " WHERE AMPERAJE = " + amperajeitmprincipal + ";");

            if (result != null)
                return decimal.Parse(result.ToString());
            else
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

        public CableAC CableCorrecto
        {
            get
            {
                return _cableACCorrecto;
            }
            set
            {
                _cableACCorrecto = value;
            }
        }

        public string ProtecionITM
        {
            get
            {
                return _protecionITMetiqueta;
            }
        }

        public decimal ProtecionITMValor
        {
            get
            {
                return _protecionITMvalor;
            }
        }

        public decimal ProtecionITMnecesarias
        {
            get
            {
                return _protecionITMnecesarias;
            }
        }

        public decimal CorrienteTotalSistema
        {
            get
            {
                return _corrienteTotalSistema;
            }
        }

        public Tablero Tablero
        {
            get
            {
                return _tablero;
            }
        }

        public int TotalMicros
        {
            get
            {
                return _totalMicros;
            }
        }

        public decimal CapacidadTotal
        {
            get
            {
                return _capacidadTotal;
            }
        }

        public decimal ModMicro
        {
            get
            {
                return _modMicro;
            }
        }

        public decimal MaxMicroBus
        {
            get
            {
                return _maxMicroBus;
            }
        }

        public int ProtecionITMrequerida
        {
            get
            {
                return _protecionITMrequerida;
            }
        }

        public int ITMPrincipalUtilizar
        {
            get
            {
                return _ITMprincipalautilizar;
            }
        }

        public int CantidadEncajonado
        {
            get
            {
                return _cantidadEncajonado;
            }
        }

        public int CantidadITMNecesarias
        {
            get
            {
                return _cantidadITMnecesarias;
            }
        }

        public int EspaciosUtilizar
        {
            get
            {
                return _espaciosUtilizar;
            }
        }

        public decimal CorrienteITMPrinProtEncajonado
        {
            get
            {
                return _corrienteITMprinprotencajonado;
            }
        }

        public decimal ImporteTablero
        {
            get
            {
                return _importeTablero;
            }
        }

        public decimal CostoModulos
        {
            get
            {
                return _costoModulos;
            }
        }
        
        public decimal CostoMicroinversores
        {
            get
            {
                return _costoMicroinversores;
            }
        }

        public decimal CostoElectrico
        {
            get
            {
                return _costoElectrico;
            }
        }

        public decimal ImporteProtecionesITM
        {
            get
            {
                return _importeProtecionesITM;
            }
        }

        public int PolosProteccionITM
        {
            get
            {
                return _polosProteccionITM;
            }
        }
        #endregion
    }
}