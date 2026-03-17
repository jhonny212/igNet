using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IG.Application.Core.Interfaces;

namespace IG.Application.Core.Request
{
    public class PagedRequest : BaseRequest, IPaged
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}
