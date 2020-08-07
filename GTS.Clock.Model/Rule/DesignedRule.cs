using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model 
{
   public class DesignedRule : IEntity
    {
        #region
        public virtual Decimal ID { get; set; }
        public virtual String CSharpCode { get; set; }
        public virtual String VariablesObject { get; set; }
        public virtual String ParameterObject { get; set; }
        public virtual String RuleObject { get; set; }
        public virtual String RuleStateObject { get; set; }
        public virtual int RulePriority { get; set; }
        public virtual RuleTemplate RuleTemplate { get; set; }
        public virtual String PersionScript { get; set; }
        public virtual DateTime RuleRgisterDate { get; set; }
        public virtual int RuleEstate { get; set; }
        public virtual List<RuleTemplateParameter> RuleTemplateParameterList { get; set; }

        #endregion
    }
}
