using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities.DBComisiones;
using api_guardian.Entities.GrdSion;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/asesor")]
    public class AsesorController : ControllerBase
    {
        private readonly ILogger<AsesorController> _logger;
        private readonly AdministracionContactoModule administracionContactoModule;

        public AsesorController(
            ILogger<AsesorController> _logger,
            AdministracionContactoModule administracionContactoModule
        )
        {
            this._logger = _logger;
            this.administracionContactoModule = administracionContactoModule;
        }

        [HttpGet]
        public async Task<ResponseDto<List<AdministracionContacto>>> ObtenerTodo()
        {
            this._logger.LogInformation("{methodo}{path} ObtenerTodo() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await this.administracionContactoModule.ObtenerTodo();
                this._logger.LogWarning("ObtenerTodo() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<List<AdministracionContacto>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Lista de freelancer"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AdministracionContacto>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("ObtenerTodo() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpGet("asesor-select")]
        public async Task<ResponseDto<List<AdministracionContacto>>> ObtenerTodoSelect()
        {
            this._logger.LogInformation("{methodo}{path} ObtenerTodoSelect() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await this.administracionContactoModule.ObtenerTodoSelect();
                this._logger.LogWarning("ObtenerTodoSelect() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<List<AdministracionContacto>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Lista de freelancer"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AdministracionContacto>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("ObtenerTodoSelect() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto<AdministracionContacto>> ObtenerUno(int id)
        {
            this._logger.LogInformation("{methodo}{path} ObtenerUno({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var resultado = await this.administracionContactoModule.ObtenerUno(id);
                this._logger.LogWarning("ObtenerUno() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<AdministracionContacto>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Obtener freelancer"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AdministracionContacto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("ObtenerUno() ERROR => {e}", e.Message);
                return response;
            }
        }
       /*  [HttpPost]
        public async Task<ResponseDto<AdministracionContacto>> Guardar([FromBody] AdministracionContacto administracionContacto)
        {
            this._logger.LogInformation("{methodo}{path} Guardar({administracionContacto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(administracionContacto);
            try
            {
                var resultado = await this.administracionContactoModule.ObtenerUno(1);
                this._logger.LogWarning("Guardar() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<AdministracionContacto>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Registrado correctamente"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AdministracionContacto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("Guardar() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponseDto<AdministracionContacto>> Modificar(int id, [FromBody] AdministracionContacto administracionContacto)
        {
            this._logger.LogInformation("{methodo}{path} Modificar({id}{administracionContacto}) Inizialize ...", Request.Method, Request.Path, id, Helper.Log(administracionContacto));
            try
            {
                var resultado = await this.administracionContactoModule.ObtenerTodoUno(id);
                this._logger.LogWarning("Modificar() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<AdministracionContacto>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Modificado correctamente"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<AdministracionContacto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("Modificar() ERROR => {e}", e.Message);
                return response;
            }
        } */
    }
}