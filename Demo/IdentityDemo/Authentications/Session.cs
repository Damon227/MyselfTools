using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IdentityDemo.Authentications
{
    public class Session : ISession
    {
        private readonly bool _isNewSessionKey;

        private bool _isAvailable;

        private HttpContext HttpContext { get; set; }

        public SessionData SessionData { get; set; }

        public Session(HttpContext httpContext, string sessionId, bool isNewSessionKey)
        {
            HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            Id = sessionId;
            _isNewSessionKey = isNewSessionKey;
        }

        /// <summary>
        /// Load the session from the data store. This may throw if the data store is unavailable.
        /// </summary>
        /// <returns></returns>
        public async Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (IsAvailable)
            {
                return;
            }

            if (_isNewSessionKey)
            {
                SessionData = SessionStore.InitSessionData(Id);
            }
            else
            {
                SessionData = SessionStore.Store.FirstOrDefault(t => t.SessionId == Id);
            }

            IsAvailable = true;
        }

        /// <summary>
        /// Store the session in the data store. This may throw if the data store is unavailable.
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (!IsAvailable)
            {
                return Task.CompletedTask;
            }

            SessionData sessionData = new SessionData
            {
                SessionId = Id,
                UserId = SessionData.UserId,
                UserName = SessionData.UserName,
                Data = SessionData.Data
            };

            Identity identity = new Identity
            {
                UserId = sessionData.UserId,
                UserName = sessionData.UserName,
                PermissionCodes = new List<string> { "User", "Admin" }
            };

            sessionData.Data["identity"] = JsonConvert.SerializeObject(identity);

            SessionStore.Store.Add(sessionData);

            return Task.CompletedTask;
        }

        /// <summary>Retrieve the value of the given key, if present.</summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out byte[] value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            value = null;

            if (key == "sessionId")
            {
                value = Encoding.UTF8.GetBytes(SessionData.SessionId);
            }
            else if (key == "userId")
            {
                value = Encoding.UTF8.GetBytes(SessionData.UserId);
            }
            else if (SessionData.Data.TryGetValue(key, out string stringValue))
            {
                value = Encoding.UTF8.GetBytes(stringValue);
            }

            return value != null;
        }

        /// <summary>
        /// Set the given key and value in the current session. This will throw if the session
        /// was not established prior to sending the response.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, byte[] value)
        {
            if (key == "userId")
            {
                SessionData.UserId = Encoding.UTF8.GetString(value);
            }
            else if (key == "userName")
            {
                SessionData.UserName = Encoding.UTF8.GetString(value);
            }
        }

        /// <summary>Remove the given key from the session if present.</summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            
        }

        /// <summary>
        /// Remove all entries from the current session, if any.
        /// The session cookie is not removed.
        /// </summary>
        public void Clear()
        {
            
        }

        /// <summary>Indicate whether the current session has loaded.</summary>
        public bool IsAvailable
        {
            get => _isAvailable && SessionData != null;
            private set => _isAvailable = value;
        }

        /// <summary>
        /// A unique identifier for the current session. This is not the same as the session cookie
        /// since the cookie lifetime may not be the same as the session entry lifetime in the data store.
        /// </summary>
        public string Id { get; }

        /// <summary>Enumerates all the keys, if any.</summary>
        public IEnumerable<string> Keys { get; }
    }
}
