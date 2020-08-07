using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Exceptions;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    public class ValidationWarning : ValidationException
    {
        public ExceptionResourceKeys ResourceKey { get; set; }
        public ValidationWarning(ExceptionResourceKeys key, string msg, string execptionSrc)
            : base(key, msg, execptionSrc)
        {
            ResourceKey = key;
        }
        public override string GetLogMessage()
        {
            return base.GetLogMessage();
        }
    }
}
