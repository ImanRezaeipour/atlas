﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business;
using GTS.Clock.Model.Security;
using GTS.Clock.Model;
using System.Collections;
using ComponentArt.Web.UI;
using System.Resources;
using System.Reflection;
using System.Data;
using System.Xml;
using System.IO;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace GTS.Clock.Presentaion.WebForms
{

    public partial class Users : GTSBasePage
    {
        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }

        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch

        }


        public BUser UserBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BUser>();
            }
        }

        public BRole RoleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BRole>();
            }
        }

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
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

        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }

        enum Scripts
        {
            Users_onPageLoad,
            tbUsers_TabStripMenus_Operations,
            DropDownDive,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations,
            CryptoJS
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbDomainName_Users.IsCallback && !CallBack_cmbDomainUserName_Users.IsCallback && !CallBack_cmbPersonnel_Users.IsCallback && !CallBack_cmbSearchField_Users.IsCallback && !CallBack_cmbUserRole_Users.IsCallback && !CallBack_GridUsers_Users.IsCallback)
            {
                Page UsersPage = this;
                Ajax.Utility.GenerateMethodScripts(UsersPage);

                this.CheckUsersLoadAccess_Users();
                this.SetUsersPageSize_Users();
                this.SetUsersPageCount_Users(LoadState.Normal, UserSearchKeys.Name, string.Empty);
                this.SetPersonnelPageSize_cmbPersonnel_Users();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckUsersLoadAccess_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                this.UserBusiness.CheckUsersLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            } 
        }

        private void SetPersonnelPageSize_cmbPersonnel_Users()
        {
            this.hfPersonnelPageSize_Users.Value = this.cmbPersonnel_Users.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_Users(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                }
                this.hfPersonnelPageCount_Users.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void SetUsersPageSize_Users()
        {
            this.hfUsersPageSize_Users.Value = this.GridUsers_Users.PageSize.ToString();
        }

        private void SetUsersPageCount_Users(LoadState Ls, UserSearchKeys SearchKey, string SearchTerm)
        {
            int UsersCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    UsersCount = this.UserBusiness.GetAllByPageBySearchCount(SearchKey, string.Empty);
                    break;
                case LoadState.Search:
                    UsersCount = this.UserBusiness.GetAllByPageBySearchCount(SearchKey, SearchTerm);
                    break;
            }
            this.hfUsersCount_Users.Value = UsersCount.ToString();
            this.hfUsersPageCount_Users.Value = Utility.GetPageCount(UsersCount, this.GridUsers_Users.PageSize).ToString();
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

        protected void CallBack_GridUsers_Users_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.SetUsersPageCount_Users((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]));
            this.Fill_GridUsers_Users((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), (UserSearchKeys)Enum.Parse(typeof(UserSearchKeys), this.StringBuilder.CreateString(e.Parameters[3])), this.StringBuilder.CreateString(e.Parameters[4]));
            this.GridUsers_Users.RenderControl(e.Output);
            this.hfUsersCount_Users.RenderControl(e.Output);
            this.hfUsersPageCount_Users.RenderControl(e.Output);
            this.ErrorHiddenField_Users.RenderControl(e.Output);
        }

        private void Fill_GridUsers_Users(LoadState Ls, int pageSize, int pageIndex, UserSearchKeys SearchKey, string SearchTerm)
        {
            string[] retMessage = new string[4];
            IList<UserProxy> UsersList = null;
            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        UsersList = this.UserBusiness.GetAllByPageBySearch(SearchKey, string.Empty, pageIndex, pageSize);
                        break;
                    case LoadState.Search:
                        UsersList = this.UserBusiness.GetAllByPageBySearch(SearchKey, SearchTerm, pageIndex, pageSize);
                        break;
                }
                this.GridUsers_Users.DataSource = UsersList;
                this.GridUsers_Users.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbPersonnel_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbPersonnel_Users.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_Users((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbUsers_Users((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_Users.Enabled = true;
            this.cmbPersonnel_Users.RenderControl(e.Output);
            this.hfPersonnelPageCount_Users.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_Users.RenderControl(e.Output);
        }

        private void Fill_cmbUsers_Users(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Person> PersonList = null;
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, string.Empty);
                        break;
                    case LoadState.Search:
                        PersonList = this.PersonSearchBusiness.QuickSearchByPageApplyCulture(pageIndex, pageSize, SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonList = this.PersonSearchBusiness.GetPersonInAdvanceSearchApplyCulture(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                        break;
                }
                foreach (Person personItem in PersonList)
                {
                    ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                    personCmbItem["BarCode"] = personItem.BarCode;
                    personCmbItem["CardNum"] = personItem.CardNum;
                    personCmbItem.Value = personItem.ID.ToString();
                    this.cmbPersonnel_Users.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_Users.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbSearchField_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbSearchField_Users.Dispose();
            this.Fill_cmbSearchField_Users();
            this.cmbSearchField_Users.Enabled = true;
            this.ErrorHiddenField_SearchFields.RenderControl(e.Output);
            this.cmbSearchField_Users.RenderControl(e.Output);
        }

        private void Fill_cmbSearchField_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                foreach (UserSearchKeys userSearchKeyItem in Enum.GetValues(typeof(UserSearchKeys)))
                {
                    ComboBoxItem cmbItemUserSearchKey = new ComboBoxItem(GetLocalResourceObject(userSearchKeyItem.ToString()).ToString());
                    cmbItemUserSearchKey.Value = ((int)userSearchKeyItem).ToString();
                    this.cmbSearchField_Users.Items.Add(cmbItemUserSearchKey);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbUserRole_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbUserRole_Users.Dispose();
            this.Fill_cmbUserRole_cmbUserRole_Users();
            this.cmbUserRole_Users.Enabled = true;
            this.ErrorHiddenField_UsersRoles.RenderControl(e.Output);
            this.cmbUserRole_Users.RenderControl(e.Output);
        }

        private void Fill_cmbUserRole_cmbUserRole_Users()
        {
            this.Fill_trvUserRole_cmbUserRole_Users();
        }

        private void Fill_trvUserRole_cmbUserRole_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Role> rolesList = this.RoleBusiness.GetAll();
                Role rootRole = this.UserBusiness.GetRoleTree();
                TreeViewNode rootRoleNode = new TreeViewNode();
                rootRoleNode.ID = rootRole.ID.ToString();
                string rootRoleNodeText = string.Empty;
                if (GetLocalResourceObject("RolesNode_trvRoles_Roles") != null)
                    rootRoleNodeText = GetLocalResourceObject("RolesNode_trvRoles_Roles").ToString();
                else
                    rootRoleNodeText = rootRole.Name;
                rootRoleNode.Text = rootRoleNodeText;
                rootRoleNode.Value = rootRole.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                    rootRoleNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                this.trvUserRole_cmbUserRole_Users.Nodes.Add(rootRoleNode);
                rootRoleNode.Expanded = true;

                this.GetChildRoles_trvUserRole_cmbUserRole_Users(rolesList, rootRoleNode, rootRole);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UsersRoles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UsersRoles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UsersRoles.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildRoles_trvUserRole_cmbUserRole_Users(IList<Role> rolesList, TreeViewNode parentRoleNode, Role parentRole)
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
                    this.GetChildRoles_trvUserRole_cmbUserRole_Users(rolesList, childRoleNode, childRole);
            }
        }

        protected void CallBack_cmbDomainName_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDomainName_Users.Dispose();
            this.Fill_cmbDomainName_Users();
            this.cmbDomainName_Users.Enabled = true;
            this.ErrorHiddenField_Domains.RenderControl(e.Output);
            this.cmbDomainName_Users.RenderControl(e.Output);
        }

        private void Fill_cmbDomainName_Users()
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Domains> DomainsList = this.UserBusiness.GetActiveDirectoryDomains();
                this.cmbDomainName_Users.DataSource = DomainsList;
                this.cmbDomainName_Users.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_SearchFields.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbDomainUserName_Users_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDomainUserName_Users.Dispose();
            this.Fill_cmbDomainUserName_Users(decimal.Parse(this.StringBuilder.CreateString(e.Parameter)));
            this.cmbDomainUserName_Users.Enabled = true;
            this.ErrorHiddenField_DomainUsers.RenderControl(e.Output);
            this.cmbDomainUserName_Users.RenderControl(e.Output);
        }

        private void Fill_cmbDomainUserName_Users(decimal DomainID)
        {
            string[] retMessage = new string[4];
            try
            {
                if (DomainID == -1)
                {
                    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoDomainSelected").ToString()), retMessage);
                    this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
                    return;
                }
                IList<string> DomainUsersList = this.UserBusiness.GetActiveDirectoryUsers(DomainID);
                foreach (string domainUserItem in DomainUsersList)
                {
                    ComboBoxItem cmbItemDomainUser = new ComboBoxItem(domainUserItem);
                    this.cmbDomainUserName_Users.Items.Add(cmbItemDomainUser);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DomainUsers.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateUser_UsersPage", "UpdateUser_UsersPage_onCallBack", null, null)]
        public string[] UpdateUser_UsersPage(string state, string SelectedUserID, string IsActiveUser, string PersonnelID, string RoleID, string UserName, string Password, string ConfirmPassword, string IsActiveDirectoryAuthenticated, string DomainID, string IsPasswordChange)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal UserID = 0;
                decimal selectedUserID = decimal.Parse(this.StringBuilder.CreateString(SelectedUserID), CultureInfo.InvariantCulture);
                bool isActiveUser = bool.Parse(this.StringBuilder.CreateString(IsActiveUser));
                decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
                decimal roleID = decimal.Parse(this.StringBuilder.CreateString(RoleID), CultureInfo.InvariantCulture);
                UserName = this.StringBuilder.CreateString(UserName);
                Password = this.StringBuilder.CreateString(Password);
                ConfirmPassword = this.StringBuilder.CreateString(ConfirmPassword);
                bool isActiveDirectoryAuthenticated = bool.Parse(this.StringBuilder.CreateString(IsActiveDirectoryAuthenticated));
                decimal domainID = decimal.Parse(this.StringBuilder.CreateString(DomainID), CultureInfo.InvariantCulture);
                bool isPasswordChange = bool.Parse(this.StringBuilder.CreateString(IsPasswordChange));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
                UserProxy user = new UserProxy();
                user.ID = selectedUserID;
                user.PersonID = personnelID;
                if (uam != UIActionType.DELETE)
                {
                    user.Active = isActiveUser;
                    user.RoleID = roleID;
                    user.UserName = UserName;
                    if (isActiveDirectoryAuthenticated)
                        user.DomainId = domainID;
                    else
                    {
                        user.Password = Password;
                        user.ConfirmPassword = ConfirmPassword;
                        user.IsPasswodChanged = isPasswordChange;
                    }
                    user.ActiveDirectoryAuthenticate = isActiveDirectoryAuthenticated;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        UserID = this.UserBusiness.InsertUser(user);
                        break;
                    case UIActionType.EDIT:
                        if (selectedUserID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoUserSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        UserID = this.UserBusiness.EditUser(user);
                        break;
                    case UIActionType.DELETE:
                        if (selectedUserID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoUserSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        UserID = this.UserBusiness.DeleteUser(user);
                        break;
                    default:
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
                retMessage[3] = UserID.ToString();
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

        //[Ajax.AjaxMethod("ExcelExport_GridUsers_Users_UsersPage", "ExcelExport_GridUsers_Users_UsersPage_onCallBack", null, false, null)]
        //public string ExcelExport_GridUsers_Users_UsersPage(string GridColumns, string SearchKey, string SearchText, string PageIndex, string PageSize)
        //{
        //    return string.Empty;
        //    IList<UserInfo> UserInfoList = null;
        //    if (SearchKey != null && SearchText != null)
        //        UserInfoList = this.usersPresenter.GetAllByPage(this.GetCurrentUserSearchKey(SearchKey), this.StringBuilder(SearchText), Convert.ToInt32(PageIndex), Convert.ToInt32(PageSize));
        //    else
        //        UserInfoList = this.usersPresenter.GetAllByPage(Convert.ToInt32(PageIndex), Convert.ToInt32(PageSize));

        //    string FileName = this.CreateExportDs_GridUsers_Users(GridColumns, UserInfoList);
        //    return FileName;
        //}

        //private string CreateExportDs_GridUsers_Users(string GridColumns, IList<UserInfo> UserInfoList)
        //{
        //    string FileName = "";
        //    if (UserInfoList != null)
        //    {
        //        DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        string[] ColumnsCol = GridColumns.Split(new char[] { '#' });
        //        Dictionary<string, string> dicColumns = new Dictionary<string, string>();
        //        foreach (string ColumnItem in ColumnsCol)
        //        {
        //            if (ColumnItem != "")
        //            {
        //                string[] ColumnFeaturesCol = ColumnItem.Split(new char[] { ':' });
        //                string ColumnDataField = ColumnFeaturesCol[0];
        //                string ColumnCaption = this.StringBuilder(ColumnFeaturesCol[1]);
        //                dicColumns.Add(ColumnDataField, ColumnCaption);
        //            }
        //        }
        //        foreach (PropertyInfo PropInfo in typeof(UserInfo).GetProperties())
        //        {
        //            if (dicColumns.ContainsKey(PropInfo.Name))
        //            {
        //                DataColumn dc = new DataColumn(PropInfo.Name, PropInfo.PropertyType);
        //                dc.Caption = dicColumns[PropInfo.Name];
        //                dt.Columns.Add(dc);
        //            }
        //        }
        //        ds.Tables.Add(dt);

        //        foreach (UserInfo uInfo in UserInfoList)
        //        {
        //            DataRow dr = ds.Tables[0].NewRow();
        //            foreach (PropertyInfo pInfo in typeof(UserInfo).GetProperties())
        //            {
        //                if (dicColumns.ContainsKey(pInfo.Name))
        //                    dr[pInfo.Name] = pInfo.GetValue(uInfo, null);
        //            }
        //            ds.Tables[0].Rows.Add(dr);
        //        }

        //        FileName = this.ExportDs_GridUsers_Users(ds, dicColumns);
        //    }
        //    return FileName;
        //}

        //private string ExportDs_GridUsers_Users(DataSet ds, Dictionary<string, string> dicColumns)
        //{
        //    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/XLS"))
        //        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/XLS");

        //    string FileName = Guid.NewGuid().ToString();
        //    ds.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xml");

        //    Spreadsheet document = new Spreadsheet();
        //    SimpleXMLConvertor tools = new SimpleXMLConvertor(document);
        //    tools.ColumnsCol = dicColumns;
        //    this.InitializeCulture();
        //    tools.WorkSheetName = this.GetLocalResourceObject("WorkSheetName_ExportedData_GridUsers_Users").ToString();
        //    tools.LoadXML(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xml");
        //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xml");
        //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls"))
        //        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    document.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    document.Close();

        //    Spreadsheet exportedDocument = new Spreadsheet();
        //    exportedDocument.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    Worksheet worksheet = exportedDocument.Workbook.Worksheets[1];
        //    worksheet.Rows.Delete(0, 14);
        //    exportedDocument.Worksheet(1).Visible = SHEETVISIBILITY.VeryHidden;
        //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    exportedDocument.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "/XLS/" + FileName + ".xls");
        //    exportedDocument.Close();

        //    return FileName + ".xls";
        //}

    }

}