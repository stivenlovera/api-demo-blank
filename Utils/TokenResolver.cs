using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Utils
{
    public class TokenResolver
    {
        public static int GetFuncionarioId(HttpContext httpContext)
        {
            var claimUser = httpContext.User.Claims.Where(user => user.Type == "funcionario_id").FirstOrDefault();
            var lcontacto_id = claimUser.Value;
            return Convert.ToInt32(lcontacto_id);
        }
    }
}