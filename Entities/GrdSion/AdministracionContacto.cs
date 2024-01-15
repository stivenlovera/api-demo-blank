using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Entities.GrdSion
{
    public class AdministracionContacto
    {
        public string Susuarioadd { get; set; }
        public DateTime Dtfechaadd { get; set; }
        public string Susuariomod { get; set; }
        public DateTime Dtfechamod { get; set; }
        public long LcontactoId { get; set; }
        public string Scodigo { get; set; }
        public string Stelefonofijo { get; set; }
        public string Stelefonomovil { get; set; }
        public string Scorreoelectronico { get; set; }
        public string Cestado { get; set; }
        public DateTime Dtfechanacimiento { get; set; }
        public string Sdireccion { get; set; }
        public long LpaisId { get; set; }
        public string Sciudad { get; set; }
        public string Scedulaidentidad { get; set; }
        public long LpatrocinanteId { get; set; }
        public long LnivelId { get; set; }
        public DateTime Dtfecharegistro { get; set; }
        public string Snombrecompleto { get; set; }
        public DateTime Dtfechacalculo { get; set; }
        public decimal Dlotes { get; set; }
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
        public long LtipocontactoId { get; set; }
        public string Cpresentafactura { get; set; }
        public decimal Ddescuentolote { get; set; }
        public string Snotadescuentolote { get; set; }
        public string Stelefonooficina { get; set; }
        public string Scontrasena { get; set; }
        public long LpatrotempId { get; set; }
        public long Lnit { get; set; }
        public string Lcuentabanco { get; set; }
        public long Lcodigobanco { get; set; }
        public string Ctienecuenta { get; set; }
        public string Cbaja { get; set; }
        public DateTime Dtfechabaja { get; set; }
        public long Ltipobaja { get; set; }
        public string Smotivobaja { get; set; }
        public long Pmax { get; set; }
        public long Pvitalicio { get; set; }
    }
    public class ObtenerUnoAdministracionContactoQuery : AdministracionContacto
    {
        public int Id { get; set; }
        public string ImgPerfil { get; set; }
        public string Ssigla { get; set; }
        public string Snombre { get; set; }
        public int LPaisId { get; set; }
        public string SNombre { get; set; }
    }

}