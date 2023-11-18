using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;
using api_guardian.Repository;
using Newtonsoft.Json;

namespace api_guardian.Module
{
    public class RolFuncionarioModule
    {

        private readonly ILogger<RolFuncionarioModule> logger;
        private readonly RolFuncionarioRepository rolFuncionarioRepository;

        public RolFuncionarioModule(
            ILogger<RolFuncionarioModule> logger,
            RolFuncionarioRepository rolFuncionarioRepository
        )
        {
            this.logger = logger;
            this.rolFuncionarioRepository = rolFuncionarioRepository;
            
        }

        public async Task<RolFuncionarioOne> Edit(int FuncionarioId)
        {
            this.logger.LogInformation("RolFuncionario/Edit(FuncionarioId)", FuncionarioId);
            var edit = await this.rolFuncionarioRepository.GetOne(FuncionarioId);
            return edit;
        }
        /*  public async Task<List<RolFuncionario>> DataTable()
         {
             this.logger.LogInformation("RolFuncionario/DataTable()");
             var datatable = await this.rolFuncionarioRepository.GetAll();
             return datatable;
         }
         public async Task<RolFuncionario> Edit(int RolFuncionarioId)
         {
             this.logger.LogInformation("RolFuncionario/Editar(RolFuncionarioId)", RolFuncionarioId);
             var edit = await this.rolFuncionarioRepository.GetOne(RolFuncionarioId);
             return edit;
         }
         public async Task<RolFuncionario> Store(RolFuncionario rolFuncionario)
         {
             this.logger.LogInformation("RolFuncionario/Insert({rol})", JsonConvert.SerializeObject(rolFuncionario, Formatting.Indented));
             var insert = await this.rolFuncionarioRepository.Insert(rolFuncionario);
             return insert;
         }
         public async Task<RolFuncionario> Update(RolFuncionario rolFuncionario)
         {
             this.logger.LogInformation("RolFuncionario/Update({rol})", JsonConvert.SerializeObject(rolFuncionario, Formatting.Indented));
             var update = await this.rolFuncionarioRepository.Update(rolFuncionario);
             return update;
         }
         public async Task<int> Delete(int RolId)
         {
             this.logger.LogInformation("RolFuncionario/Update({RolId})", RolId);
             var eliminar = await this.rolFuncionarioRepository.Delete(RolId);
             return eliminar;
         } */
    }
}