using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities.GrdSion;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Module.ConsolidadosModule;
using api_guardian.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/historial-pago")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HistorialPagoConsolidadoController : ControllerBase
    {
        private readonly ILogger<HistorialPagoConsolidadoController> logger;
        private readonly PagoConsolidadoModule pagoConsolidadoModule;

        public HistorialPagoConsolidadoController(
            ILogger<HistorialPagoConsolidadoController> logger,
            PagoConsolidadoModule pagoConsolidadoModule
        )
        {
            this.logger = logger;
            this.pagoConsolidadoModule = pagoConsolidadoModule;
        }
        [HttpGet("consolidados")]
        public async Task<ResponseDto<List<PagoConsolidado>>> ObtenerTodoConsolidado()
        {
            this.logger.LogInformation("{methodo}{path} ObtenerTodoConsolidado() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var historicos = await this.pagoConsolidadoModule.ObtenerTodoPagoConsolidado();
                this.logger.LogWarning("ObtenerTodoConsolidado() SUCCESS => {response}", Helper.Log(null));
                var response = new ResponseDto<List<PagoConsolidado>>
                {
                    Status = 1,
                    Data = historicos,
                    Message = "Lista de pago consolidado"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<PagoConsolidado>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("ObtenerTodoConsolidado() ERROR => {e}", e.Message);
                return response;
            }
        }
        [HttpGet("historial/{id}")]
        public async Task<ResponseDto<List<HistorialPagoComisionConsolidado>>> ObtenerHistorialPagoConsolidado(int id)
        {
            this.logger.LogInformation("{methodo}{path} ObtenerHistorialPagoConsolidado({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var historicos = await this.pagoConsolidadoModule.ObtenerTodoHistorialPagoByPagoComisionId(id);
                this.logger.LogWarning("ObtenerHistorialPagoConsolidado() SUCCESS => {response}", Helper.Log(null));
                var response = new ResponseDto<List<HistorialPagoComisionConsolidado>>
                {
                    Status = 1,
                    Data = historicos,
                    Message = "Historial pago consolidado"
                };
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<HistorialPagoComisionConsolidado>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("ObtenerHistorialPagoConsolidado() ERROR => {e}", e.Message);
                return response;
            }
        }

        [HttpPost("export-pdf")]
        public FileStreamResult ExportPdf([FromBody] ReqExportHistorialPagoComisionesDto reqExportHistorialPagoComisionesDto)
        {
            this.logger.LogInformation("{methodo}{path} ExportPdf({reqConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqExportHistorialPagoComisionesDto));
            var resultado = this.pagoConsolidadoModule.ExportPdfPlantilla(reqExportHistorialPagoComisionesDto);
            return resultado;
        }

        [HttpPost("export-excel")]
        public FileResult ExportExcel([FromBody] ReqExportHistorialPagoComisionesDto reqExportHistorialPagoComisionesDto)
        {
            this.logger.LogInformation("{methodo}{path} ExportExcel({reqConsolidadoDto}) Inizialize ...", Request.Method, Request.Path, Helper.Log(reqExportHistorialPagoComisionesDto));
            var resultado = this.pagoConsolidadoModule.ExportExcelPlantilla(reqExportHistorialPagoComisionesDto);
            return File(resultado, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }
    }
}