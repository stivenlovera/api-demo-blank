using api_guardian.Entities.GrdSion;
using api_guardian.Repository.BDGrdSion;

namespace api_guardian.Module
{
    public class PaisModule
    {
        private readonly ILogger<PaisModule> logger;
        private readonly BasePaisRepository basePaisRepository;

        public PaisModule(
            ILogger<PaisModule> logger,
            BasePaisRepository basePaisRepository
        )
        {
            this.logger = logger;
            this.basePaisRepository = basePaisRepository;
        }
        public async Task<List<BasePais>> ObtenerPais()
        {
            this.logger.LogInformation("PaisModule/ObtenerPais()");
            var paises = await this.basePaisRepository.ObtenerTodo();
            return paises;
        }
    }
}