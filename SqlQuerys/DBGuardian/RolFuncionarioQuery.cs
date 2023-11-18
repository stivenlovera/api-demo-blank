using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class RolFuncionarioQuery
    {
        public static string GetAll()
        {
            return @"
                SELECT
                    RF.*,
                    R.*
                FROM `rol_funcionario` AS RF
                    INNER JOIN `rol` AS R ON R.`rol_id` = RF.`funcionario_id`;
            ";
        }
        public static string GetOne()
        {
            return @"
                SELECT
                    RF.*,
                    R.*
                FROM `rol_funcionario` AS RF
                    INNER JOIN `rol` AS R ON R.`rol_id` = RF.`funcionario_id`
                WHERE
                    rol_funcionario_id='@rol_funcionario_id';
            ";
        }
        public static string GetAllByRol()
        {
            return @"
                SELECT
                    RF.*,
                    R.*
                FROM `rol_funcionario` AS RF
                    INNER JOIN `rol` AS R ON R.`rol_id` = RF.`funcionario_id`;
                WHERE
                    rol_id='@rol_id';
            ";
        }
        public static string GetAllByFuncionario()
        {
            return @"
                SELECT 
                    RF.*,
                    R.*
                FROM `rol_funcionario` AS rf
                    INNER JOIN rol AS r ON r.`rol_id` = rf.`rol_id`
                WHERE rf.`funcionario_id` = @funcionario_id
            ";
        }
        public static string GetAllByFuncionarioId()
        {
            return @"
                SELECT 
                    R.*
                FROM `rol_funcionario` AS rf
                    INNER JOIN rol AS r ON r.`rol_id` = rf.`rol_id`
                WHERE rf.`funcionario_id` = @funcionarioId
            ";
        }
        public static string StoreRolFuncionario()
        {
            return @"
                insert into 
                    `rol_funcionario` (
                    `rol_id`, 
                    `funcionario_id`
                    )
                values(
                    @rolId, 
                    @funcionarioId
                );";
        }
        public static string DeleteRolFuncionarioByFuncionarioId()
        {
            return @"
                delete from 
                    `rol_funcionario` 
                where 
                    `funcionario_id` = @funcionarioId;
                ;";
        }
    }
}