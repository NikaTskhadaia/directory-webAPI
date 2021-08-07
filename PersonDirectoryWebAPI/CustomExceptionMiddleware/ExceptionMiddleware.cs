using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PersonDirectory.Domain.Models;
using PersonDirectory.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PersonDirectory.WebAPI.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PersonModel> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<PersonModel> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong. {ex}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errorDeatils = new ErrorDetails { 
                    StatusCode = context.Response.StatusCode, 
                    Message = ex.Message 
                }.ToString();
                await context.Response.WriteAsync(errorDeatils);
            }
        }
    }
}
