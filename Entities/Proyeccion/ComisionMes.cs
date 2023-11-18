using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Proyeccion
{
    public class ComisionMes
    {
        public int ComisionMesId { get; set; }
        public int VentaId { get; set; }
        public int ComisionId { get; set; }
        public string Observacion { get; set; }
        public decimal Comision { get; set; }
    }
}