using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Authentications
{
    public class SessionData
    {
        public string SessionId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
