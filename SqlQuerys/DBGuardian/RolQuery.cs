using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class RolQuery
    {
        public static string GetAll()
        {
            return @"
                SELECT 
                    `rol_id`,
                    `nombre_rol` 
                FROM 
                    `rol`;
            ";
        }
        public static string GetOne()
        {
            return @"
                SELECT 
                    `rol_id`,
                    `nombre_rol` 
                FROM 
                    `rol`;
                WHERE
                    rol_id='@rol_id'
                LIMIT 1;
            ";
        }
    }
}