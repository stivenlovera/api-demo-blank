using api_guardian.Dtos.Controllers;
using api_guardian.Entities.GrdSion;
using api_guardian.Helpers;
using api_guardian.Repository.BDGrdSion;
using api_guardian.Utils;
using ClosedXML.Excel;
using DocumentoVentaSion.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Module.ConsolidadosModule
{
    public class PagoConsolidadoModule
    {
        private readonly ILogger<PagoConsolidadoModule> logger;
        private readonly HistorialPagoComisionesRepository historialPagoComisionesRepository;
        private readonly PagoComisionesRepository pagoComisionesRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly GetTemplate getTemplate;
        private readonly GeneratePdf generatePdf;
        private readonly Converters converters;

        public PagoConsolidadoModule(
            ILogger<PagoConsolidadoModule> logger,
            HistorialPagoComisionesRepository historialPagoComisionesRepository,
            PagoComisionesRepository pagoComisionesRepository,
            IWebHostEnvironment webHostEnvironment,
            GetTemplate getTemplate,
            GeneratePdf generatePdf,
            Converters converters
        )
        {
            this.logger = logger;
            this.historialPagoComisionesRepository = historialPagoComisionesRepository;
            this.pagoComisionesRepository = pagoComisionesRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.getTemplate = getTemplate;
            this.generatePdf = generatePdf;
            this.converters = converters;
        }
        public async Task<List<PagoConsolidado>> ObtenerTodoPagoConsolidado()
        {
            var pagoConsolidados = await pagoComisionesRepository.ObtenerTodo();
            return pagoConsolidados;
        }

        public byte[] ExportExcelPlantilla(ReqExportHistorialPagoComisionesDto reqConsolidadoDto)
        {
            this.logger.LogInformation("ubicacion de plantilla excel {path}", Helper.Log(reqConsolidadoDto));
            //OBTENER plantilla
            var path = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "template", "consolidado", "template_consolidado.xlsx");
            this.logger.LogWarning("ubicacion de plantilla excel {path}", path);
            //Modificacion de plantilla
            XLWorkbook workbook = new XLWorkbook(path);
            var hoja = workbook.Worksheets.Worksheet(1);
            //nombre documento
            hoja.Cell(1, 4).SetValue(Helper.MayusMinus(reqConsolidadoDto.Filtro.Nombre));
            //ciclo
            hoja.Cell(4, 2).SetValue(Helper.MayusMinus("reqConsolidadoDto.Filtro.Ciclo.snombre"));
            //fecha inicio
            hoja.Cell(4, 5).SetValue(Helper.MayusMinus("reqConsolidadoDto.Filtro.Ciclo.dtfechainicio.ToString()"));
            //fecha fin
            hoja.Cell(4, 9).SetValue(Helper.MayusMinus("reqConsolidadoDto.Filtro.Ciclo.dtfechafin.ToString()"));
            //lista empresas
            var groupEmpresas = reqConsolidadoDto.DataTable.GroupBy(x => x.Empresa).ToList();
            var listEmpresas = groupEmpresas.Select(x => x).ToList();
            this.logger.LogWarning("cantidad de empresas {listaEmpressas}", Helper.Log(listEmpresas));
            //totales
            decimal totalComision = 0;
            decimal servicio = 0;
            decimal totalComisiones = 0;
            decimal porcentaje13 = 0;
            decimal porcentaje87 = 0;
            decimal totalRetencion = 0;
            decimal total = 0;
            decimal totaPagar = 0;

            //var stringListaEmpressas = System.String.Join(", ", listaEmpressas.ToArray());
            hoja.Cell(3, 5).SetValue(Helper.MayusMinus("stringListaEmpressas"));
            var row = 7;
            var ultimaLinea = reqConsolidadoDto.DataTable.Count() + row;
            foreach (var data in reqConsolidadoDto.DataTable)
            {
                hoja.Cell(row, 1).SetValue(data.Scodigo);
                hoja.Cell(row, 2).SetValue(data.Empresa);
                hoja.Cell(row, 3).SetValue(data.Snombrecompleto);
                hoja.Cell(row, 4).SetValue(data.TotalComisionVtaPersonal);
                hoja.Cell(row, 5).SetValue(data.TotalComisionVtaGrupoResidual);
                hoja.Cell(row, 6).SetValue(data.TotalComision);
                hoja.Cell(row, 7).SetValue(data.Valor13);
                hoja.Cell(row, 8).SetValue(data.Valor87);
                hoja.Cell(row, 9).SetValue(data.Retencion);
                hoja.Cell(row, 10).SetValue(data.TotalComisionRetencion);
                hoja.Cell(row, 11).SetValue(data.TotalComision);
                hoja.Cell(row, 12).SetValue(data.TotalPagar);

                //suma de totales
                totalComision += data.TotalComisionVtaPersonal;
                servicio += data.TotalComisionVtaGrupoResidual;
                totalComisiones += data.TotalComision;
                porcentaje13 += data.Valor13;
                porcentaje87 += data.Valor87;
                totalRetencion += data.TotalComisionRetencion;
                total += data.TotalComision;
                totaPagar += data.TotalPagar;

                row++;
            }
            //add ultima linea

            hoja.Cell(ultimaLinea, 1).SetValue("Total:");
            hoja.Cell(ultimaLinea, 4).SetValue(totalComision);
            hoja.Cell(ultimaLinea, 5).SetValue(servicio);
            hoja.Cell(ultimaLinea, 6).SetValue(totalComisiones);
            hoja.Cell(ultimaLinea, 7).SetValue(porcentaje13);
            hoja.Cell(ultimaLinea, 8).SetValue(porcentaje87);
            hoja.Cell(ultimaLinea, 10).SetValue(totalRetencion);
            hoja.Cell(ultimaLinea, 11).SetValue(total);
            hoja.Cell(ultimaLinea, 12).SetValue(totaPagar);

            hoja.Row(ultimaLinea).Style.Fill.BackgroundColor = XLColor.YellowRyb;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }

        public FileStreamResult ExportPdfPlantilla(ReqExportHistorialPagoComisionesDto reqConsolidadoDto)
        {
            this.logger.LogInformation("ExportPdfPlantilla ({reqConsolidadoDto})", Helper.Log(reqConsolidadoDto));
            var htmlContent = this.getTemplate.SearchTemplate("consolidado", reqConsolidadoDto);

            var pdf = this.generatePdf.Generate(htmlContent, "");
            return this.converters.ConverterToPdf(pdf, "Reporte consolidado comsion servicio");
        }

        public async Task<List<HistorialPagoComisionConsolidado>> ObtenerTodoHistorialPagoByPagoComisionId(int PagoComisionId)
        {
            var historial = await this.historialPagoComisionesRepository.ObtenerTodoByPagoComisionId(PagoComisionId);
            return historial;
        }
    }
}