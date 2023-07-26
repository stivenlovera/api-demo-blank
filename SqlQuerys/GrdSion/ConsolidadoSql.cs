using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class ConsolidadoSql
    {
        public string obtenerConsolidado(long cicloId)
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
                , if(ciclo.lciclo_id >= 55, (select porcentajeret from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id), if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0)) as retencion
                , if(ciclo.lciclo_id >= 55 , (select montocomision from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id), (dat.total_comision_vta_personal + dat.total_comision_vta_grupo_residual)) as total_comision
                , if(ciclo.lciclo_id >= 55,(select montoretencion from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id),format(if(if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0) = 0, 0, (((((dat.total_comision_vta_personal + dat.total_comision_vta_grupo_residual) * if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0))) / 100))), 2)) as total_comision_retencion
                -- , format((dat.total_comision_vta_personal + dat.total_comision_vta_grupo_residual)  - if(if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0) = 0, 0, (((((dat.total_comision_vta_personal + dat.total_comision_vta_grupo_residual) * if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0))) / 100))), 2) as total_pagar
                ,  if(ciclo.lciclo_id >= 55, (select total_comision from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id), round(((dat.total_comision_vta_personal + dat.total_comision_vta_grupo_residual))  - format(if(if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0) = 0, 0, (((((dat.total_comision_vta_personal + dat.total_comision_vta_grupo_residual) * if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0))) / 100))), 2), 2)) as total_pagar
                , if(ciclo.lciclo_id >= 55,
                            if((select porcentajeret from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id) = 0, 
                            round(((select montocomision from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id) * 13) / 100, 2)
                            , 0)
                    , case
                when if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0) = 0 then format((((dat.total_comision_vta_grupo_residual + dat.total_comision_vta_personal) * 13) / 100), 2)
                else 0
                end) as valor_13
                , if(ciclo.lciclo_id >= 55,
                    if((select porcentajeret from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id) = 0, 
                            round(((select montocomision from tbl_retencionempresa where lciclo_id = ciclo.lciclo_id and lcontacto_id = dat.lcontacto_id and idempresa = dat.lempresa_id) * 87) / 100, 2)
                            , 0)
                    ,case
                when if(factura.lcontacto_id is null, if(arg.lcontacto_id is null, 15.5, 12.5), 0) = 0 then format((((dat.total_comision_vta_grupo_residual + dat.total_comision_vta_personal) * 87) / 100), 2)
                else 0
                end) as valor_87
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
                , case
                        when contrato.lcomplejo_id in (1, 2, 5) then 1
                        when contrato.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 2
                        when contrato.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 3
                        when contrato.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 4
                        when contrato.lcomplejo_id in (16,19,21,26,38, 51) then 5
                        when contrato.lcomplejo_id in (17,20, 25) then 6
                        when contrato.lcomplejo_id in (14,15) then 7
                        when contrato.lcomplejo_id in (18) then 8
                        -- when complejo.lcomplejo_id in (23,31) then 9
                        when contrato.lcomplejo_id in (22, 58, 59, 62) then 10
                        when contrato.lcomplejo_id in (27) then 11
                        when contrato.lcomplejo_id in (29) then 12
                        when contrato.lcomplejo_id in (28) then 13
                        when contrato.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 14
                        when contrato.lcomplejo_id in (31,81) then 15
                        when contrato.lcomplejo_id in (23) then 16
                        when contrato.lcomplejo_id in (46, 49) then 17
                        when contrato.lcomplejo_id in (34) then 18
                        when contrato.lcomplejo_id in (85) then 21
                    end
                    as lempresa_id
                , case
                        when contrato.lcomplejo_id in (1, 2, 5) then 'SION'
                        when contrato.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 'KINTAS'
                        when contrato.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 'ZURIEL'
                        when contrato.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 'NICAPOLIS'
                        when contrato.lcomplejo_id in (16,19,21,26,38, 51) then 'ASHER'
                        when contrato.lcomplejo_id in (17,20, 25) then 'SHOFAR'
                        when contrato.lcomplejo_id in (14,15) then 'CEMENTERIO'
                        when contrato.lcomplejo_id in (18) then 'MEXICO'
                        -- when complejo.lcomplejo_id in (23,31) then 'NEIZAN'
                        when contrato.lcomplejo_id in (22, 58, 59,62) then 'SEDE LAS PRADERAS/ROYAL PARI'
                        when contrato.lcomplejo_id in (27) then 'MURANO'
                        when contrato.lcomplejo_id in (29) then 'KALOMAI'
                        when contrato.lcomplejo_id in (28) then 'VALLE ANGOSTURA/ELIAN'
                        when contrato.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 'JAYIL'
                        when contrato.lcomplejo_id in (31,81) then 'NEIZAN JAYIL'
                        when contrato.lcomplejo_id in (23) then 'NEIZAN ASHER'
                        when contrato.lcomplejo_id in (46, 49) then 'CLUB DEPORTIVO ROYAL PARI / JAIM'
                        when contrato.lcomplejo_id in (34) then 'MENORAH'
                        when contrato.lcomplejo_id in (85) then 'AVDEL'
                    end
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
                vtaGrupo.lciclo_id = '{cicloId}'
                and vtaGrupo.lcontacto_id > 1

            UNION ALL

            SELECT
                residual.lcontacto_id
                , residual.lcomplejo_id
                , case
                        when complejo.lcomplejo_id in (1, 2, 5) then 1
                        when complejo.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 2
                        when complejo.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 3
                        when complejo.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 4
                        when complejo.lcomplejo_id in (16,19,21,26,38, 51) then 5
                        when complejo.lcomplejo_id in (17,20, 25) then 6
                        when complejo.lcomplejo_id in (14,15) then 7
                        when complejo.lcomplejo_id in (18) then 8
                        -- when complejo.lcomplejo_id in (23,31) then 9
                        when complejo.lcomplejo_id in (22, 58, 59,62) then 10
                        when complejo.lcomplejo_id in (27) then 11
                        when complejo.lcomplejo_id in (29) then 12
                        when complejo.lcomplejo_id in (28) then 13
                        when complejo.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 14
                        when complejo.lcomplejo_id in (31,81) then 15
                        when complejo.lcomplejo_id in (23) then 16
                        when complejo.lcomplejo_id in (46, 49) then 17
                        when complejo.lcomplejo_id in (34) then 18
                        when complejo.lcomplejo_id in (85) then 21
                    end
                    as lempresa_id
                , case
                        when complejo.lcomplejo_id in (1, 2, 5) then 'SION'
                        when complejo.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 'KINTAS'
                        when complejo.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 'ZURIEL'
                        when complejo.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 'NICAPOLIS'
                        when complejo.lcomplejo_id in (16,19,21,26,38, 51) then 'ASHER'
                        when complejo.lcomplejo_id in (17,20, 25) then 'SHOFAR'
                        when complejo.lcomplejo_id in (14,15) then 'CEMENTERIO'
                        when complejo.lcomplejo_id in (18) then 'MEXICO'
                        -- when complejo.lcomplejo_id in (23,31) then 'NEIZAN'
                        when complejo.lcomplejo_id in (22, 58, 59, 62) then 'SEDE LAS PRADERAS/ROYAL PARI'
                        when complejo.lcomplejo_id in (27) then 'MURANO'
                        when complejo.lcomplejo_id in (29) then 'KALOMAI'
                        when complejo.lcomplejo_id in (28) then 'VALLE ANGOSTURA/ELIAN'
                        when complejo.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 'JAYIL'
                        when complejo.lcomplejo_id in (31,81) then 'NEIZAN JAYIL'
                        when complejo.lcomplejo_id in (23) then 'NEIZAN ASHER'
                        when complejo.lcomplejo_id in (46, 49) then 'CLUB DEPORTIVO ROYAL PARI / JAIM'
                        when complejo.lcomplejo_id in (34) then 'MENORAH'
                        when complejo.lcomplejo_id in (85) then 'AVDEL'
                    end
                    as empresa
            , residual.dmonto as comision_vta_grupo_residual
            , 0 as comision_vta_personal
            , residual.lciclo_id
            FROM
                administracionredempresacomplejo residual
                inner join administracioncomplejo complejo using(lcomplejo_id)
                -- inner join administracionventapersonal vtaPersonal on vtaPersonal.lcontacto_id = residual.lcontacto_id and vtaPersonal.lcontacto_id = residual.lciclo_id
            WHERE
                residual.lciclo_id = '{cicloId}'

            union all

            SELECT
                    vtaPersonal.lcontacto_id
                , contrato.lcomplejo_id
                , case
                        when contrato.lcomplejo_id in (1, 2, 5) then 1
                        when contrato.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 2
                        when contrato.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 3
                        when contrato.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 4
                        when contrato.lcomplejo_id in (16,19,21,26,38, 51) then 5
                        when contrato.lcomplejo_id in (17,20, 25) then 6
                        when contrato.lcomplejo_id in (14,15) then 7
                        when contrato.lcomplejo_id in (18) then 8
                        -- when contrato.lcomplejo_id in (23,31) then 9
                        when contrato.lcomplejo_id in (22, 58, 59,62) then 10
                        when contrato.lcomplejo_id in (27) then 11
                        when contrato.lcomplejo_id in (29) then 12
                        when contrato.lcomplejo_id in (28) then 13
                        when contrato.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 14
                        when contrato.lcomplejo_id in (31,81) then 15
                        when contrato.lcomplejo_id in (23) then 16
                        when contrato.lcomplejo_id in (46, 49) then 17
                        when contrato.lcomplejo_id in (34) then 18
                        when contrato.lcomplejo_id in (85) then 21
                    end
                    as lempresa_id
                , case
                    when contrato.lcomplejo_id in (1, 2, 5) then 'SION'
                    when contrato.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 'KINTAS'
                    when contrato.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 'ZURIEL'
                    when contrato.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 'NICAPOLIS'
                    when contrato.lcomplejo_id in (16,19,21,26,38, 51) then 'ASHER'
                    when contrato.lcomplejo_id in (17,20, 25) then 'SHOFAR'
                    when contrato.lcomplejo_id in (14,15) then 'CEMENTERIO'
                    when contrato.lcomplejo_id in (18) then 'MEXICO'
                    -- when contrato.lcomplejo_id in (23,31) then 'NEIZAN'
                    when contrato.lcomplejo_id in (22, 58, 59,62) then 'SEDE LAS PRADERAS/ROYAL PARI'
                    when contrato.lcomplejo_id in (27) then 'MURANO'
                    when contrato.lcomplejo_id in (29) then 'KALOMAI'
                    when contrato.lcomplejo_id in (28) then 'VALLE ANGOSTURA/ELIAN'
                    when contrato.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 'JAYIL'
                    when contrato.lcomplejo_id in (31,81) then 'NEIZAN JAYIL'
                    when contrato.lcomplejo_id in (23) then 'NEIZAN ASHER'
                    when contrato.lcomplejo_id in (46, 49) then 'CLUB DEPORTIVO ROYAL PARI / JAIM'
                    when contrato.lcomplejo_id in (34) then 'MENORAH'
                    when contrato.lcomplejo_id in (85) then 'AVDEL'
                    end
                    as empresa
                , 0 as comision_vta_grupo_residual
                , vtaPersonal.dcomision as comision_vta_personal
                , vtaPersonal.lciclo_id
            FROM
                administracionventapersonal vtaPersonal
                INNER JOIN administracioncontrato contrato USING ( lcontrato_id ) 
            WHERE
                vtaPersonal.lciclo_id = '{cicloId}'
                
            union all
            SELECT
                liderazgo.vendedores_Mes_id as lcontacto_id
                , liderazgo.lcomplejo_id
                , case
                        when complejo.lcomplejo_id in (1, 2, 5) then 1
                        when complejo.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 2
                        when complejo.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 3
                        when complejo.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 4
                        when complejo.lcomplejo_id in (16,19,21,26,38, 51) then 5
                        when complejo.lcomplejo_id in (17,20, 25) then 6
                        when complejo.lcomplejo_id in (14,15) then 7
                        when complejo.lcomplejo_id in (18) then 8
                        -- when complejo.lcomplejo_id in (23,31) then 9
                        when complejo.lcomplejo_id in (22, 58, 59) then 10
                        when complejo.lcomplejo_id in (27) then 11
                        when complejo.lcomplejo_id in (29) then 12
                        when complejo.lcomplejo_id in (28) then 13
                        when complejo.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 14
                        when complejo.lcomplejo_id in (31,81) then 15
                        when complejo.lcomplejo_id in (23) then 16
                        when complejo.lcomplejo_id in (46, 49) then 17
                        when complejo.lcomplejo_id in (34) then 18
                        when complejo.lcomplejo_id in (85) then 21
                    end
                    as lempresa_id
                , case
                        when complejo.lcomplejo_id in (1, 2, 5) then 'SION'
                        when complejo.lcomplejo_id in (3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 'KINTAS'
                        when complejo.lcomplejo_id in (7, 8, 9, 10, 52,53, 54, 57,60,86,92) then 'ZURIEL'
                        when complejo.lcomplejo_id in (13,37,40,41,42,43, 47, 50, 61,64) then 'NICAPOLIS'
                        when complejo.lcomplejo_id in (16,19,21,26,38, 51) then 'ASHER'
                        when complejo.lcomplejo_id in (17,20, 25) then 'SHOFAR'
                        when complejo.lcomplejo_id in (14,15) then 'CEMENTERIO'
                        when complejo.lcomplejo_id in (18) then 'MEXICO'
                        -- when complejo.lcomplejo_id in (23,31) then 'NEIZAN'
                        when complejo.lcomplejo_id in (22, 58, 59) then 'SEDE LAS PRADERAS/ROYAL PARI'
                        when complejo.lcomplejo_id in (27) then 'MURANO'
                        when complejo.lcomplejo_id in (29) then 'KALOMAI'
                        when complejo.lcomplejo_id in (28) then 'VALLE ANGOSTURA/ELIAN'
                        when complejo.lcomplejo_id in (30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84,87,88,89,90,91) then 'JAYIL'
                        when complejo.lcomplejo_id in (31,81) then 'NEIZAN JAYIL'
                        when complejo.lcomplejo_id in (23) then 'NEIZAN ASHER'
                        when complejo.lcomplejo_id in (46, 49) then 'CLUB DEPORTIVO ROYAL PARI / JAIM'
                        when complejo.lcomplejo_id in (34) then 'MENORAH'
                        when complejo.lcomplejo_id in (85) then 'AVDEL'
                    end
                    as empresa
            , liderazgo.monto as comision_vta_grupo_residual
            , 0 as comision_vta_personal
            , liderazgo.lciclo_id
            FROM
                T_GANADORES_BONOLIDERAZGO_EMPRESA_PAGAR liderazgo
                inner join administracioncomplejo complejo using(lcomplejo_id)
                -- inner join administracionventapersonal vtaPersonal on vtaPersonal.lcontacto_id = liderazgo.lcontacto_id and vtaPersonal.lcontacto_id = liderazgo.lciclo_id
            WHERE
                liderazgo.lciclo_id = '{cicloId}'              
            )dat
            inner join administracioncontacto contacto on dat.lcontacto_id = contacto.lcontacto_id
            group by dat.lcontacto_id, dat.lempresa_id)dat
            left outer join (
                select lcontacto_id from administracionventapersonal where lciclo_id = '{cicloId}' group by lcontacto_id
            )datVtaPersonal on dat.lcontacto_id = datVtaPersonal.lcontacto_id
            inner join administracionempresa empresa on empresa.lempresa_id = dat.lempresa_id
            inner join administracionciclo ciclo on ciclo.lciclo_id = dat.lciclo_id
            left outer join administracionciclopresentafactura factura on factura.lcontacto_id = dat.lcontacto_id and factura.lciclo_id = dat.lciclo_id
            left outer join administracioncontacargentina1 arg on arg.lcontacto_id = dat.lcontacto_id and arg.lciclo_id = dat.lciclo_id
            where
                datVtaPersonal.lcontacto_id is not null
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