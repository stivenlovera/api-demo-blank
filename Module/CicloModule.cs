using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Repository;
using api_guardian.Utils;

namespace api_guardian.Module
{
    public class CicloModule
    {
        private readonly ILogger<CicloModule> logger;
        private readonly CicloRepository cicloRepository;
        private readonly FuncionarioRepository funcionarioRepository;

        public CicloModule(
            ILogger<CicloModule> logger,
            CicloRepository cicloRepository,
            FuncionarioRepository funcionarioRepository
        )
        {
            this.logger = logger;
            this.cicloRepository = cicloRepository;
            this.funcionarioRepository = funcionarioRepository;
        }
        public async Task<List<AdministracionCiclo>> DataTable()
        {
            this.logger.LogInformation("CicloModule/DataTable()");
            var resultado = await this.cicloRepository.GetAll();
            return resultado;
        }
        public async Task<AdministracionCiclo> EditUno(int lcicloId)
        {
            this.logger.LogInformation("CicloModule/EditUno({lcicloId})", lcicloId);
            var resultado = await this.cicloRepository.GetOne(lcicloId);
            return resultado;
        }
        public async Task<string> Store(AdministracionCiclo administracionCiclo, int funcionarioId)
        {
            var funcionario = await this.funcionarioRepository.GetOne(funcionarioId);
            administracionCiclo.susuarioadd = funcionario.Alias;
            administracionCiclo.susuariomod = funcionario.Alias;
            administracionCiclo.dtfechaadd = DateTime.Now;
            administracionCiclo.estadogestor = "0";
            this.logger.LogInformation("CicloModule/Store({administracionCiclo})", Helper.Log(administracionCiclo));
            var resultado = await this.cicloRepository.Store(administracionCiclo);

            return "registrado correctamente";
        }
        public async Task<string> Update(AdministracionCiclo administracionCiclo, int funcionarioId)
        {
            var funcionario = await this.funcionarioRepository.GetOne(funcionarioId);
            administracionCiclo.susuarioadd = funcionario.Alias;
            administracionCiclo.susuariomod = funcionario.Alias;
            this.logger.LogInformation("CicloModule/DataTable({administracionCiclo})", administracionCiclo);
            var resultado = await this.cicloRepository.Update(administracionCiclo);
            return "Modificado correctamente";
        }
        public async Task<string> Delete(int lcicloId, int funcionarioId)
        {
            var funcionario = await this.funcionarioRepository.GetOne(funcionarioId);
            this.logger.LogInformation("CicloModule/DataTable({administracionCiclo})", lcicloId);
            var resultado = await this.cicloRepository.Delete(lcicloId);
            return "Eliminado correctamente";
        }
    }
}