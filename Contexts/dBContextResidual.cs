using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Contexts
{
    public class dBContextResidual : DbContext
    {
        public dBContextResidual(DbContextOptions<dBContextResidual> options) : base(options)
        {

        }
    }
}