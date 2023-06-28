using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Helpers
{
    public class Converters
    {
        private readonly ILogger<Converters> _logger;

        public Converters(
            ILogger<Converters> logger
        )
        {
            this._logger = logger;
        }
        public FileStreamResult ConverterToPdf(byte[] pdf, string nombreDocumento)
        {
            var stream = new MemoryStream(pdf);
            var contentType = @"application/pdf";
            var fileName = $"{nombreDocumento}.pdf";
            this._logger.LogWarning($"Converters/ConverterToPdf({nombreDocumento}) convertido");
            return new FileStreamResult(stream, contentType);
        }
    }
}