using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities;
using api_guardian.Entities.DBComisiones;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Repository
{
    public class AdministracionCicloRepository
    {
        private readonly ILogger<AdministracionCicloRepository> logger;
        private readonly DbContextGrdSion dbContextGrdSion;

        public AdministracionCicloRepository(
            ILogger<AdministracionCicloRepository> logger,
            DbContextGrdSion dbContextGrdSion
        )
        {
            this.logger = logger;
            this.dbContextGrdSion = dbContextGrdSion;
        }
        public async Task<AdministracionCiclo> GetOne(long lcicloId)
        {
            this.logger.LogInformation("AdministracionCicloRespository/GetOne({lcicloId})", lcicloId);
            var resultado = await this.dbContextGrdSion.AdministracionCiclo.Where(x => x.lcicloId == lcicloId).FirstOrDefaultAsync();
            this.logger.LogInformation("AdministracionCicloRespository/GetOne SUCCESS => {resultado} registros", resultado != null ? 1 : 0);
            return resultado;
        }
        public async Task<List<AdministracionCiclo>> GetAll()
        {
            this.logger.LogInformation("AdministracionCicloRespository/GetAll()");
            var resultado = await this.dbContextGrdSion.AdministracionCiclo.OrderByDescending(x => x.lcicloId).ToListAsync();
            this.logger.LogInformation("AdministracionCicloRespository/GetAll SUCCESS => {resultado} registros", resultado.Count);
            return resultado;
        }
    }
}