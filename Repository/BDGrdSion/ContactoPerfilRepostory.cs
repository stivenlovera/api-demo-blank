using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.GrdSion;
using api_guardian.SqlQuerys.GrdSion;
using Dapper;

namespace api_guardian.Repository.BDGrdSion
{
    public class ContactoPerfilRepostory
    {
        private readonly DBGrdSionContext dBGrdSionContext;
        private readonly ILogger<AdministracionEmpresaRepository> logger;
        private readonly IDbConnection connection;
        public ContactoPerfilRepostory(
            DBGrdSionContext dBGrdSionContext,
            ILogger<AdministracionEmpresaRepository> logger
        )
        {
            this.dBGrdSionContext = dBGrdSionContext;
            this.logger = logger;
            this.connection = this.dBGrdSionContext.CreateConnection();
        }
        public async Task<List<ContactoPerfil>> ObtenerTodo()
        {
            this.logger.LogInformation("AdministracionContactoRepository/ObtenerTodo()");
            var query = ContactoPerfilSql.ObtenerTodo();
            var modulos = await connection.QueryAsync<ContactoPerfil>(query);
            this.logger.LogInformation("ObtenerTodo SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
        public async Task<List<ContactoPerfil>> ObtenerUno()
        {
            this.logger.LogInformation("AdministracionContactoRepository/ObtenerUno()");
            var query = ContactoPerfilSql.ObtenerUno();
            var modulos = await connection.QueryAsync<ContactoPerfil>(query);
            this.logger.LogInformation("ObtenerUno SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
        public async Task<List<ContactoPerfil>> Insertar()
        {
            this.logger.LogInformation("AdministracionContactoRepository/Insertar()");
            var query = ContactoPerfilSql.Insertar();
            var modulos = await connection.QueryAsync<ContactoPerfil>(query);
            this.logger.LogInformation("Insertar SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
        public async Task<List<ContactoPerfil>> Update()
        {
            this.logger.LogInformation("AdministracionContactoRepository/Update()");
            var query = ContactoPerfilSql.Update();
            var modulos = await connection.QueryAsync<ContactoPerfil>(query);
            this.logger.LogInformation("Update SUCCESS => {modulos} registros encontrados", modulos.ToList().Count);
            return modulos.ToList();
        }
    }
}