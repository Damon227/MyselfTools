using System.Collections.Generic;

namespace IdentityDemo.Authentications
{
    public class Identity
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        ///     权限列表
        /// </summary>
        public IList<string> PermissionCodes { get; set; }
    }
}
