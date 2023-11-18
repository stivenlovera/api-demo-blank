using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities.Guardian;
using api_guardian.Module;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/autorizado")]
    public class AutorizadoController : ControllerBase
    {
        private readonly ILogger<AutorizadoController> logger;
        private readonly AutorizadoModule autorizadoModule;

        public AutorizadoController(
            ILogger<AutorizadoController> logger,
            AutorizadoModule autorizadoModule
        )
        {
            this.logger = logger;
            this.autorizadoModule = autorizadoModule;
        }
        /* [HttpGet("data-table")]
        public async Task<ResponseDto<List<AutorizadoData>>> DataTable()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var dataTable = await this.autorizadoModule.DataTable();
                var response = new ResponseDto<List<AutorizadoData>>
                {
                    Status = 1,
                    Data = dataTable,
                    Message = "Tabla de autorizados"
                };
                this.logger.LogWarning("DataTable() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AutorizadoData>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        } */
        [HttpGet("{id}")]
        public async Task<ResponseDto<AuthorizadoDto>> Edit(int id)
        {
            this.logger.LogInformation("{methodo}{path} Edit({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var editar = await this.autorizadoModule.EditOne(id);
                var response = new ResponseDto<AuthorizadoDto>
                {
                    Status = 1,
                    Data = editar,
                    Message = "Editar de funcionario"
                };
                this.logger.LogInformation("Edit() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AuthorizadoDto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Edit() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPut("{id}")]
        public async Task<ResponseDto<Autorizacion>> Update([FromBody] AuthorizadoDto autorizado, int id)
        {
            this.logger.LogInformation("{methodo}{path} Update({id},{funcionario}) Inizialize ...", Request.Method, Request.Path, id, autorizado);
            try
            {
                var editar = await this.autorizadoModule.Update(autorizado, id);
                var response = new ResponseDto<Autorizacion>
                {
                    Status = 1,
                    Data = null,
                    Message = editar
                };
                this.logger.LogWarning("Update() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<Autorizacion>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Update() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        /* [HttpPost]
        public async Task<ResponseDto<Autorizacion>> Insert([FromBody] Autorizacion autorizado)
        {
            this.logger.LogInformation("{methodo}{path} Insert({funcionario}) Inizialize ...", Request.Method, Request.Path, autorizado);
            try
            {
                var insert = await this.autorizadoModule.Store(autorizado);
                var response = new ResponseDto<Autorizacion>
                {
                    Status = 1,
                    Data = insert,
                    Message = "Registrado correctamente"
                };
                this.logger.LogWarning("Insert() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<Autorizacion>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Insert() SUCCESS => {e}", e.Message);
                return response;

            }
        }
       
        [HttpDelete("{id}")]
        public async Task<ResponseDto<int>> Delete(int id)
        {
            this.logger.LogInformation("{methodo}{path} Delete({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var eliminar = await this.autorizadoModule.Delete(id);
                var response = new ResponseDto<int>
                {
                    Status = 1,
                    Data = eliminar,
                    Message = "Eliminado correctamente"
                };
                this.logger.LogWarning("Delete() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<int>
                {
                    Status = 0,
                    Data = 0,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Delete() SUCCESS => {e}", e.Message);
                return response;

            }
        } */
    }
}