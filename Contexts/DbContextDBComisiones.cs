using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.DBComisiones.Queries;
using api_guardian.Entities.DBComisiones.Views;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Contexts
{
    public class DbContextDBComisiones : DbContext
    {
        public DbContextDBComisiones(DbContextOptions<DbContextDBComisiones> options) : base(options)
        {

        }
        public virtual DbSet<VwListaVentaGral> VWLISTAVENTAS_GRAL { get; set; }
        /*QUERY*/
        public virtual DbSet<QueryVwListaVentaGralGroup> QueryVwListaVentaGralGroup { get; set; }

    }
}