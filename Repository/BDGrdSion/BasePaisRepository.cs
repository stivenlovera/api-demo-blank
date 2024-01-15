using System.Data;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class BasePaisRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<BasePaisRepository> logger;
        private readonly IDbConnection connection;
        public BasePaisRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<BasePaisRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<List<BasePais>> ObtenerTodo()
        {
            this.logger.LogInformation("BasePaisRepository/ObtenerTodo()");
            var query = BasePaisSql.ObtenerPaises();
            var modulos = await connection.QueryAsync<BasePais>(query);
            this.logger.LogInformation("ObtenerTodo SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
    }
}