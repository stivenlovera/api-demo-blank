using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.DBComisiones;
using api_guardian.Entities.GrdSion;
using api_guardian.Repository.BDGrdSion;

namespace api_guardian.Module
{
    public class AdministracionContactoModule
    {
        private readonly ILogger<AdministracionContactoModule> logger;
        private readonly AdministracionContactoRepository administracionContactoRepository;

        public AdministracionContactoModule(
            ILogger<AdministracionContactoModule> logger,
            AdministracionContactoRepository administracionContactoRepository
        )
        {
            this.logger = logger;
            this.administracionContactoRepository = administracionContactoRepository;
        }
        public async Task<List<AdministracionContacto>> ObtenerTodo()
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodo()");
            var frelancers = await this.administracionContactoRepository.ObtenerTodoSelect();
            return frelancers;
        }
        public async Task<List<AdministracionContacto>> ObtenerTodoSelect()
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodoSelect()");
            var frelancers = await this.administracionContactoRepository.ObtenerTodoSelect();
            return frelancers;
        }

        public async Task<AdministracionContacto> ObtenerUno(int LcontactoId)
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodoUno({LcontactoId})", LcontactoId);
            var frelancer = await this.administracionContactoRepository.ObtenerUno(LcontactoId);
            return frelancer;
        }
        public async Task<AdministracionContacto> Insertar(int LcontactoId)
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodoUno({LcontactoId})", LcontactoId);
            var frelancer = await this.administracionContactoRepository.ObtenerUno(LcontactoId);
            return frelancer;
        }
        /* public async Task<AdministracionContacto> InsertarAdicionales(AdministracionContacto administracionContacto)
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodoUno({LcontactoId})", LcontactoId);
            var frelancer = await this.administracionContactoRepository.ObtenerUno(LcontactoId);
            return frelancer;
        }
        public async Task<AdministracionContacto> InsertarAdicionalesCompleto(AdministracionContacto administracionContacto)
        {
            this.logger.LogInformation("AdministracionContactoModule/ObtenerTodoUno({LcontactoId})", LcontactoId);
            var frelancer = await this.administracionContactoRepository.ObtenerUno(LcontactoId);
            return frelancer;
        } */
    }
}