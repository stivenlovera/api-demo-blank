using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities;
using api_guardian.Entities.DBComisiones;
using api_guardian.Entities.DBComisiones.Queries;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Entities.GrdSion.Views;
using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_guardian.Contexts
{
    public class DbContextGrdSion : DbContext
    {
        public DbContextGrdSion(DbContextOptions<DbContextGrdSion> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public virtual DbSet<AdministracionEmpresa> Administracionempresas { get; set; }
        public virtual DbSet<AdministracionContacto> AdministracionContacto { get; set; }
        public virtual DbSet<ObtenerConsolidadoView> ObtenerConsolidadoViews { get; set; }
        public virtual DbSet<ObtenerResultadorRedQuery> ObtenerResultadorRedQuery { get; set; }
        public virtual DbSet<AdministracionCiclo> AdministracionCiclo { get; set; }
        public virtual DbSet<QueryDetalleRed> QueryDetalleRed { get; set; }
        public virtual DbSet<VwDetalleRed> VwDetalleRed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                    .Entity<VwDetalleRed>(
                        eb =>
                        {
                            eb.HasNoKey();
                            eb.ToView("v_detalle_red");
                        });
        }
    }
}