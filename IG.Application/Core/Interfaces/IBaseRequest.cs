using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IG.Application.Core.Interfaces
{
    public interface IBaseRequest
    {
        public string LoggedInUser { get; set; }
    }
}
