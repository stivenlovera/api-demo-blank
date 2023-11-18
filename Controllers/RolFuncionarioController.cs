using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities.Guardian;
using api_guardian.Module;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/rol-funcionario")]
    public class RolFuncionarioController : ControllerBase
    {
        private readonly ILogger<RolFuncionarioController> logger;
        private readonly RolFuncionarioModule rolFuncionarioModule;

        public RolFuncionarioController(
            ILogger<RolFuncionarioController> logger,
            RolFuncionarioModule rolFuncionarioModule
        )
        {
            this.logger = logger;
            this.rolFuncionarioModule = rolFuncionarioModule;
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto<RolFuncionarioOne>> Edit(int id)
        {
            this.logger.LogInformation("{methodo}{path} Edit({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var editar = await this.rolFuncionarioModule.Edit(id);
                var response = new ResponseDto<RolFuncionarioOne>
                {
                    Status = 1,
                    Data = editar,
                    Message = "Editar de rol funcionario"
                };
                this.logger.LogWarning("Edit() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<RolFuncionarioOne>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Edit() SUCCESS => {e}", e.Message);
                return response;

            }
        }
      /*   [HttpGet("Data-table")]
        public async Task<ResponseDto<List<RolFuncionario>>> DataTable()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var DataTable = await this.rolFuncionarioModule.DataTable();
                var response = new ResponseDto<List<RolFuncionario>>
                {
                    Status = 1,
                    Data = DataTable,
                    Message = "Tabla de roles funcionarios"
                };
                this.logger.LogWarning("DataTable() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<RolFuncionario>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        } */
        /*
      
        [HttpPost]
        public async Task<ResponseDto<RolFuncionario>> Store([FromBody] RolFuncionario rolFuncionario)
        {
            this.logger.LogInformation("{methodo}{path} Insert({funcionario}) Inizialize ...", Request.Method, Request.Path, JsonConvert.SerializeObject(rolFuncionario, Formatting.Indented));
            try
            {
                var insert = await this.rolFuncionarioModule.Store(rolFuncionario);
                var response = new ResponseDto<RolFuncionario>
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
                var response = new ResponseDto<RolFuncionario>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Insert() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPut]
        public async Task<ResponseDto<RolFuncionario>> Update([FromBody] RolFuncionario rolFuncionario, int id)
        {
            this.logger.LogInformation("{methodo}{path} Update({id},{rolFuncionario}) Inizialize ...", Request.Method, Request.Path, id, rolFuncionario);
            try
            {
                var editar = await this.rolFuncionarioModule.Update(rolFuncionario);
                var response = new ResponseDto<RolFuncionario>
                {
                    Status = 1,
                    Data = editar,
                    Message = "Modificado correctamente"
                };
                this.logger.LogWarning("Update() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<RolFuncionario>
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
        public async Task<ResponseDto<int>> Delete(int id)
        {
            this.logger.LogInformation("{methodo}{path} Delete({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var eliminar = await this.rolFuncionarioModule.Delete(id);
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