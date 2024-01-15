using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class AdministracionNivelSql
    {
        public static string ObtenerTodo()
        {
            return @"
                SELECT 
                susuarioadd,
                dtfechaadd,
                susuariomod,
                dtfechamod,
                lnivel_id,
                ssigla,
                snombre,
                ddesde,
                dhasta,
                dbono,
                dbonomembresia 
                FROM 
                administracionnivel
            ";
        }
        public static string ObtenerTodoSelect()
        {
            return @"
                SELECT 
                susuarioadd,
                dtfechaadd,
                susuariomod,
                dtfechamod,
                lnivel_id,
                ssigla,
                CONCAT(ssigla,' - ',snombre) as snombre,
                ddesde,
                dhasta,
                dbono,
                dbonomembresia 
                FROM 
                administracionnivel
            ";
        }
    }
}