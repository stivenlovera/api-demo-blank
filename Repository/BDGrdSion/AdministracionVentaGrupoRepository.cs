using System.Data;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class AdministracionVentaGrupoRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<AdministracionEmpresaRepository> logger;
        private readonly IDbConnection connection;
        public AdministracionVentaGrupoRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<AdministracionEmpresaRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();

        }
        public async Task<List<AdministracionVentaGrupo>> GetAll(int cicloId, List<int> empresaId)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/GetAll({cicloId},{empresaId})", cicloId, Helper.Log(empresaId));
            var query = AdministracionVentaGrupoSql.ObteneTodo();
            var consolidado = await connection.QueryAsync<AdministracionVentaGrupo>(query, new { cicloId, empresaId });

            return consolidado.ToList();
        }
        public async Task<ComisionConsolidado> ObtenerUno(int ComisionConsolidadoId)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/ObtenerUno({comisionConsolidadoId})", ComisionConsolidadoId);
            var query = ComisionConsolidadoSql.ObtenerUno();
            var consolidado = await connection.QueryAsync<ComisionConsolidado>(query, new { ComisionConsolidadoId });

            return consolidado.FirstOrDefault();
        }
        public async Task<ComisionConsolidado> Update(ComisionConsolidado comisionConsolidado)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/Update({comisionConsolidadoId})", Helper.Log(comisionConsolidado));
            var query = ComisionConsolidadoSql.Update();
            var consolidado = await connection.QueryAsync<ComisionConsolidado>(query, comisionConsolidado);

            return consolidado.FirstOrDefault();
        }

        public async Task<ComisionConsolidado> Eliminar(int comisionConsolidadoId)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/Eliminar({comisionConsolidadoId})", comisionConsolidadoId);
            var query = ComisionConsolidadoSql.Delete();
            var consolidado = await connection.QueryAsync<ComisionConsolidado>(query, new { comisionConsolidadoId });

            return consolidado.FirstOrDefault();
        }
    }
}