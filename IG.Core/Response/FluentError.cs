using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Core.Response
{
    public class FluentError
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Field that failed when trying to validate
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; set; }

        public FluentError(string message, string fieldName, string errorCode)
        {
            ErrorMessage = message;
            FieldName = fieldName;
            ErrorCode = errorCode;
        }
    }
}
