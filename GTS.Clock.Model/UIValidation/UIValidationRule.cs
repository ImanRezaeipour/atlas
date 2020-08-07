using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
namespace GTS.Clock.Model.UIValidation
{
    public class UIValidationRule : IEntity
    {
        #region
        public virtual Decimal ID { get; set; }
        public virtual String Name { get; set; }
        public virtual String CustomCode { get; set; }
        public virtual Decimal Order { get; set; }
        public virtual int RuleConcept { get; set; }
        public  virtual int  SubsystemID { get; set; }
        public virtual bool ExistTag { get; set; }
        public virtual UIValidationRuleType Type { get; set; }
        public virtual IList<UIValidationRuleGroup> RuleGroupList { get; set; }
        public virtual IList<UIValidationRuleTempParameter> TempParamList { get; set; }
        public virtual IList<UIValidationAllowedRulePrecard> AllowedRulePrecardList { get; set; }
        #endregion
    }
}
