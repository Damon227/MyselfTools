using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Models
{
    public class ApiResponse
    {
        /// <summary>
        ///     结果是否成功
        /// </summary>
        [Required]
        public bool Succeeded { get; set; }

        /// <summary>
        ///     如果 Succeeded 为 true，则该值为 "ok"；否则，为报错信息。可以将该信息提供给用户。
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        ///     如果 Succeeded 为 true，则该值为 0；否则，为错误码。
        /// </summary>
        [Required]
        public int Code { get; set; }

        /// <summary>
        ///     如果 Succeeded 为 true，则该值为相应的响应数据；否则，为 null。
        /// </summary>
        [Required]
        public object ExtraData { get; set; }

        public string StackTrace { get; set; }

        public static ApiResponse Success { get; } = new ApiResponse { Succeeded = true, Message = "成功", Code = 0 };

        public static ApiResponse BuildSuccessResponse(int code, string message, IDictionary<string, object> extraData = null)
        {
            return new ApiResponse
            {
                Succeeded = true,
                Message = message,
                Code = code,
                ExtraData = extraData ?? new Dictionary<string, object>()
            };
        }

        public static ApiResponse BuildSuccessResponse(string message, IDictionary<string, object> extraData = null)
        {
            return new ApiResponse
            {
                Succeeded = true,
                Message = message,
                Code = 0,
                ExtraData = extraData ?? new Dictionary<string, object>()
            };
        }

        public static ApiResponse BuildFailedResponse(int code, string message, IDictionary<string, object> extraData = null)
        {
            return new ApiResponse
            {
                Succeeded = false,
                Message = message,
                Code = code,
                ExtraData = extraData ?? new Dictionary<string, object>()
            };
        }
    }

    public class ApiResponse<TResponse> : ApiResponse
    {
        public TResponse Data { get; set; }

        public static ApiResponse<TResponse> BuildSuccessResponse(TResponse response, IDictionary<string, object> extraData = null)
        {
            return new ApiResponse<TResponse>
            {
                Succeeded = true,
                Message = "成功",
                Code = 0,
                Data = response,
                ExtraData = extraData ?? new Dictionary<string, object>()
            };
        }

        public static ApiResponse<TResponse> BuildFailedResponse(int code, string message, TResponse response = default(TResponse), IDictionary<string, object> extraData = null)
        {
            return new ApiResponse<TResponse>
            {
                Succeeded = false,
                Message = message,
                Code = code,
                Data = response,
                ExtraData = extraData ?? new Dictionary<string, object>()
            };
        }
    }
}
