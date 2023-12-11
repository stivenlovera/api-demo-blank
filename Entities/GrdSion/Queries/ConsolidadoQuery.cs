using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.GrdSion.Queries
{
    public class ConsolidadoQuery
    {
        public int LcontactoId { get; set; }
        public int Scodigo { get; set; }
        public string Scedulaidentidad { get; set; }
        public string Snombrecompleto { get; set; }
        public int LempresaId { get; set; }
        public string Empresa { get; set; }
        public decimal TotalComisionVtaGrupoResidual { get; set; }
        public decimal TotalComisionVtaPersonal { get; set; }
        public int LcicloId { get; set; }
        public decimal TotalComisionVtaGrupoResiduaBs { get; set; }
        public decimal TotalComisionVtaPersonalBs { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public string NombreCiclo { get; set; }
        public DateTime FechaInicioCiclo { get; set; }
        public DateTime FechaFinCiclo { get; set; }
        public decimal Retencion { get; set; }
        public decimal TotalComision { get; set; }
        public decimal TotalComisionServicio { get; set; }
        public decimal TotalComisionRetencion { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Valor13 { get; set; }
        public decimal Valor87 { get; set; }

    }
}