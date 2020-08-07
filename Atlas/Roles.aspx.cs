using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Security;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.IO;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.GridSettings;
using GTS.Clock.Infrastructure;
namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Roles : GTSBasePage
    {

        public BMonthlyOperationGridRoleSettings bMonthlyOperationGridRoleSettings
        {
            get
            {
                return new BMonthlyOperationGridRoleSettings();
            }
        }
        public BRole RoleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BRole>();
            }
        }
        public IDataAccess UserBusiness
        {
            get
            {
                return (IDataAccess)BusinessHelper.GetBusinessInstance<BUser>();
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

        enum Scripts
        {
            Roles_onPageLoad,
            tbRoles_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_trvRoles_Roles.IsCallback)
            {
                Page RolesPage = this;
                Ajax.Utility.GenerateMethodScripts(RolesPage);

                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckRolesLoadAccess_Roles();
            }
        }

        private void CheckRolesLoadAccess_Roles()
        {
            string[] retMessage = new string[4];
            try
            {
                this.RoleBusiness.CheckRolesLoadAccess();
            }
            catch (BaseException ex)
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

        private void Fill_trvRoles_Roles()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Role> rolesList = this.RoleBusiness.GetAll();
                IList<decimal> accessibleRoleIds = this.UserBusiness.GetAccessibleRoles();
                Role rootDep = this.RoleBusiness.GetRoleTree();
                TreeViewNode rootRoleNode = new TreeViewNode();
                rootRoleNode.ID = rootDep.ID.ToString();
                string rootRoleNodeText = string.Empty;
                if (GetLocalResourceObject("RolesNode_trvRoles_Roles") != null)
                    rootRoleNodeText = GetLocalResourceObject("RolesNode_trvRoles_Roles").ToString();
                else
                    rootRoleNodeText = rootDep.Name;
                rootRoleNode.Text = rootRoleNodeText;
                rootRoleNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                    rootRoleNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                this.trvRoles_Roles.Nodes.Add(rootRoleNode);
                rootRoleNode.Expanded = true;

                this.GetChildRoles_trvRoles_Roles(rolesList, rootRoleNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildRoles_trvRoles_Roles(IList<Role> rolesList, TreeViewNode parentRoleNode, Role parentRole)
        {
            foreach (Role childRole in this.RoleBusiness.GetRoleChilds(parentRole.ID, rolesList))
            {
                TreeViewNode childRoleNode = new TreeViewNode();
                childRoleNode.ID = childRole.ID.ToString();
                childRoleNode.Text = childRole.Name;
                childRoleNode.Value = childRole.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\folder.gif"))
                    childRoleNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                parentRoleNode.Nodes.Add(childRoleNode);
                try
                {
                    if (parentRoleNode.Parent.Parent == null)
                        parentRoleNode.Expanded = true;
                }
                catch
                { }
                if (this.RoleBusiness.GetRoleChilds(childRole.ID, rolesList).Count > 0)
                    this.GetChildRoles_trvRoles_Roles(rolesList, childRoleNode, childRole);
            }
        }

        protected void CallBack_trvRoles_Roles_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_trvRoles_Roles();
            this.ErrorHiddenField_Roles.RenderControl(e.Output);
            this.trvRoles_Roles.RenderControl(e.Output);
        }

        [Ajax.AjaxMethod("UpdateRoles_RolesPage", "UpdateRoles_RolesPage_onCallBack", null, null)]
        public string[] UpdateRoles_RolesPage(string state, string SelectedRoleID, string RoleCode, string RoleName)
        {

            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal RoleID = 0;
                decimal selectedRoleID = decimal.Parse(this.StringBuilder.CreateString(SelectedRoleID), CultureInfo.InvariantCulture);
                RoleCode = this.StringBuilder.CreateString(RoleCode);
                RoleName = this.StringBuilder.CreateString(RoleName);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                Role role = new Role();
                if (uam != UIActionType.DELETE)
                {
                    role.CustomCode = RoleCode;
                    role.Name = RoleName;
                    role.Active = true;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        role.ParentId = selectedRoleID;
                        role.ID = 0;
                        RoleID = this.RoleBusiness.InsertRole(role, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedRoleID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRoleSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            role.ID = selectedRoleID;
                        RoleID = this.RoleBusiness.UpdateRole(role, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedRoleID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoRoleSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            role.ID = selectedRoleID;
                        RoleID = this.RoleBusiness.DeleteRole(role, uam);
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
                retMessage[3] = RoleID.ToString();
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
        protected void CallBack_GridSettings_Roles_onCallBack(object sender, CallBackEventArgs e)
        {
            string[] retMessage = new string[4];
            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal RoleId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1]));
                switch (e.Parameters[0])
                {
                    case "Get":
                        IList<MonthlyOperationGridRoleSettingsProxy> monthlyOperationGridRoleSettingsProxy = bMonthlyOperationGridRoleSettings.GetMonthlyOperationGridRoleSettings(RoleId);
                        this.hfExist_GridSettings_Roles.Value = monthlyOperationGridRoleSettingsProxy[0].Exist.ToString();
                        this.hfId_GridSettings_Roles.Value = monthlyOperationGridRoleSettingsProxy[0].ID.ToString();
                        this.hfRuleId_GridSettings_Roles.Value = monthlyOperationGridRoleSettingsProxy[0].RoleID.ToString();
                        this.GridSettings_Roles.DataSource = GetLocalResource(monthlyOperationGridRoleSettingsProxy);
                        this.GridSettings_Roles.DataBind();
                        break;
                    case "Set":
                        //retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                        //retMessage[1] = GetLocalResourceObject("EditComplete").ToString();
                        //retMessage[2] = "success";
                        bool GridSettingExist = bool.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
                        decimal GridSettingId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
                        Dictionary<string, string> SettingsColArray = this.CreateRecievedColumnsArray_Roles(e.Parameters[4]);
                        Dictionary<string, string> MonthlyOperationGridColumn = this.CreatMonthlyOperationGridColumns();
                        bMonthlyOperationGridRoleSettings.UpdateGridMonthlyOperationGridRoleSettings(GridSettingId, RoleId, GridSettingExist, SettingsColArray, MonthlyOperationGridColumn);
                        this.hfSuccessType_GridSettings_Roles.Value = GetLocalResourceObject("RetSuccessType").ToString();
                        this.hfComplete_GridSettings_Roles.Value = GetLocalResourceObject("Complete").ToString();
                        this.hfSuccess_GridSettings_Roles.Value = "success";
                        break;
                }

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_GridSettings_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_GridSettings_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_GridSettings_Roles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

            this.ErrorHiddenField_GridSettings_Roles.RenderControl(e.Output);
            this.hfExist_GridSettings_Roles.RenderControl(e.Output);
            this.hfId_GridSettings_Roles.RenderControl(e.Output);
            this.hfRuleId_GridSettings_Roles.RenderControl(e.Output);
            this.hfSuccess_GridSettings_Roles.RenderControl(e.Output);
            this.hfSuccessType_GridSettings_Roles.RenderControl(e.Output);
            this.hfComplete_GridSettings_Roles.RenderControl(e.Output);
            this.GridSettings_Roles.RenderControl(e.Output);
        }
        private Dictionary<string, string> CreateRecievedColumnsArray_Roles(string RecievedStr)
        {
            string[] ColStrArray = RecievedStr.Split(new char[] { ':' });
            Dictionary<string, string> ColArray = new Dictionary<string, string>();
            string[] ColStr;
            foreach (string Col in ColStrArray)
            {
                ColStr = Col.Split(new char[] { '%' });
                ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), ColStr[1]);

            }
            return ColArray;
        }
        private IList<MonthlyOperationGridRoleSettingsProxy> GetLocalResource(IList<MonthlyOperationGridRoleSettingsProxy> MonthlyOperationGridRoleSettingsProxyList)
        {
            IList<MonthlyOperationGridRoleSettingsProxy> monthlyOperationGridRoleSettingsProxyList = new List<MonthlyOperationGridRoleSettingsProxy>();
            foreach (MonthlyOperationGridRoleSettingsProxy setting in MonthlyOperationGridRoleSettingsProxyList)
            {
                MonthlyOperationGridRoleSettingsProxy monthlyOperationGridRoleSettingsProxy = new MonthlyOperationGridRoleSettingsProxy();
                monthlyOperationGridRoleSettingsProxy.ID = setting.ID;
                monthlyOperationGridRoleSettingsProxy.RoleID = setting.RoleID;
                monthlyOperationGridRoleSettingsProxy.GridColumn = GetLocalResourceObject(setting.GridColumn).ToString();
                monthlyOperationGridRoleSettingsProxy.ViewState = setting.ViewState;
                monthlyOperationGridRoleSettingsProxy.Exist = setting.Exist;
                monthlyOperationGridRoleSettingsProxyList.Add(monthlyOperationGridRoleSettingsProxy);
            }
            return monthlyOperationGridRoleSettingsProxyList;
        }
        private Dictionary<string, string> CreatMonthlyOperationGridColumns()
        {
            Dictionary<string, string> MonthlyOperationGridColumn = new Dictionary<string, string>();
            foreach (GridRoleSettings setting in Enum.GetValues(typeof(GridRoleSettings)))
            {
                MonthlyOperationGridColumn.Add(setting.ToString(), GetLocalResourceObject(setting.ToString()).ToString());
            }
            return MonthlyOperationGridColumn;
        }
    }
}