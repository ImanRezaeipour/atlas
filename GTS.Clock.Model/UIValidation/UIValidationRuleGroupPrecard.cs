using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.UIValidation
{
    public class UIValidationRuleGroupPrecard : IEntity
    {
        #region
        public virtual Decimal ID { get; set; }
        public virtual UIValidationRuleGroup UIValidationRuleGroup { get; set; }
        public virtual Precard Precard { get; set; }
        public virtual IList<UIValidationRuleParam> ParamList { get; set; }
        public virtual bool Active { get; set; }
        public virtual bool Operator { get; set; }

        #endregion
    }
}
