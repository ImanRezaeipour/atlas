using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using GTS.Clock.Business.Proxy;

namespace GTS.Clock.Business.Concept
{
   public class BRuleGeneratorSetting
    {
       public BRuleGeneratorSetting()
       { }
       public RuleGeneratorSettingProxy RuleGeneratorSetting()
       {
           bool LocalVariableEnabled = false;
           bool GeneralVariableEnabled = true;
           const string RuleGeneratorLocalVariableEnabled = "RuleGeneratorLocalVariableEnabled";
           const string RuleGeneratorGeneralVariables = "RuleGeneratorGeneralVariableEnabled";
           RuleGeneratorSettingProxy RuleGeneratorSettingProxyObj = new RuleGeneratorSettingProxy();
           string RuleGeneratorLocalVariableEnabledValue = WebConfigurationManager.AppSettings[RuleGeneratorLocalVariableEnabled];
           string RuleGeneratorGeneralVariablesValue = WebConfigurationManager.AppSettings[RuleGeneratorGeneralVariables];
           bool.TryParse(RuleGeneratorLocalVariableEnabledValue, out LocalVariableEnabled);
           bool.TryParse(RuleGeneratorGeneralVariablesValue, out GeneralVariableEnabled);
           RuleGeneratorSettingProxyObj.LocalVariable=LocalVariableEnabled;
           RuleGeneratorSettingProxyObj.GeneralVariable = GeneralVariableEnabled;
           return RuleGeneratorSettingProxyObj;
       }

    }
}
