using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KC.Foundation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRDemo.Controllers
{
    [Route("[controller]")]
    public class IndexController : Controller
    {
        private ObjectPool<Guid> _pool;
        private readonly IHubContext<MessageHub> _hubContext;

        public IndexController(ObjectPool<Guid> pool, IHubContext<MessageHub> hubContext)
        {
            _pool = pool;
            _hubContext = hubContext;
        }

        [HttpPost("sendmessage/{user}/{message}")]
        public async Task SendMessage(string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("dododo", message);

        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            //Guid guid = _pool.GetObject();
            //_pool.PutObject(guid);

            Guid guid2 = new Guid();
           
            _pool.Using(guid =>
            {
                guid2 = guid;
                throw new Exception();
            });
            
            return Ok(guid2);
        }
    }
}
