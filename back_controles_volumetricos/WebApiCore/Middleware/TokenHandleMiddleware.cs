using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace WebApiCore.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json;text/html";
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    status = "Unauthorized",
                    Mensaje = "No esta autorizado para acceder el servicio protegido."
                }));
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenHandleMiddleware>();
        }
    }
}
