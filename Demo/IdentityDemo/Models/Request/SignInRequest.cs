using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Models.Request
{
    public class SignInRequest
    {
        /// <summary>
        ///     用户名
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "用户名长度必须为1到20位")]
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度必须为6到20位")]
        public string Password { get; set; }
    }
}
