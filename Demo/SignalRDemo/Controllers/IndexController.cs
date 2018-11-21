using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRDemo.Controllers
{
    [Route("[controller]")]
    public class IndexController : Controller
    {

        [HttpPost("sendmessage/{user}/{message}")]
        public async Task SendMessage(string user, string message)
        {

           
        }
    }
}
