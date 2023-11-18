using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api_guardian.Dtos.Controllers;
using api_guardian.Dtos.Response;
using api_guardian.Module;
using api_guardian.Utils;
using ClosedXML.Excel;
using GrapeCity.Documents.Excel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_guardian.Controllers
{
    [ApiController]
    [Route("api/proyeccion")]
    /* [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] */
    public class ProyeccionController : ControllerBase
    {
        private readonly ILogger<ProyeccionController> logger;
        private readonly ProyeccionModule proyeccionModule;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProyeccionController(
            ILogger<ProyeccionController> logger,
            ProyeccionModule proyeccionModule,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this.logger = logger;
            this.proyeccionModule = proyeccionModule;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("import-consolidado/{id}")]
        public async Task<ResponseDto<bool>> DesplegarServicio(int id)
        {
            this.logger.LogInformation("{methodo}{path} DesplegarServicio({id}) Inizialize ...", Request.Method, Request.Path, id);
            try
            {
                var store = await this.proyeccionModule.CopiarCosolidadoByCiclo(id);
                var response = new ResponseDto<bool>
                {
                    Status = 1,
                    Data = store,
                    Message = ""
                };
                this.logger.LogWarning("DesplegarServicio() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<bool>
                {
                    Status = 1,
                    Data = false,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DesplegarServicio() SUCCESS => {e}", e.Message);
                return response;
            }
        }
        [HttpGet("data-table")]
        public async Task<ResponseDto<bool>> DataTableProyeccion()
        {
            this.logger.LogInformation("{methodo}{path} DataTableProyeccion() Inizialize ...", Request.Method, Request.Path);
            try
            {
                var lista = await this.proyeccionModule.CopiarCosolidadoByCiclo(110);
                var response = new ResponseDto<bool>
                {
                    Status = 1,
                    Data = lista,
                    Message = ""
                };
                this.logger.LogWarning("DataTableProyeccion() SUCCESS => {response}", Helper.Log(response));
                return response;
            }
            catch (System.Exception e)
            {
                var response = new ResponseDto<bool>
                {
                    Status = 1,
                    Data = false,
                    Message = "Ocurrio un error inesperado"
                };
                this.logger.LogCritical("DataTableProyeccion() SUCCESS => {e}", e.Message);
                return response;
            }
        }

        [HttpPost("export-excel")]
        public FileResult ExportExcel()
        {

            List<User> users = new()
            {
                new User
                {
                    Id = 1,
                    Username = "ArminZia",
                    Email = "armin.zia@gmail.com",
                    SerialNumber = "NX33-AZ47",
                    JoinedOn = new DateTime(1988, 04, 20)
                },
                new User
                {
                    Id = 2,
                    Username = "DoloresAbernathy",
                    Email = "dolores.abernathy@gmail.com",
                    SerialNumber = "CH1D-4AK7",
                    JoinedOn = new DateTime(2021, 03, 24)
                },
                new User
                {
                    Id = 3,
                    Username = "MaeveMillay",
                    Email = "maeve.millay@live.com",
                    SerialNumber = "A33B-0JM2",
                    JoinedOn = new DateTime(2021, 03, 23)
                },
                new User
                {
                    Id = 4,
                    Username = "BernardLowe",
                    Email = "bernard.lowe@hotmail.com",
                    SerialNumber = "H98M-LIP5",
                    JoinedOn = new DateTime(2021, 03, 10)
                },
                new User
                {
                    Id = 5,
                    Username = "ManInBlack",
                    Email = "maininblack@gmail.com",
                    SerialNumber = "XN01-UT6C",
                    JoinedOn = new DateTime(2021, 03, 9)
                }
            };

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;

            //header
            //worksheet.Row(currentRow).Height = 25.0;
            worksheet.Row(currentRow).Style.Font.Bold = true;
            worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell(currentRow, 1).Value = "Nro";
            worksheet.Cell(currentRow, 2).Value = "Username";
            worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Columns(1, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Columns(2, 5).Width = 20;
            worksheet.Columns(3, 3).Width = 30;
            worksheet.Cell(currentRow, 3).Value = "Email";
            worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 4).Value = "Serial Number";
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 5).Value = "Joined On";
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Columns().AdjustToContents();
            foreach (var user in users)
            {
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = user.Id;
                worksheet.Cell(currentRow, 2).Value = user.Username;
                worksheet.Cell(currentRow, 3).Value = user.Email;
                worksheet.Cell(currentRow, 4).Value = user.SerialNumber;
                worksheet.Cell(currentRow, 5).Value = user.JoinedOn.ToShortDateString();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }
       /*  public FileResult EditExcel([FromBody] ReqConsolidadoDto reqConsolidadoDto)
        {
            //template
            var path = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "template", "consolidado", "template_consolidado.xlsx");
            this.logger.LogWarning("Path raiz {path}", path);
            XLWorkbook workbook = new XLWorkbook(path);

            var hoja = workbook.Worksheets.Worksheet(1);
            var row = 5;
            foreach (var data in reqConsolidadoDto.DataTable)
            {
                hoja.Cell(row, 1).SetValue(data.Scodigo);
                hoja.Cell(row, 2).SetValue(data.Empresa);
                row++;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        } */
    }
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string SerialNumber { get; set; }

        public DateTime JoinedOn { get; set; }
    }
}

