using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class UiValidationRuleParamProxy
    {
        public string ID { get; set; }
        public decimal ParamID { get; set; }
        public string KeyName { get; set; }
        public decimal ExistParam { get; set; }
        public string ParameterValue { get; set; }
        public decimal ParamType { get; set; }
        public bool ContinueOnTomorrow { get; set; }

    }
}
