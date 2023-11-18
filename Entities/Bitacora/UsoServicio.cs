using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Bitacora
{
    public class UsoServicio
    {
        [Key]
        public int uso_servicio_id { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string estado { get; set; }
        public string observaciones { get; set; }
        public int tiempo_ejecucion { get; set; }
        public int servicio_id { get; set; }
        public Servicio servicio { get; set; }
    }
}