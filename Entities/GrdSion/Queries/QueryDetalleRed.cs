using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.GrdSion.Queries
{
    [Keyless]
    public class QueryDetalleRed
    {
        public int lcontrato_id { get; set; }
        public int lcontacto_id { get; set; }
        public string scedulaidentidad { get; set; }
        public string stelefonomovil { get; set; }
        public string snombrecompleto { get; set; }
        public string cbaja { get; set; }
        public int lciclo_id { get; set; }
        public decimal total_vendido { get; set; }
        public int cantidad { get; set; }
    }
}