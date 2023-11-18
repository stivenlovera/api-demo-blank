using api_guardian.Dtos.Controllers;
using api_guardian.Entities.DBComisiones.Queries;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Entities.GrdSion.Views;
using api_guardian.Helpers;
using api_guardian.Models.Templates;
using api_guardian.Repository;
using api_guardian.Utils;
using DocumentoVentaSion.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Module
{
    public class ReporteRedModule
    {
        private readonly ILogger<ReporteRedModule> logger;
        private readonly ReporteRedRepository reporteRedRepository;
        private readonly AdministracionContactoRepository administracionContactoRepository;
        private readonly AdministracionCicloRepository administracionCicloRepository;
        private readonly GeneratePdf generatePdf;
        private readonly GetTemplate getTemplate;
        private readonly Converters converters;

        public ReporteRedModule(
            ILogger<ReporteRedModule> logger,
            ReporteRedRepository reporteRedRepository,
            AdministracionContactoRepository administracionContactoRepository,
            AdministracionCicloRepository administracionCicloRepository,
            GeneratePdf generatePdf,
            GetTemplate getTemplate,
            Converters converters
        )
        {
            this.logger = logger;
            this.reporteRedRepository = reporteRedRepository;
            this.administracionContactoRepository = administracionContactoRepository;
            this.administracionCicloRepository = administracionCicloRepository;
            this.generatePdf = generatePdf;
            this.getTemplate = getTemplate;
            this.converters = converters;
        }
        public async Task<List<QueryDetalleRed>> DataTable(ReqDataTableReporteRedDto reqDataTableReporteRedDto)
        {
            this.logger.LogInformation("ReporteRedModule/DataTable({dataTableReporteRedDto})", Helper.Log(reqDataTableReporteRedDto));
            var obtenerVenta = await this.reporteRedRepository.GetAllVentasNuevas(
                reqDataTableReporteRedDto.FechaInicio,
                reqDataTableReporteRedDto.FechaFin
            );
            //filtrando solo snroventa
            var listaNroVentas = obtenerVenta.Select(x => $"{x.IDVENTA}-{x.IDPRODUCTO}").ToList();
            if (reqDataTableReporteRedDto.Tipo == "nuevasRedes")
            {
                var listRedes = await this.reporteRedRepository.GetAmpliacionRedes(listaNroVentas,
                    reqDataTableReporteRedDto.FechaInicio,
                    reqDataTableReporteRedDto.FechaFin,
                    reqDataTableReporteRedDto.Ciclo,
                    reqDataTableReporteRedDto.Baja,
                    reqDataTableReporteRedDto.AutoVenta
                );
                return listRedes;
            }
            else
            {
                var listRedes = await this.reporteRedRepository.GetNuevasRedes(listaNroVentas,
                   reqDataTableReporteRedDto.FechaInicio,
                    reqDataTableReporteRedDto.FechaFin,
                    reqDataTableReporteRedDto.Ciclo,
                    reqDataTableReporteRedDto.Baja,
                    reqDataTableReporteRedDto.AutoVenta
                );
                return listRedes;
            }
        }
        public async Task<FileStreamResult> ViewReportRedesActivas(ReqPreviewReporteRedDto reqPreviewReporteRedDto)
        {
            var ciclo = await administracionCicloRepository.GetAll();
            var rows = reqPreviewReporteRedDto.Rows;

            var data = new ReporteRedActivas()
            {
                Titulo = "Reporte Redes activa",
                DataGrid = new ListaPersonas()
                {
                    Titulo = "",
                    ListPersonas = rows
                }

            };
            var htmlContent = this.getTemplate.SearchTemplate("redesActivas", data);
            var pdf = this.generatePdf.Generate(htmlContent, "xxxx-xxxx-xxxx", true);
            return converters.ConverterToPdf(pdf, "demo");
        }
    }
}