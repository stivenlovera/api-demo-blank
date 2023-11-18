using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.Guardian;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_guardian.Repository
{
    public class RolRepository
    {
        private readonly ILogger<RolRepository> logger;
        private readonly DbContextGuardian dbContextGuardian;

        public RolRepository(
            ILogger<RolRepository> logger,
            DbContextGuardian dbContextGuardian
        )
        {
            this.logger = logger;
            this.dbContextGuardian = dbContextGuardian;
        }

        //cambiar consultas a dapper
        public async Task<List<Rol>> GetAll()
        {
            this.logger.LogInformation("RolRespository/GetAll()");
            var funcionario = await this.dbContextGuardian.Rol.ToListAsync();
            return funcionario;
        }
        public async Task<Rol> GetOne(int funcionarioId)
        {
            this.logger.LogInformation("RolRespository/GetOne({funcionario})", funcionarioId);
            var roles = await this.dbContextGuardian.Rol
            .Where(x => x.RolId == funcionarioId)
            .FirstOrDefaultAsync();
            return roles;
        }
        public async Task<Rol> Store(Rol rol)
        {
            this.logger.LogInformation("RolRespository/Store({rol})", JsonConvert.SerializeObject(rol, Formatting.Indented));
            await this.dbContextGuardian.Rol.AddAsync(rol);
            var verificar = await this.dbContextGuardian.SaveChangesAsync();
            if (verificar == 0)
            {
                this.logger.LogError("No se pudo insertar rol");
                throw new Exception("No se pudo insertar rol");
            }
            else
            {
                this.logger.LogInformation("Registrado correctamente");
                return rol;
            }
        }
        public async Task<Rol> Update(Rol rol)
        {
            this.logger.LogInformation("RolRespository/Update({funcionario})", JsonConvert.SerializeObject(rol, Formatting.Indented));
            this.dbContextGuardian.Rol.Update(rol);
            var verificar = await this.dbContextGuardian.SaveChangesAsync();
            if (verificar != 1)
            {
                this.logger.LogError("No se pudo modificar rol");
                throw new Exception("No se pudo modificar rol");
            }
            else
            {
                this.logger.LogInformation("Modificado correctamente");
                return rol;
            }
        }
        public async Task<int> Delete(int RolId)
        {
            this.logger.LogInformation("RolRespository/Delete({RolId})", RolId);
            this.dbContextGuardian.Rol.Remove(new Rol { RolId = RolId });
            var verificar = await this.dbContextGuardian.SaveChangesAsync();
            if (verificar != 1)
            {
                this.logger.LogError("No se pudo eliminar rol");
                throw new Exception("No se pudo eliminar rol");
            }
            else
            {
                this.logger.LogInformation("Eliminado correctamente");
                return RolId;
            }
        }
    }
}