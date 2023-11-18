using System.IdentityModel.Tokens.Jwt;

using api_guardian;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
//JWT HABILITADO
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//lOG DE CONFIGURACION
builder.Host.UseSerilog();
//CREATE LOGS
Guid id = Guid.NewGuid();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Error)
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/Log-.log", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u15}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
// Add services to the container.
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); 
// calling ConfigureServices method
var app = builder.Build();
startup.Configure(app, app.Environment);
// Configure the HTTP request pipeline.

try
{
    Log.Information("Iniciando Web API ");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return;
}
