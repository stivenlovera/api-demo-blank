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
    [Route("api/estado-reporte")]
    public class EstadoReporteController : ControllerBase
    {
        private readonly ILogger<EstadoReporteController> logger;
        private readonly EstadoReporteModule estadoReporteModule;

        public EstadoReporteController(
            ILogger<EstadoReporteController> logger,
            EstadoReporteModule estadoReporteModule
        )
        {
            this.logger = logger;
            this.estadoReporteModule = estadoReporteModule;
        }
        [HttpGet]
        public async Task<ResponseDto<List<EstadoReporte>>> MostrarTodo()
        {
            this.logger.LogInformation("{methodo}{path} GetAll()) Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await this.estadoReporteModule.MostrarTodo();
                var response = new ResponseDto<List<EstadoReporte>>()
                {
                    Status = 1,
                    Message = "datos generados correctamente",
                    Data = resultado
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<EstadoReporte>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Verificar() ERROR => {e}", e.Message);
                return response;
            }
        }
    }
}