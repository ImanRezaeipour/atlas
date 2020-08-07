using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class ManagerFlowConditionProxy
    {
        public string State { get; set; }
        public decimal ID { get; set; }
        public decimal PrecardAccessGroupDetailID { get; set; }
        public decimal PrecardID { get; set; }
        public string PrecardName { get; set; }
        public string PrecardCode { get; set; }
        public bool EndFlow { get; set; }
        public bool Access { get; set; }
        public string OperatorKey { get; set; }
        public string OperatorTitle { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string ValueType { get; set; }
    }
}
