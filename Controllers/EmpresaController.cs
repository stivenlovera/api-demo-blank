using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/empresa")]
    public class EmpresaController : ControllerBase
    {
        private readonly ILogger<EmpresaController> logger;
        private readonly AdministracionEmpresaModule administracionEmpresaModule;

        public EmpresaController(
            ILogger<EmpresaController> logger,
            AdministracionEmpresaModule AdministracionEmpresaModule
        )
        {
            this.logger = logger;
            administracionEmpresaModule = AdministracionEmpresaModule;
        }
        [HttpGet]
        public async Task<ResponseDto<List<AdministracionEmpresa>>> DataTable()
        {
            this.logger.LogInformation("{methodo}{path} Index() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await administracionEmpresaModule.DataTable();
                var response = new ResponseDto<List<AdministracionEmpresa>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Todas la empresas"
                };
                this.logger.LogWarning("Index() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AdministracionEmpresa>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Index() SUCCESS => {e}", e.Message);
                return response;

            }
        }
       /*  [HttpGet("editar/{id}")]
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
        } */
    }
}