using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.Concept;
using Newtonsoft.Json;
using GTS.Clock.Business.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using System.Web.Configuration;
using GTS.Clock.Model;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.Proxy;
using System.Reflection;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure;
using System.Web.Script.Serialization;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class RuleGenerator : GTSBasePage
    {


        private enum scripts
        {
            DialogRuleGenerator_Operation,
            RuleGenerator_OnPageLoad,
            //DialogDeclareVariable_Operation,
            //DeclareVariable_onPageLoad,
            //DialogDeclareVariable_onPageLoad,
            HelpForm_Operations,
        }
        public BDesignedRules DesignedRuleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDesignedRules>();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            GetDayOfWeek();
            Page RuleGeneratorPage = this;
            Ajax.Utility.GenerateMethodScripts(RuleGeneratorPage);
            ScriptHelper.InitializeScripts(this.Page, typeof(scripts));
            SkinHelper.InitializeSkin(this.Page);
            FillHF(sender, e);
            GetLocalResources();
        }
        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }
        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }
        private void SetCurrentCultureResObjs(string LangID)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }
        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }
        public IList<String> GetDayOfWeek()
        {
            IList<string> daysOfWeekList = new List<string>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                string dayName = string.Empty;
                switch (BLanguage.CurrentLocalLanguage)
                {
                    case GTS.Clock.Infrastructure.LanguagesName.Unknown:
                        dayName = GTS.Clock.Infrastructure.Utility.PersianDateTime.GetPershianDayName(day);
                        break;
                    case GTS.Clock.Infrastructure.LanguagesName.Parsi:
                        dayName = GTS.Clock.Infrastructure.Utility.PersianDateTime.GetPershianDayName(day);
                        break;
                    case GTS.Clock.Infrastructure.LanguagesName.English:
                        dayName = GTS.Clock.Infrastructure.Utility.PersianDateTime.GetEnglishDayName(day);
                        break;
                    default:
                        break;
                }
                daysOfWeekList.Add(dayName);
            }
            return daysOfWeekList;
        }
        public void FillHF(object sender, EventArgs e)
        {
            BConceptProxy BConceptProxyObj = new BConceptProxy();
            BRuleGeneratorSetting BRuleGeneratorObj = new BRuleGeneratorSetting();
            string BConceptList = JsonConvert.SerializeObject(BConceptProxyObj.RuleGeneratorConcept());
            string BRuleGeneratorSettingList = JsonConvert.SerializeObject(BRuleGeneratorObj.RuleGeneratorSetting());
            this.hfConcept_Concept.Value = BConceptList;
            this.hfRGSetting_Setting.Value = BRuleGeneratorSettingList;
        }


        public List<RuleGeneratorRuleParameterProxy> GetListOfParameterObject(string ParameterObject)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            List<RuleGeneratorRuleParameterProxy> paramss = (List<RuleGeneratorRuleParameterProxy>)JsSerializer.Deserialize<List<RuleGeneratorRuleParameterProxy>>(ParameterObject);
            return paramss;
        }
        [Ajax.AjaxMethod("UpdateRuleGenerator_RuleGeneratorPage", "UpdateRuleGenerator_RuleGeneratorPage_OnCallBack", null, null)]
        public string[] UpdateRuleGenerator_RuleGeneratorPage(
            string state,
            string ID,
            string VariablesObject,
            string ParameterObject,
            string RuleStateObject,
            string CSharpCode,
            //string RuleObject,
            string PersionScript,
            string RuleTemplateID,
            string DesignedRuleID)
        {
            this.InitializeCulture();
            string[] retMessage = new string[4];
            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal RuleGeneratorID = 0;
                //decimal iID = Convert.ToDecimal(this.StringBuilder.CreateString(ID));
                decimal designedRuleID = Convert.ToDecimal(DesignedRuleID);
                VariablesObject = this.StringBuilder.CreateString(VariablesObject, StringGeneratorExceptionType.ConceptRuleManagement);
                ParameterObject = this.StringBuilder.CreateString(ParameterObject, StringGeneratorExceptionType.ConceptRuleManagement);
                RuleStateObject = this.StringBuilder.CreateString(RuleStateObject, StringGeneratorExceptionType.ConceptRuleManagement);
                CSharpCode = this.StringBuilder.CreateString(CSharpCode, StringGeneratorExceptionType.ConceptRuleManagement);
                PersionScript = this.StringBuilder.CreateString(PersionScript, StringGeneratorExceptionType.ConceptRuleManagement);
                BDesignedRules bDesignedRule = new BDesignedRules();
                bool CheckPageState = bDesignedRule.CheckToExistRuleCSharpCode(Convert.ToDecimal(RuleTemplateID));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
                if (CheckPageState)
                {
                    uam = (UIActionType)Enum.Parse(typeof(UIActionType), "EDIT");
                }
                else
                {
                    uam = (UIActionType)Enum.Parse(typeof(UIActionType), "ADD");
                }
                DesignedRule designedrule = new DesignedRule();

                List<RuleGeneratorRuleParameterProxy> ParameterList = GetListOfParameterObject(ParameterObject);
                if (uam != UIActionType.DELETE)
                {
                    designedrule.VariablesObject = VariablesObject;
                    designedrule.ParameterObject = ParameterObject;
                    designedrule.RuleStateObject = RuleStateObject;
                    designedrule.CSharpCode = CSharpCode;
                    designedrule.PersionScript = PersionScript;
                    List<RuleTemplateParameter> ruleTemplateParameterList = new List<RuleTemplateParameter>();
                    designedrule.RuleTemplate = new RuleTemplate { ID = Convert.ToInt32(RuleTemplateID) };
                    foreach (RuleGeneratorRuleParameterProxy item in ParameterList)
                    {
                        RuleTemplateParameter ruleTemplateParameter = new RuleTemplateParameter();
                        ruleTemplateParameter.Name = item.Parametername;
                        ruleTemplateParameter.RuleTemplateId = Convert.ToInt32(RuleTemplateID);
                        ruleTemplateParameter.Title = item.Parametername;
                        switch (Convert.ToInt32(item.Parametertype))
                        {
                            case 0:
                                ruleTemplateParameter.Type = RuleParamType.Numeric;
                                break;
                            case 1:
                                ruleTemplateParameter.Type = RuleParamType.Time;
                                break;
                            case 2:
                                ruleTemplateParameter.Type = RuleParamType.Date;
                                break;
                        }
                        ruleTemplateParameterList.Add(ruleTemplateParameter);

                    }
                    designedrule.RuleTemplateParameterList = ruleTemplateParameterList;
                    designedrule.RuleRgisterDate = DateTime.Now;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        designedrule.RuleEstate = (int)UIActionType.ADD;
                        RuleGeneratorID = this.DesignedRuleBusiness.InsertDesignedRule(designedrule, uam);

                        break;
                    case UIActionType.EDIT:
                        designedrule.RuleEstate = (int)UIActionType.EDIT;
                        designedrule.ID = designedRuleID;
                        RuleGeneratorID = this.DesignedRuleBusiness.UpdateDesignedRule(designedrule, uam);
                        break;
                    case UIActionType.DELETE:
                        //if (selectedRuleGeneratorID == 0)
                        //{
                        //    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRuleforEdit").ToString()), retMessage);
                        //    return retMessage;
                        //}
                        RuleGeneratorID = this.DesignedRuleBusiness.InsertDesignedRule(designedrule, uam);
                        break;
                }
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uam)
                {
                    case UIActionType.ADD:
                        SuccessMessageBody = GetLocalResourceObject("AddComplete").ToString();
                        break;
                    case UIActionType.EDIT:
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        break;
                    case UIActionType.DELETE:
                        SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                        break;
                    default:
                        break;
                }
                retMessage[1] = SuccessMessageBody;
                retMessage[2] = "success";
                retMessage[3] = RuleGeneratorID.ToString();
                return retMessage;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                return retMessage;
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                return retMessage;
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                return retMessage;
            }
        }
        private void GetLocalResources()
        {
            List<string> Days = new List<string>();
            List<string> LettersList = new List<string>();
            string AddCompleteResualt = string.Empty;
            string OperationError = string.Empty;
            string ReplicatedName = string.Empty;
            RuleGeneratorResourceProxy ruleGeneratorResourceProxy = new RuleGeneratorResourceProxy();
            PropertyInfo[] propsInfo = typeof(RuleGeneratorResourceProxy).GetProperties();
            foreach (var item in propsInfo)
            {
                item.SetValue(ruleGeneratorResourceProxy, GetLocalResourceObject(item.Name));
            }
            foreach (SpecificDays day in Enum.GetValues(typeof(SpecificDays)))
            {
                Days.Add(GetLocalResourceObject(day.ToString()).ToString());
            }
            foreach (preposition Letters in Enum.GetValues(typeof(preposition)))
            {
                LettersList.Add(GetLocalResourceObject(Letters.ToString()).ToString());
            }
            AddCompleteResualt = GetLocalResourceObject("AddComplete").ToString();
            OperationError = GetLocalResourceObject("OperationError").ToString();
            ReplicatedName = GetLocalResourceObject("ReplicatedName").ToString();
            string RuleLocalResourcesList = JsonConvert.SerializeObject(ruleGeneratorResourceProxy);
            string DaysLocalResourcesList = JsonConvert.SerializeObject(Days);
            string LettersLocalResourceList = JsonConvert.SerializeObject(LettersList);
            this.hfResources_Resources.Value = RuleLocalResourcesList;
            this.hfDayResource_Resource.Value = DaysLocalResourcesList;
            this.hfpreposition_preposition.Value = LettersLocalResourceList;
            this.hfOperationSuccessResult.Value = AddCompleteResualt;
            this.hfOperationError.Value = OperationError;
            this.hfReplicatedName.Value = ReplicatedName;
        }
    }

}