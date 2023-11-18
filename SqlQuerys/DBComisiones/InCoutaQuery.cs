using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBComisiones
{
    public class InCoutaQuery
    {
        public static string GetInCuotaByBDEmpresa(string conexionBDEmpresa)
        {
            return @$"
            SELECT 
                IC.NROCUOTA as nro_cuota,
                IC.FVENCIMIENTO as fecha_vencimiento,
                IC.MONTOCUOTA as monto_couta,
                IC.MONTODEUDA as monto_deuda,
                IC.FECHA_PAGO as fecha_pago,
                (IC.MONTOCUOTA-IC.MONTODEUDA) AS monto_pago
            FROM
                {conexionBDEmpresa.Trim()}.dbo.INCUOTA AS IC
            WHERE IC.IDVENTA  =@Idventa;
            ";
        }
    }
}