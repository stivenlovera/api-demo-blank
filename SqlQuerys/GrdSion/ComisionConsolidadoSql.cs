using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class ComisionConsolidadoSql
    {
        public static string ObtenerUno()
        {
            return @"
                   SELECT 
                   comision_consolidado_id,
                   nombre,descripcion,
                   fecha_creacion,
                   estado_reporte_id,ciclo_id 
                   FROM 
                   comision_consolidado
                WHERE
                    comision_consolidado_id=@ComisionConsolidadoId
                   ;
            ";
        }
        public static string Update()
        {
            return @"
                     update 
                        comision_consolidado 
                    set 
                        nombre = Nombre,
                        descripcion = Descripcion,
                        estado_reporte_id = EstadoReporteId,
                    where 
                        comision_consolidado_id = @ComisionConsolidadoId;
                   ;
            ";
        }
        public static string Delete()
        {
            return @"
                    update 
                        comision_consolidado 
                    set 
                        nombre = Nombre,
                        descripcion = Descripcion,
                        estado_reporte_id = EstadoReporteId,
                    where 
                        comision_consolidado_id = @ComisionConsolidadoId;
                   ;
            ";
        }
    }
}