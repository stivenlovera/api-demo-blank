using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Guardian
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int CargoId { get; set; }
        public string NroDocumento { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string Observaciones { get; set; }
        public int Estado { get; set; }
    }
    public class FuncionarioGetAll : Cargo
    {
        public int FuncionarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string NroDocumento { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
    }
    public class FuncionarioGetOne : FuncionarioGetAll
    {
        public int FuncionarioImagenId { get; set; }
        public string File { get; set; }
    }
}