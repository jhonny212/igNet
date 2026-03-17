using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaged
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
