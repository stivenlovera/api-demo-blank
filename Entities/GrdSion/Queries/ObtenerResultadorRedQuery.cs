using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.GrdSion.Queries
{
    [Keyless]
    public class ObtenerResultadorRedQuery
    {
        public DateTime Dtfechaadd { get; set; }
        public string Scedulaidentidad { get; set; }
        public DateTime Dtfecha { get; set; }
        public string Snombrecompleto { get; set; }
        public int Lcontacto_id { get; set; }
        public int Lpatrocinante_id { get; set; }
        public int Lnivel_id { get; set; }
        public int Lcontrato_id { get; set; }
        public string Slote { get; set; }
        public string Smanzano { get; set; }
        public string Cbaja { get; set; }
        public string Stelefonomovil { get; set; } 
    }
}