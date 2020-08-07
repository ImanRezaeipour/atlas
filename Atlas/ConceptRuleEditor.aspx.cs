using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using System.Web;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ConceptRuleEditor : GTSBasePage
    {
        public BConceptExpression BConceptExpression
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BConceptExpression>();
            }
        }

        public enum LoadState
        {
            New,
            Edit
        }

        public enum ConceptEditorCaller
        {
            ConceptManagement,
            RuleManagement
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

        private enum Scripts
        {
            ConceptRuleEditor_onPageLoad,
            DialogConceptRuleEditor_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.GenerateMethodScripts(this);
            if (!CallBack_trvConceptExpression_ConceptRuleEditor.IsCallback && !CallBack_trvDetails_ConceptRuleEditor.IsCallback)
            {
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SkinHelper.InitializeSkin(this.Page);
                this.CheckConceptRuleEditorLoadAccess_ConceptRuleEditor();
            }
        }

        private void CheckConceptRuleEditorLoadAccess_ConceptRuleEditor()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Caller"))
            {
                ConceptEditorCaller CEC = (ConceptEditorCaller)Enum.Parse(typeof(ConceptEditorCaller), this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["Caller"]));
                switch (CEC)
                {
                    case ConceptEditorCaller.ConceptManagement:
                        this.BConceptExpression.CheckConceptRuleEditorLoadAccess_OnConcepts();
                        break;
                    case ConceptEditorCaller.RuleManagement:
                        this.BConceptExpression.CheckConceptRuleEditorLoadAccess_OnRules();
                        break;
                }
            }
        }

        #region trvDetails

        protected void CallBack_trvDetails_ConceptRuleEditor_onCallBack(object sender, CallBackEventArgs e)
        {
            var csSource = string.Empty;

            if (trvDetails_ConceptRuleEditor.Nodes != null &&
                trvDetails_ConceptRuleEditor.Nodes.Count > 0)
            {
                csSource = NodeToSource(trvDetails_ConceptRuleEditor.Nodes);
                ObjectJsonHiddenField_trDetails_Concept.Value = csSource;
            }
        }
        private string NodeToSource(TreeViewNodeCollection treeViewNodeCollection)
        {
            var startSourceCode = string.Empty;
            var ChildsSourceCode = string.Empty;
            var finishSourceCode = string.Empty;

            foreach (TreeViewNode treeViewNode in treeViewNodeCollection)
            {
                var curCncptExprsn =
                BConceptExpression.GetByID(Convert.ToDecimal(treeViewNode.Value));

                startSourceCode = curCncptExprsn.ScriptBeginEn;
                if (treeViewNode.Nodes != null &&
                    treeViewNode.Nodes.Count > 1)
                    ChildsSourceCode = NodeToSource(treeViewNode.Nodes);
                finishSourceCode = curCncptExprsn.ScriptEndEn;

            }

            return string.Format("{0}{2}{3}", startSourceCode, ChildsSourceCode, finishSourceCode);
        }
        protected void Fill_trvDetails_ConceptRuleEditor()
        {
            trvDetails_ConceptRuleEditor.NodeEditingEnabled = true;
            trvDetails_ConceptRuleEditor.DragAndDropEnabled = true;
            trvDetails_ConceptRuleEditor.DropChildEnabled = true;
            trvDetails_ConceptRuleEditor.DropSiblingEnabled = true;

            var childCncptExprsnNode =
                new
                TreeViewNode
                    {
                        ID = "C1",
                        Value =
                         NodeValueExpressions.Serialize(
                         new NodeValueExpressions()),
                        Text = "قانون تستی",
                        ImageUrl = Session["ModulePathU"] + "Images/TreeView/folder.gif",
                        EditingEnabled = false
                    };

            hfConceptOrRuleIdentification.Value = childCncptExprsnNode.Value;
            hfPageIsLoadedBefore.Value = "true";

            this.trvDetails_ConceptRuleEditor.Nodes.Add(childCncptExprsnNode);
        }

        #endregion


        #region trvConceptExpression
        protected void CallBack_trvConceptExpression_ConceptRuleEditor_onCallBack(object sender, CallBackEventArgs e)
        {
            Fill_trvConceptExpression_ConceptRuleEditor();
            trvConceptExpression_ConceptRuleEditor.RenderControl(e.Output);
            ErrorHiddenField_trvConceptExpression_ConceptRuleEditor.RenderControl(e.Output);
            SetVariableItemCodeInExpression();
        }

        private void Fill_trvConceptExpression_ConceptRuleEditor()
        {
            #region image and initialization
            string[] retMessage = new string[4];
            this.InitializeCulture();
            #endregion

            try
            {
                var rootExprsnCncpt = BConceptExpression.GetRoot();
                if (rootExprsnCncpt == null) return;

                #region Create root node and details

                var NodeValueExpressions_Object = new NodeValueExpressions();

                #endregion

                List<ConceptExpression> organizationUnitChlidList = this.BConceptExpression.GetByParentId(rootExprsnCncpt.ID);

                NodeValueExpressions_Object.MakeJsonObjectListString(organizationUnitChlidList, this.trvConceptExpression_ConceptRuleEditor, this.LangProv.GetCurrentLanguage(),false);

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_trvConceptExpression_ConceptRuleEditor.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_trvConceptExpression_ConceptRuleEditor.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_trvConceptExpression_ConceptRuleEditor.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void SetVariableItemCodeInExpression()
        {
            if (BConceptExpression.GetByParentId(null).FirstOrDefault() == null) return;
            VariableItemCodeInExpression.Value =
                BConceptExpression.GetByParentId(null).FirstOrDefault().ID.ToString();
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_ConceptsPage", "GetLoadonDemandError_ConceptsPage_onCallBack", null, null)]
        public string GetLoadonDemandError_PostsPage()
        {
            this.InitializeCulture();
            string retError = string.Empty;
            if (Session["LoadonDemandError_ConceptsPage"] != null)
            {
                retError = Session["LoadonDemandError_ConceptsPage"].ToString();
                Session["LoadonDemandError_ConceptsPage"] = null;
            }
            else
            {
                if (GetLocalResourceObject("RetErrorType") != null &&
                    GetLocalResourceObject("ParentNodeFillProblem") != null)
                {
                    var retMessage = new[]
                {
                    GetLocalResourceObject("RetErrorType").ToString(),
                    GetLocalResourceObject("ParentNodeFillProblem").ToString(),
                    "error"
                };
                    retError = this.exceptionHandler.CreateErrorMessage(retMessage);
                }
            }
            return retError;
        }

        #endregion


        [Ajax.AjaxMethod("GetChildrenOnCreation_ConceptEditorPage", "GetChildrenOnCreation_ConceptEditorPage_onCallBack", null, null)]
        public string[] GetChildrenOnCreation_ConceptEditorPage(string parentDbId, string parentId)
        {
            StringBuilder.CreateString(parentDbId);

            string[] retMessage = new string[5];
            try
            {
                UIActionType uam = UIActionType.ADD;

                var NodeValueExpressions = new NodeValueExpressions();

                var organizationUnitChlidList = this.BConceptExpression.GetChildrenOnCreation(Convert.ToDecimal(StringBuilder.CreateString(parentDbId)));
                
                var strJSON = NodeValueExpressions.MakeJsonObjectListString(organizationUnitChlidList);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();

                retMessage[1] = "";
                retMessage[2] = "success";
                retMessage[3] = StringBuilder.CreateString(parentId); ;
                retMessage[4] = strJSON;

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