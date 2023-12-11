using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class EstadoReporteRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<EstadoReporteRepository> logger;
        private readonly IDbConnection connection;
        public EstadoReporteRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<EstadoReporteRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<List<EstadoReporte>> ObtenerTodo()
        {
            this.logger.LogInformation("EstadoReporteRepository/ObtenerTodo()");
            var query = EstadoReporteSql.ObtenerTodo();
            var consolidado = await connection.QueryAsync<EstadoReporte>(query);
            return consolidado.ToList();
        }
    }
}