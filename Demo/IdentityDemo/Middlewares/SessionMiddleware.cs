using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityDemo.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Primitives;

namespace IdentityDemo.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            bool isNewSessionKey = !httpContext.Request.Headers.TryGetValue("x-sid", out StringValues sessionId);

            httpContext.Features.Set<ISessionFeature>(new SessionFeature
            {
                Session = SessionStore.InitSession(httpContext, sessionId.ToString(), isNewSessionKey)
            });

            await httpContext.Session.LoadAsync();

            httpContext.Response.OnStarting(() =>
            {
                if (string.IsNullOrEmpty(httpContext.Session.Id))
                {
                    httpContext.Response.Headers["x-sid"] = "DELETED";
                }
                else
                {
                    httpContext.Response.Headers["x-sid"] = httpContext.Session.Id;
                }

                return Task.CompletedTask;
            });

            try
            {
                await _next(httpContext);
            }
            finally
            {
                if (httpContext.Session.IsAvailable)
                {
                    await httpContext.Session.CommitAsync();
                }
            }
        }
    }
}
