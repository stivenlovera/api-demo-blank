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
    public class RolSubModuloRepository
    {
        private readonly ILogger<RolSubModuloRepository> logger;
        private readonly DBGuardianContext dBGuardianContext;
        private readonly IDbConnection connection;

        public RolSubModuloRepository(
            ILogger<RolSubModuloRepository> logger,
            DBGuardianContext dBGuardianContext
        )
        {
            this.logger = logger;
            this.dBGuardianContext = dBGuardianContext;
            this.connection = this.dBGuardianContext.CreateConnection();
        }
        public async Task<List<SubModuloAuthenticate>> GetAllByRolModuloId(int RolModuloId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByRolModuloId({ModuloId})", RolModuloId);
            var query = RolSubModuloQuery.GetAllByRolSubModuloId();
            this.logger.LogInformation("query ({query})", query);
            var modulos = await connection.QueryAsync<SubModuloAuthenticate>(query, new { rol_modulo_id = RolModuloId });
            return modulos.ToList();
        }
    }
}