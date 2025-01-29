using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Api.Middlewares
{
    public class ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger, IHostEnvironment env )
    {
        public async Task InvokeAsync(HttpContext ctx) {
            try
            {
                await next(ctx);
            }
            catch (Exception ex)
            {
                string? msg = ex.Message;

                logger.LogError(ex, msg);

                ctx.Response.ContentType = "application/json";

                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment() 
                    ? new ApiException(ctx.Response.StatusCode, msg, ex.StackTrace)
                    : new ApiException(ctx.Response.StatusCode, msg, "Internal server error");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await ctx.Response.WriteAsync(json);

            }
        }
        
    }
}