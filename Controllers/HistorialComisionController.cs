using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities.GrdSion;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Module.ConsolidadosModule;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/historial-consolidado")]
    public class HistorialComisionController : ControllerBase
    {
        private readonly ILogger<HistorialComisionController> _logger;
        private readonly ConsolidadoModule consolidadoModule;

        public HistorialComisionController(
            ILogger<HistorialComisionController> _logger,
            ConsolidadoModule consolidadoModule
        )
        {
            this._logger = _logger;
            this.consolidadoModule = consolidadoModule;
        }

        [HttpGet]
        public async Task<ResponseDto<List<ComisionConsolidado>>> ObtenerConsolidado()
        {
            this._logger.LogInformation("{methodo}{path} ObtenerConsolidado() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var resultado = await this.consolidadoModule.GetConsolidadosHistoricos();
                this._logger.LogWarning("ObtenerConsolidado() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<List<ComisionConsolidado>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Lista de consolidados"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<ComisionConsolidado>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("ObtenerConsolidado() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpGet("consolidado/{id}")]
        public async Task<ResponseDto<List<ConsolidadoQuery>>> ObtenerOneConsolidado(int id)
        {
            this._logger.LogInformation("{methodo}{path} ObtenerOneConsolidado({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var resultado = await this.consolidadoModule.GetOneConsolidadosHistorico(id);
                this._logger.LogWarning("ObtenerOneConsolidado() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<List<ConsolidadoQuery>>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "datos extraidos correctamente"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<ConsolidadoQuery>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("ObtenerOneConsolidado() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpPost("config-pago-historico")]
        public async Task<ResponseDto<ResHistorialComisioneCambioEmpresaDto>> CambioEmpresa([FromBody] ReqHistorialComisionesCambioEmpresaDto reqHistorialComisionesCambioEmpresaDto)
        {
            this._logger.LogInformation("{methodo}{path} CambioEmpresa({reqHistorialComisionesCambioEmpresaDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqHistorialComisionesCambioEmpresaDto));
            try
            {
                var resultado = await this.consolidadoModule.PrepararCambioEmpresa(reqHistorialComisionesCambioEmpresaDto);
                this._logger.LogWarning("CambioEmpresa() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<ResHistorialComisioneCambioEmpresaDto>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "configuracion de para pago"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<ResHistorialComisioneCambioEmpresaDto>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this._logger.LogCritical("CambioEmpresa() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpPost("store-pago-historico")]
        public async Task<ResponseDto<bool>> StoreCambioEmpresa([FromBody] ReqHistorialComisionesRegistrarCambioEmpresaDto reqHistorialComisionesRegistrarCambioEmpresaDto)
        {
            this._logger.LogInformation("{methodo}{path} StoreCambioEmpresa({reqHistorialComisionesRegistrarCambioEmpresaDto}) Inizialize ...", Request.Method, Request.Path, reqHistorialComisionesRegistrarCambioEmpresaDto);
            try
            {
                var resultado = await this.consolidadoModule.ProcesarCambioEmpresa(reqHistorialComisionesRegistrarCambioEmpresaDto);
                this._logger.LogWarning("StoreCambioEmpresa() SUCCESS => {response}", Helper.Log(resultado));
                var response = new ResponseDto<bool>
                {
                    Status = 1,
                    Data = resultado,
                    Message = "Registrado correctamente"
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
                this._logger.LogCritical("StoreCambioEmpresa() ERROR => {e}", e.Message);
                return response;
            }
        }
        //REPORTE EXPORT
        [HttpPost("export-pdf")]
        public async Task<FileStreamResult> ExportPdf([FromBody] ReqExportHistorialComisionesDto reqExportHistorialComisionesDto)
        {
            this._logger.LogInformation("{methodo}{path} ExportPdf({reqConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqExportHistorialComisionesDto));
            var resultado = await this.consolidadoModule.ExportPdfPlantilla(reqExportHistorialComisionesDto);
            return resultado;
        }

        [HttpPost("export-excel")]
        public async Task<FileResult> ExportExcel([FromBody] ReqExportHistorialComisionesDto reqExportHistorialComisionesDto)
        {
            this._logger.LogInformation("{methodo}{path} ExportExcel({reqConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqExportHistorialComisionesDto));
            var resultado = await this.consolidadoModule.ExportExcelPlantilla(reqExportHistorialComisionesDto);
            return File(resultado, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }
    }
}