using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Response;
using IG.Application.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IG.Application.Core.Extensions
{
    public static class StatusCodeExtensions
    {
        public static ObjectResult ServerError(this ControllerBase controller, Exception? ex = null)
        {
            var data = new Response<string>();
            var error = $"{ex?.Message} {ex?.StackTrace}. Error in {ex?.InnerException?.Message}";
            if (ex != null && ex.GetType() == typeof(FluentException))
            {
                FluentException exception = (FluentException)ex;
                data.Errors = exception.Errors;
                data.Message = exception.Message;
                return controller.BadRequest(data);
            }
            return controller.StatusCode(
                StatusCodes.Status500InternalServerError,
                new { Success = false, ServerMessage = error }
            );
        }

        public static ObjectResult HandleResult(
            this ControllerBase controller,
            BaseResponse response
        )
        {
            if (response.Success)
            {
                return controller.Ok(response);
            }
            else
            {
                return controller.BadRequest(response);
            }
        }
    }
}
