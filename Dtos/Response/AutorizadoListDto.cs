using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Dtos.Response
{
    public class AutorizadoListDto
    {
        public int AutorizacionId { get; set; }
        public string Documento { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Activo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int FuncionarioId { get; set; }
    }
}