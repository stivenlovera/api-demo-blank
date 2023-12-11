using api_guardian.Entities.GrdSion;

namespace api_guardian.Dtos.Controllers
{
    public class HistorialPagoComisionesDto
    {
        
    }

    public class ReqExportHistorialPagoComisionesDto
    {
        public PagoConsolidado Filtro { get; set; }
        public List<HistorialPagoComisionConsolidado> DataTable { get; set; }
    }
}