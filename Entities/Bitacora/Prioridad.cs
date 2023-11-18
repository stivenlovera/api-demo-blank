using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Bitacora
{
    public class Prioridad
    {
        [Key]
        public int prioridad_id { get; set; }
        public string nombre { get; set; }
        public List<Servicio> servicios { get; set; }
    }
}