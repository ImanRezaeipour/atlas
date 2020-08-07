using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.UIValidation;
using System.Drawing;

namespace GTS.Clock.Business.Proxy
{
   public class UIValidationRuleParameterProxy
    {
        public string ID { get; set; }
        public decimal PrecardID { get; set; }
        public decimal ParamID { get; set; }
        public string PrameterName { get; set; }
        public string ParameterValue { get; set; }
        public string KeyName { get; set; }
        public decimal ExistParam { get; set; }
        public decimal ParamType { get; set; }
        public bool ContinueOnTomorrow { get; set; }
        public string ParameterColor { get; set; }      
        public UIValidationRuleGroupPrecard RuleGroupPrecard { get; set; }
       
    }
}
