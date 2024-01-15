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
    [Route("api/pais")]
    public class BasePaisController : ControllerBase
    {
        private readonly ILogger<BasePaisController> logger;
        private readonly PaisModule paisModule;

        public BasePaisController(
            ILogger<BasePaisController> logger,
            PaisModule paisModule
        )
        {
            this.logger = logger;
            this.paisModule = paisModule;
        }
        [HttpGet]
        public async Task<ResponseDto<List<BasePais>>> ObtenerTodo()
        {
            this.logger.LogInformation("{methodo}{path} ObtenerTodo() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultados = await this.paisModule.ObtenerPais();
                this.logger.LogInformation("ObtenerTodo() SUCCESS => {response}", Helper.Log(resultados));
                var response = new ResponseDto<List<BasePais>>
                {
                    Status = 1,
                    Data = resultados,
                    Message = "Lista de paises"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<BasePais>>
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