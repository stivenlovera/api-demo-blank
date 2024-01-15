using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion;
using api_guardian.Entities.GrdSion.Queries;

namespace api_guardian.Dtos.Controllers
{
    public class ConsolidadoDto
    {

    }
    public class Filtro
    {
        public AdministracionCiclo Ciclo { get; set; }
        public List<AdministracionEmpresa> Empresas { get; set; }
    }
    public class ReqConsolidadoDto
    {
        public Filtro Filtro { get; set; }
        public List<ConsolidadoQuery> DataTable { get; set; }
    }
    public class ReqGenerarConsolidadoDto
    {
        public int CicloId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public EstadoReporte EstadoReporte { get; set; }
        public List<ConsolidadoQuery> DataTable { get; set; }
    }
}