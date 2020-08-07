using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Rules;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Model;
using GTS.Clock.Business.Compiler;
using Newtonsoft.Json;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class RulesManagement : GTSBasePage
    {
        #region Initialize

        public BSecondaryConceptUserDefined BSecondaryConceptUserDefined
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BSecondaryConceptUserDefined>();
            }

        }

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        public BRuleTemp RuleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BRuleTemp>();
            }
        }

        public BRuleParameterTemp RuleParameterTemp
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BRuleParameterTemp>();
            }
        }

        public BRuleType BRuleType
        {
            get
            {
                return new BRuleType();
            }
        }


        #endregion

        private enum Scripts
        {
            RulesManagement_onPageLoad,
            DialogRulesManagement_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        public enum LoadState
        {
            Normal,
            Search,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!CallBack_cmbRuleParameterType_Rules.IsCallback && !CallBack_cmbRuleType_Rules.IsCallback && !CallBack_GridRuleParameters_Rules.IsCallback && !CallBack_GridRules_Rules.IsCallback)
                if (!CallBack_cmbRuleScope_Rules.IsCallback && !CallBack_cmbRuleType_Rules.IsCallback && !CallBack_GridRules_Rules.IsCallback)
            {
                Ajax.Utility.GenerateMethodScripts(this);
                this.SetRulesPageSize_Rules();
                this.SetRulesPageCount_Rules(LoadState.Normal, string.Empty);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SkinHelper.InitializeSkin(this.Page);
                SetJsonEnumIntoHiddenField();
                CheckRuleManagementLoadAccess_RuleManagement();
                FillRuleScopeHiddenField_RulesManagement();
            }
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        private void SetRulesPageSize_Rules()
        {
            this.hfRulesPageSize_Rules.Value =
                this.GridRules_Rules.PageSize.ToString(CultureInfo.InvariantCulture);
        }

        private void CheckRuleManagementLoadAccess_RuleManagement()
        {
            string[] retMessage = new string[4];
            try
            {
                this.BSecondaryConceptUserDefined.CheckRuleManagementLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void SetJsonEnumIntoHiddenField()
        {
            if (string.IsNullOrEmpty(hfJsonRuleParameterTypeEnum.Value))
                hfJsonRuleParameterTypeEnum.Value = ConvertEnumToJavascript(typeof(RuleParamType));
        }

        private void SetRulesPageCount_Rules(LoadState Ls, string SearchTerm)
        {
            var RulesCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    RulesCount = this.RuleBusiness.GetAllByPageBySearchCount(string.Empty);
                    break;
                case LoadState.Search:
                    RulesCount = this.RuleBusiness.GetAllByPageBySearchCount(SearchTerm);
                    break;
            }
            this.hfRulesCount_Rules.Value = RulesCount.ToString();
            this.hfRulesPageCount_Rules.Value =
                Utility.GetPageCount(RulesCount, this.GridRules_Rules.PageSize).ToString();
        }

        protected void CallBack_GridRules_Rules_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            this.SetRulesPageCount_Rules(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.Fill_GridRules_Rules(
                (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[1])),
                int.Parse(this.StringBuilder.CreateString(e.Parameters[2])),
                this.StringBuilder.CreateString(e.Parameters[4]));

            this.GridRules_Rules.RenderControl(e.Output);
            this.hfRulesCount_Rules.RenderControl(e.Output);
            this.hfRulesPageCount_Rules.RenderControl(e.Output);
            this.ErrorHiddenField_Rules.RenderControl(e.Output);
        }

        private void Fill_GridRules_Rules(LoadState Ls, int pageSize, int pageIndex, string searchTerm)
        {
            var retMessage = new string[4];
            IList<RuleTemplateProxy> RulesProxyList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        RulesProxyList = RuleBusiness.GetAllByPageBySearch(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        RulesProxyList = RuleBusiness.GetAllByPageBySearch(pageIndex, pageSize, searchTerm);
                        break;
                }
                
                foreach (var ruleTemplateProxy in RulesProxyList)
                {
                    ruleTemplateProxy.Type = GetLocalResourceObject(ruleTemplateProxy.Type).ToString();
                if (ruleTemplateProxy.OperationalArea == 0)
                {
                    ruleTemplateProxy.OperationalAreaName = GetLocalResourceObject("Daily").ToString();
                }
                else if (ruleTemplateProxy.OperationalArea == 2)
                {
                    ruleTemplateProxy.OperationalAreaName = GetLocalResourceObject("Monthly").ToString();
                }
                else
                {
                    ruleTemplateProxy.OperationalAreaName = "";
                }
                }
                
                this.GridRules_Rules.DataSource = RulesProxyList;
                this.GridRules_Rules.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Rules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Rules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Rules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Rules.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        public Decimal RuleIdentifierCode(Decimal RuleTypeId)
        {
            Decimal IdentifierCode = this.RuleBusiness.GetLastCodeByRuleTypeID(RuleTypeId);
            return IdentifierCode;
        }

         
        /// <summary>
        /// This method is used to generate javascript equivalents of C# enumerations.
        /// This makes it possible to use the C# enum without having to code and maintain
        /// an equivalent javascript class
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string ConvertEnumToJavascript(Type t)
        {
            if (!t.IsEnum) throw new Exception("Type must be an enumeration");

            var values = System.Enum.GetValues(t);
            var dict = new Dictionary<int, string>();

            foreach (object obj in values)
            {
                try
                {
                    var resorce = GetLocalResourceObject(obj.ToString()).ToString();
                    dict.Add(Convert.ToInt32(System.Enum.Format(t, obj, "D")), resorce);
                }
                catch (Exception)
                {
                    string name = System.Enum.GetName(t, obj);
                    dict.Add(Convert.ToInt32(System.Enum.Format(t, obj, "D")), name);
                }
            }

            var sss = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

            return sss;
        }

        [Ajax.AjaxMethod("UpdateRule_RulesPage", "UpdateRule_RulesPage_onCallBack", null, null)]
        public string[] UpdateRule_RulesPage(
                  string ID,
                  string IdentifierCode,
                  string Name,
                  string CustomCategoryCode,
                  string TypeId,
                  string UserDefined,
                  string RuleOrder,
                  string RuleOperationalAreaId,
                  //string Script,
                  //string CSharpCode,
                  //string DesinegRuleID,
                  //string JsonObject,
                  //string ParameterObject,
                  //string VariableObject,
                  //string RuleObject,
                  //string RuleStateobject,
                  //string RulePriority,
                  //string RuleTemplateID,
                  string PageState)
        {

            this.InitializeCulture();

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
            string[] retMessage = new string[4];

            decimal iID = 0;
            RuleTemplate RuleRecived = new RuleTemplate();
            //DesignedRule DesignedRuleObj = new DesignedRule();
            //RuleRecived.OperationalArea = 0;
            RuleRecived.ID = Convert.ToDecimal(StringBuilder.CreateString(ID));
            //DesignedRuleObj.ID = Convert.ToDecimal(StringBuilder.CreateString(DesinegRuleID));
            PageState = StringBuilder.CreateString(PageState);
            if (PageState != "Delete")
            {
                //Decimal RuleId = Convert.ToDecimal(StringBuilder.CreateString(IdentifierCode));
                //Decimal RuleIdentifierCode = this.RuleIdentifierCode(RuleId);
                RuleRecived.Name = StringBuilder.CreateString(Name);
                RuleRecived.CustomCategoryCode = StringBuilder.CreateString(CustomCategoryCode);
                RuleRecived.TypeId = Convert.ToDecimal(StringBuilder.CreateString(TypeId));
                Decimal TypeID = Convert.ToDecimal(StringBuilder.CreateString(TypeId));
                RuleRecived.IdentifierCode = this.RuleIdentifierCode(TypeID);

                RuleRecived.UserDefined = bool.Parse(StringBuilder.CreateString(UserDefined));
                RuleRecived.Order = Convert.ToInt32(StringBuilder.CreateString(RuleOrder));
                RuleRecived.OperationalArea = Convert.ToDecimal(StringBuilder.CreateString(RuleOperationalAreaId));

            }
            
            #region Effect on DB

            try
            {
                #region Set UIActionType Enum
                UIActionType uiActionType = UIActionType.ADD;
                switch (PageState.ToUpper())
                {
                    #region Add
                    case "ADD":
                        uiActionType = UIActionType.ADD;
                        iID = RuleBusiness.InsertRule(RuleRecived);
                        break;
                    #endregion
                    #region Edit
                    case "EDIT":
                        uiActionType = UIActionType.EDIT;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }

                        var concept = RuleBusiness.GetByID(RuleRecived.ID);
                        RuleBusiness.Copy(RuleRecived, ref concept);

                        iID = RuleBusiness.UpdateRule(concept);
                        break;
                    #endregion
                    #region Delete
                    case "DELETE":
                        uiActionType = UIActionType.DELETE;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        iID = RuleBusiness.DeleteRule(RuleRecived);
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uiActionType)
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
                retMessage[3] = iID.ToString();
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
            #endregion

        }

        protected void CallBack_cmbRuleType_Rules_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbRuleType_Rules.Dispose();
            Fill_cmbRuleType_Rules();
            cmbRuleType_Rules.Enabled = true;
            ErrorHiddenField_TypeFields.RenderControl(e.Output);
            cmbRuleType_Rules.RenderControl(e.Output);
        }
        public void FillRuleScopeHiddenField_RulesManagement()
        {
            foreach (RuleGeneratorRuleScope ruleScope in Enum.GetValues(typeof(RuleGeneratorRuleScope)))
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem(GetLocalResourceObject(ruleScope.ToString()).ToString());
                cbItem.Value = ((int)ruleScope).ToString();
                cmbRuleScope_Rules.Items.Add(cbItem);
            }            
        }
        protected void CallBack_cmbRuleScope_Rules_onCallBack(object sender, CallBackEventArgs e)
        {
            cmbRuleScope_Rules.Dispose();
            //Fill_cmbRuleScope_Rules();
            FillRuleScopeHiddenField_RulesManagement();
            cmbRuleScope_Rules.Enabled = true;
            ErrorHiddenField_ScopeFields.RenderControl(e.Output);
            cmbRuleScope_Rules.RenderControl(e.Output);
        }

        private void Fill_cmbRuleType_Rules()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var bRuleType in BRuleType.GetAll())
                {
                    var newComboBoxConcept = new ComboBoxItem((GetLocalResourceObject(bRuleType.Name)).ToString())
                        {
                            Value = (bRuleType.ID).ToString()
                        };
                    cmbRuleType_Rules.Items.Add(newComboBoxConcept);
                }
                cmbRuleType_Rules.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void Fill_cmbRuleScope_Rules()
        {
            var retMessage = new string[4];
            try
            {

                foreach (RuleGeneratorRuleScope ruleScope in Enum.GetValues(typeof(RuleGeneratorRuleScope)))
                {
                    ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                    cbItem.Text = GetLocalResourceObject(ruleScope.ToString()).ToString();
                    cbItem.Value = ((int)ruleScope).ToString();
                    cmbRuleScope_Rules.Items.Add(cbItem);
                }
                 cmbRuleScope_Rules.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_GridRuleParameters_Rules_OnCallBack(object sender, CallBackEventArgs e)
        {
            decimal selectedRuleId = Convert.ToDecimal(StringBuilder.CreateString(e.Parameters[0]));
            Fill_GridRuleParameters_Rules(selectedRuleId);
            //this.GridRuleParameters_Rules.RenderControl(e.Output);
            //this.ErrorHiddenFieldRuleParameter_Rules.RenderControl(e.Output);
        }

        private void Fill_GridRuleParameters_Rules(decimal selectedRuleId)
        {
            var rulTempParams = RuleParameterTemp.GeRuleTempParams(selectedRuleId);
            //this.GridRuleParameters_Rules.DataSource = rulTempParams;
            //this.GridRuleParameters_Rules.DataBind();
        }

        protected void CallBack_cmbRuleParameterType_Rules_onCallBack(object sender, CallBackEventArgs e)
        {
            //cmbRuleParameterType_Rules.Dispose();
            //Fill_cmbRuleParameterType_Rules();
            //cmbRuleParameterType_Rules.Enabled = true;
            //ErrorHiddenField_RuleParameterTypeFields.RenderControl(e.Output);
            //cmbRuleParameterType_Rules.RenderControl(e.Output);

        }

        private void Fill_cmbRuleParameterType_Rules()
        {
            var retMessage = new string[4];
            try
            {
                foreach (var enumValue in Enum.GetValues(typeof(RuleParamType)))
                {
                    var comboBoxItem = new ComboBoxItem(GetLocalResourceObject(enumValue.ToString()).ToString());
                    comboBoxItem.Value = ((int)enumValue).ToString();
                    //cmbRuleParameterType_Rules.Items.Add(comboBoxItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_TypeFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateRuleParameter_RulesPage", "UpdateRuleParameter_RulesPage_onCallBack", null, null)]
        public string[] UpdateRuleParameter_RulesPage(
                  string ID,
                  string RuleID,
                  string Title,
                  string Name,
                  string TypeId,
                  string PageState)
        {

            this.InitializeCulture();
            string[] retMessage = new string[4];

            try
            {

                decimal RuleParameterId = 0;

                decimal SelectedRuleParameterId = decimal.Parse(this.StringBuilder.CreateString(ID), CultureInfo.InvariantCulture);
                decimal SelectedRuleId = decimal.Parse(this.StringBuilder.CreateString(RuleID), CultureInfo.InvariantCulture);
                decimal SelectedRuleParameterTypeId = decimal.Parse(this.StringBuilder.CreateString(TypeId), CultureInfo.InvariantCulture);

                Title = this.StringBuilder.CreateString(Title);
                Name = this.StringBuilder.CreateString(Name);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(PageState).ToUpper());

                RuleTemplateParameter RuleParameter = new RuleTemplateParameter();
                RuleParameter.ID = SelectedRuleParameterId;
                RuleParameter.RuleTemplateId = SelectedRuleId;
                RuleParameter.Type = (RuleParamType)SelectedRuleParameterTypeId;

                if (PageState != "Delete")
                {
                    RuleParameter.Title = Title;
                    RuleParameter.Name = Name;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        RuleParameterId = this.RuleParameterTemp.InsertRuleTempParam(RuleParameter);
                        break;
                    case UIActionType.EDIT:
                        if (SelectedRuleParameterId == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoShiftPairSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        RuleParameterId = this.RuleParameterTemp.UpdateRuleTempParam(RuleParameter);
                        break;
                    case UIActionType.DELETE:
                        if (SelectedRuleParameterId == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoShiftPairSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        RuleParameterId = this.RuleParameterTemp.DeleteRuleTempParam(RuleParameter);
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
                retMessage[3] = RuleParameterId.ToString(CultureInfo.InvariantCulture);
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

        protected void CallBackSaveRules_Rules_OnCallBack(object sender, CallBackEventArgs e)
        {
            string ID = e.Parameters[0];
            string IdentifierCode = e.Parameters[1];
            string Name = e.Parameters[2];
            string CustomCategoryCode = e.Parameters[3];
            string TypeId = e.Parameters[4];
            string UserDefined = e.Parameters[5];
            string Script = e.Parameters[6];
            string CSharpCode = e.Parameters[7];
            string JsonObject = e.Parameters[8];
            string PageState = e.Parameters[9];

            this.InitializeCulture();

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
            string[] retMessage = new string[4];

            decimal iID = 0;
            RuleTemplate RuleRecived = new RuleTemplate();
            RuleRecived.ID = Convert.ToDecimal(StringBuilder.CreateString(ID));

            PageState = StringBuilder.CreateString(PageState);
            if (PageState != "Delete")
            {
                RuleRecived.IdentifierCode = Convert.ToDecimal(StringBuilder.CreateString(IdentifierCode));
                RuleRecived.Name = StringBuilder.CreateString(Name);
                RuleRecived.CustomCategoryCode = StringBuilder.CreateString(CustomCategoryCode);
                RuleRecived.TypeId = Convert.ToDecimal(StringBuilder.CreateString(TypeId));
                RuleRecived.UserDefined = bool.Parse(StringBuilder.CreateString(UserDefined));
                RuleRecived.Script = StringBuilder.CreateString(Script);
                RuleRecived.CSharpCode = StringBuilder.CreateString(CSharpCode);
                RuleRecived.JsonObject = JsonObject;
            }

            #region Effect on DB

            try
            {
                #region Set UIActionType Enum
                UIActionType uiActionType = UIActionType.ADD;
                switch (PageState.ToUpper())
                {
                    #region Add
                    case "ADD":
                        uiActionType = UIActionType.ADD;
                        iID = RuleBusiness.InsertRule(RuleRecived);
                        break;
                    #endregion
                    #region Edit
                    case "EDIT":
                        uiActionType = UIActionType.EDIT;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforEdit").ToString()), retMessage);
                            hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                            this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
                        }

                        var concept = RuleBusiness.GetByID(RuleRecived.ID);
                        RuleBusiness.Copy(RuleRecived, ref concept);

                        iID = RuleBusiness.UpdateRule(concept);
                        break;
                    #endregion
                    #region Delete
                    case "DELETE":
                        uiActionType = UIActionType.DELETE;
                        if (RuleRecived.ID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(GetLocalResourceObject("NoRuleSelectedforDelete").ToString()), retMessage);
                            hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                            this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
                        }
                        iID = RuleBusiness.DeleteRule(RuleRecived);
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                switch (uiActionType)
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
                retMessage[3] = iID.ToString(CultureInfo.InvariantCulture);

                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                hfCallBackDataSaveRules_Rules.Value = Newtonsoft.Json.JsonConvert.SerializeObject(retMessage);
                this.hfCallBackDataSaveRules_Rules.RenderControl(e.Output);
            }
            #endregion

        }

        [Ajax.AjaxMethod("CompileAndMakeDll_Rules", "CompileAndMakeDll_Rules_onCallBack", null, null)]
        public string[] CompileAndMakeDll_Rules()
        {
            this.InitializeCulture();
            var retMessage = new string[3];

            try
            {
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();

                var concatMessages = BusinessHelper.GetBusinessInstance<CsCompiler>().GenerateAndCompile();

                if (!string.IsNullOrEmpty(concatMessages))
                {
                    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, new Exception(concatMessages), retMessage);
                    return retMessage;
                }
                retMessage[1] = GetLocalResourceObject("DllCompileAndConstructOperationCompleted").ToString();
                retMessage[2] = "success";

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

    }
}