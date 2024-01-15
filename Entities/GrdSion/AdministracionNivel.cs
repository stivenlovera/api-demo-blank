using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.GrdSion
{
    public class AdministracionNivel
    {
        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public int LnivelId { get; set; }
        public string Ssigla { get; set; }
        public string Snombre { get; set; }
        public decimal Ddesde { get; set; }
        public decimal Dhasta { get; set; }
        public decimal Dbono { get; set; }
        public decimal Dbonomembresia { get; set; }
    }
}