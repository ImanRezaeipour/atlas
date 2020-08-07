using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
   public  class UiValidationRulePrecardParamProxy
    {
       public decimal ID { get; set; }
       public decimal PrecardID { get; set; }
       public bool Active { get; set; }
       public bool Operator { get; set; }
       public decimal ExistPrecard { get; set; }

       public List<UiValidationRuleParamProxy> ObjRuleParams { get; set; }
    }
}
