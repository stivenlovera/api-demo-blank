using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.GrdSion
{
    public class ContactoPerfilSql
    {
        public static string ObtenerTodo()
        {
            return @" SELECT * FROM contacto_perfil";
        }
        public static string ObtenerUno()
        {
            return @"
                SELECT * FROM contacto_perfil where id=@Id;
            ";
        }

        public static string Insertar()
        {
            return @"
                insert into 
                contacto_perfil (
                    lcontacto_id, 
                    img_perfil
                )
                values
                ( 
                    @LcontactoId, 
                    @ImgPerfil
                );
            ";
        }
        public static string Update()
        {
            return @"
                update 
                    contacto_perfil 
                set 
                    lcontacto_id = @LcontactoId,
                    img_perfil = @ImgPerfil
                where 
                    id = @Id;
            ";
        }
        public static string Delete()
        {
            return @"
                delete from 
                    contacto_perfil 
                where 
                    id = @Id;
            ";
        }
    }
}