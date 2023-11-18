using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.Guardian
{
    //Model
    public class RolFuncionario
    {
        [Key]
        public int RolFuncionarioId { get; set; }
        public int RolId { get; set; }
        public int FuncionarioId { get; set; }
    }
    public class RolFuncionarioOne : Rol
    {
        [Key]
        public int RolFuncionarioId { get; set; }
        public int FuncionarioId { get; set; }
    }
}