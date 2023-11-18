using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.Proyeccion;
using api_guardian.SqlQuerys.DBComisiones;
using Dapper;

namespace api_guardian.Repository.BDComisiones
{
    public class InCoutaRespository
    {
        private readonly ILogger<InCoutaRespository> logger;
        private readonly DBComisionesContext dBComisionesContext;

        private readonly IDbConnection connectionBDComsiones;
        public InCoutaRespository(
            ILogger<InCoutaRespository> logger,
            DBComisionesContext dBComisionesContext
        )
        {
            this.dBComisionesContext = dBComisionesContext;
            this.logger = logger;
            this.connectionBDComsiones = this.dBComisionesContext.CreateConnection();
        }
        public async Task<List<Cuota>> GetInCoutaToProyeccion(string bdEmpresa, int Idventa)
        {
            this.logger.LogInformation("InCoutaRespository/GetInCoutaToProyeccion({bdEmpresa},{Idventa})", bdEmpresa, Idventa);
            var query = InCoutaQuery.GetInCuotaByBDEmpresa(bdEmpresa);
            var ventasConsolidadas = await connectionBDComsiones.QueryAsync<Cuota>(query, new { Idventa });
            return ventasConsolidadas.ToList();
        }
    }
}