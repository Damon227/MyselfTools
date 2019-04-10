using System;
using IdentityDemo.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityDemo.Authentications
{
    public static class SessionExtensions
    {
        public static AuthenticationBuilder AddSessionAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddSessionAuthentication("Sessions");
        }

        public static AuthenticationBuilder AddSessionAuthentication(this AuthenticationBuilder builder, string authenticationScheme)
        {
            return builder.AddSessionAuthentication(authenticationScheme, null);
        }

        public static AuthenticationBuilder AddSessionAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<SessionAuthenticationOptions> configureOptions)
        {
            builder.Services.Configure((Action<AuthenticationOptions>)(o =>
            {
                o.DefaultScheme = authenticationScheme;

                o.AddScheme(authenticationScheme, scheme =>
                {
                    scheme.HandlerType = typeof(SessionAuthenticationHandler);
                    scheme.DisplayName = "IdentityDemo.AspNetCore.Authentication.Sessions";
                });
            }));

            if (configureOptions != null)
            {
                builder.Services.Configure(configureOptions);
            }

            builder.Services.AddTransient<IAuthenticationHandler, SessionAuthenticationHandler>();
            builder.Services.AddTransient<IAuthenticationSignInHandler, SessionAuthenticationHandler>();
            builder.Services.AddTransient<IAuthenticationSignOutHandler, SessionAuthenticationHandler>();
            return builder;
        }
    }
}
