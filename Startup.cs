using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Helpers;
using api_guardian.Module.ConsolidadosModule;
using api_guardian.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentoVentaSion.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace api_guardian
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var getStringConnectionMysql = configuration.GetSection("connectionMysql").Get<ConectionString>();
            var mysqlConnect = $"Server={getStringConnectionMysql.IpServer};Port={getStringConnectionMysql.Port};Database={getStringConnectionMysql.Database};User={getStringConnectionMysql.User};Password={getStringConnectionMysql.Password};";

            services.AddCors(options =>
            {
                options.AddPolicy(name: "mypolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.AddDbContext<DbContextGrdSion>(options =>
            {
                options.UseMySql(mysqlConnect, ServerVersion.AutoDetect(mysqlConnect));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);

            this.ServicesUtils(services);
            this.ServicesModulos(services);
            services.AddControllers();
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddEndpointsApiExplorer();
            services.AddAuthorization();
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
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else //use en produccion - ambiente de desarrollo
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void ServicesUtils(IServiceCollection services)
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
        private void ServicesModulos(IServiceCollection services)
        {
            //CONSOLIDADOS
            services.AddTransient<ConsolidadosReports>();

        }
    }
}