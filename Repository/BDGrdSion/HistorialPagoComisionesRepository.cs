using System.Data;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class HistorialPagoComisionesRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<HistorialPagoComisionesRepository> logger;
        private readonly IDbConnection connection;
        public HistorialPagoComisionesRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<HistorialPagoComisionesRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<int> InsertPagoComision(List<HistorialPagoComisionConsolidado> HistorialPagoComisionConsolidado)
        {
            this.logger.LogInformation("HistorialPagoComisionesRepository/InsertHistorialPagoComision({HistorialPagoComisionConsolidado})",Helper.Log(HistorialPagoComisionConsolidado));
            var query = HistorialPagoConsolidadoSql.InsertConsolidadoPago();
            var consolidado = await connection.ExecuteAsync(query, HistorialPagoComisionConsolidado);
            return consolidado;
        }
        public async Task<List<HistorialPagoComisionConsolidado>> ObtenerTodoByPagoComisionId(int PagoConsolidadoId)
        {
            this.logger.LogInformation("HistorialPagoComisionesRepository/ObtenerTodoByPagoComisionId({pagoComisioneId})", PagoConsolidadoId);
            var query = HistorialPagoConsolidadoSql.ObtenerTodo();
            var ComisionConsolidado = await connection.QueryAsync<HistorialPagoComisionConsolidado>(query, new { PagoConsolidadoId });
            this.logger.LogInformation("HistorialPagoComisionesRepository/ObtenerTodoByPagoComisionId => {ComisionConsolidado} regisros ", ComisionConsolidado.Count());
            return ComisionConsolidado.ToList();
        }
    }
}