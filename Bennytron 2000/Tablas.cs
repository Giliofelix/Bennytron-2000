using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bennytron_2000
{
    class Tablas
    {
        public DataTable TiposProyecto()
        {
            DataTable dt = new DataTable("TiposProyectos");

            DataColumn dcTipo = new DataColumn();
            dcTipo.ColumnName = "TipoProyecto";
            dcTipo.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcTipo);


            DataColumn dcNumModulos = new DataColumn();
            dcNumModulos.ColumnName = "NumeroModulos";
            dcNumModulos.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcNumModulos);

            DataColumn dcCapacidadkw = new DataColumn();
            dcCapacidadkw.ColumnName = "Capacidadkw";
            dcCapacidadkw.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcCapacidadkw);

            DataColumn dcCapacidadw = new DataColumn();
            dcCapacidadw.ColumnName = "Capacidadw";
            dcCapacidadw.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dcCapacidadw);

            DataColumn[] pk = new DataColumn[1];
            pk[0] = dcTipo;
            dt.PrimaryKey = pk;

            return dt;
        }
    }
}
