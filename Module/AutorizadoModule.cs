using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Entities.Guardian;
using api_guardian.Repository;
using api_guardian.Utils;

namespace api_guardian.Module
{
    public class AutorizadoModule
    {
        private readonly ILogger<AutorizadoModule> logger;
        private readonly AutorizadoRepository autorizadoRespository;
        private readonly RolFuncionarioRepository rolFuncionarioRepository;

        public AutorizadoModule(
            ILogger<AutorizadoModule> logger,
            AutorizadoRepository autorizadoRespository,
            RolFuncionarioRepository rolFuncionarioRepository
        )
        {
            this.logger = logger;
            this.autorizadoRespository = autorizadoRespository;
            this.rolFuncionarioRepository = rolFuncionarioRepository;
        }
        public async Task<AuthorizadoDto> GetOne(int funcionarioId)
        {
            logger.LogInformation("AutorizadoModule/GetOne({funcionarioId})", funcionarioId);
            var resultado = new AuthorizadoDto();
            var getOne = await this.autorizadoRespository.EditOne(funcionarioId);
            var roles = await this.rolFuncionarioRepository.GetAllRolByFuncionarioId(funcionarioId);
            resultado.Activo = getOne.Activo;
            resultado.AutorizacionId = getOne.AutorizacionId;
            resultado.FuncionarioId = getOne.FuncionarioId;
            resultado.Usuario = getOne.Usuario;
            resultado.Password = getOne.Password;
            resultado.Roles = roles;
            return resultado;
        }
        public async Task<AuthorizadoDto> EditOne(int funcionarioId)
        {
            logger.LogInformation("AutorizadoModule/EditOne({funcionarioId})", funcionarioId);
            var resultado = new AuthorizadoDto();
            var getOne = await this.autorizadoRespository.EditOne(funcionarioId);
            var roles = await this.rolFuncionarioRepository.GetAllRolByFuncionarioId(funcionarioId);
            resultado.Activo = getOne.Activo;
            resultado.AutorizacionId = getOne.AutorizacionId;
            resultado.FuncionarioId = getOne.FuncionarioId;
            resultado.Usuario = getOne.Usuario;
            resultado.Password = getOne.Password;
            resultado.Roles = roles;
            return resultado;
        }
        public async Task<string> Update(AuthorizadoDto autorizacion, int funcionarioId)
        {
            logger.LogInformation("AutorizadoModule/Update({autorizacion},{funcionarioId})", autorizacion, funcionarioId);
            var update = await autorizadoRespository.Update(autorizacion);
            var listRolFuncionarios = autorizacion.Roles.Select(x => new RolFuncionario { FuncionarioId = autorizacion.FuncionarioId, RolId = x.RolId }).ToList();
            await this.rolFuncionarioRepository.DeleteRolByFuncionarioId(funcionarioId);
            var insertRoles = await rolFuncionarioRepository.StoreRolByFuncionarioId(listRolFuncionarios);

            return "Registrado correctamente";
        }
    }
}