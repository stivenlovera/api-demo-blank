using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.repository;
using api_guardian.Entities;
using api_guardian.SqlQuerys.DBGdrSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class AdministracionEmpresaRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<AdministracionEmpresaRepository> logger;
        private readonly IDbConnection connection;
        public AdministracionEmpresaRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<AdministracionEmpresaRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();

        }
        public async Task<List<AdministracionEmpresa>> GetAll()
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/GetAll()");
            var query = AdministracionEmpresaQuery.GetAll();
            var modulos = await connection.QueryAsync<AdministracionEmpresa>(query);
            return modulos.ToList();
        }
        public async Task<AdministracionEmpresa> GetOne(int LEmpresaId)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/GetOne({LEmpresaId})", LEmpresaId);
            var query = AdministracionEmpresaQuery.Edit();
            var validate = await connection.QueryAsync<AdministracionEmpresa>(query, new { LEmpresaId });
            this.logger.LogInformation("Login {query}", validate);
            if (validate.FirstOrDefault() != null)
            {
                return validate.FirstOrDefault();
            }
            else
            {
                throw new Exception("Ocurrio un error");
            }
        }
        public async Task<int> Store(AdministracionEmpresa AdministracionEmpresa)
        {
            var ultimoCiclo = await this.GetUltimo();
            AdministracionEmpresa.LempresaId = ultimoCiclo.LempresaId + 1;
            this.logger.LogInformation("AdministracionEmpresaRepository/Store({AdministracionEmpresa})", Helper.Log(AdministracionEmpresa));
            var query = AdministracionEmpresaQuery.Store();
            var store = await connection.QueryAsync<InsertGetId>(query, AdministracionEmpresa);

            return store.FirstOrDefault().Id;
        }
        public async Task<int> Update(AdministracionEmpresa AdministracionEmpresa)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/Update({AdministracionEmpresa})", Helper.Log(AdministracionEmpresa));
            var query = AdministracionEmpresaQuery.Update();
            var update = await connection.ExecuteAsync(query, AdministracionEmpresa);
            if (update == 1)
            {
                this.logger.LogInformation("AdministracionEmpresaRepository/Update {query} ejecutado correctamente", query);
            }
            else
            {
                throw new Exception("No se modifico correctamente");
            }
            return update;
        }
        public async Task<int> Delete(int lcicloId)
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/Delete({lcicloId})", lcicloId);
            var query = AdministracionEmpresaQuery.Delete();
            var update = await connection.ExecuteAsync(query, new { lcicloId });
            if (update == 1)
            {
                this.logger.LogInformation("AdministracionEmpresaRepository/Delete {query} ejecutado correctamente", query);
            }
            else
            {
                throw new Exception("No se elimino correctamente");
            }
            return update;
        }
        public async Task<AdministracionEmpresa> GetUltimo()
        {
            this.logger.LogInformation("AdministracionEmpresaRepository/GetUltimo()");
            var query = AdministracionEmpresaQuery.GetOneUltimo();
            var update = await connection.QueryAsync<AdministracionEmpresa>(query);
            return update.FirstOrDefault();
        }
    }
}