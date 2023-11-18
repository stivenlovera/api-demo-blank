using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Guardian
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string NombreRol { get; set; }
    }
}