using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion;
using api_guardian.Entities.GrdSion.Views;

namespace api_guardian.Dtos.Controllers
{
    public class ReqHistorialComisionesCambioEmpresaDto
    {
        public int ComisionConsolidadoId { get; set; }
    }
    public class ResHistorialComisioneCambioEmpresaDto
    {
        public PagoConsolidado PagoConsolidado { get; set; }
        public List<ConfigCambioEmpresaDto> ConfigCambioEmpresa { get; set; }
        public List<HistorialPagoComisionConsolidado> DataTable { get; set; }

    }
    public class ReqHistorialComisionesRegistrarCambioEmpresaDto : ResHistorialComisioneCambioEmpresaDto
    {

    }
    public class ConfigCambioEmpresaDto
    {
        public int CambioEmpresaId { get; set; }
        public AdministracionEmpresa Empresa { get; set; }
        public AdministracionEmpresa PagoEmpresa { get; set; }
        public string Nota { get; set; }
        public int PagoConsolidadoId { get; set; }
    }
    public class ReqExportHistorialComisionesDto
    {
        public ComisionConsolidado Consolidado { get; set; }
        public List<HistorialComisionConsolidado> DataTable { get; set; }
    }
}