
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Module.ConsolidadosModule;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/generar-consolidado")]
    public class GenerarConsolidadoController : ControllerBase
    {
        private readonly ILogger<GenerarConsolidadoController> logger;
        private readonly GenerarConsolidadoModule generarConsolidadoModule;

        public GenerarConsolidadoController(
            ILogger<GenerarConsolidadoController> logger,
            GenerarConsolidadoModule generarConsolidadoModule
        )
        {
            this.logger = logger;
            this.generarConsolidadoModule = generarConsolidadoModule;
        }

        [HttpPost]
        public async Task<ResponseDto<List<ConsolidadoQuery>>> Index([FromBody] Filtro filtro)
        {
            this.logger.LogInformation("{methodo}{path} Index({filtro}) Inizialize ...", Request.Method, Request.Path, Helper.Log(filtro));
            var resultado = await this.generarConsolidadoModule.Index(filtro.Ciclo, filtro.Empresas);
            var response = new ResponseDto<List<ConsolidadoQuery>>()
            {
                Status = 1,
                Message = "datos generados correctamente",
                Data = resultado
            };
            this.logger.LogInformation($"ResportConsolidadoController/Index() => RESPONSE: /n {JsonConvert.SerializeObject(response, Formatting.Indented)} ");
            return response;
        }

        [HttpPost("export-pdf")]
        public FileStreamResult ExportPdf([FromBody] ReqConsolidadoDto reqConsolidadoDto)
        {
            this.logger.LogInformation("{methodo}{path} ExportPdf({reqConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqConsolidadoDto));
            var resultado = this.generarConsolidadoModule.ExportPdfPlantilla(reqConsolidadoDto);
            return resultado;
        }

        [HttpPost("export-excel")]
        public FileResult ExportExcel([FromBody] ReqConsolidadoDto reqConsolidadoDto)
        {
            this.logger.LogInformation("{methodo}{path} ExportExcel({reqConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqConsolidadoDto));
            var resultado = this.generarConsolidadoModule.ExportExcelPlantilla(reqConsolidadoDto);
            return File(resultado, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }
        
        [HttpPost("verificar")]
        public async Task<ResponseDto<bool>> Verificar([FromBody] AdministracionCiclo Ciclo)
        {
            this.logger.LogInformation("{methodo}{path} Verificar({Ciclo}) Inizialize ...", Request.Method, Request.Path, Helper.Log(Ciclo));
            try
            {
                var resultado = await this.generarConsolidadoModule.VerificarConsolidado(Ciclo);
                this.logger.LogWarning("Verificar() SUCCESS => {response}", Helper.Log(resultado));
                return resultado;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<bool>
                {
                    Status = 0,
                    Data = false,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("Verificar() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpPost("store-consolidado")]
        public async Task<ResponseDto<bool>> GenerarConsolidado([FromBody] ReqGenerarConsolidadoDto reqGenerarConsolidadoDto)
        {
            this.logger.LogInformation("{methodo}{path} GenerarConsolidado({reqGenerarConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqGenerarConsolidadoDto));
            try
            {
                var resultado = await this.generarConsolidadoModule.StoreConsolidado(reqGenerarConsolidadoDto);
                this.logger.LogWarning("GenerarConsolidado() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<bool>
                {
                    Status = 1,
                    Data = true,
                    Message = resultado
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<bool>
                {
                    Status = 0,
                    Data = false,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("GenerarConsolidado() ERROR => {e}", e.Message);
                return response;
            }
        }
    }
}