using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IG.Application.Core.Interfaces
{
    public interface IPaged
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
