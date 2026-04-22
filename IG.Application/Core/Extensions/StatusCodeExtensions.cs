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
            if (ex is FluentException fluentException)
            {
                data.Errors = fluentException.Errors;
                data.Message = fluentException.Message;
                return controller.BadRequest(data);
            }

            if (ex is ExceptionIg exceptionIg)
            {
                return controller.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = exceptionIg.Message,
                        EntityName = exceptionIg.EntityName,
                        Operation = exceptionIg.Operation,
                        ErrorCode = exceptionIg.ErrorCode,
                        ServerMessage = exceptionIg.InnerException?.Message,
                    }
                );
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
