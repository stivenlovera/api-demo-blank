using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities.Guardian;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/modulo")]
    public class ModuloController : ControllerBase
    {
        private readonly ILogger<ModuloController> logger;
        private readonly ModuloModule moduloModule;

        public ModuloController(
            ILogger<ModuloController> logger,
            ModuloModule moduloModule
        )
        {
            this.logger = logger;
            this.moduloModule = moduloModule;
        }
        [HttpGet("Data-table")]
        public async Task<ResponseDto<List<Modulo>>> DataTable()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var DataTable = await this.moduloModule.DataTable();
                var response = new ResponseDto<List<Modulo>>
                {
                    Status = 1,
                    Data = DataTable,
                    Message = "Tabla de Modulos"
                };
                this.logger.LogWarning("DataTable() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<Modulo>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPost]
        public async Task<ResponseDto<List<Modulo>>> Store()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var DataTable = await this.moduloModule.DataTable();
                var response = new ResponseDto<List<Modulo>>
                {
                    Status = 1,
                    Data = DataTable,
                    Message = "Tabla de Modulos"
                };
                this.logger.LogWarning("DataTable() => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<Modulo>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        }
        [HttpPut]
        public async Task<ResponseDto<List<Modulo>>> Update()
        {
            this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var DataTable = await this.moduloModule.DataTable();
                var response = new ResponseDto<List<Modulo>>
                {
                    Status = 1,
                    Data = DataTable,
                    Message = "Tabla de Modulos"
                };
                this.logger.LogWarning("DataTable() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<Modulo>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
                return response;

            }
        }
    }
}