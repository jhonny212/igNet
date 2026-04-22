using System;
using System.Collections.Generic;

namespace IG.Application.Core.Exceptions
{
    public class ExceptionIg : Exception
    {
        public string? EntityName { get; }
        public string? Operation { get; }
        public string? ErrorCode { get; }
        public IReadOnlyDictionary<string, object?> Metadata { get; }

        public ExceptionIg(
            string message,
            string? entityName = null,
            string? operation = null,
            string? errorCode = null,
            Exception? innerException = null,
            IDictionary<string, object?>? metadata = null
        )
            : base(message, innerException)
        {
            EntityName = entityName;
            Operation = operation;
            ErrorCode = errorCode;
            Metadata = metadata != null
                ? new Dictionary<string, object?>(metadata)
                : new Dictionary<string, object?>();
        }
    }
}