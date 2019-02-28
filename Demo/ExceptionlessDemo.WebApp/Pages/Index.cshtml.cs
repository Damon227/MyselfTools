using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExceptionlessDemo.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ExceptionlessClient.Default.SubmitLog("测试Exceptionless");

            ExceptionlessClient.Default.SubmitLog("测试Exceptionless", LogLevel.Info);
            ExceptionlessClient.Default.SubmitLog("测试Exceptionless", LogLevel.Trace);
            ExceptionlessClient.Default.SubmitLog("测试Exceptionless", LogLevel.Warn);
            ExceptionlessClient.Default.SubmitLog("测试Exceptionless", LogLevel.Error);

            ExceptionlessClient.Default.SubmitLog(typeof(IndexModel).FullName, "测试Exceptionless", LogLevel.Error);

            ExceptionlessClient.Default.SubmitFeatureUsage("MyFeature");
            ExceptionlessClient.Default.CreateFeatureUsage("MyFeature")
                .AddTags("ExceptionlessTag", "Demo")
                .Submit();
            var user = new { Name = "Damon" };
            ExceptionlessClient.Default.CreateFeatureUsage("MyFeature")
                .AddTags("ExceptionlessTag", "Demo")
                .AddObject(user, "UserInfo")//  添加一个对象信息
                .SetProperty("Cellphone", "13100000000")//  设置手机号
                .SetReferenceId(Guid.NewGuid().ToString("N"))// 为事件设定一个编号，以便于你搜索 
                .MarkAsCritical()// 标记为关键异常
                .SetGeo(43, 44)// 设置地理位置坐标
                .SetUserIdentity("userId", "userName")// 设置触发异常的用户信息
                .SetUserDescription("emailAddress", "")// 设置触发用户的一些描述
                .Submit();

            try
            {
                string s = null;
                string s1 = s.ToString();
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
            }
        }
    }
}
