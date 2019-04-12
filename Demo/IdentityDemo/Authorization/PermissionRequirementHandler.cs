using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IdentityDemo.Authorization
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.HasClaim(t => t.Type == "Permission"))
            {
                return Task.CompletedTask;
            }

            string permissionString = context.User.FindFirst(t => t.Type == "Permission")?.Value;

            if (!string.IsNullOrEmpty(permissionString))
            {
                IList<string> permissions = permissionString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (requirement.Permissions.Any(t => permissions.Contains(t)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
