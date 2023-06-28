using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Models.Utils;

namespace api_guardian.Utils
{
    public class GetTemplate
    {
        private readonly ILogger<GetTemplate> _logger;
        private readonly IConfiguration _configuration;
        private readonly RazorRendererHelper _razorRendererHelper;

        public GetTemplate(
            ILogger<GetTemplate> logger,
            IConfiguration configuration,
            RazorRendererHelper razorRendererHelper
        )
        {
            this._logger = logger;
            this._configuration = configuration;
            this._razorRendererHelper = razorRendererHelper;
        }
        public string GetTemplateReporteConsolidado<TModel>(TModel dataTemplate)
        {

            var templates = this._configuration.GetSection("Templates").Get<TemplateDocuments>();
            this._logger.LogWarning($"GetTemplate/ObtenerTemplateConsolidados Template a usar {templates.Reportes.Consolidados} Iniciando...");
            var plantilla = _razorRendererHelper.RenderPartialToString(templates.Reportes.Consolidados, dataTemplate);
            return plantilla;
        }
    }
}