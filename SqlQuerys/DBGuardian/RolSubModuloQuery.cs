using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class RolSubModuloQuery
    {
        public static string GetAllByRolSubModuloId()
        {
            return @"
                SELECT
                    sm.*,
                    rsm.`rol_modulo_id`,
                    rsm.`rol_sub_modulo_id`
                FROM `rol_sub_modulo` AS rsm
                    INNER JOIN `sub_modulo` AS sm ON rsm.`sub_modulo_id` = sm.`sub_modulo_id`
                WHERE rsm.`rol_modulo_id` = @rol_modulo_id;
            ";
        }
    }
}