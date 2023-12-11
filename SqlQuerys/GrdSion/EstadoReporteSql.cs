using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class EstadoReporteSql
    {
        public static string ObtenerTodo()
        {
            return @"
                SELECT * FROM estado_reporte
            ";
        }
    }
}