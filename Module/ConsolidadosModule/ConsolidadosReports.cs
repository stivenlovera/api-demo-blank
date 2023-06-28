using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Helpers;
using api_guardian.Models.Templates;
using api_guardian.Utils;
using DocumentoVentaSion.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Module.ConsolidadosModule
{
    public class ConsolidadosReports
    {
        private readonly ILogger<ConsolidadosReports> _logger;
        private readonly GetTemplate _getTemplate;
        private readonly GeneratePdf _generatePdf;
        private readonly Converters _converters;

        public ConsolidadosReports(
            ILogger<ConsolidadosReports> logger,
            GetTemplate getTemplate,
            GeneratePdf generatePdf,
            Converters converters
        )
        {
            this._logger = logger;
            this._getTemplate = getTemplate;
            this._generatePdf = generatePdf;
            this._converters = converters;
        }
        public FileStreamResult ReportePorEmpresa()
        {
            var dataAsesores=new List<Asesor>();
            dataAsesores.Add(new Asesor{
                asesor="stiven lovera",
                codigo=25566,
                comision=522.66m,
                impuesto=52.66m,
                sinImpuesto=96.52m,
                retencion=70.66m,
                servicio=87.66m,
                total=100.00m,
                totalComision=568.85m,
                totalPagar=568.85m,
                totalRetencion=568.85m,
            });
            dataAsesores.Add(new Asesor{
                asesor="Juan Jose",
                codigo=88999,
                comision=522.66m,
                impuesto=52.66m,
                sinImpuesto=96.52m,
                retencion=70.66m,
                servicio=87.66m,
                total=100.00m,
                totalComision=568.85m,
                totalPagar=568.85m,
                totalRetencion=568.85m,
            });
            var data = new ReporteConsolidados
            {
                nombreReporte = "REPORTE CONSOLIDADO COMISION - SERVICIO",
                ciclo = "junio",
                asesores = dataAsesores,
                empresa = "Zuriel",
                fechaFin = DateTime.Now,
                fechaInicio = DateTime.Now,
                nit = 250000089
            };
            var htmlContent = this._getTemplate.GetTemplateReporteConsolidado(data);
            
            var pdf = this._generatePdf.Generate(htmlContent, "xxxx-xxxx-xxxx", true);
            return _converters.ConverterToPdf(pdf, "demo");
        }
    }
}