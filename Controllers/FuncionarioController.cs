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
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly ILogger<FuncionarioController> logger;
        private readonly FuncionarioModule funcionarioModule;

        public FuncionarioController(
            ILogger<FuncionarioController> logger,
            FuncionarioModule funcionarioModule
        )
        {
            this.logger = logger;
            this.funcionarioModule = funcionarioModule;
        }
        [HttpGet("data-table")]
        public async Task<ResponseDto<List<FuncionarioGetAll>>> DataTable()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var DataTable = await this.funcionarioModule.DataTable();
                var response = new ResponseDto<List<FuncionarioGetAll>>
                {
                    Status = 1,
                    Data = DataTable,
                    Message = "Tabla de funcionarios"
                };
                this.logger.LogWarning("DataTable() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<FuncionarioGetAll>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto<FuncionarioGetOne>> Edit(int id)
        {
            this.logger.LogInformation("{methodo}{path} Edit({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var show = await this.funcionarioModule.GetFuncionario(id);
                var response = new ResponseDto<FuncionarioGetOne>
                {
                    Status = 1,
                    Data = show,
                    Message = "Show de funcionario"
                };
                this.logger.LogWarning("Edit() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<FuncionarioGetOne>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Edit() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPost]
        public async Task<ResponseDto<int>> Store([FromBody] Funcionario funcionario)
        {
            this.logger.LogInformation("{methodo}{path} Store({funcionario}) Inizialize ...", Request.Method, Request.Path, funcionario);
            try
            {
                var insert = await this.funcionarioModule.Store(funcionario);
                var response = new ResponseDto<int>
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
                var response = new ResponseDto<int>
                {
                    Status = 0,
                    Data = 0,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Insert() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        /*
        [HttpPut]
        public async Task<ResponseDto<Funcionario>> Update([FromBody] Funcionario funcionario, int id)
        {
            this.logger.LogInformation("{methodo}{path} Update({id},{funcionario}) Inizialize ...", Request.Method, Request.Path, id, funcionario);
            try
            {
                var editar = await this.funcionarioModule.Update(funcionario);
                var response = new ResponseDto<Funcionario>
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
                var response = new ResponseDto<Funcionario>
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
                var eliminar = await this.funcionarioModule.Delete(id);
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