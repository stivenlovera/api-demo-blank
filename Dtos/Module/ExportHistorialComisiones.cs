using api_guardian.Entities.GrdSion.Views;

namespace api_guardian.Dtos.Module
{
    public class ExportHistorialComisiones
    {
        public string Nombre { get; set; }
        public string Empresas { get; set; }
        public string NombreCiclo { get; set; }
        public string Nit { get; set; }
        public DateTime FechaFinCiclo { get; set; }
        public DateTime FechaInicioCiclo { get; set; }
        public List<HistorialComisionConsolidado> historialComisionConsolidados { get; set; }
    }
}