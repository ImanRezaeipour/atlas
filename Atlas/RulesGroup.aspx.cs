using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using ComponentArt.Web.UI;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;

public partial class RulesGroup : GTSBasePage
{
    internal class TargetDetails
    {
        public string TargetType { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public bool IsRoot { get; set; }
        public string CustomCode { get; set; }
        public bool IsGroup { get; set; }
        public decimal ParentId { get; set; }
    }

    public enum TargetType
    {
        RuleGroup,
        Rule
    }

    public BRuleCategory RulesGroupBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BRuleCategory>();
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

    internal class ObjRuleGroup
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    enum Scripts
    {
        RulesGroup_onPageLoad,
        tbRulesGroup_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        Page RulesGroupPage = this;
        Ajax.Utility.GenerateMethodScripts(RulesGroupPage);
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        this.CheckRulesGroupLoadAccess_RulesGroup();
    }

    private void CheckRulesGroupLoadAccess_RulesGroup()
    {
        string[] retMessage = new string[4];
        try
        {
            this.RulesGroupBusiness.CheckRulesGroupLoadAccess();
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

    /// <summary>
    /// CallBack درخت گروه قوانین
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CallBack_trvRulesGroup_RulesGroup_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_trvRulesGroup_RulesGroup((LoadSttate)Enum.Parse(typeof(LoadSttate), StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]));
        this.trvRulesGroup_RulesGroup.RenderControl(e.Output);
        this.ErrorHiddenField_RulesGroup.RenderControl(e.Output);
    }

    /// <summary>
    ///پر کردن درخت گروه های قوانین
    /// </summary>
    private void Fill_trvRulesGroup_RulesGroup(LoadSttate Ls, string SearchItem)
    {
        string imageUrl = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif";
        string imagePath = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";

        string groupImageUrl = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\group.png";
        string groupImagePath = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/group.png";

        string[] retMessage = new string[4];
        this.InitializeCulture();

        try
        {
            IList<RuleCategory> rulesGroupList=this.RulesGroupBusiness.GetAll();
            RuleCategory rootRuleGroup = this.RulesGroupBusiness.GetRoot();
            TreeViewNode rootRuleGroupNode = new TreeViewNode();
            rootRuleGroupNode.ID = rootRuleGroup.ID.ToString();
            string rootRuleGroupNodeText = string.Empty;
            if (GetLocalResourceObject("RulesGroupsNode_trvRulesGroup_RulesGroup") != null)
                rootRuleGroupNodeText = GetLocalResourceObject("RulesGroupsNode_trvRulesGroup_RulesGroup").ToString();
            else
                rootRuleGroupNodeText = rootRuleGroup.Name;
            rootRuleGroupNode.Text = rootRuleGroupNodeText;
            rootRuleGroupNode.Value = rootRuleGroup.Discription;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + groupImageUrl))
                rootRuleGroupNode.ImageUrl = groupImagePath;
            this.trvRulesGroup_RulesGroup.Nodes.Add(rootRuleGroupNode);


            //foreach (RuleCategory childRuleCategory in rootRuleGroup.ChildList)
            //{
            //    TreeViewNode childRuleCategoryNode = new TreeViewNode();
            //    childRuleCategoryNode.ID = childRuleCategory.ID.ToString();
            //    childRuleCategoryNode.Text = childRuleCategory.Name;
            //    childRuleCategoryNode.Value = childRuleCategory.Discription;
            //    if ( childRuleCategory.IsGroup==false && File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
            //        childRuleCategoryNode.ImageUrl = imagePath;
            //    if (childRuleCategory.IsGroup == true && File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
            //        childRuleCategoryNode.ImageUrl = groupImagePath;
            //    rootRuleGroupNode.Nodes.Add(childRuleCategoryNode);
            //}


            switch (Ls)
            {
                case LoadSttate.Normal:
                    // برگرداندن فرزندان بدون جستجو
                    this.GetChildItem_trvRulesGroup_RulesGroup(rootRuleGroupNode, rootRuleGroup, rulesGroupList);
                    break;
                case LoadSttate.Search:
                    // برگرداندن فرزندان با جستجو
                    if (!string.IsNullOrEmpty(SearchItem))
                        this.GetChildItem_trvRulesGroup_RulesGroup(rootRuleGroupNode, rootRuleGroup, rulesGroupList.Where(c => c.Name.Contains(SearchItem) || c.IsGroup == true).ToList());
                    else
                        this.GetChildItem_trvRulesGroup_RulesGroup(rootRuleGroupNode, rootRuleGroup, rulesGroupList);
                    break;
            }


