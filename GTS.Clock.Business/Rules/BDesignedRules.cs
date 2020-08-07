using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using NHibernate;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Business.Rules
{
   public class BDesignedRules :BaseBusiness<DesignedRule>
    {
       private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
       protected override void InsertValidate(DesignedRule obj)
       {
          
       }
       protected override void DeleteValidate(DesignedRule designrule)
        {
      
        }
        protected override void UpdateValidate(DesignedRule designrule)
        {
            
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDesignRuleLoadAccess()
        {
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
       public decimal InsertDesignedRule(DesignedRule designedrule,UIActionType UIA)
        {
            List<RuleTemplateParameter> ParamNames = designedrule.RuleTemplateParameterList;
            BRuleParameterTemp RuleParamTemp = new BRuleParameterTemp();
            foreach (var item in ParamNames)
            {

                RuleParamTemp.SaveChanges(item, UIActionType.ADD);

            }
                return base.SaveChanges(designedrule, UIA);
            
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
       public decimal UpdateDesignedRule(DesignedRule designedrule, UIActionType UAT)
       {
           //return base.SaveChanges(designedrule, UAT);
           List<RuleTemplateParameter> ParamNames = designedrule.RuleTemplateParameterList;
           BRuleParameterTemp RuleParamTemp = new BRuleParameterTemp();
           foreach (var item in ParamNames)
           {

               RuleParamTemp.SaveChanges(item, UIActionType.EDIT);

           }
           return base.SaveChanges(designedrule, UAT);
            
       }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteDesignedRule(DesignedRule designedrule, UIActionType UAT)
        {
            return base.SaveChanges(designedrule, UAT);
        }
        public bool CheckToExistRuleCSharpCode(decimal RuleTemplateID)
        {
         
            DesignedRule designedRuleAlias = null;
            int designedRuleCount = NHSession.QueryOver<DesignedRule>(() => designedRuleAlias).Where(r=>r.RuleTemplate.ID == RuleTemplateID).RowCount();
            if (designedRuleCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
