using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Models.Utils
{
    public class TemplateDocuments
    {
        public List<Reportes> Reportes { get; set; }
    }
    public class Reportes
    {
        public string Nombre { get; set; }
        public string Path { get; set; }
    }
}