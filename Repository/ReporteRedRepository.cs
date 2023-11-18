using api_guardian.Contexts;
using api_guardian.Entities.DBComisiones.Views;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Entities.GrdSion.Views;
using api_guardian.SqlQuerys.DBComisiones;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Repository
{
    public class ReporteRedRepository
    {
        private readonly ILogger<ReporteRedRepository> logger;
        private readonly DbContextDBComisiones dbContextDBComisiones;
        private readonly DbContextGrdSion dbContextGrdSion;

        public ReporteRedRepository(
            ILogger<ReporteRedRepository> logger,
            DbContextDBComisiones dbContextDBComisiones,
            DbContextGrdSion dbContextGrdSion
        )
        {
            this.logger = logger;
            this.dbContextDBComisiones = dbContextDBComisiones;
            this.dbContextGrdSion = dbContextGrdSion;
        }
        public async Task<List<VwListaVentaGral>> GetAllVentasNuevas(DateTime fechaInicio, DateTime fechaFin)
        {
            this.logger.LogInformation("ReporteRedRepository/GetByFechas({fechaInicio},{fechaFin})", fechaInicio.ToString("yyyy-MM-dd"), fechaFin.ToString("yyyy-MM-dd"));
            this.logger.LogInformation("ReporteRedRepository/QUERY {query}", ObtenerRedesQuery.ObtenerNuevasRedes(fechaInicio, fechaFin));
            var resultado = await this.dbContextDBComisiones.VWLISTAVENTAS_GRAL
                .FromSqlRaw(ObtenerRedesQuery.ObtenerNuevasRedes(fechaInicio, fechaFin))
                .AsNoTracking()
                .ToListAsync();
            this.logger.LogInformation("ReporteRedRepository/GetByFechas SUCCESS => {resultado} registros ", resultado.Count);
            return resultado;
        }
        public async Task<List<QueryDetalleRed>> GetAmpliacionRedes(List<string> snroventa, DateTime fechaInicio, DateTime fechaFin, int cicloId, bool baja, bool autoVenta)
        {
            this.logger.LogInformation("ReporteRedRepository/GetRedesNuevas({lcontratos},{fechaInicio},{fechaFin},{cicloId},{baja},{autoVenta})", Helper.Log(snroventa), fechaInicio.ToString("yyyy-MM-dd"), fechaFin.ToString("yyyy-MM-dd"), cicloId, baja, autoVenta);
            var query = ObtenerRedesGuardianQuery.QueryAmpliacionRedes(snroventa, fechaInicio, fechaFin, cicloId, baja, autoVenta);
            this.logger.LogInformation("query en ejecucion {query}", query);
            var resultado = await this.dbContextGrdSion.QueryDetalleRed.FromSqlRaw(query)
            .AsNoTracking()
            .ToListAsync();
            this.logger.LogInformation("ReporteRedRepository/GetRedesNuevas SUCCESS => {resultado} registros ", resultado.Count);
            return resultado;
        }
        public async Task<List<QueryDetalleRed>> GetNuevasRedes(List<string> snroventa, DateTime fechaInicio, DateTime fechaFin, int cicloId, bool baja, bool autoVenta)
        {
            this.logger.LogInformation("ReporteRedRepository/GetPersonasNuevas({lcontratos},{fechaInicio},{fechaFin},{cicloId},{baja},{autoVenta})", Helper.Log(snroventa), fechaInicio.ToString("yyyy-MM-dd"), fechaFin.ToString("yyyy-MM-dd"), cicloId, baja, autoVenta);
            var query = ObtenerRedesGuardianQuery.QueryRedesNuevas(snroventa, fechaInicio, fechaFin, cicloId, baja, autoVenta);
            this.logger.LogInformation("query en ejecucion {query}", query);
            var resultado = await this.dbContextGrdSion.QueryDetalleRed.FromSqlRaw(query)
            .AsNoTracking()
            .ToListAsync();
            this.logger.LogInformation("ReporteRedRepository/GetPersonasNuevas SUCCESS => {resultado} registros ", resultado.Count);
            return resultado;
        }
    }
}