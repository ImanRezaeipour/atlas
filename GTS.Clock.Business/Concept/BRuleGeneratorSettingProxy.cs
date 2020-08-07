using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using GTS.Clock.Business.Proxy;

namespace GTS.Clock.Business.Concept
{
   public class BRuleGeneratorSettingProxy
    {
       public BRuleGeneratorSettingProxy()
       { }
       public RuleGeneratorSettingProxy RuleGeneratorSetting()
       {
           const string RuleGeneratorLocalVariableEnabled = "RuleGeneratorLocalVariableEnabled";
           RuleGeneratorSettingProxy RuleGeneratorSettingProxyObj = new RuleGeneratorSettingProxy();
           string value = WebConfigurationManager.AppSettings[RuleGeneratorLocalVariableEnabled];
           bool LocalVariableEnabled = bool.Parse(value);
           RuleGeneratorSettingProxyObj.LocalVariable=LocalVariableEnabled;
           return RuleGeneratorSettingProxyObj;
       }

    }
}
