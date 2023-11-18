using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.Guardian
{
    public class Autorizacion
    {
        [Key]
        public int AutorizacionId { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Activo { get; set; }
        public int FuncionarioId { get; set; }
    }
    [Keyless]
    public class AutorizadoData : Autorizacion
    {
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
    }
}