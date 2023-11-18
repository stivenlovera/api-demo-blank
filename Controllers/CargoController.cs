using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Dtos.Response;
using api_guardian.Utils;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/cargo")]
    public class CargoController : ControllerBase
    {
        private readonly ILogger<CargoController> logger;


        public CargoController(
            ILogger<CargoController> logger
        )
        {
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ResponseDto<List<object>>> GetAll()
        {
            this.logger.LogInformation("{methodo}{path} GetAll() Inizialize ...", Request.Method, Request.Path);
            try
            {
                //var editar = await this.autorizadoModule.GetOne(id);
                var response = new ResponseDto<List<object>>
                {
                    Status = 1,
                    Data = new List<object>(){
                        new {
                            cargoId= 1,
                            nombreCargo = "analista sistema",
                            descripcion =""
                        },
                        new
                        {
                            cargoId= 2,
                            nombreCargo = "atc",
                            descripcion =""
                        },
                        new
                        {
                            cargoId= 2,
                            nombreCargo = "Contabilidad",
                            descripcion =""
                        }
                    },
                    Message = "lista de cargos"
                };
                this.logger.LogWarning("GetAll() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<List<object>>
                {
                    Status = 0,
                    Data = null,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("GetAll() SUCCESS => {e}", e.Message);
                return response;

            }
        }
    }
}