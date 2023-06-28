using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Models.Templates
{
    public class ReporteConsolidados
    {
        public string nombreReporte { get; set; }
        public int nit { get; set; }
        public string empresa { get; set; }
        public string ciclo { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public List<Asesor> asesores { get; set; }
    }
    public class Asesor
    {
        public int codigo { get; set; }
        public string asesor { get; set; }
        public decimal comision { get; set; }
        public decimal servicio { get; set; }
        public decimal totalComision { get; set; }
        public decimal impuesto { get; set; }
        public decimal sinImpuesto { get; set; }
        public decimal retencion { get; set; }
        public decimal totalRetencion { get; set; }
        public decimal total { get; set; }
        public decimal totalPagar { get; set; }
    }
}