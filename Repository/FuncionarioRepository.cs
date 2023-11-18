using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Dtos.repository;
using api_guardian.Entities.Guardian;
using api_guardian.SqlQuerys.DBGuardian;
using api_guardian.Utils;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_guardian.Repository
{
    public class FuncionarioRepository
    {
        private readonly DBGuardianContext dBGuardianContext;
        private readonly ILogger<FuncionarioRepository> logger;
        private readonly IDbConnection connection;
        public FuncionarioRepository(
            DBGuardianContext dBGuardianContext,
            ILogger<FuncionarioRepository> logger
        )
        {
            this.dBGuardianContext = dBGuardianContext;
            this.logger = logger;
            this.connection = this.dBGuardianContext.CreateConnection();

        }
        public async Task<List<FuncionarioGetAll>> GetAll()
        {
            this.logger.LogInformation("FuncionarioRepository/GetAll");
            var query = FuncionarioQuery.GetAllUsuario();
            var modulos = await connection.QueryAsync<FuncionarioGetAll>(query);
            return modulos.ToList();
        }
        public async Task<FuncionarioGetOne> GetOne(int funcionarioId)
        {
            this.logger.LogInformation("FuncionarioRepository/GetOne({funcionarioId})", funcionarioId);
            var query = FuncionarioQuery.GetOne();
            var modulos = await connection.QueryAsync<FuncionarioGetOne>(query, new { funcionarioId });
            return modulos.FirstOrDefault();
        }
        public async Task<int> Store(Funcionario funcionario, string imagen)
        {
            this.logger.LogInformation("FuncionarioRepository/GetOne({funcionario},{imagen})", Helper.Log(funcionario), imagen);
            var query = FuncionarioQuery.SPStoreFuncionario();
            var resultado = await connection.QueryAsync<InsertGetId>(query, new
            {
                funcionario.Nombres,
                funcionario.Apellidos,
                funcionario.CargoId,
                funcionario.NroDocumento,
                funcionario.Observaciones,
                funcionario.FechaIngreso,
                imagen,
                funcionario.Alias,
                funcionario.Email
            });

            return resultado.FirstOrDefault().Id;
        }
    }
}
