
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.GrdSion.Views
{
    public class VwDetalleRed
    {
        public string cbaja_comisiona { get; set; }
        public decimal dprecio { get; set; }
        public DateTime dtfecha { get; set; }
        public long lasesor_id { get; set; }
        public long lciclo_id { get; set; }
        public long lcontacto_id { get; set; }
        public long lcontacto_id_asesor { get; set; }
        public long lcontacto_id_cliente { get; set; }
        public long lcontacto_id_comisiona { get; set; }
        public long lcontrato_id { get; set; }
        public int lgeneracion { get; set; }
        public string scedula_identidad_comisiona { get; set; }
        public string snombre_completo_asesor { get; set; }
        public string snombre_completo_cliente { get; set; }
        public string snombre_completo_comisiona { get; set; }
        public string stelefono_movil_asesor { get; set; }
        public string stelefono_movil_cliente { get; set; }
        public string stelefono_movil_comisiona { get; set; }
    }
}