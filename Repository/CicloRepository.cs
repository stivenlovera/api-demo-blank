using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.repository;
using api_guardian.Entities;
using api_guardian.SqlQuerys.DBGuardian;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository
{
    public class CicloRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<CicloRepository> logger;
        private readonly IDbConnection connection;
        public CicloRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<CicloRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();

        }
        public async Task<List<AdministracionCiclo>> GetAll()
        {
            this.logger.LogInformation("CicloRepository/GetAll()");
            var query = CicloQuery.GetAll();
            var modulos = await connection.QueryAsync<AdministracionCiclo>(query);
            return modulos.ToList();
        }
        public async Task<AdministracionCiclo> GetOne(int lcicloId)
        {
            this.logger.LogInformation("CicloRepository/GetOne()", lcicloId);
            var query = CicloQuery.GetOne();
            var validate = await connection.QueryAsync<AdministracionCiclo>(query, new { lcicloId });
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
        public async Task<int> Store(AdministracionCiclo administracionCiclo)
        {
            var ultimoCiclo = await this.GetUltimo();
            administracionCiclo.lcicloId = ultimoCiclo.lcicloId + 1;
            this.logger.LogInformation("CicloRepository/Store({administracionCiclo})", Helper.Log(administracionCiclo));
            var query = CicloQuery.Store();
            var store = await connection.QueryAsync<InsertGetId>(query, administracionCiclo);

            return store.FirstOrDefault().Id;
        }
        public async Task<int> Update(AdministracionCiclo administracionCiclo)
        {
            this.logger.LogInformation("CicloRepository/Update({administracionCiclo})", Helper.Log(administracionCiclo));
            var query = CicloQuery.Update();
            var update = await connection.ExecuteAsync(query, administracionCiclo);
            if (update == 1)
            {
                this.logger.LogInformation("CicloRepository/Update {query} ejecutado correctamente", query);
            }
            else
            {
                throw new Exception("No se modifico correctamente");
            }
            return update;
        }
        public async Task<int> Delete(int lcicloId)
        {
            this.logger.LogInformation("CicloRepository/Delete({lcicloId})", lcicloId);
            var query = CicloQuery.Delete();
            var update = await connection.ExecuteAsync(query, new { lcicloId });
            if (update == 1)
            {
                this.logger.LogInformation("CicloRepository/Delete {query} ejecutado correctamente", query);
            }
            else
            {
                throw new Exception("No se elimino correctamente");
            }
            return update;
        }
        public async Task<AdministracionCiclo> GetUltimo()
        {
            this.logger.LogInformation("CicloRepository/GetUltimo()");
            var query = CicloQuery.GetOneUltimo();
            var update = await connection.QueryAsync<AdministracionCiclo>(query);
            return update.FirstOrDefault();
        }
    }
}