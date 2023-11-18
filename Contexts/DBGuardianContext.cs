using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Utils;
using Dapper;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace api_guardian.Contexts
{
    public class DBGuardianContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DBGuardianContext(IConfiguration configuration)
        {
            _configuration = configuration;
            //Cadena de conexion
            var getStringConnectionGuardian = _configuration.GetSection("connectionMysqlGuardian").Get<ConectionString>();
            _connectionString = $"Server={getStringConnectionGuardian.IpServer};Port={getStringConnectionGuardian.Port};Database={getStringConnectionGuardian.Database};User={getStringConnectionGuardian.User};Password={getStringConnectionGuardian.Password};Connection Timeout=0;default command timeout=0;";
        }
        public IDbConnection CreateConnection()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return new MySqlConnection(_connectionString);
        }
    }
}