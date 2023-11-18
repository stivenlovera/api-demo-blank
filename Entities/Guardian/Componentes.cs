using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Guardian
{
    public class Componentes
    {
        public int ComponenteId { get; set; }
        public int SubModuloId { get; set; }
        public string ComponenteNombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenReferencia { get; set; }
        
    }
}