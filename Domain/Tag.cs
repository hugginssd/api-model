using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Domain
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string CreatorId { get;  set; }
        public DateTime CreatedOn { get;  set; }
    }
}
