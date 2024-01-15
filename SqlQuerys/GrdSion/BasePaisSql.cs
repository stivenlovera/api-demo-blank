using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class BasePaisSql
    {
        public static string ObtenerPaises()
        {
            return @"
                SELECT lPais_id,sNombre,sUsuarioAdd,dtFechaAdd,sUsuarioMod,dtFechaMod FROM basepais
            ";
        }
    }
}