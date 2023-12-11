using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.GrdSion.Queries;

namespace api_guardian.Entities.GrdSion.Views
{
    public class HistorialComisionConsolidado : ConsolidadoQuery
    {
        public int HistorialComisionConsolidadoId { get; set; }
        public int ComisionConsolidado_id { get; set; }
    }
}