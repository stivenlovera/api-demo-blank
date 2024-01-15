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
    public class AdministracionNiveRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<AdministracionNiveRepository> logger;
        private readonly IDbConnection connection;
        public AdministracionNiveRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<AdministracionNiveRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<List<AdministracionNivel>> ObtenerTodo()
        {
            this.logger.LogInformation("AdministracionNiveRepository/ObtenerTodo()");
            var query = AdministracionNivelSql.ObtenerTodoSelect();
            var modulos = await connection.QueryAsync<AdministracionNivel>(query);
            this.logger.LogInformation("ObtenerTodo SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
    }
}