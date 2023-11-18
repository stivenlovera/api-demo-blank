using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Entities.Bitacora;
using api_guardian.Module.JobsModule;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobTimeController : ControllerBase
    {
       private readonly ILogger<JobTimeController> _logger;
        private readonly JobTimeModulo _jobTimeModulo;

        public JobTimeController(
            ILogger<JobTimeController> logger,
            JobTimeModulo jobTimeModulo 
        )
        {
            _logger = logger;
            _jobTimeModulo = jobTimeModulo;
        }
        [HttpGet("index")]
        public ResponseDto<List<JobTime>> Index()
        {
            _logger.LogInformation("{methodo}{path} Index() Inizialize ...", Request.Method, Request.Path);
            try
            {
                /* var resultado = await _jobTimeModulo.ObtenerLista(); */
                var response = new ResponseDto<List<JobTime>>
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
                var response = new ResponseDto<List<JobTime>>
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