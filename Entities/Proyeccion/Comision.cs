using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Proyeccion
{
    public class Comision
    {
        public int ComisionId { get; set; }
        public string NombreComision { get; set; }
        public string DescripcionComision { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Monto { get; set; }
        public int UsarRed { get; set; }
    }
}