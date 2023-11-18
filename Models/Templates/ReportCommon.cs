using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Models.Templates
{
    public class ReportCommon
    {
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string Ciclo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechFin { get; set; }
    }
}