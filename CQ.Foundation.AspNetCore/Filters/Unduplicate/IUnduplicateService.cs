using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQ.Foundation.AspNetCore.Filters
{
    public interface IUnduplicateService
    {
        Task<bool> IsResubmitAsync(int code);

        Task ClearAsync(int code);
    }
}
