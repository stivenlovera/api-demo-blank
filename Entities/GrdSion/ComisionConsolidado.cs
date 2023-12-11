using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.GrdSion
{
    public class ComisionConsolidado
    {
        public int ComisionConsolidadoId { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EstadoReporteId { get; set; }
        public int Ciclo_id { get; set; }
    }
}