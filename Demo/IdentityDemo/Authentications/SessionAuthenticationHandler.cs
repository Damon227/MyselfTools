using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IdentityDemo.Authentications
{
    public class SessionAuthenticationHandler : IAuthenticationSignInHandler
    {
        private AuthenticationScheme Scheme { get; set; }

        private HttpContext HttpContext { get; set; }

        /// <summary>
        /// The handler should initialize anything it needs from the request and scheme here.
        /// </summary>
        /// <param name="scheme">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationScheme" /> scheme.</param>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> context.</param>
        /// <returns></returns>
        public async Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            Scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
            HttpContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>Authentication behavior.</summary>
        /// <returns>The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticateResult" /> result.</returns>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            if (!HttpContext.Session.IsAvailable)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!HttpContext.Session.TryGetValue("userId", out byte[] userIdBytes) 
                || userIdBytes == null 
                || string.IsNullOrEmpty(Encoding.UTF8.GetString(userIdBytes)))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            try
            {
                if (HttpContext.Session.TryGetValue("identity", out byte[] identityBytes))
                {
                    Identity identity = JsonConvert.DeserializeObject<Identity>(Encoding.UTF8.GetString(identityBytes));

                    Claim userIdClaim = new Claim(ClaimTypes.NameIdentifier, identity.UserId);
                    Claim userNameClaim = new Claim(ClaimTypes.Name, identity.UserName);
                    Claim permissionClaim = new Claim("Permission", string.Join(',', identity.PermissionCodes));
                    IList<Claim> claims = new List<Claim> { userIdClaim, userNameClaim, permissionClaim };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name, ClaimTypes.Name, ClaimTypes.Role);
                    ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                    AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }

                return Task.FromResult(AuthenticateResult.NoResult());
            }
            catch (Exception e)
            {
                return Task.FromResult(AuthenticateResult.Fail(e));
            }
        }

        /// <summary>Challenge behavior.</summary>
        /// <param name="properties">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" /> that contains the extra meta-data arriving with the authentication.</param>
        /// <returns>A task.</returns>
        public async Task ChallengeAsync(AuthenticationProperties properties)
        {
            ApiResponse response = new ApiResponse
            {
                Succeeded = false,
                Code = 401,
                ExtraData = new Dictionary<string, object>(),
                Message = string.Empty
            };

            HttpContext.Response.StatusCode = 401;
            HttpContext.Response.ContentType = "application/json; charset=utf-8";
            await HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        /// <summary>Forbid behavior.</summary>
        /// <param name="properties">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" /> that contains the extra meta-data arriving with the authentication.</param>
        /// <returns>A task.</returns>
        public async Task ForbidAsync(AuthenticationProperties properties)
        {
            ApiResponse response = new ApiResponse
            {
                Succeeded = false,
                Code = 403,
                ExtraData = new Dictionary<string, object>(),
                Message = string.Empty
            };

            HttpContext.Response.StatusCode = 401;
            HttpContext.Response.ContentType = "application/json; charset=utf-8";
            await HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        /// <summary>Signout behavior.</summary>
        /// <param name="properties">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" /> that contains the extra meta-data arriving with the authentication.</param>
        /// <returns>A task.</returns>
        public Task SignOutAsync(AuthenticationProperties properties)
        {
            HttpContext.Session.Clear();
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            return Task.CompletedTask;
        }

        /// <summary>Handle sign in.</summary>
        /// <param name="user">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> user.</param>
        /// <param name="properties">The <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" /> that contains the extra meta-data arriving with the authentication.</param>
        /// <returns>A task.</returns>
        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("ClaimsPrincipal must has NameIdentifier claim.", nameof(user));
            }

            IList<Claim> claims = new List<Claim>();

            // 如果 Session 中已经有userId，则需要清除重新创建 Session。
            if (HttpContext.Session.TryGetValue("userId", out byte[] _))
            {
                HttpContext.Session.Clear();
            }

            // 设置 UserId
            if (user.HasClaim(t => t.Type == ClaimTypes.NameIdentifier))
            {
                claims.Add(user.FindFirst(ClaimTypes.NameIdentifier));

                HttpContext.Session.Set("userId", Encoding.UTF8.GetBytes(user.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

            // 设置 UserName
            if (user.HasClaim(t => t.Type == ClaimTypes.Name))
            {
                claims.Add(user.FindFirst(ClaimTypes.Name));

                HttpContext.Session.Set("userId", Encoding.UTF8.GetBytes(user.FindFirst(ClaimTypes.Name).Value));
            }

            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name, ClaimTypes.Name, ClaimTypes.Role));

            return Task.CompletedTask;
        }
    }
}
