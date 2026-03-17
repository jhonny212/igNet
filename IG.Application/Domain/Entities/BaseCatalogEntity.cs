using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Application.Domain.Entities
{
    public class BaseCatalogEntity<PK> : BaseEntity<PK>
    {
        public required string Name { get; set; }
    }
}
