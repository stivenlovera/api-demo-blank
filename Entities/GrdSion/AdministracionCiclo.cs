using System.ComponentModel.DataAnnotations;
namespace api_guardian.Entities
{
    public class AdministracionCiclo
    {
        public string susuarioadd { get; set; }
        public DateTime dtfechaadd { get; set; }
        public string susuariomod { get; set; }
        public DateTime dtfechamod { get; set; }
        [Key]
        public long lcicloId { get; set; }
        public string snombre { get; set; }
        public DateTime dtfechainicio { get; set; }
        public DateTime dtfechafin { get; set; }
        public int lestado { get; set; }
        public DateTime dtfechacierre { get; set; }
        public DateTime dtfechaprecierre1 { get; set; }
        public DateTime dtfechaprecierre2 { get; set; }
        public string cverenweb { get; set; }
        public string estadogestor { get; set; }
    }
}