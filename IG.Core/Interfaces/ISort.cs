using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISort
    {
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
