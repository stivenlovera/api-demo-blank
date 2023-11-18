
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.DBComisiones.Views
{
    [Keyless]
    public class VwListaVentaGral
    {
        public string EMPRESA { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public int IDALMACEN { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public int IDVENTA { get; set; }
        public DateTime FECHAVENTA { get; set; }
        public string IDPRODUCTO { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public int IDCLIENTE { get; set; }
        public string CI_CLIENTE { get; set; } = null;
        public string NOMBRE_CLIENTE { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public int IDVENDEDOR { get; set; }
        public string CI_VENDEDOR { get; set; } = null;
        public string NOMBRE_VENDEDOR { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal IDTIPOVENTA { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal PRECIOVENTA { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal CUOTAINICIAL { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal? VALOR_CI { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal? MONTOABONADO { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal TOTALDEUDA { get; set; }
        public string TIPOVENTA { get; set; }
        public int IDESTADO_VENTA { get; set; }
        [Column(TypeName = "numeric(12,2)")]
        public decimal IDESTADO { get; set; }
        public string GLOSA { get; set; }
        public string NRODOC { get; set; }
        public int? Kit { get; set; }
        public DateTime? FECHA_RESERVA { get; set; }
    }
}