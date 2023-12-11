using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGdrSion
{
    public class AdministracionEmpresaQuery
    {
        public static string GetAll()
        {
            return @"
            SELECT
                `susuarioadd`,
                `dtfechaadd`,
                `susuariomod`,
                `dtfechamod`,
                `lempresa_id`,
                `snombre`,
                `snit`,
                `fecha_creacion`,
                `empresa`
            FROM `administracionempresa`
            order by lempresa_id desc
            ";
        }
        public static string Store()
        {
            return @"
                insert into 
                    `administracionempresa` (
                        `susuarioadd`, 
                        `dtfechaadd`, 
                        `susuariomod`, 
                        `dtfechamod`, 
                        `lempresa_id`, 
                        `snombre`, 
                        `snit`, 
                        `fecha_creacion`, 
                        `empresa`
                    )
                    values
                    (
                        @susuarioadd, 
                        @dtfechaadd, 
                        @susuariomod, 
                        @dtfechamod, 
                        @lempresaId, 
                        @snombre, 
                        @snit, 
                        @fechaCreacion, 
                        @empresa
                    );
                    SELECT LAST_INSERT_ID() as id;
            ";
        }
        public static string Edit()
        {
            return @"
               SELECT
                `susuarioadd`,
                `dtfechaadd`,
                `susuariomod`,
                `dtfechamod`,
                `lempresa_id`,
                `snombre`,
                `snit`,
                `fecha_creacion`,
                `empresa`
            FROM `administracionempresa`
            WHERE  lempresa_id = @LEmpresaId; 
            ";
        }
        public static string Update()
        {
            return @"
                update 
                    `administracionempresa` 
                set 
                    snombre = @snombre,
                    snit = @snit,
                    empresa = @empresa
                where 
                    lempresa_id = @lempresaId;
            ";
        }

        public static string Delete()
        {
            return @"
            ";
        }
        public static string GetOneUltimo()
        {
            return @"
            SELECT
                `susuarioadd`,
                `dtfechaadd`,
                `susuariomod`,
                `dtfechamod`,
                `lempresa_id`,
                `snombre`,
                `snit`,
                `fecha_creacion`,
                `empresa`
            FROM `administracionempresa`
                ORDER BY lempresa_id DESC
                LIMIT 1;
            ";
        }
    }
}