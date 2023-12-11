using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Helpers;
using api_guardian.Repository.BDGrdSion;
using api_guardian.Utils;
using ClosedXML.Excel;
using DocumentoVentaSion.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Module.ConsolidadosModule
{
    public class GenerarConsolidadoModule
    {
        private readonly ILogger<GenerarConsolidadoModule> logger;
        private readonly ConsolidadoRepository consolidadoRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly HistorialConsolidadoRepository historialConsolidadoRepository;
        private readonly GetTemplate getTemplate;
        private readonly GeneratePdf generatePdf;
        private readonly Converters converters;

        public GenerarConsolidadoModule(
           ILogger<GenerarConsolidadoModule> logger,
           ConsolidadoRepository ConsolidadoRepository,
           IWebHostEnvironment webHostEnvironment,
           HistorialConsolidadoRepository historialConsolidadoRepository,
           GetTemplate getTemplate,
           GeneratePdf generatePdf,
           Converters converters
       )
        {
            this.logger = logger;
            this.consolidadoRepository = ConsolidadoRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.historialConsolidadoRepository = historialConsolidadoRepository;
            this.getTemplate = getTemplate;
            this.generatePdf = generatePdf;
            this.converters = converters;
        }
        public async Task<List<ConsolidadoQuery>> Index(AdministracionCiclo ciclo, List<AdministracionEmpresa> empresas)
        {
            logger.LogInformation("ConsolidadosReportsModule/Index() => iniciando...");
            var listEmpresasId = empresas.Select(x => x.LempresaId).ToList();
            var obtenerConsolidado = await this.consolidadoRepository.GetAll(Convert.ToInt32(ciclo.lcicloId), listEmpresasId);
            logger.LogInformation("ConsolidadosReportsModule/Index() => {obtenerConsolidado.Count} registros encontrados ", obtenerConsolidado.Count);
            //convert nombre completo a minuscula
            foreach (var data in obtenerConsolidado)
            {
                data.Snombrecompleto = Helper.MayusMinus(data.Snombrecompleto);
            }
            return obtenerConsolidado;
        }

        public byte[] ExportExcelPlantilla(ReqConsolidadoDto reqConsolidadoDto)
        {
            this.logger.LogInformation("ubicacion de plantilla excel {path}", Helper.Log(reqConsolidadoDto));
            //OBTENER plantilla
            var path = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "template", "consolidado", "template_consolidado.xlsx");
            this.logger.LogWarning("ubicacion de plantilla excel {path}", path);
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
            this.logger.LogWarning("cantidad de empresas {listaEmpressas}", Helper.Log(listaEmpressas));
            //totales
            decimal totalComision = 0;
            decimal servicio = 0;
            decimal totalComisiones = 0;
            decimal porcentaje13 = 0;
            decimal porcentaje87 = 0;
            decimal totalRetencion = 0;
            decimal total = 0;
            decimal totaPagar = 0;

            var stringListaEmpressas = System.String.Join(", ", listaEmpressas.ToArray());
            hoja.Cell(3, 5).SetValue(Helper.MayusMinus(stringListaEmpressas));
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
        public FileStreamResult ExportPdfPlantilla(ReqConsolidadoDto reqConsolidadoDto)
        {
            this.logger.LogInformation("ExportPdfPlantilla ({reqConsolidadoDto})", Helper.Log(reqConsolidadoDto));
            var htmlContent = this.getTemplate.SearchTemplate("consolidado", reqConsolidadoDto);

            var pdf = this.generatePdf.Generate(htmlContent, "");
            return this.converters.ConverterToPdf(pdf, "Reporte consolidado comsion servicio");
        }
        //verificar consolidado
        public async Task<ResponseDto<bool>> VerificarConsolidado(AdministracionCiclo Ciclo)
        {
            var resultado = await historialConsolidadoRepository.GetVerificar(Convert.ToInt32((Ciclo.lcicloId)));
            if (resultado == null)
            {
                return new ResponseDto<bool>
                {
                    Data = true,
                    Message = "registrando consolidado",
                    Status = 1
                };
            }
            else
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Message = "Ya existe consolidado registrado",
                    Status = 1
                };
            }
        }
        public async Task<string> StoreConsolidado(ReqGenerarConsolidadoDto reqGenerarConsolidadoDto)
        {
            this.logger.LogInformation("ConsolidadosReportsModule/StoreConsolidado ({reqGenerarConsolidadoDto})", Helper.Log(reqGenerarConsolidadoDto));
            var insertComision = await historialConsolidadoRepository.InsertComision(reqGenerarConsolidadoDto.Descripcion, reqGenerarConsolidadoDto.Nombre, Convert.ToInt32(reqGenerarConsolidadoDto.EstadoReporte.EstadoReporteId), reqGenerarConsolidadoDto.Ciclo_id);
            var insertHistorial = await historialConsolidadoRepository.InsertHistorialConsolidado(insertComision.Id, reqGenerarConsolidadoDto.DataTable);
            this.logger.LogInformation("ConsolidadosReportsModule/StoreConsolidado SUCCESS => {insertHistorial} columnas afectadas", insertHistorial);
            return "Consolidado Guardado correctamente";
        }
    }
}