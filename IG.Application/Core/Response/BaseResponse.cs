using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IG.Core.Response;

namespace Core.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool ShowMessage { get; set; } = true;
        public List<FluentError> Errors { get; set; } = new List<FluentError>();
    }
}
