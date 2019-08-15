using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationData.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GasStationWeb.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthencationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GasStationData.Repositories.IRepositories.IUserRepository _userRepository;
        
        
        public AuthencationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            bool conditionWeb = path.Value.StartsWith("/user");
            bool conditionApi = path.Value.StartsWith("/api");
            bool condition = conditionWeb || conditionApi;
            if (path.HasValue && !condition )
            {
                if(httpContext.Session.GetString("userId") == null)
                {
                    httpContext.Response.Redirect("/user");
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthencationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthencationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthencationMiddleware>();
        }
    }
}
