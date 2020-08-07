using GTS.Clock.Model.Concepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
    public class UIValidationAllowedRulePrecard : IEntity
    {
        #region
        public virtual Decimal ID { get; set; }
        public virtual UIValidationRule Rule { get; set; }
        public virtual Precard Precard { get; set; }
        #endregion
    }
}
