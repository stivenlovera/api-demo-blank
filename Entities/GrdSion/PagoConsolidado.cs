using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.GrdSion
{
    public class PagoConsolidado
    {
        public int PagoConsolidadoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int EstadoReporteId { get; set; }
        public int ComisionConsolidadoId { get; set; }
    }
}