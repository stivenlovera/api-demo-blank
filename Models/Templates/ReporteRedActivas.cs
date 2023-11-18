using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Entities.GrdSion.Queries;

namespace api_guardian.Models.Templates
{
    public class ReporteRedActivas : ReportCommon
    {
        public ListaPersonas DataGrid { get; set; }
    }
    public class ListaPersonas
    {
        public string Titulo { get; set; }
        public List<RespDataTableReporteRedDto> ListPersonas { get; set; }
    }
}