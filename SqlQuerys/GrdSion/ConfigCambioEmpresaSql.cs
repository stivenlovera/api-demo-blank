using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class ConfigCambioEmpresaSql
    {
        public static string InsertConfigCambioEmpresa()
        {
            return @"
                insert into 
                    config_cambio_empresa (
                    empresa_id, 
                    pago_empresa_id, 
                    pago_consolidado_id, 
                    nota
                    )
                values
                    (
                    @EmpresaId, 
                    @PagoEmpresaId, 
                    @PagoConsolidadoId, 
                    @Nota
            );";
        }
    }
}