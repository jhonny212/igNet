using IG.Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Application.Core.Request
{
    public class BaseRequest : IBaseRequest
    {
        public string LoggedInUser { get; set; } = "SYSTEM";
    }
}
