using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    public class Cable
    {
        string _descipcion;
        protected decimal _calibre;
        protected decimal _maxv;

        public Cable (string descripcion, decimal calibre, decimal maxv)
        {
            _descipcion = descripcion;
            _calibre = calibre;
            _maxv = maxv;
        }

        public string Descripcion
        {
            get
            {
                return _descipcion;
            }
        }

        public decimal Calibre
        {
            get
            {
                return _calibre;
            }
        }

        public decimal MaxV
        {
            get
            {
                return _maxv;
            }
        }
    }
}
