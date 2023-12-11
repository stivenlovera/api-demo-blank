using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.GrdSion.Queries;

namespace api_guardian.Entities.GrdSion
{
    public class HistorialPagoComisionConsolidado : ConsolidadoQuery
    {
        public int HistorialPagoComisionConsolidadoId { get; set; }
        public int PagoConsolidadoId { get; set; }
    }
}