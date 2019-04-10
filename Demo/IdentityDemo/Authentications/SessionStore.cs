using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IdentityDemo.Authentications
{
    public static class SessionStore
    {
        // 模拟数据存储
        public static List<SessionData> Store { get; set; } = new List<SessionData>();

        public static Session InitSession(HttpContext httpContext, string sessionId, bool isNewSessionKey)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
            }
            Session session = new Session(httpContext, sessionId, isNewSessionKey);

            return session;
        }

        public static SessionData InitSessionData(string sessionId)
        {
           SessionData sessionData = new SessionData
           {
               SessionId = sessionId,
               UserId = string.Empty,
               UserName = string.Empty
           };

           return sessionData;
        }
    }
}
