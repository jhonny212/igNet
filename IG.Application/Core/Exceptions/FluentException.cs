using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IG.Core.Response;

namespace IG.Application.Core.Exceptions
{
    public class FluentException : Exception
    {
        public readonly List<FluentError> Errors;

        public FluentException(string message, List<FluentError> fluentErrors)
            : base(message)
        {
            Errors = fluentErrors;
        }
    }
}
