using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Repository;

namespace api_guardian.Module
{
    public class AdministracionCicloModule
    {
        private readonly ILogger<AdministracionCicloModule> logger;
        private readonly AdministracionCicloRepository administracionCicloRepository;

        public AdministracionCicloModule(
            ILogger<AdministracionCicloModule> logger,
            AdministracionCicloRepository administracionCicloRepository
        )
        {
            this.logger = logger;
            this.administracionCicloRepository = administracionCicloRepository;
        }
        public async Task<List<AdministracionCiclo>> GetAll()
        {
            this.logger.LogInformation("AdministracionCicloModule/GetAll()");
            var listaCiclos = await administracionCicloRepository.GetAll();
            return listaCiclos;
        }
    }
}