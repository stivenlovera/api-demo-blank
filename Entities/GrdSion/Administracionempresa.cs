using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_guardian.Entities;

public partial class Administracionempresa
{
    public string Susuarioadd { get; set; }

    public DateTime Dtfechaadd { get; set; }

    public string Susuariomod { get; set; }

    public DateTime Dtfechamod { get; set; }
    [Key]
    public int LempresaId { get; set; }

    public string Snombre { get; set; }

    public string Snit { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Empresa { get; set; }
}
