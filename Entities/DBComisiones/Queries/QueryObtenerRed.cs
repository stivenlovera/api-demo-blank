using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.DBComisiones.Queries
{
    [Keyless]
    public class QueryObtenerRed
    {
        public string Scedulaidentidad { get; set; }
        public int Lcontacto_id { get; set; }
        public int Lcontrato_id { get; set; }
        public int Cantidad { get; set; }
        public string SnombreCompleto { get; set; }
        public int Lgeneracion { get; set; }
        public DateTime Dtfecha { get; set; }
        public int Lciclo_id { get; set; }
        public string Cbaja { get; set; }
    }
}