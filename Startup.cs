using System.Text;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.Helpers;
using api_guardian.Module;
using api_guardian.Module.ConsolidadosModule;
using api_guardian.Module.JobsModule;
using api_guardian.Repository;
using api_guardian.Repository.BDComisiones;
using api_guardian.Repository.BDGrdSion;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentoVentaSion.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace api_guardian
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(
            IConfiguration configuration
            )
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var getStringConnectionMysql = configuration.GetSection("connectionMysql").Get<ConectionString>();
            var mysqlConnect = $"Server={getStringConnectionMysql.IpServer};Port={getStringConnectionMysql.Port};Database={getStringConnectionMysql.Database};User={getStringConnectionMysql.User};Password={getStringConnectionMysql.Password};Connection Timeout=0;default command timeout=0;";
            var getStringConnectionGuardian = configuration.GetSection("connectionMysqlGuardian").Get<ConectionString>();
            var mysqlConnectGuardian = $"Server={getStringConnectionGuardian.IpServer};Port={getStringConnectionGuardian.Port};Database={getStringConnectionGuardian.Database};User={getStringConnectionGuardian.User};Password={getStringConnectionGuardian.Password};Connection Timeout=0;default command timeout=0;";

            //DAPPER CONEXION
            services.AddSingleton<DBGuardianContext>();
            services.AddSingleton<DBGrdSionContext>();
            services.AddSingleton<DBProyeccionContext>();
            services.AddSingleton<DBComisionesContext>();


            services.AddDbContext<DbContextGrdSion>(options =>
            {
                options.UseMySql(mysqlConnect, new MySqlServerVersion(new Version(5, 5, 2)));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient
            );

            services.AddDbContext<DbContextGuardian>(options =>
            {
                options.UseMySql(mysqlConnectGuardian, ServerVersion.AutoDetect(mysqlConnectGuardian));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            },
                ServiceLifetime.Transient
            );

            ConnectDBComisiones(services);

            ServicesUtils(services);
            ServicesModulos(services);
            ServicesSql(services);
            ServicesRespositories(services);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddAuthorization();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "mypolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API GUARDIAN",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                  {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                      new string[]{}
                  }
              });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetSection("KeyTokken").Value)
                ),
                ClockSkew = TimeSpan.Zero
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                        c.InjectStylesheet("/swagger/custom.css");
                    }
                );
            }
            else //use en produccion - ambiente de desarrollo
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(
               options =>
               {
                   options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Authorization");
               }
            );
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
        private static void ServicesUtils(IServiceCollection services)
        {
            //convert to pdf
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            //active view
            services.AddMvc().AddControllersAsServices();
            services.AddTransient<GetTemplate>();
            services.AddTransient<GeneratePdf>();
            services.AddTransient<Converters>();
            services.AddTransient<RazorRendererHelper>();
            /* services.AddTransient<FilesConvert>();
            services.AddTransient<Letras>(); */
        }
        private static void ServicesModulos(IServiceCollection services)
        {
            //CONSOLIDADOS
            services.AddTransient<ConsolidadoModule>();
            services.AddTransient<JobModule>();
            services.AddTransient<JobTimeModulo>();
            services.AddTransient<FuncionarioModule>();
            services.AddTransient<AutorizadoModule>();
            services.AddTransient<RolModule>();
            services.AddTransient<RolFuncionarioModule>();
            services.AddTransient<ReporteRedModule>();
            services.AddTransient<ModuloModule>();
            services.AddTransient<AuthenticateModule>();
            services.AddTransient<CicloModule>();
            services.AddTransient<ProyeccionModule>();
            services.AddTransient<AdministracionEmpresaModule>();
            services.AddTransient<EstadoReporteModule>();
            services.AddTransient<PagoConsolidadoModule>();
            services.AddTransient<GenerarConsolidadoModule>();
            services.AddTransient<PagoConsolidadoModule>();
            services.AddTransient<AdministracionContactoModule>();
            services.AddTransient<PaisModule>();
            services.AddTransient<AdministracionNivelModule>();
        }
        private static void ServicesRespositories(IServiceCollection services)
        {
            //CONSOLIDADOS
            services.AddTransient<ServicioRespository>();
            services.AddTransient<JobTimeRepository>();
            services.AddTransient<FuncionarioRepository>();
            services.AddTransient<AutorizadoRepository>();
            services.AddTransient<RolRepository>();
            services.AddTransient<RolFuncionarioRepository>();
            services.AddTransient<ReporteRedRepository>();
            services.AddTransient<ModuloRespository>();
            services.AddTransient<RolModuloRepository>();
            services.AddTransient<RolSubModuloRepository>();
            services.AddTransient<RolComponenteRepository>();
            services.AddTransient<CicloRepository>();
            services.AddTransient<ProyeccionRepository>();
            services.AddTransient<CnxBdComsionesRepository>();
            services.AddTransient<InCoutaRespository>();
            services.AddTransient<AdministracionEmpresaRepository>();
            services.AddTransient<ConsolidadoRepository>();
            services.AddTransient<HistorialConsolidadoRepository>();
            services.AddTransient<EstadoReporteRepository>();
            services.AddTransient<PagoComisionesRepository>();
            services.AddTransient<HistorialPagoComisionesRepository>();
            services.AddTransient<ConfigCambioEmpresaRepository>();
            services.AddTransient<AdministracionContactoRepository>();
            services.AddTransient<BasePaisRepository>();
            services.AddTransient<AdministracionNiveRepository>();
        }
        private static void ServicesSql(IServiceCollection services)
        {
            //CONSOLIDADOS
            services.AddTransient<ConsolidadoSql>();
        }

        private void ConnectDBComisiones(IServiceCollection services)
        {
            var getStringConnection = this.configuration.GetSection("connectionSqlServerDBComisiones").Get<ConectionString>();
            var sqlServerConnectDBComisiones = $"Server={getStringConnection.IpServer};Database={getStringConnection.Database};User={getStringConnection.User};Password={getStringConnection.Password};TrustServerCertificate=True;";
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