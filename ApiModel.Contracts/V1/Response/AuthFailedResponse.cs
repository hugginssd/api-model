using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Contracts.V1.Response
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; } 
    }
}
