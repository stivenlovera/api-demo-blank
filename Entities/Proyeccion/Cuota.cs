using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Proyeccion
{
    public class Cuota
    {
        public int CuotaId { get; set; }
        public int VentaId { get; set; }
        public int NroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCouta { get; set; }
        public decimal MontoDeuda { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoPago { get; set; }
    }
}