using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class ObtenerRedesGuardianQuery
    {
        public static string QueryAmpliacionRedes(List<string> snroVenta, DateTime fechaInicio, DateTime fechaFin, int cicloId, bool baja, bool autoVenta)
        {
            ///ciclo
            var filtro = "";
            if (cicloId == 0)
            {
                filtro = @$"tr.dtfecha >= '{fechaInicio.ToString("yyyy-MM-dd")}'
                    and tr.dtfecha <= '{fechaFin.ToString("yyyy-MM-dd")}'";
            }
            else
            {
                filtro = @$"tr.lciclo_id = {cicloId}";
            }
            return @$"
            SELECT 
                tr.lcontrato_id,
                tr.lcontacto_id,
                tr.scedulaidentidad,
                tr.stelefonomovil,
                tr.snombrecompleto,
                tr.cbaja,
                tr.lciclo_id,
                sum(tr.dprecio) as total_vendido,
                count(tr.lcontrato_id) as cantidad
            FROM (
                SELECT
                    aco.lcontrato_id,
                    comisiona.lcontacto_id,
                    comisiona.scedulaidentidad,
                    comisiona.stelefonomovil,
                    comisiona.snombrecompleto,
                    comisiona.cbaja,
                    asesor.lcontacto_id AS lcontacto_id_asesor,
                    asesor.snombrecompleto AS snombre_completo_asesor,
                    asesor.stelefonomovil AS stelefono_movil_asesor,
                    cliente.lcontacto_id AS lcontacto_id_cliente,
                    cliente.snombrecompleto AS snombre_completo_cliente,
                    cliente.stelefonomovil AS stelefono_movil_cliente,
                    aco.dprecio,
                    aco.lasesor_id,
                    avg.lgeneracion,
                    aco.dtfecha,
                    avg.lciclo_id,
                    aco.snroventa
                FROM
                    administracionventagrupo as avg
                    INNER JOIN administracioncontacto as comisiona on comisiona.lcontacto_id = avg.lcontacto_id
                    INNER JOIN administracioncontacto as asesor on asesor.lcontacto_id = avg.lasesor_id
                    INNER JOIN administracioncontrato as aco on aco.lcontrato_id = avg.lcontrato_id
                    INNER JOIN administracioncontacto as cliente on cliente.lcontacto_id = aco.lcontacto_id
            ) as tr
            WHERE {filtro}
                AND tr.snroventa IN('{string.Join("','", snroVenta)}')
            GROUP BY  tr.lcontacto_id
            ";
        }
        public static string QueryRedesNuevas(List<string> snroVenta, DateTime fechaInicio, DateTime fechaFin, int cicloId, bool baja, bool autoVenta)
        {
            ///ciclo
            var filtro = "";
            if (cicloId == 0)
            {
                filtro = @$"tr.dtfecha >= '{fechaInicio.ToString("yyyy-MM-dd")}'
                    and tr.dtfecha <= '{fechaFin.ToString("yyyy-MM-dd")}'";
            }
            else
            {
                filtro = @$"tr.lciclo_id = {cicloId}";
            }
            return @$"
            SELECT tr.* FROM (
                SELECT
                    aco.lcontrato_id,
                    comisiona.lcontacto_id as lcontacto_id_comisiona,
                    comisiona.`scedulaidentidad` AS scedula_identidad_comisiona,
                    comisiona.stelefonomovil AS stelefono_movil_comisiona,
                    comisiona.snombrecompleto AS snombre_completo_comisiona,
                    comisiona.`cbaja` AS cbaja_comisiona,
                    asesor.lcontacto_id AS lcontacto_id_asesor,
                    asesor.snombrecompleto AS snombre_completo_asesor,
                    asesor.stelefonomovil AS stelefono_movil_asesor,
                    cliente.lcontacto_id AS lcontacto_id_cliente,
                    cliente.snombrecompleto AS snombre_completo_cliente,
                    cliente.stelefonomovil AS stelefono_movil_cliente,
                    aco.dprecio,
                    aco.lcontacto_id,
                    aco.lasesor_id,
                    avg.lgeneracion,
                    aco.dtfecha,
                    avg.lciclo_id,
                    aco.snroventa
                FROM
                    administracionventagrupo as avg
                    INNER JOIN administracioncontacto as comisiona on comisiona.lcontacto_id = avg.lcontacto_id
                    INNER JOIN administracioncontacto as asesor on asesor.lcontacto_id = avg.lasesor_id
                    INNER JOIN administracioncontrato as aco on aco.lcontrato_id = avg.lcontrato_id
                    INNER JOIN administracioncontacto as cliente on cliente.lcontacto_id = aco.lcontacto_id
            ) as tr
            WHERE {filtro}
                AND tr.snroventa IN('{string.Join("','", snroVenta)}')
            GROUP BY  tr.lcontacto_id_asesor
            ";
        }
    }
}