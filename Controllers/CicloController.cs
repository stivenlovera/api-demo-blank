using api_guardian.Dtos.Response;
using api_guardian.Entities;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/ciclo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CicloController : ControllerBase
    {
        private readonly ILogger<CicloController> logger;
        private readonly CicloModule CicloModule;

        public CicloController(
            ILogger<CicloController> logger,
            CicloModule CicloModule
        )
        {
            this.logger = logger;
            this.CicloModule = CicloModule;
        }
        [HttpGet]
        public async Task<ResponseDto<List<AdministracionCiclo>>> Index()
        {
            this.logger.LogInformation("{methodo}{path} Index() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await CicloModule.DataTable();
                var response = new ResponseDto<List<AdministracionCiclo>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Todos los ciclos"
                };
                this.logger.LogWarning("Index() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AdministracionCiclo>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Index() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto<AdministracionCiclo>> GetOne(int id)
        {
            this.logger.LogInformation("{methodo}{path} Index() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await CicloModule.EditUno(id);
                var response = new ResponseDto<AdministracionCiclo>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Todos los ciclos"
                };
                this.logger.LogWarning("Index() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AdministracionCiclo>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Index() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet("data-table")]
        public async Task<ResponseDto<List<AdministracionCiclo>>> DataTable()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await CicloModule.DataTable();
                var response = new ResponseDto<List<AdministracionCiclo>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Todos los ciclos"
                };
                this.logger.LogWarning("DataTable() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AdministracionCiclo>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPost]
        public async Task<ResponseDto<string>> Store([FromBody] AdministracionCiclo administracionCiclo)
        {
            this.logger.LogInformation("{methodo}{path} Store() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var funcionarioId = TokenResolver.GetFuncionarioId(HttpContext);
                var resultado = await CicloModule.Store(administracionCiclo, funcionarioId);
                var response = new ResponseDto<string>
                {
                    Status = 1,
                    Data = null,
                    Message = resultado
                };
                this.logger.LogWarning("Store() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<string>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Store() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPut]
        public async Task<ResponseDto<AdministracionCiclo>> Update([FromBody] AdministracionCiclo administracionCiclo)
        {
            this.logger.LogInformation("{methodo}{path} Update() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var funcionarioId = TokenResolver.GetFuncionarioId(HttpContext);
                var resultado = await CicloModule.Update(administracionCiclo, funcionarioId);
                var response = new ResponseDto<AdministracionCiclo>
                {
                    Status = 1,
                    Data = null,
                    Message = resultado
                };
                this.logger.LogWarning("Update() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AdministracionCiclo>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Update() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpDelete("{id}")]
        public async Task<ResponseDto<AdministracionCiclo>> Delete(int id)
        {
            this.logger.LogInformation("{methodo}{path} Delete() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var funcionarioId = TokenResolver.GetFuncionarioId(HttpContext);
                var resultado = await CicloModule.Delete(id, funcionarioId);
                var response = new ResponseDto<AdministracionCiclo>
                {
                    Status = 1,
                    Data = null,
                    Message = resultado
                };
                this.logger.LogWarning("Delete() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AdministracionCiclo>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Delete() SUCCESS => {e}", e.Message);
                return response;

            }
        }
    }
}