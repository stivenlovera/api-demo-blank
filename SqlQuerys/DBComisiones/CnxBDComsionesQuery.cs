using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBComisiones
{
    public class CnxBDComsionesQuery
    {
        public static string GetOneByIdVenta()
        {
            return @"
            SELECT CNX.*
            FROM BDComisiones.dbo.vwLOTES_GRL_DOCID as VWLGD
                INNER JOIN BDComisiones.dbo.CNX_BDCOMISIONES AS CNX
                ON CNX.IDBD=VWLGD.IDEMPRESA
            WHERE IDVENTA=@Idventa
            ";
        }
    }
}