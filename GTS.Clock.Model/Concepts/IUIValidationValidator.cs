using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.UIValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
    public interface IUIValidationValidator
    {
        UIValidationExceptions ValidateRulesParametersValue(string customCode, Dictionary<string, string> dicKeyNameValue, UIValidationExceptions exception, int ActiverulePrecardParamProxyList, IList<string> RuleParameterValueList);
    }
}
