using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.GrdSion;
using api_guardian.Entities.GrdSion.Views;

namespace api_guardian.Dtos.Module
{
    public class ExportHistorialPagoComisiones
        {
            public string Nombre { get; set; }
            public string Empresas { get; set; }
            public string NombreCiclo { get; set; }
            public string Nit { get; set; }
            public DateTime FechaFinCiclo { get; set; }
            public DateTime FechaInicioCiclo { get; set; }
            public List<HistorialPagoComisionConsolidado> HistorialPagoConsolidado { get; set; }
        }
}