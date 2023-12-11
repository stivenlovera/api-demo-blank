using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class ConfigCambioEmpresaRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<ConfigCambioEmpresaRepository> logger;
        private readonly IDbConnection connection;
        public ConfigCambioEmpresaRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<ConfigCambioEmpresaRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<int> InsertConfigCambioEmpresa(List<ConfigCambioEmpresa> configCambioEmpresas)
        {
            this.logger.LogInformation("ConfigCambioEmpresaRepository/InsertConfigCambioEmpresa({configCambioEmpresas})", Helper.Log(configCambioEmpresas));
            var query = ConfigCambioEmpresaSql.InsertConfigCambioEmpresa();
            var consolidado = await connection.ExecuteAsync(query, configCambioEmpresas);
            return consolidado;
        }
    }
}