using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Teacherrpcservice;

namespace RpcServer2
{
    public class TeacherService : TeacherRpcService.TeacherRpcServiceBase
    {
        /// <summary>
        ///
        ///获取老师姓名，通过id来查询。
        ///该方法是一个简单的RPC方法，方法内部处理完成后返回结果。 
        /// </summary>
        /// <param name="request">The request received from the client.</param>
        /// <param name="context">The context of the server-side call handler being invoked.</param>
        /// <returns>The response to send back to the client (wrapped by a task).</returns>
        public override async Task<Teacher> GetName(IdRequest request, ServerCallContext context)
        {
            if (request.Id % 2 == 0)
            {
                return await Task.FromResult(new Teacher
                {
                    Age = 0,
                    Name = "偶老师"
                });
            }

            return await Task.FromResult(new Teacher
            {
                Age = 1,
                Name = "奇老师"
            });
        }
    }
}
