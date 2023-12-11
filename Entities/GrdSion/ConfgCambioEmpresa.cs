using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.GrdSion
{
    public class ConfigCambioEmpresa
    {
        public int ConfigCambioEmpresaId { get; set; }
        public int EmpresaId { get; set; }
        public int PagoEmpresaId { get; set; }
        public string Nota { get; set; }
        public int PagoConsolidadoId { get; set; }
    }
}