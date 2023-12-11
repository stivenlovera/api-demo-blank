using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class PagoConsolidadoSql
    {
        public static string InsertarPagoComisiones()
        {
            return @"
            insert into 
                pago_consolidado (
                    nombre, 
                    descripcion, 
                    fecha_creacion, 
                    estado_reporte_id, 
                    comision_consolidado_id
                )
            values
                (
                    @Nombre, 
                    @Descripcion, 
                    NOW(), 
                    @EstadoReporteId, 
                    @ComisionConsolidadoId
                );
            SELECT LAST_INSERT_ID() as id;
            ";
        }
        public static string ObtenerPagoConsolidado()
        {
           return @"
            SELECT
                pago_consolidado_id,
                nombre,
                descripcion,
                fecha_creacion,
                estado_reporte_id,
                comision_consolidado_id
            FROM pago_consolidado;
           ";
        }
    }
}