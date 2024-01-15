using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.DBComisiones;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using api_guardian.Utils;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class AdministracionContactoRepository
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<AdministracionEmpresaRepository> logger;
        private readonly IDbConnection connection;
        public AdministracionContactoRepository(
            DBGrdSionContext dBGrdSionContext,
            ILogger<AdministracionEmpresaRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<List<AdministracionContacto>> ObtenerTodo()
        {
            this.logger.LogInformation("AdministracionContactoRepository/ObtenerTodo()");
            var query = AdministracionContactoSql.ObtenerTodo();
            var modulos = await connection.QueryAsync<AdministracionContacto>(query);
            this.logger.LogInformation("ObtenerTodo SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
         public async Task<List<AdministracionContacto>> ObtenerTodoSelect()
        {
            this.logger.LogInformation("AdministracionContactoRepository/ObtenerTodoSelect()");
            var query = AdministracionContactoSql.ObtenerTodoSelect();
            var modulos = await connection.QueryAsync<AdministracionContacto>(query);
            this.logger.LogInformation("ObtenerTodoSelect SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
        public async Task<AdministracionContacto> ObtenerUno(int LcontactoId)
        {
            this.logger.LogInformation("AdministracionContactoRepository/ObtenerUno({LcontactoId})", LcontactoId);
            var query = AdministracionContactoSql.ObtenerUno();
            var modulos = await connection.QueryAsync<AdministracionContacto>(query, new { LcontactoId });
            this.logger.LogInformation("ObtenerUno SUCCESS => {modulos} ", modulos.FirstOrDefault());
            return modulos.FirstOrDefault();
        }
        public async Task<AdministracionContacto> Modificar(AdministracionContacto AdministracionContacto)
        {
            this.logger.LogInformation("AdministracionContactoRepository/Modificar({LcontactoId})", Helper.Log(AdministracionContacto));
            var query = AdministracionContactoSql.ObtenerUno();
            var modulos = await connection.QueryAsync<AdministracionContacto>(query, AdministracionContacto);
            this.logger.LogInformation("Modificar SUCCESS => {modulos} ", modulos.FirstOrDefault());
            return modulos.FirstOrDefault();
        }
        public async Task<AdministracionContacto> Insertar(AdministracionContacto AdministracionContacto)
        {
            this.logger.LogInformation("AdministracionContactoRepository/Insertar({LcontactoId})", Helper.Log(AdministracionContacto));
            var query = AdministracionContactoSql.ObtenerUno();
            var modulos = await connection.QueryAsync<AdministracionContacto>(query, AdministracionContacto);
            this.logger.LogInformation("Insertar SUCCESS => {modulos} ", modulos.FirstOrDefault());
            return modulos.FirstOrDefault();
        }
    }
}