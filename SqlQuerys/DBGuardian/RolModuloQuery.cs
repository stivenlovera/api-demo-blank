using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class RolModuloQuery
    {
        public static string GetAll()
        {
            return @"
                SELECT *
                FROM `rol_modulo` AS rm
                    INNER JOIN `modulo` AS m ON rm.`modulo_id` = m.`modulo_id`;
            ";
        }
        public static string GetAllByRol()
        {
            return @"
                SELECT m.*,
                rm.rol_modulo_id,
                rm.rol_id
                FROM `rol_modulo` AS rm
                    INNER JOIN `modulo` AS m ON rm.`modulo_id` = m.`modulo_id`
                WHERE rm.`rol_id` = 1;
            ";
        }
        public static string GetAllByRolIds(List<int> rolesId)
        {
            return $@"
                SELECT m.*,
                rm.rol_modulo_id,
                rm.rol_id
                FROM `rol_modulo` AS rm
                    INNER JOIN `modulo` AS m ON rm.`modulo_id` = m.`modulo_id`
                WHERE rm.`rol_id` = {string.Join(", ", rolesId)};
            ";
        }
    }
}