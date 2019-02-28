using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace QuartzDemo
{
    public class DataOptions : IOptions<DataOptions>
    {
        public string ConnectionString { get; set; }

        DataOptions IOptions<DataOptions>.Value => this;
    }
}
