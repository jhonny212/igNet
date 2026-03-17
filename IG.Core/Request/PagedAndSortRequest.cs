using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Request
{
    public class PagedAndSortRequest : IPagedAndSort
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
