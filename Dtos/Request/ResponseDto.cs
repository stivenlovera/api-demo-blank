using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.Dtos.Resquest
{
    public class ResponseDto<TModel>
    {
        public int status { get; set; }
        public string message { get; set; }
        public TModel data { get; set; }
    }
}