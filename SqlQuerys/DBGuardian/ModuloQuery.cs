using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class ModuloQuery
    {
        public static string GetAll()
        {
            return @"
                SELECT 
                    `modulo_id`,
                    `url`,
                    `modulo_nombre`,
                    `descripcion`,
                    `imagen_referencia`
                FROM 
                    `modulo`
                WHERE
                    modulo_id='@modulo_id';
            ";
        }
        public static string GetOne()
        {
            return @"
                SELECT 
                    `modulo_id`,
                    `url`,
                    `modulo_nombre`,
                    `descripcion`,
                    `imagen_referencia`
                FROM 
                    `modulo`
                WHERE
                    modulo_id='@modulo_id'
                LIMIT 1;
            ";
        }
    }
}