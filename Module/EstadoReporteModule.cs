using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.GrdSion;
using api_guardian.Repository.BDGrdSion;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace api_guardian.Module
{
    public class EstadoReporteModule
    {
        private readonly ILogger<EstadoReporteModule> logger;
        private readonly EstadoReporteRepository estadoReporteRepository;

        public EstadoReporteModule(
             ILogger<EstadoReporteModule> logger,
             EstadoReporteRepository estadoReporteRepository
        )
        {
            this.logger = logger;
            this.estadoReporteRepository = estadoReporteRepository;
        }
        public async Task<List<EstadoReporte>> MostrarTodo()
        {
            var estado = await this.estadoReporteRepository.ObtenerTodo();
            return estado;
        }
    }
}