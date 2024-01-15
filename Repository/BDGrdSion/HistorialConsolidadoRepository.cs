using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.repository;
using api_guardian.Entities.GrdSion;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Entities.GrdSion.Views;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class HistorialConsolidadoRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<HistorialConsolidadoRepository> logger;
        private readonly IDbConnection connection;
        public HistorialConsolidadoRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<HistorialConsolidadoRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<InsertGetId> InsertComision(string descripcion, string nombre, int EstadoReporteId, int CicloId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/InsertComision({descripcion},{nombre},{estadoReporte},{ciclo_id})", descripcion, nombre, EstadoReporteId, CicloId);
            var query = HistorialConsolidadoSql.InsertComisiones();
            var consolidado = await connection.QueryAsync<InsertGetId>(query, new { descripcion, nombre, EstadoReporteId, CicloId });
            return consolidado.FirstOrDefault();
        }
        public async Task<HistorialComisionConsolidado> GetVerificar(int cicloId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/GetVerificar({cicloId})", cicloId);
            var query = HistorialConsolidadoSql.ObtenerConsolidado();
            var consolidado = await connection.QueryAsync<HistorialComisionConsolidado>(query, new { cicloId });
            return consolidado.FirstOrDefault();
        }
        public async Task<int> InsertHistorialConsolidado(int ComisionConsolidadoId, List<ConsolidadoQuery> datatable)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/InsertHistorialConsolidado({ComisionConsolidadoId},{empresaId})", ComisionConsolidadoId, Helper.Log(datatable));
            var query = HistorialConsolidadoSql.Store();
            var insert = datatable.Select(x => new
            {
                ComisionConsolidadoId = ComisionConsolidadoId,
                LcontactoId = x.LcontactoId,
                Scodigo = x.Scodigo,
                Scedulaidentidad = x.Scedulaidentidad,
                Snombrecompleto = x.Snombrecompleto,
                LempresaId = x.LempresaId,
                Empresa = x.Empresa,
                TotalComisionVtaGrupoResidual = x.TotalComisionVtaGrupoResidual,
                TotalComisionVtaPersonal = x.TotalComisionVtaPersonal,
                LcicloId = x.LcicloId,
                TotalComisionVtaGrupoResiduaBs = x.TotalComisionVtaGrupoResiduaBs,
                TotalComisionVtaPersonalBs = x.TotalComisionVtaPersonalBs,
                RazonSocial = x.RazonSocial,
                Nit = x.Nit,
                NombreCiclo = x.NombreCiclo,
                FechaInicioCiclo = x.FechaInicioCiclo,
                FechaFinCiclo = x.FechaFinCiclo,
                Retencion = x.Retencion,
                TotalComision = x.TotalComision,
                TotalComisionServicio = x.TotalComisionServicio,
                TotalComisionRetencion = x.TotalComisionRetencion,
                TotalPagar = x.TotalPagar,
                Valor13 = x.Valor13,
                Valor87 = x.Valor87
            }).ToList();
            var consolidado = await connection.ExecuteAsync(query, insert);

            if (consolidado > 0)
            {
                this.logger.LogInformation("se insertaron {consolidado} registros", consolidado);
                return consolidado;
            }
            else
            {
                throw new Exception("Ocurrio un error al inserta historial");
            }
        }
        public async Task<List<ConfigCambioEmpresa>> BuscarEmpresaConsolidado(int ComisionConsolidadoId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/BuscarEmpresaConsolidado({ComisionConsolidadoId})", ComisionConsolidadoId);
            var query = HistorialConsolidadoSql.BuscarEmpresaConsolidado();
            var empresas = await connection.QueryAsync<ConfigCambioEmpresa>(query, new { ComisionConsolidadoId });
            this.logger.LogInformation("empresas encontradas {empresas}", Helper.Log(empresas));
            return empresas.ToList();
        }
        public async Task<bool> InsertCambioEmpresa(List<ConfigCambioEmpresa> cambiosEmpresa)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/InsertCambioEmpresa({cambiosEmpresa})", Helper.Log(cambiosEmpresa));
            var query = HistorialConsolidadoSql.InsertCambioEmpresa();
            var insertCambioEmpresa = await connection.ExecuteAsync(query, cambiosEmpresa);
            if (insertCambioEmpresa > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Ocurrio un error al inserta historial");
            }
        }
        //Cambio de empresa
        public async Task<List<ComisionConsolidado>> ObtenerComisionesConsolidado()
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerComisionesConsolidado()");
            var query = HistorialConsolidadoSql.ObtenerComisionesConsolidado();
            var ComisionConsolidado = await connection.QueryAsync<ComisionConsolidado>(query);
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerComisionesConsolidado => {ComisionConsolidado} regisros ", ComisionConsolidado.Count());
            return ComisionConsolidado.ToList();
        }
        public async Task<ComisionConsolidado> ObtenerOneComisionesConsolidado(int ComisionConsolidadoId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerOneComisionesConsolidado({ComisionConsolidadoId})", ComisionConsolidadoId);
            var query = HistorialConsolidadoSql.ObtenerComisionesConsolidado();
            var ComisionConsolidado = await connection.QueryAsync<ComisionConsolidado>(query);
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerOneComisionesConsolidado => {ComisionConsolidado} regisros ", ComisionConsolidado.Count());
            return ComisionConsolidado.FirstOrDefault();
        }
        public async Task<List<ConsolidadoQuery>> ObtenerUnoHistoricoConsolidado(int ComisionConsolidadoId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerUnoHistoricoConsolidado({ComisionConsolidadoId})", ComisionConsolidadoId);
            var query = HistorialConsolidadoSql.ObtenerHistoricoConsolidado();
            var ComisionConsolidado = await connection.QueryAsync<ConsolidadoQuery>(query, new { ComisionConsolidadoId });
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerUnoHistoricoConsolidado => {ComisionConsolidado} regisros ", ComisionConsolidado.Count());
            return ComisionConsolidado.ToList();
        }
        public async Task<List<ConfigCambioEmpresa>> PrepararCambioEmpresa(int ComisionConsolidadoId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/ModificarCambioEmpresa()");
            var query = HistorialConsolidadoSql.ObtenerCambioEmpresa();
            var confgCambioEmpresa = await connection.QueryAsync<ConfigCambioEmpresa>(query, new { ComisionConsolidadoId });
            this.logger.LogInformation("empresas encontradas {confgCambioEmpresa}", Helper.Log(confgCambioEmpresa));
            return confgCambioEmpresa.ToList();
        }
        public async Task<List<ConfigCambioEmpresa>> ObtenerUnoConfgCambioEmpresa(int ComisionConsolidadoId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerUnoHistoricoConsolidado({ComisionConsolidadoId})", ComisionConsolidadoId);
            var query = HistorialConsolidadoSql.ObtenerHistoricoConsolidado();
            var ComisionConsolidado = await connection.QueryAsync<ConfigCambioEmpresa>(query, new { ComisionConsolidadoId });
            this.logger.LogInformation("HistorialConsolidadoRepository/ObtenerUnoHistoricoConsolidado => {ComisionConsolidado} regisros ", ComisionConsolidado.Count());
            return ComisionConsolidado.ToList();
        }
        public async Task<bool> ProcesarCambioEmpresa(List<ConfigCambioEmpresa> cambioEmpresas)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/ProcesarCambioEmpresa()");
            var query = HistorialConsolidadoSql.ProcesarCambioEmpresa();
            var updateCambioEmpresa = await connection.ExecuteAsync(query, cambioEmpresas);
            this.logger.LogInformation("registro procesados encontradas {confgCambioEmpresa}", Helper.Log(updateCambioEmpresa));
            if (updateCambioEmpresa > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Ocurrio un error al modificar cambio empresas");
            }
        }
        public async Task<bool> InsertPagoCambioEmpresa(int consolidadoComisionId)
        {
            this.logger.LogInformation("HistorialConsolidadoRepository/InsertPagoCambioEmpresa({consolidadoComisionId})", consolidadoComisionId);
            var query = HistorialConsolidadoSql.ProcesarCambioEmpresa();
            var updateCambioEmpresa = await connection.ExecuteAsync(query, consolidadoComisionId);
            this.logger.LogInformation("registro procesados encontradas {confgCambioEmpresa}", Helper.Log(updateCambioEmpresa));
            if (updateCambioEmpresa > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Ocurrio un error al modificar cambio empresas");
            }
        }
    }
}