using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities.Bitacora;
using api_guardian.Module.JobsModule;
using api_guardian.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/job-automaticos")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> _logger;
        private readonly JobModule _jobModule;

        public JobsController(
            ILogger<JobsController> logger,
            JobModule jobModule
        )
        {
            _logger = logger;
            _jobModule = jobModule;
        }
        [HttpGet("index")]
        public ResponseDto<List<Servicio>> Index()
        {
            _logger.LogInformation("{methodo}{path} Index() Inizialize ...", Request.Method, Request.Path);
            try
            {
              /*   var resultado = await _jobModule.ObtenerLista(); */
                var response = new ResponseDto<List<Servicio>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Tabla de resultado"
                };
                _logger.LogWarning("Index() SUCCESS => {response}", JsonConvert.SerializeObject(response, Formatting.Indented));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<Servicio>>
                {
                    Status = 1,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                _logger.LogCritical("Index() SUCCESS => {e}", e.Message);
                return response;

            }
        }
    }
}