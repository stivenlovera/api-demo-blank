using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class SubModuloQuery
    {
        public static string GetAll()
        {
            return @"
                    SELECT `sub_modulo_id`,
                        `url`,
                        `modulo_sub_nombre`,
                        `descripcion`,
                        `imagen_referencia`,
                        `modulo_id` 
                    FROM 
                        `sub_modulo`;
                ;
            ";
        }
        public static string GetOne()
        {
            return @"
                SELECT 
                    `sub_modulo_id`,
                    `url`,
                    `modulo_sub_nombre`,
                    `descripcion`,
                    `imagen_referencia`,
                    `modulo_id` 
                FROM 
                    `sub_modulo`;
                WHERE
                    sub_modulo_id='@sub_modulo_id'
                LIMIT 1;
            ";
        }
    }
}