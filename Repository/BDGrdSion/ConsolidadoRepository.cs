using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class ConsolidadoRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<AdministracionEmpresaRepository> logger;
        private readonly IDbConnection connection;
        public ConsolidadoRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<AdministracionEmpresaRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();

        }
        public async Task<List<ConsolidadoQuery>> GetAll(int cicloId, List<int> empresaId)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/GetAll({cicloId},{empresaId})", cicloId, Helper.Log(empresaId));
            var query = ConsolidadoSql.ObtenerConsolidado();
            var consolidado = await connection.QueryAsync<ConsolidadoQuery>(query, new { cicloId, empresaId });

            return consolidado.ToList();
        }
        
    }
}