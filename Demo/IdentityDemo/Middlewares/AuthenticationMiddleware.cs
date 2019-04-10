using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityDemo.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public AuthenticationMiddleware(RequestDelegate next, IAuthenticationSchemeProvider schemes)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _authenticationSchemeProvider = schemes ?? throw new ArgumentNullException(nameof(schemes));
        }

        public async Task Invoke(HttpContext context)
        {
            context.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
            {
                OriginalPath = context.Request.Path,
                OriginalPathBase = context.Request.PathBase
            });

            IAuthenticationHandlerProvider handlers = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (AuthenticationScheme scheme in await _authenticationSchemeProvider.GetRequestHandlerSchemesAsync())
            {
                if (await handlers.GetHandlerAsync(context, scheme.Name) is IAuthenticationRequestHandler handler
                    && await handler.HandleRequestAsync())
                {
                    return;
                }
            }

            AuthenticationScheme defaultAuthenticate = await _authenticationSchemeProvider.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                AuthenticateResult result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                if (result?.Principal != null)
                {
                    context.User = result.Principal;
                }
            }

            try
            {
                await _next(context);
            }
            finally
            {
                //
            }
        }
    }
}
