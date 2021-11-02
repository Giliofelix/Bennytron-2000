using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    class CriterioBusqueda
    {
        public string Campo, Mostrar, Tipo;

        public CriterioBusqueda(string campo, string mostrar, string tipo)
        {
            this.Campo = campo;
            this.Mostrar = mostrar;
            this.Tipo = tipo;
        }
    }
}
