using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class RolComponenteQuery
    {
         public static string GetAllByRolSubModulo()
        {
            return @"
                SELECT *
                FROM `rol_componente` AS rc
                    INNER JOIN `componente` AS c ON rc.`componente_id` = c.`componente_id`
                WHERE rc.`rol_sub_modulo_id` = '@RolSubModuloId';
            ";
        }
    }
}