using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IG.Application.Core.Interfaces;

namespace IG.Application.Core.Request
{
    public class SortRequest : BaseRequest, ISort
    {
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
