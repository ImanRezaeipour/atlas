using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Proxy
{
    public class RuleTemplateProxy
    {
        public RuleTemplateProxy(RuleTemplate ruleTemplate)
        {

            Id = ruleTemplate.ID;
            Script = ruleTemplate.Script;
            Name = ruleTemplate.Name;
            IdentifierCode = ruleTemplate.IdentifierCode;
            CustomCategoryCode = ruleTemplate.CustomCategoryCode;
            TypeId = ruleTemplate.TypeId;
            CSharpCode = ruleTemplate.CSharpCode;
            UserDefined = ruleTemplate.UserDefined;
            JsonObject = ruleTemplate.JsonObject;
            Order = ruleTemplate.Order;
            OperationalArea = ruleTemplate.OperationalArea;
            if (ruleTemplate.DesignedRule != null)
            {
                RuleStateObject = ruleTemplate.DesignedRule.RuleStateObject;
                RuleParametersObject = ruleTemplate.DesignedRule.ParameterObject;
                RuleVariablesObject = ruleTemplate.DesignedRule.VariablesObject;
                DesignedRuleID = ruleTemplate.DesignedRule.ID;
            }
            else
            {
                RuleStateObject = string.Empty;
                RuleParametersObject = string.Empty;
                RuleVariablesObject = string.Empty;
            }
        }

        #region properties
        public string CustomCategoryCode { get; set; }
        public decimal IdentifierCode { get; set; }
        public string Name { get; set; }
        public decimal Id { get; set; }
        public decimal CategoryId { get; set; }
        public decimal TypeId { get; set; }
        public string Type { get; set; }
        public bool IsForcible { get; set; }
        public string Script { get; set; }
        public string CSharpCode { get; set; }
        public bool UserDefined { get; set; }
        public string JsonObject { get; set; }
        public decimal Order { get; set; }
        public string RuleStateObject { get; set; }
        public string RuleParametersObject { get; set; }
        public string RuleVariablesObject { get; set; }
        public decimal DesignedRuleID { get; set;}
        public decimal OperationalArea { get; set; }
        public string OperationalAreaName { get; set; }
        #endregion
    }

}
