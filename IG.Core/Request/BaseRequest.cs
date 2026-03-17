using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Request
{
    public class BaseRequest
    {
        public string LoggedInUser { get; set; } = "SYSTEM";
    }
}
