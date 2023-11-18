using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;
using api_guardian.Repository;
using Newtonsoft.Json;

namespace api_guardian.Module
{
    public class RolModule
    {
        private readonly ILogger<RolModule> logger;
        private readonly RolRepository rolRepository;

        public RolModule(
            ILogger<RolModule> logger,
            RolRepository rolRepository
        )
        {
            this.logger = logger;
            this.rolRepository = rolRepository;
        }
        public async Task<List<Rol>> DataTable()
        {
            this.logger.LogInformation("RolModule/DataTable()");
            var datatable = await this.rolRepository.GetAll();
            return datatable;
        }
        public async Task<Rol> Edit(int RolId)
        {
            this.logger.LogInformation("RolModule/Editar(RolId)", RolId);
            var edit = await this.rolRepository.GetOne(RolId);
            return edit;
        }
        public async Task<Rol> Store(Rol rol)
        {
            this.logger.LogInformation("RolModule/Store({rol})", JsonConvert.SerializeObject(rol, Formatting.Indented));
            var insert = await this.rolRepository.Store(rol);
            return insert;
        }
        public async Task<Rol> Update(Rol rol)
        {
            this.logger.LogInformation("RolModule/Update({rol})", JsonConvert.SerializeObject(rol, Formatting.Indented));
            var update = await this.rolRepository.Update(rol);
            return update;
        }
        public async Task<int> Delete(int RolId)
        {
            this.logger.LogInformation("RolModule/Update({RolId})", RolId);
            var eliminar = await this.rolRepository.Delete(RolId);
            return eliminar;
        }
    }
}