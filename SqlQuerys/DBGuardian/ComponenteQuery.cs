using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class ComponenteQuery
    {
        public static string GetAll()
        {
            return @"
                    SELECT 
                        `componente_id`,
                        `sub_modulo_id`,
                        `componente_nombre`,
                        `descripcion`,
                        `imagen_referencia` 
                    FROM 
                        `componente`;
            ";
        }
        public static string GetOne()
        {
            return @"
                SELECT 
                    `componente_id`,
                    `sub_modulo_id`,
                    `componente_nombre`,
                    `descripcion`,
                    `imagen_referencia` 
                FROM 
                    `componente`;
                WHERE
                    componente_id='@componente_id'
                LIMIT 1;
            ";
        }
    }
}