using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class FuncionarioQuery
    {
        public static string SPStoreFuncionario()
        {
            return @"
                CALL INSERT_FUNCIONARIO(@Nombres,@Apellidos,@FechaIngreso,@CargoId,@NroDocumento,@Observaciones,@Imagen,@Alias,@Email);
            ";
        }
        public static string GetAllUsuario()
        {
            return @"
                SELECT
                    F.*,
                    C.`nombre_cargo`,
                    C.`descripcion`
                FROM `funcionario` AS F
                    INNER JOIN `cargo` AS C ON C.`cargo_id` = F.`cargo_id`
                WHERE `estado`=1;
            ";
        }
        public static string GetOne()
        {
            return @"
               SELECT
                    F.*,
                    C.`nombre_cargo`,
                    C.`descripcion`,
                    FI.`funcionario_imagen_id`,
                    FI.`file`
                FROM `funcionario` AS F
                    INNER JOIN `funcionario_imagen` AS FI ON FI.`funcionario_id` = F.`funcionario_id`
                    INNER JOIN `cargo` AS C ON C.`cargo_id` = F.`cargo_id`
                    WHERE F.funcionario_id= @funcionarioId
            ";
        }
    }
}