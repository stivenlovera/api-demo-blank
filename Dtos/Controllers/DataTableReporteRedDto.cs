using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Dtos.Controllers
{
    public class ReqDataTableReporteRedDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Baja { get; set; }
        public int Ciclo { get; set; }
        public string Tipo { get; set; }
        public bool AutoVenta { get; set; }
    }

    public class RespDataTableReporteRedDto
    {
        public string scedulaidentidad { get; set; }
        public int cantidad { get; set; }
        public DateTime dtfecha { get; set; }
        public int lcontacto_id { get; set; }
        public int lcontrato_id { get; set; }
        public int lcicloId { get; set; }
        public string cbaja { get; set; }
        public string snombreCompleto { get; set; }
        public decimal total_vendido { get; set; }
    }
    public class ReqPreviewReporteRedDto
    {
        public FiltroPreviewReporteRedDto Filtros { get; set; }
        public List<RespDataTableReporteRedDto> Rows { get; set; }
    }
    public class FiltroPreviewReporteRedDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Baja { get; set; }
        public int Ciclo { get; set; }
        public string Tipo { get; set; }
        public bool AutoVenta { get; set; }
    }
}