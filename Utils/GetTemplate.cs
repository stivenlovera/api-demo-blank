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
        public string SearchTemplate<TModel>(string template, TModel dataTemplate)
        {
            var listaTemplate = this._configuration.GetSection("Templates").Get<TemplateDocuments>();
            var plantilla = listaTemplate.Reportes.Where(x => x.Nombre == template).FirstOrDefault();
            this._logger.LogWarning("GetTemplate/SearchTemplate({template}) Template a usar {plantilla} Iniciando...", template, Helper.Log(plantilla));
            var data = _razorRendererHelper.RenderPartialToString(plantilla.Path, dataTemplate);
            return data;
        }
    }
}