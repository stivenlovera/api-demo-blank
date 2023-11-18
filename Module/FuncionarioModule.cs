using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Guardian;
using api_guardian.Repository;
using Newtonsoft.Json;

namespace api_guardian.Module
{
    public class FuncionarioModule
    {
        private readonly ILogger<FuncionarioModule> logger;
        private readonly FuncionarioRepository funcionarioRespository;

        public FuncionarioModule(
            ILogger<FuncionarioModule> logger,
            FuncionarioRepository funcionarioRespository
        )
        {
            this.logger = logger;
            this.funcionarioRespository = funcionarioRespository;
        }
        public async Task<List<FuncionarioGetAll>> DataTable()
        {
            this.logger.LogInformation("FuncionarioModule/DataTable()");
            var datatable = await this.funcionarioRespository.GetAll();
            return datatable;
        }
        public async Task<FuncionarioGetOne> GetFuncionario(int funcionarioId)
        {
            this.logger.LogInformation("FuncionarioModule/Editar(funcionarioId)", funcionarioId);
            var edit = await this.funcionarioRespository.GetOne(funcionarioId);
            return edit;
        }
        public async Task<int> Store(Funcionario funcionario)
        {
            this.logger.LogInformation("FuncionarioModule/Store({funcionario})", JsonConvert.SerializeObject(funcionario, Formatting.Indented));
            var insert = await this.funcionarioRespository.Store(funcionario, "");
            return insert;
        }
        /*
        public async Task<Funcionario> Update(Funcionario funcionario)
        {
            this.logger.LogInformation("FuncionarioModule/Update({funcionario})", JsonConvert.SerializeObject(funcionario, Formatting.Indented));
            var update = await this.funcionarioRespository.Update(funcionario);
            return update;
        } */
        /*  public async Task<int> Delete(int funcionarioId)
         {
             this.logger.LogInformation("FuncionarioModule/Update({funcionarioId})", funcionarioId);
             //var eliminar = await this.funcionarioRespository.Delete(funcionarioId);
             return 0;
         } */
    }
}