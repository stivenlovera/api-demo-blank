using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Module.ConsolidadosModule;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/reporte-consolidado")]
    public class ResportConsolidadoController : ControllerBase
    {
        private readonly ILogger<ResportConsolidadoController> _logger;
        private readonly ConsolidadosReports _consolidadosReports;

        public ResportConsolidadoController(
            ILogger<ResportConsolidadoController> logger,
            ConsolidadosReports consolidadosReports
        )
        {
            this._logger = logger;
            this._consolidadosReports = consolidadosReports;
        }

        [HttpGet(Name = "ViewReport")]
        public FileStreamResult ViewReport()
        {
            return this._consolidadosReports.ReportePorEmpresa();
        }
    }
}