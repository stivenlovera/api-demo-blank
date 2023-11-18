using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.Guardian;
using api_guardian.SqlQuerys.DBGuardian;
using api_guardian.Utils;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_guardian.Repository
{
    public class AutorizadoRepository
    {
        private readonly DBGuardianContext dBGuardianContext;
        private readonly ILogger<FuncionarioRepository> logger;
        private readonly IDbConnection connection;
        public AutorizadoRepository(
            DBGuardianContext dBGuardianContext,
            ILogger<FuncionarioRepository> logger
        )
        {
            this.dBGuardianContext = dBGuardianContext;
            this.logger = logger;
            this.connection = this.dBGuardianContext.CreateConnection();

        }
        public async Task<Autorizacion> GetOne(int funcionarioId)
        {
            this.logger.LogInformation("AutorizadoRepository/GetOne({funcionarioId})", funcionarioId);
            var query = AutorizacionQuery.GetOne();
            var modulos = await connection.QueryAsync<Autorizacion>(query, new { funcionarioId });
            return modulos.FirstOrDefault();
        }
        public async Task<Autorizacion> EditOne(int funcionarioId)
        {
            this.logger.LogInformation("AutorizadoRepository/EditOne({funcionarioId})", funcionarioId);
            var query = AutorizacionQuery.EditarOne();
            var modulos = await connection.QueryAsync<Autorizacion>(query, new { funcionarioId });
            return modulos.FirstOrDefault();
        }
        public async Task<bool> Update(Autorizacion autorizacion)
        {
            this.logger.LogInformation("AutorizadoRepository/GetOne({autorizacion})", Helper.Log(autorizacion));
            var query = AutorizacionQuery.Update();
            var update = await connection.ExecuteAsync(query, autorizacion);
            if (update != 0)
            {
                return true;
            }
            else
            {
                throw new Exception("No se pudo modificar atorizacion");
            }
        }
        public async Task<FuncionarioGetOne> GetLogin(string usuario, string password)
        {
            this.logger.LogInformation("AutorizadoRepository/GetLogin({usuario},{password})", usuario, password);
            var query = AutorizacionQuery.GetLogin();
            var validate = await connection.QueryAsync<FuncionarioGetOne>(query, new { usuario, password });
            this.logger.LogInformation("Login {query}", validate);
            if (validate.FirstOrDefault() != null)
            {
                return validate.FirstOrDefault();
            }
            else
            {
                throw new Exception("Credenciales no validas");
            }
        }
    }
}