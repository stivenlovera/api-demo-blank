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
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly ILogger<RolController> logger;
        private readonly RolModule rolModule;

        public RolController(
            ILogger<RolController> logger,
            RolModule rolModule
        )
        {
            this.logger = logger;
            this.rolModule = rolModule;
        }
        [HttpGet]
        public async Task<ResponseDto<List<Rol>>> GetAll()
        {
            this.logger.LogInformation("{methodo}{path} GetAll() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var DataTable = await this.rolModule.DataTable();
                var response = new ResponseDto<List<Rol>>
                {
                    Status = 1,
                    Data = DataTable,
                    Message = "Todos los roles"
                };
                this.logger.LogWarning("GetAll() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<Rol>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("GetAll() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto<Rol>> Edit(int id)
        {
            this.logger.LogInformation("{methodo}{path} Edit({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var editar = await this.rolModule.Edit(id);
                var response = new ResponseDto<Rol>
                {
                    Status = 1,
                    Data = editar,
                    Message = "Editar de funcionario"
                };
                this.logger.LogWarning("Edit() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<Rol>
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
        public async Task<ResponseDto<Rol>> Insert([FromBody] Rol rol)
        {
            this.logger.LogInformation("{methodo}{path} Insert({funcionario}) Inizialize ...", Request.Method, Request.Path, JsonConvert.SerializeObject(rol, Formatting.Indented));
            try
            {
                var insert = await this.rolModule.Store(rol);
                var response = new ResponseDto<Rol>
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
                var response = new ResponseDto<Rol>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Insert() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPut("{id}")]
        public async Task<ResponseDto<Rol>> Update([FromBody] Rol rol, int id)
        {
            this.logger.LogInformation("{methodo}{path} Update({id},{funcionario}) Inizialize ...", Request.Method, Request.Path, id, rol);
            try
            {
                var editar = await this.rolModule.Update(rol);
                var response = new ResponseDto<Rol>
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
                var response = new ResponseDto<Rol>
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
                var eliminar = await this.rolModule.Delete(id);
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
        }
    }
}