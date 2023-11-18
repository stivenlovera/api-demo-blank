using api_guardian.Entities.Guardian;
using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Contexts
{
    public class DbContextGuardian : DbContext
    {
        public DbContextGuardian(DbContextOptions<DbContextGuardian> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This is the only line you need to add in your context
            modelBuilder.ToSnakeCase();
        }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<AutorizadoData> AutorizadoData { get; set; }
        public virtual DbSet<Autorizacion> Autorizacion { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolFuncionario> RolFuncionario { get; set; }

    }
}