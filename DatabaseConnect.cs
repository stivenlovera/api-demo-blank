using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_guardian
{
    public class DatabaseConnect
    {
        private readonly IConfiguration configuration;

        public DatabaseConnect(
            IConfiguration configuration
        )
        {
            this.configuration = configuration;
        }
        public void ConnectDBComisiones(IServiceCollection services)
        {
            var getStringConnection = this.configuration.GetSection("connectionMysql").Get<ConectionString>();
            var sqlServerConnectDBComisiones = $"Server={getStringConnection.IpServer};Port={getStringConnection.Port};Database={getStringConnection.Database};User={getStringConnection.User};Password={getStringConnection.Password};Connection Timeout=0;default command timeout=0;";
            services.AddDbContext<DbContextDBComisiones>(options =>
            {
                options.UseSqlServer(sqlServerConnectDBComisiones);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            },
                ServiceLifetime.Transient
            );
        }
    }
}