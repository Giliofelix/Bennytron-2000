using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bennytron_2000
{
    class Generales
    {
        public static bool MostrarStackTrace = (Get("MostrarStackTrace") == "true");

        public static string Get(string key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings.Get(key);
        }

        private static Nucleo _nucleo;

        public static Nucleo Nucleo
        {
            get
            {
                if (_nucleo == null)
                    _nucleo = new Nucleo("Provider=" + Generales.Get("ProviderAccess") + ";Data Source=" + Generales.Get("archivo"));

                return _nucleo;
            }
            set
            {
                _nucleo = value;
            }
        }
    }
}
