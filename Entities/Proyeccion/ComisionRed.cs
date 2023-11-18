using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Proyeccion
{
    public class ComisionRed
    {
        public int ComisionRedId { get; set; }
        public int Nivel { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Valor { get; set; }
    }
}