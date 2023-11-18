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
    public class RolModuloRepository
    {
        private readonly ILogger<RolModuloRepository> logger;
        private readonly DBGuardianContext dBGuardianContext;
        private readonly IDbConnection connection;

        public RolModuloRepository(
            ILogger<RolModuloRepository> logger,
            DBGuardianContext dBGuardianContext
        )
        {
            this.logger = logger;
            this.dBGuardianContext = dBGuardianContext;
            this.connection = this.dBGuardianContext.CreateConnection();
        }
        public async Task<List<ModuloAuthenticate>> GetAllByRolId(int rolId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByRolId({rolId})", rolId);
            var query = RolModuloQuery.GetAllByRol();
            var modulos = await connection.QueryAsync<ModuloAuthenticate>(query, new { rolId });
            return modulos.ToList();
        }
         public async Task<List<ModuloAuthenticate>> GetAllByRolIds(List<int> rolIds)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByRolIds({rolId})", rolIds);
            var query = RolModuloQuery.GetAllByRolIds(rolIds);
            var modulos = await connection.QueryAsync<ModuloAuthenticate>(query);
            return modulos.ToList();
        }
    }
}