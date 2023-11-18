using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBGuardian
{
    public class CicloQuery
    {
        public static string GetAll()
        {
            return @"
                SELECT 
                    `susuarioadd`,
                    `dtfechaadd`,
                    `susuariomod`,
                    `dtfechamod`,
                    `lciclo_id`,
                    `snombre`,
                    `dtfechainicio`,
                    `dtfechafin`,
                    `lestado`,
                    `dtfechacierre`,
                    `dtfechaprecierre1`,
                    `dtfechaprecierre2`,
                    `cverenweb`,
                    `estadogestor` 
                FROM 
                    `administracionciclo`
                ORDER BY `lciclo_id` DESC;
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
                    `lciclo_id`,
                    `snombre`,
                    `dtfechainicio`,
                    `dtfechafin`,
                    `lestado`,
                    `dtfechacierre`,
                    `dtfechaprecierre1`,
                    `dtfechaprecierre2`,
                    `cverenweb`,
                    `estadogestor` 
                FROM 
                    `administracionciclo`
                WHERE 
                    lciclo_id=@lcicloId
            ";
        }
        public static string Store()
        {
            return @"
                insert into
                    `administracionciclo` (
                        `susuarioadd`,
                        `dtfechaadd`,
                        `susuariomod`,
                        `dtfechamod`,
                        `lciclo_id`,
                        `snombre`,
                        `dtfechainicio`,
                        `dtfechafin`,
                        `lestado`,
                        `dtfechacierre`,
                        `dtfechaprecierre1`,
                        `dtfechaprecierre2`,
                        `cverenweb`,
                        `estadogestor`
                    )
                values (
                        @susuarioadd,
                        now(),
                        @susuariomod,
                        now(),
                        @lcicloId,
                        @snombre,
                        @dtfechainicio,
                        @dtfechafin,
                        @lestado,
                        now(),
                        @dtfechaprecierre1,
                        @dtfechaprecierre2,
                        @cverenweb,
                        0
                    );
                    SELECT LAST_INSERT_ID() as id;
            ";
        }
        public static string Update()
        {
            return @"
                update 
                    `administracionciclo` 
                set 
                    susuarioadd = @susuarioadd,
                    susuariomod = @susuarioadd,
                    dtfechamod = now(),
                    snombre = @snombre,
                    dtfechainicio = @dtfechainicio,
                    dtfechafin = @dtfechafin,
                    lestado = @lestado,
                    dtfechacierre = now(),
                    dtfechaprecierre1 = @dtfechaprecierre1,
                    dtfechaprecierre2 = @dtfechaprecierre2,
                    cverenweb = @cverenweb,
                    estadogestor = 0
                    where 
                lciclo_id = @lcicloId;
            ";
        }
        public static string Delete()
        {
            return @"
            delete from 
                `administracionciclo` 
            where 
                `lciclo_id` = @lcicloId;
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
                    `lciclo_id`,
                    `snombre`,
                    `dtfechainicio`,
                    `dtfechafin`,
                    `lestado`,
                    `dtfechacierre`,
                    `dtfechaprecierre1`,
                    `dtfechaprecierre2`,
                    `cverenweb`,
                    `estadogestor` 
                FROM 
                    `administracionciclo`
                ORDER BY lciclo_id DESC
                LIMIT 1;
            ";
        }
    }
}