using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Bitacora
{
    public class JobTime
    {
        [Key]
        public int JobTimeId { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }
    }
}