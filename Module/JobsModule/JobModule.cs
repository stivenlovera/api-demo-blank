using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.Bitacora;
using api_guardian.Repository;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Module.JobsModule
{
    public class JobModule
    {
        private readonly ILogger<JobModule> _logger;
        private readonly ServicioRespository _servicioRespository;

        public JobModule(
            ILogger<JobModule> logger,
            ServicioRespository servicioRespository
        )
        {
            _logger = logger;
            _servicioRespository = servicioRespository;
        }

        /* public async Task<List<Servicio>> ObtenerLista()
        {
            _logger.LogInformation("JobModule/ObtenerLista()");
            var resultado = await _servicioRespository.GetServicio();
            return resultado;
        } */
    }
}