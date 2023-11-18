using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class TokenActivoQuery
    {
        public static string GetOneByFuncionario()
        {
            return @"
                SELECT 
                    `token_activo_id`,
                    `token`,
                    `autorizacion_id`,
                    `hora_iniciada`,
                    `estado`,
                    `browser_name`,
                    `browser_version`,
                    `os_name` 
                FROM 
                    `token_activo`
                WHERE
                    token='@token';
            ";
        }
        public static string Store()
        {
            return @"
                insert into 
                    `token_activo`
                    (
                        `token`, 
                        `autorizacion_id`, 
                        `hora_iniciada`, 
                        `estado`, 
                        `browser_name`, 
                        `browser_version`, 
                        `os_name`
                    )
                values
                    ( 
                    '@token', 
                    '@autorizacion_id', 
                    '@hora_iniciada', 
                    1, 
                    '@browser_name', 
                    '@browser_version',
                    '@os_name'
                    );
            ";
        }
    }
}