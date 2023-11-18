using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Utils;
using Dapper;
using Microsoft.Data.SqlClient;

namespace api_guardian.Contexts
{
    public class DBComisionesContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DBComisionesContext(IConfiguration configuration)
        {
            _configuration = configuration;
            //Cadena de conexion
            var getStringConnectionBDComisiones = _configuration.GetSection("connectionSqlServerDBComisiones").Get<ConectionString>();
            _connectionString = $"Server={getStringConnectionBDComisiones.IpServer};Database={getStringConnectionBDComisiones.Database};User={getStringConnectionBDComisiones.User};Password={getStringConnectionBDComisiones.Password};TrustServerCertificate=True;";
        }
        public IDbConnection CreateConnection()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return new SqlConnection(_connectionString);
        }
    }
}