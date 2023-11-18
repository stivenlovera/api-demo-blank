
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Module;
using api_guardian.Dtos.Response;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Module.ConsolidadosModule;
using api_guardian.Utils;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/consolidado")]
    public class ReportConsolidadoController : ControllerBase
    {
        private readonly ILogger<ReportConsolidadoController> _logger;
        private readonly ConsolidadosReportsModule _consolidadosReportsModule;
        private readonly ConsolidadoModule _consolidadoModule;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ReportConsolidadoController(
            ILogger<ReportConsolidadoController> logger,
            ConsolidadosReportsModule consolidadosReports,
            ConsolidadoModule consolidadoModule,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this._logger = logger;
            this._consolidadosReportsModule = consolidadosReports;
            this._consolidadoModule = consolidadoModule;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ResponseDto<List<ConsolidadoQuery>>> Index([FromBody] Filtro filtro)
        {
            this._logger.LogInformation("{methodo}{path} Index() Inizialize ...", Helper.Log(filtro));
            var resultado = await this._consolidadoModule.Index(filtro.Ciclo, filtro.Empresas);
            var response = new ResponseDto<List<ConsolidadoQuery>>()
            {
                Status = 1,
                Message = "mostrando Data",
                Data = resultado
            };
            this._logger.LogInformation($"ResportConsolidadoController/Index() => RESPONSE: /n {JsonConvert.SerializeObject(response, Formatting.Indented)} ");
            return response;
        }

        [HttpGet("report-empresa-base64")]
        public byte[] ViewReportBase64()
        {
            return this._consolidadosReportsModule.ReportePorEmpresaBase64();
        }
        [HttpPost("export-pdf")]
        public FileStreamResult ExportPdf([FromBody] ReqConsolidadoDto reqConsolidadoDto)
        {
            this._logger.LogInformation("{methodo}{path} ExportPdf({reqConsolidadoDto}) Inizialize ...", Helper.Log(reqConsolidadoDto));
            var resultado = this._consolidadoModule.ExportPdfPlantilla(reqConsolidadoDto);
            return resultado;
        }
        [HttpPost("export-excel")]
        public FileResult ExportExcel([FromBody] ReqConsolidadoDto reqConsolidadoDto)
        {
            this._logger.LogInformation("{methodo}{path} ExportExcel({reqConsolidadoDto}) Inizialize ...", Helper.Log(reqConsolidadoDto));
            var resultado = this._consolidadoModule.ExportExcelPlantilla(reqConsolidadoDto);
            return File(resultado, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }
    }
}