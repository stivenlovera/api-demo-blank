using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class HistorialConsolidadoSql
    {
        public static string ObtenerConsolidado()
        {
            return $"SELECT * FROM comision_consolidado WHERE lciclo_id=@cicloId";
        }

        public static string InsertComisiones()
        {
            return @$"
            insert into 
                comision_consolidado (
                nombre, 
                descripcion, 
                fecha_creacion, 
                estado_reporte_id,
                ciclo_id
                )
            values
                (
                    @Nombre,
                    @Descripcion,
                    NOW(),
                    @EstadoReporteId,
                    @ciclo_id
                );
            SELECT LAST_INSERT_ID() as id;
            ";
        }
        public static string Store()
        {
            return @$"
                insert into 
                    historial_comision_consolidado (
                        comision_consolidado_id, 
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
                        @ComisionConsolidadoId, 
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
        public static string BuscarEmpresaConsolidado()
        {
            return @$"
                SELECT 
                    comision_consolidado_id,
                    lempresa_id as empresa_id
                FROM
                    historial_comision_consolidado
                WHERE
                    comision_consolidado_id = @ComisionConsolidadoId
                GROUP BY lempresa_id
            ";
        }
        public static string InsertCambioEmpresa()
        {
            return @$"
                insert into
                cambio_empresa (
                    empresa_id,
                    pago_empresa_id,
                    comision_consolidado_id,
                    nota
                )
                values (
                        @EmpresaId,
                        @EmpresaId,
                        @ComisionConsolidadoId,
                        'Por defecto'
                    );
            ";
        }
        public static string CambioEmpresaUpdate()
        {
            return @$"
                update 
                    cambio_empresa
                set 
                    pago_empresa_id = @PagoEmpresaId,
                    nota = @Nota
                where 
                    cambio_empresa_id = @CambioEmpresaId
                    and 
                    comision_consolidado_id= @ComisionConsolidadoId;
                ";
        }


        public static string InsertPagoComision()
        {
            return @$"
                insert into 
                    historial_comision_consolidado (
                        comision_consolidado_id, 
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
                        @ComisionConsolidadoId, 
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
                        @TotalComisionServicio
                        @TotalComisionRetencion, 
                        @TotalPagar, 
                        @Valor13, 
                        @Valor87
                    );
            ";
        }
        public static string ObtenerCambioEmpresa()
        {
            return @$"
                SELECT * FROM cambio_empresa WHERE comision_consolidado_id=@ComisionConsolidadoId;
            ";
        }
        public static string ObtenerComisionesConsolidado()
        {
            return @$"
                SELECT *
                FROM comision_consolidado
                ORDER BY comision_consolidado_id DESC
            ";
        }
        public static string ObtenerHistoricoConsolidado()
        {
            return @$"
                SELECT *
                FROM
                    historial_comision_consolidado
                WHERE
                    comision_consolidado_id = @ComisionConsolidadoId;
            ";
        }
        public static string ObtenerConfigCambioEmpresa()
        {
            return @$"
                SELECT *
                FROM cambio_empresa;
                    comision_consolidado_id = @ComisionConsolidadoId;
            ";
        }
        public static string ProcesarCambioEmpresa()
        {
            return @$"
                    update 
                cambio_empresa 
                    set 
                    empresa_id = @EmpresaId,
                    pago_empresa_id = @PagoEmpresaId,
                    comision_consolidado_id = @comisionConsolidadoId,
                    nota = @Nota
                where 
                cambio_empresa_id = @CambioEmpresaId;
            ";
        }
    }
}