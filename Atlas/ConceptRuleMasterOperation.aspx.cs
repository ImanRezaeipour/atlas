using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using GTS.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Compiler;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure.Exceptions.UI;
using Microsoft.CSharp;
using GTS.Clock.Business.RuleDesigner;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ConceptRuleMasterOperation : GTSBasePage
    {

        public BConceptExpression ConceptExpressionsBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BConceptExpression>();
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

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }

        enum Scripts
        {
            ConceptRuleMasterOperation_onPageLoad,
            tbConceptRuleMasterOperation_TabStripMenu_Operation,
            Alert_Box,
            HelpForm_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.GenerateMethodScripts(this);
            RefererValidationProvider.CheckReferer();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckConceptRuleMasterOperationLoadAccess_ConceptRuleMasterOperation();
        }

        private void CheckConceptRuleMasterOperationLoadAccess_ConceptRuleMasterOperation()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ConceptExpressionsBusiness.CheckConceptRuleMasterOperationLoadAccess();
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }


        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }
        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        [Ajax.AjaxMethod("CompileAndMakeDll_ConceptRuleMasterOperation", "CompileAndMakeDll_ConceptRuleMasterOperation_onCallBack", null, null)]
        public string[] CompileAndMakeDll_ConceptRuleMasterOperation()
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
