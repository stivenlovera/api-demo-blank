using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> logger;
        private readonly AuthenticateModule authenticateModule;

        public AuthenticateController(
            ILogger<AuthenticateController> logger,
            AuthenticateModule authenticateModule
        )
        {
            this.logger = logger;
            this.authenticateModule = authenticateModule;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ResponseDto<ResLogin>> Login([FromBody] ReqLogin reqLogin)
        {
            this.logger.LogInformation("{methodo}{path} Login() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var authenticate = await this.authenticateModule.Login(reqLogin);
                var response = new ResponseDto<ResLogin>
                {
                    Status = 1,
                    Message = "login",
                    Data = authenticate,
                };
                this.logger.LogInformation("Login() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<ResLogin>
                {
                    Status = 0,
                    Data = null,
                    Message = "Credenciales no validas"
                };
                this.logger.LogCritical("Login() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet("refresh-token")]
        public async Task<ResponseDto<TokenDto>> RefreshToken([FromHeader] string Authorization)
        {
            var getTokenLimpio = Authorization.Remove(0, 7);
            var funcionarioId = TokenResolver.GetFuncionarioId(HttpContext);
            var refreshToken = await this.authenticateModule.RefreshToken(funcionarioId);
            this.logger.LogInformation("{methodo}{path} RefreshToken() Inizialize ...", Request.Method, Request.Path);
            try
            {

                var response = new ResponseDto<TokenDto>
                {
                    Status = 1,
                    Message = "login",
                    Data = refreshToken,
                };
                this.logger.LogInformation("RefreshToken() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<TokenDto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Credenciales no validas"
                };
                this.logger.LogCritical("RefreshToken() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet("logout")]
        public ResponseDto<ResLogin> Logout([FromHeader] string Authorization)
        {
            this.logger.LogInformation("{methodo}{path} Logout() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var getTokenLimpio = Authorization.Remove(0, 7);
                var funcionarioId = TokenResolver.GetFuncionarioId(HttpContext);
                var logout = this.authenticateModule.Logout(funcionarioId);
                var response = new ResponseDto<ResLogin>
                {
                    Status = 1,
                    Message = "session cerrada",
                    Data = null,
                };
                this.logger.LogInformation("Logout() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<ResLogin>
                {
                    Status = 0,
                    Data = null,
                    Message = "error al cerrar session"
                };
                this.logger.LogCritical("Logout() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet]
        public async Task<ResponseDto<RespAuthenticateDto>> Authenticate()
        {
            this.logger.LogInformation("{methodo}{path} Autnenticate() Inizialize ...", Request.Method, Request.Path);
            var funcionarioId = TokenResolver.GetFuncionarioId(HttpContext);
            try
            {
                var authenticate = await this.authenticateModule.Authenticate(funcionarioId);
                var response = new ResponseDto<RespAuthenticateDto>
                {
                    Status = 1,
                    Message = "Verficando authenticacion",
                    Data = authenticate,
                };
                this.logger.LogInformation("Autnenticate() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<RespAuthenticateDto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Credenciales no validas"
                };
                this.logger.LogCritical("Autnenticate() SUCCESS => {e}", e.Message);
                return response;

            }
        }
    }
}