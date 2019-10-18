using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public DevController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("SendToClient")]
        public async Task<IActionResult> SendToClient([FromQuery] string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMsg", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return Accepted();
        }
    }
}