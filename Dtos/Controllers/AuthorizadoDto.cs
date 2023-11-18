using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;

namespace api_guardian.Dtos.Controllers
{
    public class AuthorizadoDto:Autorizacion
    {
        public List<Rol> Roles { get; set; }
    }
}