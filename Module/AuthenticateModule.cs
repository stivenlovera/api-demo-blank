using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Entities.Guardian;
using api_guardian.Repository;
using api_guardian.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api_guardian.Module
{
    public class AuthenticateModule
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthenticateModule> logger;
        private readonly AutorizadoRepository autorizadoRepository;
        private readonly FuncionarioModule funcionarioModule;
        private readonly RolFuncionarioRepository rolFuncionarioRepository;
        private readonly RolModuloRepository rolModuloRepository;
        private readonly RolSubModuloRepository rolSubModuloRepository;
        private readonly RolComponenteRepository rolComponenteRepository;

        public AuthenticateModule(
            IConfiguration configuration,
            ILogger<AuthenticateModule> logger,
            AutorizadoRepository autorizadoRepository,
            FuncionarioModule funcionarioModule,
            RolFuncionarioRepository rolFuncionarioRepository,
            RolModuloRepository rolModuloRepository,
            RolSubModuloRepository rolSubModuloRepository,
            RolComponenteRepository rolComponenteRepository
        )
        {
            this.configuration = configuration;
            this.logger = logger;
            this.autorizadoRepository = autorizadoRepository;
            this.funcionarioModule = funcionarioModule;
            this.rolFuncionarioRepository = rolFuncionarioRepository;
            this.rolModuloRepository = rolModuloRepository;
            this.rolSubModuloRepository = rolSubModuloRepository;
            this.rolComponenteRepository = rolComponenteRepository;
        }
        public async Task<ResLogin> Login(ReqLogin reqLogin)
        {
            this.logger.LogInformation("AuthenticateModule/Login({loginDto})", Helper.Log(reqLogin));
            //validar login
            var validate = await this.autorizadoRepository.GetLogin(reqLogin.Usuario, reqLogin.Password);
            var roles = await this.rolFuncionarioRepository.GetAllByFuncionarioId(validate.FuncionarioId);
            var funcionario = await this.funcionarioModule.GetFuncionario(validate.FuncionarioId);
            if (funcionario != null)
            {
                var resultado = new ResLogin
                {
                    Nombre = funcionario.Nombres,
                    Rol = string.Join(", ", roles.Select(x => x.NombreRol).ToList()),
                    Cargo = funcionario.NombreCargo,
                    Apellido = funcionario.Apellidos,
                    Autheticate = this.GenerateToken(reqLogin, funcionario.FuncionarioId),
                    Modulo = await this.Roles(funcionario.FuncionarioId)
                };
                return resultado;
            }
            else
            {
                throw new Exception("Credenciales no validas");
            }
        }
        public bool Logout(int funcionario_id)
        {
            this.logger.LogInformation("AuthenticateModule/Login({loginDto})", Helper.Log(funcionario_id));
            //validar login

            return true;
        }
        public async Task<TokenDto> RefreshToken(int funcionarioId)
        {
            this.logger.LogInformation("AuthenticateModule/RefreshToken({funcionarioId})", funcionarioId);
            //validar login

            var getUsuario = await this.autorizadoRepository.EditOne(funcionarioId);
            this.logger.LogInformation("Usuario ({getUsuario})", Helper.Log(getUsuario));
            var usuario = new ReqLogin()
            {
                Usuario=getUsuario.Usuario,
                Password=getUsuario.Password,
                BrowserName="",
                BrowserVersion="",
                Mobile=false,
                OsName=""
            };
            return this.GenerateToken(usuario, funcionarioId);
        }
        public TokenDto GenerateToken(ReqLogin reqLogin, int funcionario)
        {
            this.logger.LogInformation("AuthenticateModule/GenerateToken({loginDto})", Helper.Log(reqLogin));
            var user = new IdentityUser
            {
                Id = "1",
                UserName = reqLogin.Usuario
            };
            //create identity
            var claims = new List<Claim>(){
                new Claim("funcionario_id", funcionario.ToString() ),
                new Claim("Usuario", reqLogin.Usuario),
                new Claim("Password" , reqLogin.Password)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetSection("KeyTokken").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(this.configuration.GetSection("TimeLifeToken").Get<int>());
            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: cred
            );

            var token = new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
            this.logger.LogInformation($"GenerateToken() SUCCESS => Token generado correctamente");
            return token;
        }
        public async Task<List<ModuloAuthenticate>> Roles(int funcionarioId)
        {
            var roles = await this.rolFuncionarioRepository.GetAllByFuncionarioId(funcionarioId);
            //var rolesId = roles.Select(x => x.RolId).ToList();
            var resultadoModulos = new List<ModuloAuthenticate>();
            foreach (var rol in roles)
            {
                var modulos = await this.rolModuloRepository.GetAllByRolId(rol.RolId);
                foreach (var modulo in modulos)
                {
                    var subModulos = await this.rolSubModuloRepository.GetAllByRolModuloId(modulo.RolModuloId);
                    foreach (var subModulo in subModulos)
                    {
                        var componentes = await this.rolComponenteRepository.GetAllByRolSubModuloId(subModulo.RolSubModuloId);
                        subModulo.Componente = componentes;
                    }
                    modulo.SubModulo = subModulos;

                    resultadoModulos.Add(modulo);
                }
                rol.Modulo = modulos;
            }
            return resultadoModulos;
        }

        public async Task<RespAuthenticateDto> Authenticate(int funcionarioId)
        {
            this.logger.LogInformation("AuthenticateModule/Authenticate({funcionarioId})", funcionarioId);
            //validar 
            var funcionario = await this.funcionarioModule.GetFuncionario(funcionarioId);
            if (funcionario != null)
            {
                var roles = await this.rolFuncionarioRepository.GetAllByFuncionarioId(funcionarioId);
                var resultado = new RespAuthenticateDto
                {
                    Nombre = funcionario.Nombres,
                    Rol = string.Join(", ", roles.Select(x => x.NombreRol).ToList()),
                    Cargo = funcionario.NombreCargo,
                    Apellido = funcionario.Apellidos,
                    Modulo = await this.Roles(funcionario.FuncionarioId)
                };
                return resultado;
            }
            else
            {
                throw new Exception("Credenciales no validas");
            }
        }
    }
}