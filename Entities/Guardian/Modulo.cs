using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Guardian
{
    public class Modulo
    {
        public int ModuloId { get; set; }
        public string Url { get; set; }
        public string ModuloNombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenReferencia { get; set; }
    }
}