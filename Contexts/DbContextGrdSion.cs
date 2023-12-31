using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Entities.GrdSion.Views;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Contexts
{
    public class DbContextGrdSion : DbContext
    {
        public DbContextGrdSion(DbContextOptions<DbContextGrdSion> options) : base(options)
        {

        }
        public virtual DbSet<Administracionempresa> Administracionempresas { get; set; }
        public virtual DbSet<ObtenerConsolidadoView> ObtenerConsolidadoViews { get; set; }
    }
}