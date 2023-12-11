using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class HistorialPagoConsolidadoSql
    {
        public static string InsertConsolidadoPago()
        {
            return @"
            
                insert into 
                historial_pago_comision_consolidado (
                    pago_consolidado_id, 
                    lcontacto_id, 
                    scodigo, 
                    scedulaidentidad, 
                    snombrecompleto, 
                    lempresa_id, 
                    empresa, 
                    total_comision_vta_grupo_residual, 
                    total_comision_vta_personal, 
                    lciclo_id, 
                    total_comision_vta_grupo_residual_bs, 
                    total_comision_vta_personal_bs, 
                    razon_social, 
                    nit, 
                    nombre_ciclo, 
                    fecha_inicio_ciclo, 
                    fecha_fin_ciclo, 
                    retencion, 
                    total_comision, 
                    total_comision_retencion, 
                    total_pagar, 
                    valor_13, 
                    valor_87
                )
                values
                (
                    @PagoConsolidadoId, 
                    @LcontactoId, 
                    @Scodigo, 
                    @Scedulaidentidad, 
                    @Snombrecompleto, 
                    @LempresaId, 
                    @Empresa, 
                    @TotalComisionVtaGrupoResidual, 
                    @TotalComisionVtaPersonal, 
                    @LcicloId, 
                    @TotalComisionVtaGrupoResiduaBs, 
                    @TotalComisionVtaPersonalBs, 
                    @RazonSocial, 
                    @Nit, 
                    @NombreCiclo, 
                    @FechaInicioCiclo, 
                    @FechaFinCiclo, 
                    @Retencion, 
                    @TotalComision, 
                    @TotalComisionRetencion, 
                    @TotalPagar, 
                    @Valor13, 
                    @Valor87
                );
            ";
        }
        public static string ObtenerTodo()
        {
            return @"
            SELECT
                historial_pago_comision_consolidado_id,
                pago_consolidado_id,
                lcontacto_id,
                scodigo,
                scedulaidentidad,
                snombrecompleto,
                lempresa_id,
                empresa,
                total_comision_vta_grupo_residual,
                total_comision_vta_personal,
                lciclo_id,
                total_comision_vta_grupo_residual_bs,
                total_comision_vta_personal_bs,
                razon_social,
                nit,
                nombre_ciclo,
                fecha_inicio_ciclo,
                fecha_fin_ciclo,
                retencion,
                total_comision,
                total_comision_retencion,
                total_pagar,
                valor_13,
                valor_87
            FROM
                historial_pago_comision_consolidado
            where pago_consolidado_id=@PagoConsolidadoId;
            ";
        }
    }
}