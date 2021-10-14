using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    public class CableAC : Cable
    {
        Nucleo _nucleo;
        string _descipcion;

        private int _corrienteMax60;
        private int _corrienteMax75;
        private int _corrienteMax90;

        private string _tipo;
        private decimal _areamm2;
        private decimal _corriente_prot75;

        private decimal _costo;

        private bool _correcto;

        public CableAC(Nucleo nucleo, bool cableCorrecto) : base("", 0, 0)
        {
            _nucleo = nucleo;
                _correcto = cableCorrecto;

            if (!cableCorrecto)
            {
                return;
            }

            System.Data.DataTable dt = _nucleo.Obtener("SELECT Cable FROM CABLES_AC WHERE CORRECTO");

            _descipcion = dt.Rows[0]["Cable"].ToString();
            _calibre = decimal.Parse(dt.Rows[0]["Calibre"].ToString());
            _maxv = decimal.Parse(dt.Rows[0]["Max_v"].ToString());
            _corrienteMax60 = int.Parse(dt.Rows[0]["Corriente_max_60"].ToString());
            _corrienteMax75 = int.Parse(dt.Rows[0]["Corriente_max_75"].ToString());
            _corrienteMax90 = int.Parse(dt.Rows[0]["Corriente_max_90"].ToString());
            _tipo = dt.Rows[0]["Tipo"].ToString();
            _areamm2 = decimal.Parse(dt.Rows[0]["Area"].ToString());
            _corriente_prot75 = decimal.Parse(dt.Rows[0]["Corriente_prot_75"].ToString());
            _costo = decimal.Parse(dt.Rows[0]["Costo"].ToString());

            _correcto = true;
        }

        public CableAC(Nucleo nucleo, string descripcion)
            :base(descripcion, 0, 0)
        {
            _nucleo = nucleo;
            _descipcion = descripcion;

            System.Data.DataTable dt = _nucleo.Obtener("SELECT * FROM CABLES_AC WHERE CABLE = '" + descripcion + "'");

            _calibre = decimal.Parse(dt.Rows[0]["Calibre"].ToString());
            _maxv = decimal.Parse(dt.Rows[0]["Max_v"].ToString());

            _corrienteMax60 = int.Parse(dt.Rows[0]["Corriente_max_60"].ToString());
            _corrienteMax75 = int.Parse(dt.Rows[0]["Corriente_max_75"].ToString());
            _corrienteMax90 = int.Parse(dt.Rows[0]["Corriente_max_90"].ToString());
            _tipo = dt.Rows[0]["Tipo"].ToString();
            _areamm2 = decimal.Parse(dt.Rows[0]["Area"].ToString());
            _corriente_prot75 = decimal.Parse(dt.Rows[0]["Corriente_prot_75"].ToString());
            _costo = decimal.Parse(dt.Rows[0]["Costo"].ToString());

            _correcto = (dt.Rows[0]["Correcto"].ToString().ToLower() == "true");
        }

        public int CorrienteMax60
        {
            get
            {
                return _corrienteMax60;
            }
        }

        public int CorrienteMax75
        {
            get
            {
                return _corrienteMax75;
            }
        }
        public int CorrienteMax90
        {
            get
            {
                return _corrienteMax90;
            }
        }

        public string Tipo
        {
            get
            {
                return _tipo;
            }
        }

        public decimal Areamm2
        {
            get
            {
                return _areamm2;
            }
        }

        public decimal CorreienteProt75
        {
            get
            {
                return _corriente_prot75;
            }
        }

        public decimal Costo
        {
            get
            {
                return _costo;
            }
        }

        public bool Correcto
        {
            get
            {
                return _correcto;
            }
        }
    }
}
