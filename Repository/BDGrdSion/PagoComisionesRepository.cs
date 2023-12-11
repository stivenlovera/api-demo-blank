using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.repository;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class PagoComisionesRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<PagoComisionesRepository> logger;
        private readonly IDbConnection connection;
        public PagoComisionesRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<PagoComisionesRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<InsertGetId> InsertPagoComision(PagoConsolidado pagoConsolidado)
        {
            this.logger.LogInformation("PagoComisionesRepository/InsertPagoComision({pagoConsolidado})", pagoConsolidado);
            var query = PagoConsolidadoSql.InsertarPagoComisiones();
            var consolidado = await connection.QueryAsync<InsertGetId>(query, pagoConsolidado);
            return consolidado.FirstOrDefault();
        }
        public async Task<List<PagoConsolidado>> ObtenerTodo()
        {
            this.logger.LogInformation("PagoComisionesRepository/ObtenerTodo()");
            var query = PagoConsolidadoSql.ObtenerPagoConsolidado();
            var consolidado = await connection.QueryAsync<PagoConsolidado>(query);
            return consolidado.ToList();
        }
    }
}