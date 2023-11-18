using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.Guardian;
using Dapper;

namespace api_guardian.Repository
{
    public class ModuloRespository
    {
        private readonly DBGuardianContext dBGuardianContext;
        private readonly ILogger<ModuloRespository> logger;
        private readonly IDbConnection connection;
        public ModuloRespository(
            DBGuardianContext dBGuardianContext,
            ILogger<ModuloRespository> logger
        )
        {
            this.dBGuardianContext = dBGuardianContext;
            this.logger = logger;
            this.connection = this.dBGuardianContext.CreateConnection();

        }
        public async Task<IEnumerable<Modulo>> GetAll()
        {
            this.logger.LogInformation("ModuloRespository/GetAll");

            var query = "SELECT * FROM modulo";

            var modulos = await connection.QueryAsync<Modulo>(query);
            return modulos.ToList();
        }

        public async Task<int> Store(Modulo modulo)
        {
            this.logger.LogInformation("ModuloRespository/Store");

            var query = $@"
                insert into 
                    `modulo` (
                        `url`, 
                        `modulo_nombre`, 
                        `descripcion`, 
                        `imagen_referencia`
                    )
                    values
                    (
                        @Url, 
                        @ModuloNombre, 
                        @Descripcion, 
                        @ImagenReferencia
                    );
                    select LAST_INSERT_ID()";

            var moduloId = await connection.QuerySingleAsync<int>(query, modulo);
            if (moduloId == 0)
            {
                this.logger.LogError("Se genero un error");
                throw new Exception("Ocurrio un error al insertar modulo");
            }
            return moduloId;
        }
    }
}