using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities.DBComisiones.Queries;
using api_guardian.Entities.DBComisiones.Views;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Entities.GrdSion.Views;
using api_guardian.Module;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers;

[ApiController]
[Route("api/reporte-red")]
public class ReporteRedController : ControllerBase
{
    private readonly ILogger<ReporteRedController> logger;
    private readonly ReporteRedModule reporteRedModule;

    public ReporteRedController(
        ILogger<ReporteRedController> logger,
        ReporteRedModule reporteRedModule
        )
    {
        this.logger = logger;
        this.reporteRedModule = reporteRedModule;
    }
    [HttpPost("Data-table")]
    public async Task<ResponseDto<List<QueryDetalleRed>>> DataTable(
        [FromBody] ReqDataTableReporteRedDto reqDataTableReporteRedDto
    )
    {
        this.logger.LogInformation("{methodo}{path} DataTable() Inizialize ...", Request.Method, Request.Path);
        try
        {
            var listaVentas = await this.reporteRedModule.DataTable(reqDataTableReporteRedDto);
            var response = new ResponseDto<List<QueryDetalleRed>>
            {
                Status = 1,
                Data = listaVentas,
                Message = "DataTable"
            };
            this.logger.LogInformation("DataTable() SUCCESS => {response}", Helper.Log(response));
            return response;
        }
        catch (System.Exception e)
        {
            var response = new ResponseDto<List<QueryDetalleRed>>
            {
                Status = 0,
                Data = null,
                Message = "Ocurrio un error inesperado"
            };
            this.logger.LogCritical("DataTable() SUCCESS => {e}", e.Message);
            return response;

        }
    }
    [HttpPost("report-activas")]
    public async Task<FileStreamResult> ViewReportRedesActivas(
        [FromBody] ReqPreviewReporteRedDto reqPreviewReporteRedDto
    )
    {
        return await this.reporteRedModule.ViewReportRedesActivas(reqPreviewReporteRedDto);
    }
}