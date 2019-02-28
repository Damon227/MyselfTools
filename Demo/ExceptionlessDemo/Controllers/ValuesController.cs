using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionlessDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet, Route("log/test")]
        public ActionResult<bool> LogTest()
        {
            ExceptionlessClient client = new ExceptionlessClient(c =>
            {
                c.ApiKey = "MgPAWFFyzPfWFV91qIHJu4sXEuRIJWnHtx6FhPNq";
                c.ServerUrl = "http://localhost:8008/";
            });


            client.SubmitLog("测试Exceptionless");

            client.SubmitLog("测试Exceptionless", LogLevel.Info);
            client.SubmitLog("测试Exceptionless", LogLevel.Trace);
            client.SubmitLog("测试Exceptionless", LogLevel.Warn);
            client.SubmitLog("测试Exceptionless", LogLevel.Error);

            client.SubmitLog(typeof(ValuesController).FullName, "测试Exceptionless", LogLevel.Error);

            client.SubmitFeatureUsage("MyFeature");
            client.CreateFeatureUsage("MyFeature")
                .AddTags("ExceptionlessTag", "Demo")
                .Submit();
            var user = new { Name = "Damon"};
            client.CreateFeatureUsage("MyFeature")
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
                client.SubmitException(e);
            }

            return true;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
