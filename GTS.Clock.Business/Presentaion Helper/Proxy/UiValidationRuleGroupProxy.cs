using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Proxy
{
   public class UiValidationRuleGroupProxy
    {
       public decimal ID { get; set; }
       public int CustomCode { get; set; }
       public decimal RuleID { get; set; }

       public decimal GroupID { get; set; }

       public decimal RuleGroupID { get; set; }
       public bool Active { get; set; }
       public bool Warning { get; set; }
       public bool IsWarningAllowed { get; set; }
       public bool Order { get; set; }
       public string RuleName { get; set; }
       public int  RuleGroupType { get; set; }
       public int  RuleType { get; set; }
       public int RuleTempType { get; set; }
       public string RuleColor { get; set; }
       public bool ExistRuleTag { get; set; }
    }
}
