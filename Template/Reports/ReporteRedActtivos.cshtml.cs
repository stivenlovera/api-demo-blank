using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace api_guardian.Template.Reports
{
    public class ReporteRedActtivos : PageModel
    {
        private readonly ILogger<ReporteRedActtivos> _logger;

        public ReporteRedActtivos(ILogger<ReporteRedActtivos> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}