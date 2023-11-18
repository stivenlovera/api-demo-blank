using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Repository.BDGrdSion;

namespace api_guardian.Module
{
    public class AdministracionEmpresaModule
    {
        private readonly ILogger<AdministracionEmpresaModule> logger;
        private readonly AdministracionEmpresaRepository administracionEmpresaRepository;

        public AdministracionEmpresaModule(
            ILogger<AdministracionEmpresaModule> logger,
            AdministracionEmpresaRepository administracionEmpresaRepository
        )
        {
            this.logger = logger;
            this.administracionEmpresaRepository = administracionEmpresaRepository;
        }
        public async Task<List<AdministracionEmpresa>> DataTable()
        {
            
            var dataTable =await  this.administracionEmpresaRepository.GetAll();
            return dataTable;
        }
    }
}