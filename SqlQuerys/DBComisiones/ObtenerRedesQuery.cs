using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBComisiones
{
    public class ObtenerRedesQuery
    {
        public static string ObtenerNuevasRedes(DateTime fechaInicio, DateTime fechaFinal)
        {
            DateTime fechaInicioSumar = fechaInicio.AddDays(-1);
            DateTime fechaFinalRestar = fechaFinal.AddDays(1);
            return @$"
            SELECT 
                vwlg.*
            FROM
                vwLISTAVENTAS_GRAL as vwlg
            WHERE
                vwlg.FECHAVENTA >= '{fechaInicio.ToString("yyyyMMdd")}'
                and vwlg.FECHAVENTA <= '{fechaFinal.ToString("yyyyMMdd")}'
                and vwlg.IDCLIENTE not IN (
                    SELECT
                        vwlg.IDCLIENTE
                    FROM vwLISTAVENTAS_GRAL as vwlg
                    WHERE vwlg.IDCLIENTE in (
                            SELECT vwlg.IDCLIENTE
                            FROM
                                vwLISTAVENTAS_GRAL as vwlg
                            WHERE
                                vwlg.FECHAVENTA >= '{fechaInicio.ToString("yyyyMMdd")}'
                                and vwlg.FECHAVENTA <= '{fechaFinal.ToString("yyyyMMdd")}'
                            GROUP BY
                                vwlg.IDCLIENTE
                        )
                    and  vwlg.FECHAVENTA <= '{fechaInicioSumar.ToString("yyyyMMdd")}'
                                and not vwlg.FECHAVENTA >= '{fechaFinalRestar.ToString("yyyyMMdd")}'
                    GROUP BY vwlg.NOMBRE_CLIENTE,vwlg.IDCLIENTE  
                )
           ";
        }
    }
}