using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion;
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
        private readonly HistorialConsolidadoRepository historialConsolidadoRepository;
        private readonly AdministracionEmpresaRepository administracionEmpresaRepository;
        private readonly GetTemplate getTemplate;
        private readonly GeneratePdf generatePdf;
        private readonly Converters converters;
        private readonly PagoComisionesRepository pagoComisionesRepository;
        private readonly HistorialPagoComisionesRepository historialPagoComisionesRepository;
        private readonly ConfigCambioEmpresaRepository configCambioEmpresaRepository;

        public ConsolidadoModule(
            ILogger<ConsolidadoModule> logger,
            ConsolidadoRepository ConsolidadoRepository,
            IWebHostEnvironment webHostEnvironment,
            HistorialConsolidadoRepository historialConsolidadoRepository,
            AdministracionEmpresaRepository administracionEmpresaRepository,
            GetTemplate getTemplate,
            GeneratePdf generatePdf,
            Converters converters,
            PagoComisionesRepository PagoComisionesRepository,
            HistorialPagoComisionesRepository historialPagoComisionesRepository,
            ConfigCambioEmpresaRepository configCambioEmpresaRepository
        )
        {
            this._logger = logger;
            consolidadoRepository = ConsolidadoRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.historialConsolidadoRepository = historialConsolidadoRepository;
            this.administracionEmpresaRepository = administracionEmpresaRepository;
            this.getTemplate = getTemplate;
            this.generatePdf = generatePdf;
            this.converters = converters;
            pagoComisionesRepository = PagoComisionesRepository;
            this.historialPagoComisionesRepository = historialPagoComisionesRepository;
            this.configCambioEmpresaRepository = configCambioEmpresaRepository;
        }
        public byte[] ExportExcelPlantilla(ReqExportHistorialComisionesDto reqExportHistorialComisionesDto)
        {
            this._logger.LogInformation("ubicacion de plantilla excel {path}", Helper.Log(reqExportHistorialComisionesDto));
            //OBTENER plantilla
            var path = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "template", "consolidado", "template_consolidado.xlsx");
            this._logger.LogWarning("ubicacion de plantilla excel {path}", path);
            //Modificacion de plantilla
            XLWorkbook workbook = new XLWorkbook(path);
            var hoja = workbook.Worksheets.Worksheet(1);
            //ciclo
            hoja.Cell(4, 2).SetValue(Helper.MayusMinus(reqExportHistorialComisionesDto.Consolidado.Ciclo_id.toString()));
            //fecha inicio
            hoja.Cell(4, 5).SetValue(Helper.MayusMinus(""));
            //fecha fin
            hoja.Cell(4, 9).SetValue(Helper.MayusMinus(""));
            //lista empresas
            var listaEmpressas = reqExportHistorialComisionesDto.DataTable;
            this._logger.LogWarning("cantidad de empresas {listaEmpressas}", Helper.Log(listaEmpressas));
            //totales
            decimal totalComision = 0;
            decimal servicio = 0;
            decimal totalComisiones = 0;
            decimal porcentaje13 = 0;
            decimal porcentaje87 = 0;
            decimal totalRetencion = 0;
            decimal total = 0;
            decimal totaPagar = 0;

            /* var stringListaEmpressas = System.String.Join(", ", listaEmpressas.ToArray()); */
            hoja.Cell(3, 5).SetValue(Helper.MayusMinus("stringListaEmpressas"));
            var row = 7;
            var ultimaLinea = reqExportHistorialComisionesDto.DataTable.Count() + row;
            foreach (var data in reqExportHistorialComisionesDto.DataTable)
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
        public FileStreamResult ExportPdfPlantilla(ReqExportHistorialComisionesDto reqExportHistorialComisionesDto)
        {
            this._logger.LogInformation("ExportPdfPlantilla ({reqConsolidadoDto})", Helper.Log(reqExportHistorialComisionesDto));
            var htmlContent = this.getTemplate.SearchTemplate("consolidado", reqExportHistorialComisionesDto);

            var pdf = this.generatePdf.Generate(htmlContent, "");
            return this.converters.ConverterToPdf(pdf, "Reporte consolidado comsion servicio");
        }
        /*HISTORIAL CONSOLIDAD*/
        public async Task<List<ComisionConsolidado>> GetConsolidadosHistoricos()
        {
            this._logger.LogInformation("ConsolidadoModule/GetConsolidadosHistorico ()");
            var consolidadoHistoricos = await historialConsolidadoRepository.ObtenerComisionesConsolidado();
            return consolidadoHistoricos;
        }
        public async Task<List<ConsolidadoQuery>> GetOneConsolidadosHistorico(int id)
        {
            this._logger.LogInformation("ConsolidadoModule/GetOneConsolidadosHistorico ({id})", id);
            var consolidadoHistoricos = await historialConsolidadoRepository.ObtenerUnoHistoricoConsolidado(id);
            return consolidadoHistoricos;
        }
        public async Task<ResHistorialComisioneCambioEmpresaDto> PrepararCambioEmpresa(ReqHistorialComisionesCambioEmpresaDto reqHistorialComisionesCambioEmpresaDto)
        {
            this._logger.LogInformation("ConsolidadoModule/PrepararCambioEmpresa ({reqHistorialComisionesCambioEmpresaDto})", Helper.Log(reqHistorialComisionesCambioEmpresaDto));
            //var configCambioEmpresa = await historialConsolidadoRepository.PrepararCambioEmpresa(reqHistorialComisionesCambioEmpresaDto.Nombre);

            var configCambioEmpresa = await historialConsolidadoRepository.BuscarEmpresaConsolidado(reqHistorialComisionesCambioEmpresaDto.ComisionConsolidadoId);
            //var insertCambioEmpresa = await historialConsolidadoRepository.InsertCambioEmpresa(obtenerCambioEmpresa);
            var resultado = new ResHistorialComisioneCambioEmpresaDto();

            resultado.PagoConsolidado = new PagoConsolidado
            {
                Descripcion = "",
                EstadoReporteId = 1,
                Nombre = "",
                PagoConsolidadoId = 0
            };
            resultado.ConfigCambioEmpresa = new List<ConfigCambioEmpresaDto>();
            foreach (var confg in configCambioEmpresa)
            {
                resultado.ConfigCambioEmpresa.Add(
                  new ConfigCambioEmpresaDto
                  {
                      PagoConsolidadoId = 0,
                      Empresa = await administracionEmpresaRepository.GetOne(confg.EmpresaId),
                      PagoEmpresa = await administracionEmpresaRepository.GetOne(confg.EmpresaId),
                      Nota = "Por defecto",
                      CambioEmpresaId = 0
                  }
                );
            }
            resultado.DataTable = new List<HistorialPagoComisionConsolidado>();
            return resultado;
        }
        public async Task<bool> ProcesarCambioEmpresa(ReqHistorialComisionesRegistrarCambioEmpresaDto reqHistorialComisionesRegistrarCambioEmpresaDto)
        {
            this._logger.LogInformation("ConsolidadoModule/ProcesarCambioEmpresa ({reqHistorialComisionesRegistrarCambioEmpresaDto})", Helper.Log(reqHistorialComisionesRegistrarCambioEmpresaDto));
            //desarmar propiedades
            var insertPagoComision = await pagoComisionesRepository.InsertPagoComision(reqHistorialComisionesRegistrarCambioEmpresaDto.PagoConsolidado);
            this._logger.LogInformation("Pago Id {insertPagoComision}", Helper.Log(insertPagoComision));
            //insertPagoComision.Id

            var configCambioEmpresa = reqHistorialComisionesRegistrarCambioEmpresaDto.ConfigCambioEmpresa.Select(x => new ConfigCambioEmpresa
            {
                ConfigCambioEmpresaId = x.CambioEmpresaId,
                PagoConsolidadoId = insertPagoComision.Id,
                EmpresaId = x.Empresa.LempresaId,
                Nota = x.Nota,
                PagoEmpresaId = x.PagoEmpresa.LempresaId
            }).ToList();

            var insertConfigCambioEmpresa = await configCambioEmpresaRepository.InsertConfigCambioEmpresa(configCambioEmpresa);
            this._logger.LogInformation("muestra de config {insertConfigPago}", Helper.Log(insertConfigCambioEmpresa));

            foreach (var historialPago in reqHistorialComisionesRegistrarCambioEmpresaDto.DataTable)
            {
                //cambio empresa
                var cambioEmpresaId = configCambioEmpresa.Where(x => x.EmpresaId == historialPago.LempresaId).FirstOrDefault().PagoEmpresaId;
                var CambioEmpresa = await administracionEmpresaRepository.GetOne(cambioEmpresaId);

                historialPago.PagoConsolidadoId = insertPagoComision.Id;
                historialPago.Empresa = CambioEmpresa.Empresa;
                historialPago.LempresaId = cambioEmpresaId;
            }
            var insercionHistorial = await historialPagoComisionesRepository.InsertPagoComision(reqHistorialComisionesRegistrarCambioEmpresaDto.DataTable);
            //response
            return true;
        }
    }
}