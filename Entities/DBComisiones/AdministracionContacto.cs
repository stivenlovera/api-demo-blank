using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.DBComisiones
{
     public class AdministracionContacto
    {
        [Key]
        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public long Lcontacto_id { get; set; }
        public string Scodigo { get; set; }
        public string Stelefonofijo { get; set; }
        public string Stelefonomovil { get; set; }
        public string Scorreoelectronico { get; set; }
        public string Cestado { get; set; }
        public byte[] Bifoto { get; set; }
        public DateTime Dtfechanacimiento { get; set; }
        public string Sdireccion { get; set; }
        public long Lpais_id { get; set; }
        public string Sciudad { get; set; }
        public string Scedulaidentidad { get; set; }
        public long Lpatrocinante_id { get; set; }
        public long Lnivel_id { get; set; }
        public DateTime Dtfecharegistro { get; set; }
        public string Snombrecompleto { get; set; }
        public DateTime Dtfechacalculo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Dlotes { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Dproduccion { get; set; }
        public string Cvolante { get; set; }
        public string Cpresentacion { get; set; }
        public string Ccena { get; set; }
        public string Ctv { get; set; }
        public string Cperiodico { get; set; }
        public string Cradio { get; set; }
        public string Cweb { get; set; }
        public string Sotro { get; set; }
        public string Ccorreo { get; set; }
        public long Ltipocontacto_id { get; set; }
        public string Cpresentafactura { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Ddescuentolote { get; set; }
        public string Snotadescuentolote { get; set; }
        public string Stelefonooficina { get; set; }
        public string Scontrasena { get; set; }
        public long Lpatrotemp_id { get; set; }
        public long Lnit { get; set; }
        public string Lcuentabanco { get; set; }
        public long Lcodigobanco { get; set; }
        public string Ctienecuenta { get; set; }
        public string Cbaja { get; set; }
        public DateTime Dtfechabaja { get; set; }
        public int Ltipobaja { get; set; }
        public string Smotivobaja { get; set; }
        public long Pmax { get; set; }
        public long Pvitalicio { get; set; }
    }
}