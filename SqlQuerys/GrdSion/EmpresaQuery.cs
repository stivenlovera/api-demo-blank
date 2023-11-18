using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class EmpresaQuery
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
            FROM `administracionempresa`;
            ";
        }
        public static string GetOne()
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
            WHERE lempresa_id = @lempresaId;
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
                    now(), 
                    @susuariomod, 
                    now(), 
                    @lempresa_id, 
                    @snombre, 
                    @snit, 
                    now(), 
                    @empresa
                );
            ";
        }
        public static string Update()
        {
            return @"
                update 
                `administracionempresa` 
                set 
                    susuariomod = @Susuariomod,
                    dtfechamod = now(),
                    snombre = @Snombre,
                    snit = @Snit,
                    empresa = @Empresa
                where 
                    lempresa_id = @lempresaId;
            ";
        }
        public static string Delete()
        {

            return @"
            delete from 
                `administracionempresa` 
            where 
                `lempresa_id` = @lempresaId;
            ";
        }
        public static string Other()
        {
            return @"";
        }
    }
}