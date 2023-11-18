
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Entities.DBComisiones.Queries
{
    [Keyless]
    public class QueryVwListaVentaGralGroup
    {
        public int Cantidad { get; set; }
        public string CI_CLIENTE { get; set; }
    }
}