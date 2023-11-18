using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.Controllers;
using api_guardian.Entities.Guardian;
using api_guardian.SqlQuerys.DBGuardian;
using api_guardian.Utils;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_guardian.Repository
{
    public class RolFuncionarioRepository
    {
        private readonly DBGuardianContext dBGuardianContext;
        private readonly ILogger<RolFuncionarioRepository> logger;
        private readonly IDbConnection connection;
        public RolFuncionarioRepository(
            DBGuardianContext dBGuardianContext,
            ILogger<RolFuncionarioRepository> logger
        )
        {
            this.dBGuardianContext = dBGuardianContext;
            this.logger = logger;
            this.connection = this.dBGuardianContext.CreateConnection();
        }
        public async Task<RolFuncionarioOne> GetOne(int funcionarioId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetOne({funcionarioId})", funcionarioId);
            var query = RolFuncionarioQuery.GetOne();
            var modulos = await connection.QueryAsync<RolFuncionarioOne>(query, new { funcionarioId = funcionarioId });
            return modulos.FirstOrDefault();
        }
        /*  public async Task<List<RolAuthenticate>> GetAllByFuncionarioId(int funcionarioId)
         {
             this.logger.LogInformation("RolFuncionarioRepository/GetAllByFuncionario({funcionarioId})", funcionarioId);
             var query = RolFuncionarioQuery.GetAllByFuncionario();
             this.logger.LogInformation(" query {query}", query);
             var modulos = await connection.QueryAsync<RolAuthenticate>(query, new { funcionario_id=funcionarioId });
             return modulos.ToList();
         } */
        public async Task<List<RolAuthenticate>> GetAllByFuncionarioId(int funcionarioId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByFuncionarioId({funcionarioId})", funcionarioId);
            var query = RolFuncionarioQuery.GetAllByFuncionario();
            var modulos = await connection.QueryAsync<RolAuthenticate>(query, new { funcionario_id = funcionarioId });
            return modulos.ToList();
        }
        public async Task<List<Rol>> GetAllRolByFuncionarioId(int funcionarioId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllRolByFuncionarioId({funcionarioId})", funcionarioId);
            var query = RolFuncionarioQuery.GetAllByFuncionarioId();
            var modulos = await connection.QueryAsync<Rol>(query, new { funcionarioId });
            this.logger.LogInformation("RolFuncionarioRepository/GetAllRolByFuncionarioId({funcionarioId})", Helper.Log(modulos));
            return modulos.ToList();
        }
        public async Task<bool> StoreRolByFuncionarioId(List<RolFuncionario> rolFuncionarios)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByFuncionarioId({funcionarioId})", Helper.Log(rolFuncionarios));
            var query = RolFuncionarioQuery.StoreRolFuncionario();
            var insert = await connection.ExecuteAsync(query, rolFuncionarios);
            if (insert == 0)
            {
                throw new Exception("Error al insertar roles");
            }
            return true;
        }
        public async Task<bool> DeleteRolByFuncionarioId(int funcionarioId)
        {
            this.logger.LogInformation("RolFuncionarioRepository/GetAllByFuncionarioId({funcionarioId})", Helper.Log(funcionarioId));
            var query = RolFuncionarioQuery.DeleteRolFuncionarioByFuncionarioId();
            var insert = await connection.ExecuteAsync(query, new { funcionarioId });
            if (insert == 0)
            {
                throw new Exception("Roles eliminados roles");
            }
            return true;
        }
    }
}