﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalFramework
{
    public class MessageBase
    {
        public string SpanId { get; set; }
        public string TraceId { get; set; }
    }
}
