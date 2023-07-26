using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.GrdSion.Views
{
    [Keyless]
    public class ObtenerConsolidadoView
    {
        public long lcontacto_id { get; set; }
        public string scodigo { get; set; }
        public string scedulaidentidad { get; set; }
        public string snombrecompleto { get; set; }
        public long lempresa_id { get; set; }
        public string empresa { get; set; }
        public decimal total_comision_vta_grupo_residual { get; set; }
        public decimal total_comision_vta_personal { get; set; }
        public long lciclo_id { get; set; }
        public decimal total_comision_vta_grupo_residual_bs { get; set; }
        public decimal total_comision_vta_personal_bs { get; set; }
        public string razon_social { get; set; }
        public string nit { get; set; }
        public string nombre_ciclo { get; set; }
        public DateTime fecha_inicio_ciclo { get; set; }
        public DateTime fecha_fin_ciclo { get; set; }
        public decimal retencion { get; set; }
        public decimal total_comision { get; set; }
        public string total_comision_retencion { get; set; }
        public double total_pagar { get; set; }
        public string valor_13 { get; set; }
        public string valor_87 { get; set; }
    }
}