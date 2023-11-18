using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.Proyeccion;
using api_guardian.SqlQuerys.DBProyeccion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository
{
    public class ProyeccionRepository
    {
        private readonly ILogger<ProyeccionRepository> logger;
        private readonly DBProyeccionContext DBProyeccionContext;
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly IDbConnection connectionProyeccion;
        private readonly IDbConnection connectionGrdSion;

        public ProyeccionRepository(
            ILogger<ProyeccionRepository> logger,
            DBProyeccionContext DBProyeccionContext,
            DBGrdSionContext dBGrdSionContext
        )
        {
            this.DBProyeccionContext = DBProyeccionContext;
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connectionGrdSion = this.dBGrdSionContext.CreateConnection();
            this.connectionProyeccion = this.DBProyeccionContext.CreateConnection();
        }
        public async Task<List<Venta>> GetAllConsolidadoByCicloId(int cicloId)
        {
            this.logger.LogInformation("ProyeccionRepository/GetAllConsolidadoByCiclo({cicloId})", cicloId);
            var query = ProyeccionQuery.GetAllConsolidadosByCicloId();
            var ventasConsolidadas = await connectionGrdSion.QueryAsync<Venta>(query, new { cicloId });
            return ventasConsolidadas.ToList();
        }
        public async Task<List<Venta>> GetAllByCicloId(int cicloId)
        {
            this.logger.LogInformation("ProyeccionRepository/GetAllByCicloId({cicloId})", Helper.Log(cicloId));
            var query = ProyeccionQuery.GetAllVentaByCicloId();

            var listVentas = await connectionProyeccion.QueryAsync<Venta>(query, new { cicloId });
            return listVentas.ToList();
        }
        public async Task<bool> StoreVenta(List<Venta> ventas)
        {
            this.logger.LogInformation("ProyeccionRepository/StoreVenta({ventas})", Helper.Log(ventas));
            var query = ProyeccionQuery.StoreVenta();

            var store = await connectionProyeccion.ExecuteAsync(query, ventas);
            if (store > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("No se inserto correctamente");
            }
        }
        public async Task<bool> StoreCoutas(List<Cuota> cuotas)
        {
            this.logger.LogInformation("ProyeccionRepository/StoreCoutas({cuotas})", Helper.Log(cuotas));
            var query = ProyeccionQuery.StoreCouta();

            var store = await connectionProyeccion.ExecuteAsync(query, cuotas);
            if (store > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("No se inserto correctamente");
            }
        }
        public async Task<List<Venta>> GetAllConsolidadoProyeccionByCicloId(int cicloId)
        {
            this.logger.LogInformation("ProyeccionRepository/GetAllConsolidadoByCiclo({cicloId})", cicloId);
            var query = ProyeccionQuery.GetAllConsolidadosByCicloId();

            var ventasConsolidadas = await connectionProyeccion.QueryAsync<Venta>(query, new { cicloId });
            return ventasConsolidadas.ToList();
        }
    }
}