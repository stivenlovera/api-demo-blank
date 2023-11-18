using api_guardian.Dtos.Controllers;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Helpers;
using api_guardian.Repository.BDGrdSion;
using api_guardian.Utils;
using ClosedXML.Excel;
using DocumentoVentaSion.Utils;
using GrapeCity.DataVisualization.TypeScript;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Module.ConsolidadosModule
{
    public class ConsolidadoModule
    {
        private readonly ILogger<ConsolidadoModule> _logger;
        private readonly ConsolidadoRepository consolidadoRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly GetTemplate getTemplate;
        private readonly GeneratePdf generatePdf;
        private readonly Converters converters;

        public ConsolidadoModule(
            ILogger<ConsolidadoModule> logger,
            ConsolidadoRepository ConsolidadoRepository,
            IWebHostEnvironment webHostEnvironment,
            GetTemplate getTemplate,
            GeneratePdf generatePdf,
            Converters converters
        )
        {
            this._logger = logger;
            consolidadoRepository = ConsolidadoRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.getTemplate = getTemplate;
            this.generatePdf = generatePdf;
            this.converters = converters;
        }
        public async Task<List<ConsolidadoQuery>> Index(AdministracionCiclo ciclo, List<AdministracionEmpresa> empresas)
        {
            _logger.LogInformation($"ConsolidadoModule/Index() => iniciando...");
            var listEmpresasId = empresas.Select(x => x.LempresaId).ToList();
            var obtenerConsolidado = await this.consolidadoRepository.GetAll(Convert.ToInt32(ciclo.lcicloId), listEmpresasId);
            _logger.LogInformation("ConsolidadoModule/Index() => {obtenerConsolidado.Count} registros encontrados ", obtenerConsolidado.Count);
            //convert nombre completo a minuscula
            foreach (var data in obtenerConsolidado)
            {
                data.Snombrecompleto = Helper.MayusMinus(data.Snombrecompleto);
            }
            return obtenerConsolidado;
        }

        public byte[] ExportExcelPlantilla(ReqConsolidadoDto reqConsolidadoDto)
        {
            this._logger.LogInformation("ubicacion de plantilla excel {path}", Helper.Log(reqConsolidadoDto));
            //OBTENER plantilla
            var path = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "template", "consolidado", "template_consolidado.xlsx");
            this._logger.LogWarning("ubicacion de plantilla excel {path}", path);

            //Modificacion de plantilla
            XLWorkbook workbook = new XLWorkbook(path);
            var hoja = workbook.Worksheets.Worksheet(1);
            //ciclo
            hoja.Cell(4, 2).SetValue(Helper.MayusMinus(reqConsolidadoDto.Filtro.Ciclo.snombre));
            //fecha inicio
            hoja.Cell(4, 5).SetValue(Helper.MayusMinus(reqConsolidadoDto.Filtro.Ciclo.dtfechainicio.ToString("dd/MM/yyyy")));
            //fecha fin
            hoja.Cell(4, 9).SetValue(Helper.MayusMinus(reqConsolidadoDto.Filtro.Ciclo.dtfechafin.ToString("dd/MM/yyyy")));
            //lista empresas
            var listaEmpressas = reqConsolidadoDto.Filtro.Empresas.Select(x => x.Snombre).ToList();

            this._logger.LogWarning("cantidad de empresas {listaEmpressas}", Helper.Log(listaEmpressas));
            var stringListaEmpressas = System.String.Join(", ", listaEmpressas.ToArray());
            hoja.Cell(3, 5).SetValue(Helper.MayusMinus(stringListaEmpressas));
            var row = 7;
            foreach (var data in reqConsolidadoDto.DataTable)
            {
                hoja.Cell(row, 1).SetValue(data.Scodigo);
                hoja.Cell(row, 2).SetValue(data.Empresa);
                hoja.Cell(row, 3).SetValue(data.Snombrecompleto);
                hoja.Cell(row, 4).SetValue(data.TotalComisionVtaPersonal);
                hoja.Cell(row, 5).SetValue(data.TotalComisionVtaGrupoResidual);
                hoja.Cell(row, 6).SetValue(data.Valor13);
                hoja.Cell(row, 7).SetValue(data.Valor87);
                hoja.Cell(row, 8).SetValue(data.Retencion);
                hoja.Cell(row, 9).SetValue(data.TotalComisionRetencion);
                hoja.Cell(row, 10).SetValue(data.TotalComision);
                hoja.Cell(row, 11).SetValue(data.TotalPagar);

                row++;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }
        public FileStreamResult ExportPdfPlantilla(ReqConsolidadoDto reqConsolidadoDto)
        {
             this._logger.LogInformation("ExportPdfPlantilla ({reqConsolidadoDto})", Helper.Log(reqConsolidadoDto));
            var htmlContent = this.getTemplate.SearchTemplate("consolidado", reqConsolidadoDto);

            var pdf = this.generatePdf.Generate(htmlContent, "");
            return this.converters.ConverterToPdf(pdf, "Reporte consolidado comsion servicio");
        }
    }
}