using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class ConsolidadoSql
    {
        public static string ObtenerConsolidado()
        {
            return @$"
            select
                dat.*
                , round((dat.total_comision_vta_grupo_residual * 6.96), 2) as total_comision_vta_grupo_residual_bs
                , round((dat.total_comision_vta_personal * 6.96), 2) as total_comision_vta_personal_bs
                , empresa.snombre as razon_social
                , empresa.snit as nit
                , ciclo.snombre as nombre_ciclo
                , ciclo.dtfechainicio as fecha_inicio_ciclo
                , ciclo.dtfechafin as fecha_fin_ciclo
                , tblr.porcentajeret as retencion
                , tblr.montocomision as total_comision
                , tblr.montoretencion as total_comision_retencion
                , tblr.total_comision as total_pagar
                , if(tblr.porcentajeret = 0, 
                            round((tblr.montocomision * 13) / 100, 2)
                            , 0) as valor_13
                , if(tblr.porcentajeret = 0, 
                            round((tblr.montocomision * 87) / 100, 2)
                            , 0) as valor_87
            from
            (select
                    dat.lcontacto_id
                    , contacto.scodigo
                    , contacto.scedulaidentidad
                    , contacto.snombrecompleto
                    , dat.lempresa_id
                    , dat.empresa
                    , sum(dat.comision_vta_grupo_residual) as total_comision_vta_grupo_residual
                    , sum(dat.comision_vta_personal) as total_comision_vta_personal
                    , dat.lciclo_id
            from
            (SELECT
                    vtaGrupo.lcontacto_id
                , contrato.lcomplejo_id
                
                , (SELECT ec.`empresa_id` as lempresa_id FROM `empresa_complejo` as ec WHERE ec.`complejo_id`=contrato.lcomplejo_id LIMIT 1)
                    as lempresa_id
                ,(SELECT ae.`empresa` FROM `empresa_complejo` as ec INNER JOIN `administracionempresa` as ae on ec.`empresa_id`=ae.`lempresa_id` WHERE ec.`complejo_id`=contrato.lcomplejo_id LIMIT 1)
                    as empresa
                , vtaGrupo.dcomision as comision_vta_grupo_residual
                , 0 as comision_vta_personal
                , vtaGrupo.lciclo_id
            FROM
                administracionventagrupo vtaGrupo
                INNER JOIN administracioncontrato contrato USING ( lcontrato_id )
                -- inner join administracionventapersonal vtaPersonal on vtaGrupo.lcontacto_id = vtaPersonal.lcontacto_id and vtaGrupo.lciclo_id = vtaPersonal.lciclo_id
                -- inner join administracioncomplejo complejo using(lcomplejo_id)
            WHERE
                vtaGrupo.lciclo_id = @cicloId
                and vtaGrupo.lcontacto_id > 1

            UNION ALL

            SELECT
                residual.lcontacto_id
                , residual.lcomplejo_id
                , (SELECT ec.`empresa_id` as lempresa_id FROM `empresa_complejo` as ec WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as lempresa_id
                ,(SELECT ae.`empresa` FROM `empresa_complejo` as ec INNER JOIN `administracionempresa` as ae on ec.`empresa_id`=ae.`lempresa_id` WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as empresa
            , residual.dmonto as comision_vta_grupo_residual
            , 0 as comision_vta_personal
            , residual.lciclo_id
            FROM
                administracionredempresacomplejo residual
                inner join administracioncomplejo complejo using(lcomplejo_id)
                -- inner join administracionventapersonal vtaPersonal on vtaPersonal.lcontacto_id = residual.lcontacto_id and vtaPersonal.lcontacto_id = residual.lciclo_id
            WHERE
                residual.lciclo_id = @cicloId

            union all

            SELECT
                    vtaPersonal.lcontacto_id
                , contrato.lcomplejo_id
                , (SELECT ec.`empresa_id` as lempresa_id FROM `empresa_complejo` as ec WHERE ec.`complejo_id`=contrato.lcomplejo_id LIMIT 1)
                    as lempresa_id
                ,(SELECT ae.`empresa` FROM `empresa_complejo` as ec INNER JOIN `administracionempresa` as ae on ec.`empresa_id`=ae.`lempresa_id` WHERE ec.`complejo_id`=contrato.lcomplejo_id LIMIT 1)
                    as empresa
                , 0 as comision_vta_grupo_residual
                , vtaPersonal.dcomision as comision_vta_personal
                , vtaPersonal.lciclo_id
            FROM
                administracionventapersonal vtaPersonal
                INNER JOIN administracioncontrato contrato USING ( lcontrato_id ) 
            WHERE
                vtaPersonal.lciclo_id = @cicloId
                
            union all
            SELECT
                liderazgo.vendedores_Mes_id as lcontacto_id
                , liderazgo.lcomplejo_id
                , (SELECT ec.`empresa_id` as lempresa_id FROM `empresa_complejo` as ec WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as lempresa_id
                ,(SELECT ae.`empresa` FROM `empresa_complejo` as ec INNER JOIN `administracionempresa` as ae on ec.`empresa_id`=ae.`lempresa_id` WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as empresa
            , liderazgo.monto as comision_vta_grupo_residual
            , 0 as comision_vta_personal
            , liderazgo.lciclo_id
            FROM
                T_GANADORES_BONOLIDERAZGO_EMPRESA_PAGAR liderazgo
                inner join administracioncomplejo complejo using(lcomplejo_id)
                -- inner join administracionventapersonal vtaPersonal on vtaPersonal.lcontacto_id = liderazgo.lcontacto_id and vtaPersonal.lcontacto_id = liderazgo.lciclo_id
            WHERE
                liderazgo.lciclo_id = @cicloId 
            
                UNION ALL
            SELECT
                liderazgo.vendedores_id as lcontacto_id
                , liderazgo.lcomplejo_id
                    , (SELECT ec.`empresa_id` as lempresa_id FROM `empresa_complejo` as ec WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as lempresa_id
                ,(SELECT ae.`empresa` FROM `empresa_complejo` as ec INNER JOIN `administracionempresa` as ae on ec.`empresa_id`=ae.`lempresa_id` WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as empresa
            , liderazgo.pagar as comision_vta_grupo_residual
            , 0 as comision_vta_personal
            , liderazgo.lciclo_id
            FROM
                t_bono_liderazgo liderazgo
                inner join administracioncomplejo complejo using(lcomplejo_id)
            WHERE
                liderazgo.lciclo_id = @cicloId
                UNION ALL
            SELECT
                top_vend.vendedor_lcontacto_id as lcontacto_id
                , top_vend.lcomplejo_id
                    , (SELECT ec.`empresa_id` as lempresa_id FROM `empresa_complejo` as ec WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as lempresa_id
                ,(SELECT ae.`empresa` FROM `empresa_complejo` as ec INNER JOIN `administracionempresa` as ae on ec.`empresa_id`=ae.`lempresa_id` WHERE ec.`complejo_id`=complejo.lcomplejo_id LIMIT 1)
                    as empresa
            , top_vend.pagar as comision_vta_grupo_residual
            , 0 as comision_vta_personal
            , top_vend.lciclo_id
            FROM
                t_top_vendedores top_vend
                inner join administracioncomplejo complejo using(lcomplejo_id)
            WHERE
                top_vend.lciclo_id = @cicloId   
            )dat
            inner join administracioncontacto contacto on dat.lcontacto_id = contacto.lcontacto_id
            group by dat.lcontacto_id, dat.lempresa_id)dat
            left outer join (
                select lcontacto_id from administracionventapersonal where lciclo_id = 109 group by lcontacto_id
            )datVtaPersonal on dat.lcontacto_id = datVtaPersonal.lcontacto_id
            inner join administracionempresa empresa on empresa.lempresa_id = dat.lempresa_id
            inner join administracionciclo ciclo on ciclo.lciclo_id = dat.lciclo_id 
            left outer join administracionciclopresentafactura factura on factura.lcontacto_id = dat.lcontacto_id and factura.lciclo_id = dat.lciclo_id
            left outer join administracioncontacargentina1 arg on arg.lcontacto_id = dat.lcontacto_id and arg.lciclo_id = dat.lciclo_id
            left JOIN tbl_retencionempresa as tblr on tblr.lcontacto_id=dat.lcontacto_id and tblr.lciclo_id=ciclo.lciclo_id and tblr.IdEmpresa=dat.lempresa_id 
            where
                ciclo.lciclo_id >= 55
                and 
                datVtaPersonal.lcontacto_id is not null
                AND
                dat.lempresa_id in @empresaId
                and 
                    case
                        when ciclo.lciclo_id >= 55
                        then dat.lcontacto_id != (select lcontacto_id from administracioncontacto where scedulaidentidad = '4823437')
                        else
                            dat.lcontacto_id in (select lcontacto_id from administracioncontacto where lcontacto_id > 3)
                    end
                order by valor_87 desc,dat.snombrecompleto 
            ";

        }
    }
}