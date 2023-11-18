using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Guardian
{
    public class Cargo
    {
        public int CargoId { get; set; }
        public string NombreCargo { get; set; }
        public string Descripcion { get; set; }
    }
}