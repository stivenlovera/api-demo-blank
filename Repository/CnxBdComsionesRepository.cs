using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.DBComisiones;
using api_guardian.SqlQuerys.DBComisiones;
using Dapper;

namespace api_guardian.Repository
{
    public class CnxBdComsionesRepository
    {
        private readonly ILogger<CnxBdComsionesRepository> logger;
        private readonly DBComisionesContext dBComisionesContext;

        private readonly IDbConnection connectionBDComsiones;
        public CnxBdComsionesRepository(
            ILogger<CnxBdComsionesRepository> logger,
            DBComisionesContext dBComisionesContext
        )
        {
            this.dBComisionesContext = dBComisionesContext;
            this.logger = logger;
            this.connectionBDComsiones = this.dBComisionesContext.CreateConnection();
        }
        public async Task<CnxBdcomisiones> GetConexionDataBase(int Idventa)
        {
            this.logger.LogInformation("CnxBdComsionesRepository/GetConexionDataBase({Idventa})", Idventa);
            var query = CnxBDComsionesQuery.GetOneByIdVenta();
            var ventasConsolidadas = await connectionBDComsiones.QueryAsync<CnxBdcomisiones>(query, new { Idventa });
            return ventasConsolidadas.FirstOrDefault();
        }
    }
}