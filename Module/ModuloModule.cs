using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;
using api_guardian.Repository;

namespace api_guardian.Module
{
    public class ModuloModule
    {
        private readonly ILogger<ModuloModule> logger;
        private readonly ModuloRespository moduloRespository;

        public ModuloModule(
            ILogger<ModuloModule> logger,
            ModuloRespository moduloRespository
        )
        {
            this.logger = logger;
            this.moduloRespository = moduloRespository;
        }
        public async Task<List<Modulo>> DataTable()
        {
            this.logger.LogInformation("ModuloModule/DataTable");
            var nuevo = new Modulo
            {
                Descripcion = "desde api",
                ImagenReferencia = "desde api",
                ModuloNombre = "desde api",
                Url = "desde api"
            };
            var insert = await this.moduloRespository.Store(nuevo);
            this.logger.LogInformation("insercion correcta is devuelto {id}", insert);
            var resultado = await this.moduloRespository.GetAll();
            return resultado.ToList();
        }
        public async Task<List<Modulo>> Store()
        {
            this.logger.LogInformation("ModuloModule/Store");
            var resultado = await this.moduloRespository.GetAll();
            return resultado.ToList();
        }
    }
}