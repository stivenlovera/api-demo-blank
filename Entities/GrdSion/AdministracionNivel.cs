using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.GrdSion
{
    public class AdministracionNivel
    {
        public int Susuarioadd { get; set; }
        public int Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public int Dtfechamod { get; set; }
        public int LnivelId { get; set; }
        public string Ssigla { get; set; }
        public string Snombre { get; set; }
        public decimal Ddesde { get; set; }
        public decimal Dhasta { get; set; }
        public decimal Dbono { get; set; }
        public decimal Dbonomembresia { get; set; }
    }
}