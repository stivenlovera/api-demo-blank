using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace DocumentoVentaSion.Utils
{
    public class GeneratePdf
    {
        private readonly IConverter _converter;

        /*PROPIEDADES*/
        private string HeaderLogo = @".\wwwroot\assets\html\headerLogo.html";
        private string HeaderImpresionLogo = @".\wwwroot\assets\html\preImpresion.html";
        public GeneratePdf(IConverter converter)
        {
            this._converter = converter;
        }
        public byte[] Generate(string htmlContent, string codigoFooter, bool preImpresion = false)
        {
            string header = "";
            /*preimpresion*/
            if (preImpresion)
            {
                header = this.HeaderImpresionLogo;
            }
            else
            {
                header = this.HeaderLogo;
            }

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 17, Bottom = 20, Left = 10, Right = 10 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Sans-Serif", FontSize = 9, Line = false, HtmUrl = header, Spacing = 17 },
                FooterSettings = { FontSize = 8, Center = "Página [page] de [toPage]", Line = false, Spacing = 5, Right = codigoFooter },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return _converter.Convert(htmlToPdfDocument);
        }
    }
}