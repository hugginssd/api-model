using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Contracts.V1.Requests
{
    public class UpdateTagsRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
