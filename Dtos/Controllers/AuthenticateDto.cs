using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;

namespace api_guardian.Dtos.Controllers
{
    public class ReqLogin
    {
        public string BrowserVersion { get; set; }
        public string BrowserName { get; set; }
        public string OsName { get; set; }
        public string Password { get; set; }
        public string Usuario { get; set; }
        public bool Mobile { get; set; }
    }
    public class ResLogin
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string Rol { get; set; }
        public TokenDto Autheticate { get; set; }
        public List<ModuloAuthenticate> Modulo { get; set; }
    }
    public class RespAuthenticateDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string Rol { get; set; }
        public List<ModuloAuthenticate> Modulo { get; set; }
    }
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
    public class RolAuthenticate : Rol
    {
        public int RolFuncionarioId { get; set; }
        public int FuncionarioId { get; set; }
        public List<ModuloAuthenticate> Modulo { get; set; }
    }
    public class ModuloAuthenticate : Modulo
    {
        public int RolModuloId { get; set; }
        public int RolId { get; set; }
        public List<SubModuloAuthenticate> SubModulo { get; set; }
    }
    public class SubModuloAuthenticate : SubModulo
    {
        public int RolSubModuloId { get; set; }
        public int RolModuloId { get; set; }
        public List<ComponenteAuthenticate> Componente { get; set; }
    }
    public class ComponenteAuthenticate : Componentes
    {
        public int RolComponenteId { get; set; }
        public int RolSubModuloId { get; set; }
    }
}