using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities.GrdSion;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/nivel")]
    public class NivelController : ControllerBase
    {
        private readonly ILogger<NivelController> logger;
        private readonly AdministracionNivelModule administracionNivelModule;

        public NivelController(
            ILogger<NivelController> logger,
            AdministracionNivelModule administracionNivelModule
        )
        {
            this.logger = logger;
            this.administracionNivelModule = administracionNivelModule;
        }
        [HttpGet]
        public async Task<ResponseDto<List<AdministracionNivel>>> ObtenerTodo()
        {
            this.logger.LogInformation("{methodo}{path} ObtenerTodo() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultados = await this.administracionNivelModule.ObtenerTodo();
                this.logger.LogInformation("ObtenerTodo() SUCCESS => {response}", Helper.Log(resultados));
                var response = new ResponseDto<List<AdministracionNivel>>
                {
                    Status = 1,
                    Data = resultados,
                    Message = "Lista de paises"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<AdministracionNivel>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("ObtenerTodo() ERROR => {e}", e.Message);
                return response;
            }
        }
    }
}