using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityDemo.Filters
{
    public class RequestModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Where(t => t.Value.Errors.Count > 0)
                    .Select(t => new
                    {
                        Name = t.Key,
                        Message = t.Value.Errors.First().ErrorMessage
                    }).ToArray();

                var error = errors.FirstOrDefault();
                if (error != null)
                {
                    ApiResponse response = ApiResponse.BuildFailedResponse(400, error.Message);
                    context.Result = new ObjectResult(response);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