            if (rootRuleGroup.ChildList.Count > 0)
                rootRuleGroupNode.Expanded = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_RulesGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_RulesGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_RulesGroup.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    // برگرداندن لیست فرزندان بصورت بازگشتی
    private void GetChildItem_trvRulesGroup_RulesGroup(TreeViewNode parentRuleCategoryNode, RuleCategory parentRuleCategory, IList<RuleCategory> ruleCategoryList)
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        string imageName = string.Empty;
        foreach (RuleCategory childItem in ruleCategoryList.Where(x => x.ParentId == parentRuleCategory.ID))
        {
            TreeViewNode childItemNode = new TreeViewNode();
            childItemNode.ID = childItem.ID.ToString();
            childItemNode.Text = childItem.Name;

            TargetDetails targetDetails = new TargetDetails();
            if (childItem.IsGroup)
            {
                
                    targetDetails.TargetType = TargetType.Rule.ToString();
                    targetDetails.IsGroup = true;
                    targetDetails.CustomCode = childItem.CustomCode;
                    targetDetails.Discription = childItem.Discription;
                    targetDetails.IsRoot = false;
                    targetDetails.Name = childItem.Name;
                    //targetDetails.ParentId = "";
                
                imageName = "group.png";
            }
            else
            {
                targetDetails.TargetType = TargetType.RuleGroup.ToString();
                imageName = "folder.gif";
            }

            childItemNode.Value = JsSerializer.Serialize(targetDetails);

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\" + imageName))
                childItemNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/" + imageName;

            parentRuleCategoryNode.Nodes.Add(childItemNode);
            try
            {
                if (parentRuleCategoryNode.Parent.Parent == null)
                    parentRuleCategoryNode.Expanded = true;
            }
            catch
            { }

            if (this.RulesGroupBusiness.GetRuleCategoryChilds(childItem.ID).Count > 0)
                this.GetChildItem_trvRulesGroup_RulesGroup(childItemNode, childItem, ruleCategoryList);
        }
    }

    /// <summary>
    ///  حذف گروه قانون
    /// </summary>
    /// <param name="SelectedRuleGroupID">شناسه گروه قانون</param>
    /// <returns>آرایه ای از پیغام و شناسه</returns>
    [Ajax.AjaxMethod("UpdateRuleGroup_RulesGroupPage", "UpdateRuleGroup_RulesGroupPage_onCallBack", null, null)]
    public string[] UpdateRuleGroup_RulesGroupPage(string state, string SelectedRuleGroupID,string RuleGroupName, string RuleGroupParentId)
    {

        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            var rootCategory = this.RulesGroupBusiness.GetRoot();
            decimal RuleGroupID = 0;
            string StrObjRuleGroup = string.Empty;
            string SuccessMessageBody = string.Empty;
            decimal selectedRuleGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedRuleGroupID), CultureInfo.InvariantCulture);
            string ruleGroupName = this.StringBuilder.CreateString(RuleGroupName);
            string ruleGroupParentId = this.StringBuilder.CreateString(RuleGroupParentId);
            var s = this.StringBuilder.CreateString(state);
            ObjRuleGroup objRuleGroup = new ObjRuleGroup();
            UIActionType uam = UIActionType.ADD;

            RuleCategory ruleGroup = new RuleCategory();
            switch (this.StringBuilder.CreateString(state))
            {
                case "Delete":
                    uam = UIActionType.DELETE;
                    if (selectedRuleGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRuleGroupSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    else
                        ruleGroup.ID = selectedRuleGroupID;

                    RuleGroupID = this.RulesGroupBusiness.DeleteRuleGroup(ruleGroup, uam);

                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    objRuleGroup.ID = RuleGroupID.ToString();
                    StrObjRuleGroup = this.JsSerializer.Serialize(objRuleGroup);
                    break;
                
                case "Copy":
                    if (selectedRuleGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRuleGroupSelectedforCopy").ToString()), retMessage);
                        return retMessage;
                    }
                    else
                    {
                        ruleGroup = this.RulesGroupBusiness.CopyRuleCategory(selectedRuleGroupID);

                        SuccessMessageBody = GetLocalResourceObject("CopyComplete").ToString();
                        objRuleGroup.ID = ruleGroup.ID.ToString();
                        objRuleGroup.Name = ruleGroup.Name;
                        StrObjRuleGroup = this.JsSerializer.Serialize(objRuleGroup);
                    }
                    break;

                case "AddGroup":
                     uam = UIActionType.ADD;
                    ruleGroup.IsGroup = true;
                    ruleGroup.IsRoot = false;
                    ruleGroup.Name = ruleGroupName;
                    ruleGroup.ParentId = rootCategory.ID;

                    selectedRuleGroupID = this.RulesGroupBusiness.InsertRuleGroup(ruleGroup, uam);

                    SuccessMessageBody = GetLocalResourceObject("SaveComplete").ToString();
                    objRuleGroup.ID = ruleGroup.ID.ToString();
                    objRuleGroup.Name = ruleGroup.Name;
                    StrObjRuleGroup = this.JsSerializer.Serialize(objRuleGroup);
                    break;

                case "Edit":
                    uam = UIActionType.EDIT;
                    if (selectedRuleGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoItemSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    else
                    {

                        ruleGroup.Name = ruleGroupName;
                        ruleGroup.ID = selectedRuleGroupID;
                        ruleGroup.IsRoot = false;
                        ruleGroup.IsGroup = true;

                        selectedRuleGroupID = this.RulesGroupBusiness.UpdateRuleGroup(ruleGroup, uam);
                        SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                        objRuleGroup.ID = ruleGroup.ID.ToString();
                        objRuleGroup.Name = ruleGroup.Name;
                        StrObjRuleGroup = this.JsSerializer.Serialize(objRuleGroup);
                        


                    }
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = StrObjRuleGroup;
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