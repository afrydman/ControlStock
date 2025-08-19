using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessComponents
{
    public static class reporte
    {


        public static DataTable GetDataTable(string sp, List<SqlParameter> ParametersList)
        {

            return Persistence.Conexion.GetDataTable(sp, ParametersList);

        }

    }
}
