using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Bitacora
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int PrioridadId { get; set; }
        public int JobTimeId { get; set; }
        public int Rutina { get; set; }
        public int RutinaValor { get; set; }
    }
}