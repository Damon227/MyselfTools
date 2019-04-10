using Microsoft.AspNetCore.Authentication;

namespace IdentityDemo.Authentications
{
    public class SessionAuthenticationOptions : AuthenticationSchemeOptions
    {
        public SessionAuthenticationOptions()
        {
            ClaimsIssuer = "IdentityDemo.AspNetCore.Authentication.Sessions";
        }
    }
}
