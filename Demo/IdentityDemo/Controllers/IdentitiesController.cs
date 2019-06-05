using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CQ.Foundation.AspNetCore.Filters;
using IdentityDemo.Filters;
using IdentityDemo.Models;
using IdentityDemo.Models.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers
{
    [Route("api/[controller]")]
    public class IdentitiesController : ControllerBase
    {
        /// <summary>
        ///     登录。
        /// </summary>
        [HttpPost, Route("signin")]
        [RequestModelValidation]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if (request.UserName == "test" && request.Password == "111111")
            {
                Claim nameClaim = new Claim(ClaimTypes.Name, request.UserName);
                Claim idClaim = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim> { nameClaim, idClaim });
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                // HttpContext.SignInAsync后，转到IAuthenticationSignInHandler中间件处理
            }

            return Ok(ApiResponse.Success);
        }

        /// <summary>
        ///     获取当前登录用户的信息
        /// </summary>
        [HttpGet, Route("getcurrentuser")]
        [Authorize("User")]
        public async Task<IActionResult> GetCurrentUser()
        {
            string userName = HttpContext.User.Identity.Name;

            return Ok(userName);
        }
    }
}
