using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Response
{
    public class Response<T> : BaseResponse
    {
        public T? Data { get; set; }
        public string? ServerMessage { get; set; }
    }
}
