using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Bitacora;
using api_guardian.Repository;

namespace api_guardian.Module.JobsModule
{
    public class JobTimeModulo
    {
        private readonly ILogger<JobTimeModulo> _logger;
        private readonly JobTimeRepository _jobTimeRepository;

        public JobTimeModulo(
            ILogger<JobTimeModulo> logger,
            JobTimeRepository jobTimeRepository
        )
        {
            _logger = logger;
            _jobTimeRepository = jobTimeRepository;
        }

       /*  public async Task<List<JobTime>> ObtenerLista()
        {
            _logger.LogInformation("JobTimeModulo/ObtenerLista()");
            var resultado = await _jobTimeRepository.GetJobtime();
            return resultado;
        } */
    }
}