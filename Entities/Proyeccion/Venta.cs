using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Proyeccion
{
    public class Venta
    {
        public int VentaId { get; set; }
        public int LcontratoId { get; set; }
        public int VentaSionId { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal CuotaInicial { get; set; }
        public string NroVenta { get; set; }
        public int ComplejoId { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int EstadoId { get; set; }
        public int TipoVentaId { get; set; }
        public DateTime FechaVenta { get; set; }
        public int CicloId { get; set; }
    }
}