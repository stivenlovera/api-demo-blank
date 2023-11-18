using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.Controllers;
using api_guardian.SqlQuerys.DBGuardian;
using Dapper;

namespace api_guardian.Repository
{
    public class RolComponenteRepository
    {
        private readonly ILogger<RolComponenteRepository> logger;
        private readonly DBGuardianContext dBGuardianContext;
        private readonly IDbConnection connection;

        public RolComponenteRepository(
            ILogger<RolComponenteRepository> logger,
            DBGuardianContext dBGuardianContext
        )
        {
            this.logger = logger;
            this.dBGuardianContext = dBGuardianContext;
            this.connection = this.dBGuardianContext.CreateConnection();
        }
        public async Task<List<ComponenteAuthenticate>> GetAllByRolSubModuloId(int RolSubModuloId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByRolSubModuloId({RolSubModuloId})", RolSubModuloId);
            var query = RolComponenteQuery.GetAllByRolSubModulo();
            var modulos = await connection.QueryAsync<ComponenteAuthenticate>(query, new { RolSubModuloId });
            return modulos.ToList();
        }
    }
}