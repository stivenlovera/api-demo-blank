using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Contexts
{
    public class DBContextResidual : DbContext
    {
        public DBContextResidual(DbContextOptions<DBContextResidual> options) : base(options)
        {

        }
    }
}