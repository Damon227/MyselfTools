using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public interface IDemoService
    {
        void Work();

        Task QueryStationMsgAsync();
    }
}
