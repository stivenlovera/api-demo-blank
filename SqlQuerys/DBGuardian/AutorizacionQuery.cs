using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class AutorizacionQuery
    {
        public static string SPInsertFuncionario(Funcionario funcionario)
        {

            return @"
                CALL INSERT_FUNCIONARIO(@Nombres,'LOVERA HUARACHI','2023-09-01','1','8963497 SC','funcionario de prueba','imagen.jpg');
            ";
        }
        public static string GetAllUsuario()
        {
            return @"
               SELECT
                    F.*,
                    C.`nombre_cargo`
                FROM `funcionario` AS F
                    INNER JOIN `cargo` AS C ON C.`cargo_id` = F.`cargo_id`;
            ";
        }
        public static string GetOne()
        {
            return @"
               SELECT
                    F.*,
                    C.`nombre_cargo`,
                    FI.`file`
                FROM `funcionario` AS F
                    INNER JOIN `funcionario_imagen` AS FI ON FI.`funcionario_id` = F.`funcionario_id`
                    INNER JOIN `cargo` AS C ON C.`cargo_id` = F.`cargo_id`
                    WHERE F.funcionario_id= @funcionarioId
            ";
        }
        public static string EditarOne()
        {
            return @"
                SELECT A.*
                FROM
                    `autorizacion` AS A
                WHERE
                    A.`funcionario_id` =  @funcionarioId
                ";
        }
        public static string GetLogin()
        {
            return @"
                SELECT
                    F.*,
                    C.`nombre_cargo`,
                    FI.`file`
                FROM `funcionario` AS F
                    INNER JOIN `autorizacion` as A ON A.`funcionario_id` = F.`funcionario_id`
                    INNER JOIN `funcionario_imagen` AS FI ON FI.`funcionario_id` = F.`funcionario_id`
                    INNER JOIN `cargo` AS C ON C.`cargo_id` = F.`cargo_id`
                WHERE
                    A.`usuario` = @usuario
                    AND A.`password` = @password
                    AND A.`activo` = 1;
            ";
        }
        public static string Update()
        {
            return @"
                update
                    `autorizacion`
                set
                    usuario = @usuario,
                    password = @password,
                    activo = @activo
                where
                    autorizacion_id = @funcionarioId;
            ";
        }
    }
}