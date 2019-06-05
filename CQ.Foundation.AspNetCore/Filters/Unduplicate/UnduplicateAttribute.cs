using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.Foundation.AspNetCore.Filters
{
    /// <summary>
    ///     防止重复提交过滤器
    /// </summary>
    public class UnduplicateAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 哈希字符串，与Redis匹配
            IUnduplicateService resubmitCheckService = context.HttpContext.RequestServices.GetRequiredService<IUnduplicateService>();
            bool resubmit = await resubmitCheckService.IsResubmitAsync(GetCode(context.HttpContext));

            if (resubmit)
            {
                context.HttpContext.Response.StatusCode = 409;
                context.Result = new ConflictObjectResult("正在处理中，请稍候...");
            }

            await base.OnActionExecutionAsync(context, next);
        }

        /// <inheritdoc />
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            IUnduplicateService resubmitCheckService = context.HttpContext.RequestServices.GetRequiredService<IUnduplicateService>();
            await resubmitCheckService.ClearAsync(GetCode(context.HttpContext));

            await base.OnResultExecutionAsync(context, next);
        }

        private static int GetCode(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(context.Request.Host.ToString());
            sb.Append(context.Request.Path.ToString());
            sb.Append(context.Request.QueryString.ToString());
            if (context.Request.Method == "POST")
            {
                context.Request.Body.Position = 0;
                StreamReader reader = new StreamReader(context.Request.Body);
                sb.Append(reader.ReadToEnd());
            }

            return sb.ToString().GetHashCode();
        }
    }
}
