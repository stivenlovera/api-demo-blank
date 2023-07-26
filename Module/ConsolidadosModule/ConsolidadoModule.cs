using api_guardian.Contexts;
using api_guardian.Dtos.Module;
using api_guardian.SqlQuerys.GrdSion;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_guardian.Module.ConsolidadosModule
{
    public class ConsolidadoModule
    {
        private readonly ILogger<ConsolidadoModule> _logger;
        private readonly DbContextGrdSion _dbContextGrdSion;
        private readonly ConsolidadoSql _consolidadoSql;

        public ConsolidadoModule(
            ILogger<ConsolidadoModule> logger,
            DbContextGrdSion dbContextGrdSion,
            ConsolidadoSql consolidadoSql
        )
        {
            this._logger = logger;
            this._dbContextGrdSion = dbContextGrdSion;
            this._consolidadoSql = consolidadoSql;
        }
        public async Task<ConsolidadoModuleDto> Index()
        {
            _logger.LogInformation($"ConsolidadoModule/Index() => iniciando...");
            var sql = this._consolidadoSql.obtenerConsolidado(107);
            var obtenerConsolidado = await this._dbContextGrdSion.ObtenerConsolidadoViews.FromSqlRaw(sql).ToListAsync();
            _logger.LogInformation($"ConsolidadoModule/Index() => {obtenerConsolidado.Count} registros encontrados ");
            var resultado = new ConsolidadoModuleDto()
            {
                cicloActual = "Junio"
            };
            return resultado;
        }
    }
}