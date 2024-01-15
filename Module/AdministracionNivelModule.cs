using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.GrdSion;
using api_guardian.Repository.BDGrdSion;

namespace api_guardian.Module
{
    public class AdministracionNivelModule
    {
        private readonly ILogger<AdministracionNivelModule> logger;
        private readonly AdministracionNiveRepository administracionNiveRepository;

        public AdministracionNivelModule(
            ILogger<AdministracionNivelModule> logger,
            AdministracionNiveRepository administracionNiveRepository
        )
        {
            this.logger = logger;
            this.administracionNiveRepository = administracionNiveRepository;
        }
        public async Task<List<AdministracionNivel>> ObtenerTodo()
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodo()");
            var frelancers = await this.administracionNiveRepository.ObtenerTodo();
            return frelancers;
        }
    }
}