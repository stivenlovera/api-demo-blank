using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class AdministracionContactoSql
    {
        public static string ObtenerTodo()
        {
            return @"
            SELECT 
                * 
            FROM 
                administracioncontacto
            WHERE 
                cbaja=0
            ORDER BY 
                dtfechaadd DESC
            ";
        }
        public static string ObtenerTodoSelect()
        {
            return @"
            SELECT 
                * 
            FROM 
                administracioncontacto
            WHERE 
                cbaja=0
            ORDER BY 
                dtfechaadd DESC
            ";
        }
        public static string ObtenerUno()
        {
            return @"
            SELECT
                av.ssigla,
                av.snombre as nivel,
                bp.sNombre as pais,
                a.*
            FROM
                administracioncontacto as a
                INNER JOIN administracionnivel as av on a.lnivel_id = av.lnivel_id
                INNER JOIN basepais as bp on bp.lPais_id = a.lpais_id
            WHERE
                a.snombrecompleto = 'CHAMBI VEIZAGA MAGALY VIRGINIA'
            ORDER BY a.dtfechaadd DESC;
            ";
        }
        public static string Insert()
        {
            return @"
                insert into 
                administracioncontacto (
                    susuarioadd, 
                    dtfechaadd, 
                    susuariomod, 
                    dtfechamod, 
                    lcontacto_id, 
                    scodigo, 
                    stelefonofijo, 
                    stelefonomovil, 
                    scorreoelectronico, 
                    cestado, 
                    bifoto, 
                    dtfechanacimiento, 
                    sdireccion, 
                    lpais_id, 
                    sciudad, 
                    scedulaidentidad, 
                    lpatrocinante_id, 
                    lnivel_id, 
                    dtfecharegistro, 
                    snombrecompleto, 
                    dtfechacalculo, 
                    dlotes, 
                    dproduccion, 
                    cvolante, 
                    cpresentacion, 
                    ccena, 
                    ctv, 
                    cperiodico, 
                    cradio, 
                    cweb, 
                    sotro, 
                    ccorreo, 
                    ltipocontacto_id, 
                    cpresentafactura, 
                    ddescuentolote, 
                    snotadescuentolote, 
                    stelefonooficina, 
                    scontrasena, 
                    lpatrotemp_id, 
                    lnit, 
                    lcuentabanco, 
                    lcodigobanco, 
                    ctienecuenta, 
                    cbaja, 
                    dtfechabaja, 
                    ltipobaja, 
                    smotivobaja, 
                    pmax, 
                    pvitalicio
                )
                values
                (
                    @Susuarioadd, 
                    @Dtfechaadd, 
                    @Susuariomod, 
                    @Dtfechamod, 
                    @LcontactoId, 
                    @Scodigo, 
                    @Stelefonofijo, 
                    @Stelefonomovil, 
                    @Scorreoelectronico, 
                    @Cestado, 
                    @Bifoto, 
                    @Dtfechanacimiento, 
                    @Sdireccion, 
                    @LpaisId, 
                    @Sciudad, 
                    @Scedulaidentidad, 
                    @LpatrocinanteId, 
                    @LnivelId, 
                    @Dtfecharegistro, 
                    @Snombrecompleto, 
                    @Dtfechacalculo, 
                    @Dlotes, 
                    @Dproduccion, 
                    @Cvolante, 
                    @Cpresentacion, 
                    @Ccena, 
                    @Ctv, 
                    @Cperiodico, 
                    @Cradio, 
                    @Cweb, 
                    @Sotro, 
                    @Ccorreo, 
                    @LtipocontactoId, 
                    @Cpresentafactura, 
                    @Ddescuentolote, 
                    @Snotadescuentolote, 
                    @Stelefonooficina, 
                    @Scontrasena, 
                    @LpatrotempId, 
                    @Lnit, 
                    @Lcuentabanco, 
                    @Lcodigobanco, 
                    @Ctienecuenta, 
                    @Cbaja, 
                    @Dtfechabaja, 
                    @Ltipobaja, 
                    @Smotivobaja, 
                    @Pmax, 
                    @Pvitalicio
                )
            ";
        }
        public static string UpdateAdicionales()
        {
            return @"
                    update 
                        administracioncontacto 
                    set 
                        susuariomod = ModificadoPorSistema,
                        dtfechamod = NOW(),
                        sotro = Sotro,
                        lcuentabanco = Lcuentabanco,
                        lcodigobanco = Lcodigobanco,
                        ctienecuenta = Ctienecuenta,
                        cbaja = Cbaja
                    where 
                        lcontacto_id = @LcontactoId;
            ";
        }
        public static string Update()
        {
            return @"
                    update 
                        administracioncontacto 
                    set 
                        susuariomod = ModificadoPorSistema,
                        dtfechamod = NOW(),
                        sotro = Sotro,
                        lcuentabanco = Lcuentabanco,
                        lcodigobanco = Lcodigobanco,
                        ctienecuenta = Ctienecuenta,
                        cbaja = Cbaja
                    where 
                        lcontacto_id = @LcontactoId;
            ";
        }
    }
}