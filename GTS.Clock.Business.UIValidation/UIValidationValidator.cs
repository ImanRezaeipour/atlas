using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure;
namespace GTS.Clock.Business.UIValidation
{
    public class UIValidationValidator : IUIValidationValidator
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();       
        const string ExceptionSrc = "GTS.Clock.Business.UIValidation.UIValidationValidator";
        /// <summary>
        /// اعتبارسنجی مقادیر پارامترهای قانون های خاص 
        /// 
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="ruleGroupId"></param>
        /// <param name="dicKeyNameValue"></param>
        public UIValidationExceptions ValidateRulesParametersValue(string customCode, Dictionary<string, string> dicKeyNameValue, UIValidationExceptions exception, int ActiverulePrecardParamProxyList, IList<string> RuleParameterValueList)
        {
            try
            {                
                foreach (string keyName in dicKeyNameValue.Keys)
                {
                    switch (Utility.ToInteger(customCode))
                    {
                        case (int)UIValidationCustomCode.R1:
                            exception = R1_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception);                           
                            break;
                        case (int)UIValidationCustomCode.R30:
                            exception = R30_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception);
                            break;
                        case (int)UIValidationCustomCode.R24 :
                        case (int)UIValidationCustomCode.R25:
                            exception = R_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception);
                            break;           
                        case(int)UIValidationCustomCode.R26:
                            exception = R26_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception, ActiverulePrecardParamProxyList, RuleParameterValueList);
                            break;
                        case (int)UIValidationCustomCode.R200:
                            exception = R200_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception);
                            break;
                        case (int)UIValidationCustomCode.R201:
                            exception = R201_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception);
                            break;
                        case (int)UIValidationCustomCode.R36:
                            exception = R36_ValidateRuleParametersValue(keyName, dicKeyNameValue[keyName], exception); 
                            break;
                    }           
                }
                return exception;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "UIValidationValidator", "ValidateParameterValue");
                throw ex;
            }
        }
        private UIValidationExceptions R1_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception)
        {
            switch (keyName)
            {
                case "LockCalculationFromCurrentMonth":
                    int lockCalculationFromMonth = 2;
                    if (parameterValue != string.Empty)
                        lockCalculationFromMonth = Utility.ToInteger(parameterValue);
                    if (lockCalculationFromMonth != 0 && lockCalculationFromMonth != 1)
                    {
                      exception = AddException(ExceptionResourceKeys.lockCalculationFromMonthNotValid, "  مقدار پارامترماه باید عدد 0 (ماه قبل ) یا 1 (ماه جاری) باشد", exception);
                    }
                    break;
                case "LockCalculationFromMonth":
                    int LockCalculationFromCurrentMonth = Utility.ToInteger(parameterValue);

                    if (LockCalculationFromCurrentMonth < 1 || LockCalculationFromCurrentMonth > 31)
                    {
                       exception = AddException(ExceptionResourceKeys.LockCalculationFromCurrentMonthNotValid, "مقدارپارامتر روز باید  عددی بین 1 تا 31 باشد", exception);
                    }
                    break;
            }
            return exception;
        }

        private UIValidationExceptions R30_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception)
        {
            switch (keyName)
            {
                case "LockCalculationtMonth":
                    int LockCalculationtMonth = Utility.ToInteger(parameterValue);
                    if (LockCalculationtMonth < 1 || LockCalculationtMonth > 12)
                    {
                      exception = AddException(ExceptionResourceKeys.LockCalculationtMonthNotValid, "  مقدار پارامتر دوره محاسباتی باید عددی بین 1 تا 12 باشد", exception);
                    }
                    break;
                case "LockCalculationDay":
                    int LockCalculationDay = Utility.ToInteger(parameterValue);

                    if (LockCalculationDay < 1 || LockCalculationDay > 31)
                    {
                       exception = AddException(ExceptionResourceKeys.LockCalculationDayNotValid, "مقدارپارامتر روز باید عددی بین 1 تا 31 باشد", exception);
                    }
                    break;
            }
            return exception;
        }
        private UIValidationExceptions R_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception)
        {
            int ParameterValue = Utility.ToInteger(parameterValue);
            ValidationException WeekDayNotValid = exception.ExceptionList.Where(x => x.ResourceKey == ExceptionResourceKeys.WeekDayNotValid).FirstOrDefault();
            if (ParameterValue != 0 && ParameterValue != 1 && WeekDayNotValid == null)
            {
              exception =  AddException(ExceptionResourceKeys.WeekDayNotValid, "مقدار پارامتر روزهای هفته باید عدد  0 یا 1 باشد ", exception);
            }
            return exception;
        }
        private UIValidationExceptions R26_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception, int ActiverulePrecardParamProxyList, IList<string> RuleParameterValueList)
        {
            ValidationException PrecardCountNotValid = exception.ExceptionList.Where(x => x.ResourceKey == ExceptionResourceKeys.PrecardCountNotValid).FirstOrDefault();
            ValidationException PrecardValueNotValid = exception.ExceptionList.Where(x => x.ResourceKey == ExceptionResourceKeys.PrecardValueNotValid).FirstOrDefault();

            if (ActiverulePrecardParamProxyList != 2 && PrecardCountNotValid == null)
           {
               exception = AddException(ExceptionResourceKeys.PrecardCountNotValid, "تعداد پیش کارتهای انتخاب شده باید دو عدد باشد ", exception);
           
           }
            if (RuleParameterValueList.Count > 1)
            {
                if (RuleParameterValueList[0] != RuleParameterValueList[1] && PrecardValueNotValid == null)
                {
                    exception = AddException(ExceptionResourceKeys.PrecardValueNotValid, "مقادیر پارامترهای دو پیشکارت باید مساوی باشند ", exception);
                }
            }
            return exception;
        }
        private UIValidationExceptions R200_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception)
        {
            switch (keyName)
            {
                case "Sticking":
                    int StickingParam = Utility.ToInteger(parameterValue);
                    if (!(StickingParam==0 || StickingParam == 1))
                    {
                        exception = AddException(ExceptionResourceKeys.LockCalculationtMonthNotValid, " مقدار پارامتر اتصال باید 0 و یا 1 باشد", exception);
                    }
                    break;
        
            }
            return exception;
        }
        private UIValidationExceptions R201_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception)
        {
            switch (keyName)
            {
                case "Sticking":
                    int StickingParam = Utility.ToInteger(parameterValue);
                    if (!(StickingParam == 0 || StickingParam == 1))
                    {
                        exception = AddException(ExceptionResourceKeys.LockCalculationtMonthNotValid, " مقدار پارامتر اتصال باید 0 و یا 1 باشد", exception);
                    }
                    break;

            }
            return exception;
        }
        private UIValidationExceptions R36_ValidateRuleParametersValue(string keyName, string parameterValue, UIValidationExceptions exception)
        {
            switch (keyName)
            {
                case "Discontinuous":
                    int Discontinuous = 2;
                    if (parameterValue != string.Empty)
                        Discontinuous = Utility.ToInteger(parameterValue);
                    if (Discontinuous != 0 && Discontinuous != 1)
                    {
                        exception = AddException(ExceptionResourceKeys.Discontinuous, "  مقدار پارامتر (پیوسته باشد) باید عدد 0  یا 1  باشد", exception);
                    }
                    break;
                case "DayCount":
                    int DayCount = Utility.ToInteger(parameterValue);

                    if (DayCount < 1 )
                    {
                        exception = AddException(ExceptionResourceKeys.DayCount, "مقدارپارامتر (تعداد روز) باید  عددی بزرگتر از 0 باشد", exception);
                    }
                    break;
                case "GivingBirthLeaveRespite":
                    int GivingBirthLeaveRespite = Utility.ToInteger(parameterValue);

                    if (GivingBirthLeaveRespite < 1)
                    {
                        exception = AddException(ExceptionResourceKeys.GivingBirthLeaveRespite, "مقدارپارامتر (مهلت استفاده از مرخصی بر حسب ماه) باید  عددی بزرگتر از 0 باشد", exception);
                    }
                    break;
            }
            return exception;
        }
        private UIValidationExceptions AddException(ExceptionResourceKeys key, string msg , UIValidationExceptions exception)
        {
          ValidationException  validationException = new ValidationException(key, msg, ExceptionSrc);
            exception.Add(validationException);
            return exception;
        }       
    }
}
