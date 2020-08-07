﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using System.IO;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Charts;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Business.UIValidation;
using GTS.Clock.Model.Contracts;


namespace GTS.Clock.Presentaion.WebForms
{
    public partial class PersonnelSearch : GTSBasePage
    {
        public ISearchPerson PersonnelSearchBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }
        public BUIValidationGroup bUIValidationGroup
        {
            get
            {
                return new BUIValidationGroup();
            }
        }
        public BDepartment DepartmentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDepartment>();
            }
        }
        public BOrganizationUnit OrganizationBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BOrganizationUnit>();
            }
        }
        internal class OrganizationPostNodeValue
        {
            public string CustomCode { get; set; }
            public string ParentPath { get; set; }
            public string PersonnelName { get; set; }
            public string PersonnelCode { get; set; }
            public string PersonnelID { get; set; }
        }
        internal class DepartmentObj
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string CustomCode { get; set; }
        }
        internal class OrganizationPostObj
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string CustomCode { get; set; }
        }
        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
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
        public BDepartment departmentObj
        {
            get
            {
                return new BDepartment();
            }
        }
        enum Scripts
        {
            PersonnelSearch_onPageLoad,
            DialogPersonnelSearch_Operations,
            Alert_Box
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbCalculationRange_PersonnelSearch.IsCallback && !CallBack_cmbControlStation_PersonnelSearch.IsCallback && !CallBack_cmbDepartment_PersonnelSearch.IsCallback && !CallBack_cmbEmployType_PersonnelSearch.IsCallback && !CallBack_cmbMarriageState_PersonnelSearch.IsCallback && !CallBack_cmbMilitaryState_PersonnelSearch.IsCallback && !CallBack_cmbOrganizationPost_PersonnelSearch.IsCallback && !CallBack_cmbRuleGroups_PersonnelSearch.IsCallback && !CallBack_cmbSex_PersonnelSearch.IsCallback && !CallBack_cmbWorkGroups_PersonnelSearch.IsCallback && !CallBack_cmbDepartmentSearchResult_PersonnelSearch.IsCallback && !CallBack_cmbContract_PersonnelSearch.IsCallback)
            {
                Page PersonnelSearchPage = this;
                Ajax.Utility.GenerateMethodScripts(PersonnelSearchPage);

                this.ViewCurrentLangCalendars_PersonnelSearch();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void ViewCurrentLangCalendars_PersonnelSearch()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_DualCalendars_PersonnelSearch.Visible = true;
                    this.Container_pdpFromDate_SingleCalendar_PersonnelSearch.Visible = true;
                    this.Container_pdpToDate_DualCalendars_PersonnelSearch.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpFromDate_DualCalendars_PersonnelSearch.Visible = true;
                    this.Container_gdpFromDate_SingleCalendar_PersonnelSearch.Visible = true;
                    this.Container_gdpToDate_DualCalendars_PersonnelSearch.Visible = true;
                    break;
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

        [Ajax.AjaxMethod("GetLoadonDemandError_PersonnelSearchPage", "GetLoadonDemandError_PersonnelSearchPage_onCallBack", null, null)]
        public string GetLoadonDemandError_PersonnelSearchPage()
        {
            this.InitializeCulture();
            AttackDefender.CSRFDefender(this.Page);
            string retError = string.Empty;
            if (Session["LoadonDemandError_PersonnelSearchPage"] != null)
            {
                retError = Session["LoadonDemandError_PersonnelSearchPage"].ToString();
                Session["LoadonDemandError_PersonnelSearchPage"] = null;
            }
            else
            {
                string[] retMessage = new string[3];
                retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
                retMessage[1] = GetLocalResourceObject("ParentNodeFillProblem").ToString();
                retMessage[2] = "error";
                retError = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            return retError;
        }


        protected void CallBack_cmbSex_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbSex_PersonnelSearch.Dispose();
            this.Fill_cmbSex_PersonnelSearch();
            this.ErrorHiddenField_Sex_PersonnelSearch.RenderControl(e.Output);
            this.cmbSex_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbSex_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (PersonSex personSexItem in Enum.GetValues(typeof(PersonSex)))
                {
                    ComboBoxItem cmbItemPersonSex = new ComboBoxItem(GetLocalResourceObject(personSexItem.ToString()).ToString());
                    cmbItemPersonSex.Value = ((int)personSexItem).ToString();
                    cmbItemPersonSex.Id = ((int)personSexItem).ToString();
                    this.cmbSex_PersonnelSearch.Items.Add(cmbItemPersonSex);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Sex_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Sex_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Sex_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMarriageState_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMarriageState_PersonnelSearch.Dispose();
            this.Fill_cmbMarriageState_PersonnelSearch();
            this.ErrorHiddenField_MarriageState_PersonnelSearch.RenderControl(e.Output);
            this.cmbMarriageState_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbMarriageState_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (MaritalStatus maritalStatusItem in Enum.GetValues(typeof(MaritalStatus)))
                {
                    ComboBoxItem cmbItemMaritalStatus = new ComboBoxItem(GetLocalResourceObject(maritalStatusItem.ToString()).ToString());
                    cmbItemMaritalStatus.Value = ((int)maritalStatusItem).ToString();
                    cmbItemMaritalStatus.Id = ((int)maritalStatusItem).ToString();
                    this.cmbMarriageState_PersonnelSearch.Items.Add(cmbItemMaritalStatus);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MarriageState_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MarriageState_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MarriageState_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMilitaryState_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMilitaryState_PersonnelSearch.Dispose();
            this.Fill_cmbMilitaryState_PersonnelSearch();
            this.ErrorHiddenField_MilitaryState_PersonnelSearch.RenderControl(e.Output);
            this.cmbMilitaryState_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbMilitaryState_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (MilitaryStatus militaryStatusItem in Enum.GetValues(typeof(MilitaryStatus)))
                {
                    ComboBoxItem cmbItemMilitaryStatus = new ComboBoxItem(GetLocalResourceObject(militaryStatusItem.ToString()).ToString());
                    cmbItemMilitaryStatus.Value = ((int)militaryStatusItem).ToString();
                    cmbItemMilitaryStatus.Id = ((int)militaryStatusItem).ToString();
                    this.cmbMilitaryState_PersonnelSearch.Items.Add(cmbItemMilitaryStatus);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbDepartment_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartment_PersonnelSearch.Dispose();
            this.Fill_cmbDepartment_PersonnelSearch();

            this.ErrorHiddenField_Department_PersonnelSearch.RenderControl(e.Output);
            this.cmbDepartment_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbDepartment_PersonnelSearch()
        {
            this.Fill_trvDepartment_PersonnelSearch();
        }

        private void Fill_trvDepartment_PersonnelSearch()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Department> departmentsList = this.PersonnelSearchBusiness.GetAllDepartments();
                Department rootDep = this.PersonnelSearchBusiness.GetDepartmentRoot();
                TreeViewNode rootDepNode = new TreeViewNode();
                rootDepNode.ShowCheckBox = true;
                rootDepNode.ID = rootDep.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDepartment_PersonnelSearch") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDepartment_PersonnelSearch").ToString();
                else
                    rootDepNodeText = rootDep.Name;
                rootDepNode.Text = rootDepNodeText;
                rootDepNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                    rootDepNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                this.trvDepartment_PersonnelSearch.Nodes.Add(rootDepNode);
                rootDepNode.Expanded = true;

                this.GetChildDepartment_trvDepartment_PersonnelSearch(departmentsList, rootDepNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Department_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Department_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Department_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildDepartment_trvDepartment_PersonnelSearch(IList<Department> departmentsList, TreeViewNode parentDepartmentNode, Department parentDepartment)
        {
            foreach (Department childDep in this.PersonnelSearchBusiness.GetDepartmentChild(parentDepartment.ID, departmentsList))
            {
                TreeViewNode childDepNode = new TreeViewNode();
                childDepNode.ShowCheckBox = true;
                childDepNode.ID = childDep.ID.ToString();
                childDepNode.Text = childDep.Name;
                childDepNode.Value = childDep.CustomCode;
                childDepNode.ImageUrl = parentDepartmentNode.ImageUrl;
                parentDepartmentNode.Nodes.Add(childDepNode);

                if (parentDepartmentNode.Parent != null)
                    if (parentDepartmentNode.Parent.Parent == null)
                        parentDepartmentNode.Expanded = true;

                if (this.PersonnelSearchBusiness.GetDepartmentChild(childDep.ID, departmentsList).Count > 0)
                    this.GetChildDepartment_trvDepartment_PersonnelSearch(departmentsList, childDepNode, childDep);
            }
        }

        protected void CallBack_cmbOrganizationPost_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbOrganizationPost_PersonnelSearch.Dispose();
            this.Fill_cmbOrganizationPost_PersonnelSearch();
            this.ErrorHiddenField_OrganizationPost_PersonnelSearch.RenderControl(e.Output);
            this.cmbOrganizationPost_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbOrganizationPost_PersonnelSearch()
        {
            this.Fill_trvOrganizationPost_PersonnelSearch();
        }

        private void Fill_trvOrganizationPost_PersonnelSearch()
        {
            string imageUrl = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif";
            string imagePath = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                OrganizationUnit rootOrgPost = this.PersonnelSearchBusiness.GetOrganizationRoot();
                TreeViewNode rootOrgPostNode = new TreeViewNode();
                rootOrgPostNode.ID = rootOrgPost.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvOrganizationPost_PersonnelSearch") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvOrganizationPost_PersonnelSearch").ToString();
                else
                    rootOrgPostNodeText = rootOrgPost.Name;
                rootOrgPostNode.Text = rootOrgPostNodeText;
                OrganizationPostNodeValue rootOrgPostNodeValue = new OrganizationPostNodeValue();
                rootOrgPostNodeValue.CustomCode = rootOrgPost.CustomCode;
                rootOrgPostNodeValue.ParentPath = string.Empty;
                rootOrgPostNodeValue.PersonnelName = string.Empty;
                rootOrgPostNodeValue.PersonnelCode = string.Empty;
                rootOrgPostNodeValue.PersonnelID = "0";
                rootOrgPostNode.Value = this.JsSerializer.Serialize(rootOrgPostNodeValue);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    rootOrgPostNode.ImageUrl = imagePath;
                this.trvOrganizationPost_PersonnelSearch.Nodes.Add(rootOrgPostNode);
                IList<OrganizationUnit> OrganizationUnitChildList = this.PersonnelSearchBusiness.GetOrganizationChild(rootOrgPost.ID);
                foreach (OrganizationUnit childOrgPost in OrganizationUnitChildList)
                {
                    TreeViewNode childOrgPostNode = new TreeViewNode();
                    childOrgPostNode.ID = childOrgPost.ID.ToString();
                    childOrgPostNode.Text = childOrgPost.Name;
                    OrganizationPostNodeValue childOrgPostNodeValue = new OrganizationPostNodeValue();
                    childOrgPostNodeValue.CustomCode = childOrgPost.CustomCode;
                    childOrgPostNodeValue.ParentPath = childOrgPost.ParentPath;
                    childOrgPostNodeValue.PersonnelName = childOrgPost.Person != null ? childOrgPost.Person.Name : string.Empty;
                    childOrgPostNodeValue.PersonnelCode = childOrgPost.Person != null ? childOrgPost.Person.PersonCode : string.Empty;
                    childOrgPostNodeValue.PersonnelID = childOrgPost.Person != null ? childOrgPost.Person.ID.ToString() : "0";
                    childOrgPostNode.Value = this.JsSerializer.Serialize(childOrgPostNodeValue);
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                        childOrgPostNode.ImageUrl = imagePath;
                    childOrgPostNode.ContentCallbackUrl = "XmlOrganizationPostsLoadonDemand.aspx?ParentOrgPostID=" + childOrgPost.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                    if (childOrgPost.ChildList.Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootOrgPostNode.Nodes.Add(childOrgPostNode);
                }
                if (OrganizationUnitChildList.Count > 0)
                    rootOrgPostNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbControlStation_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbControlStation_PersonnelSearch.Dispose();
            this.Fill_cmbControlStation_PersonnelSearch();
            this.ErrorHiddenField_ControlStation_PersonnelSearch.RenderControl(e.Output);
            this.cmbControlStation_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbControlStation_PersonnelSearch()
        {
            Fill_trvControlStation_PersonnelSearch();
        }
        private void Fill_trvControlStation_PersonnelSearch()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<ControlStation> ControlStationsList = this.PersonnelSearchBusiness.GetAllControlStation();
                foreach (ControlStation controlStationItem in ControlStationsList)
                {
                    TreeViewNode trvControlStation = new TreeViewNode();
                    trvControlStation.Text = controlStationItem.Name;
                    trvControlStation.Value = controlStationItem.ID.ToString();
                    trvControlStation.ID = controlStationItem.ID.ToString();
                    trvControlStation.ShowCheckBox = true;
                    this.trvControlStation_PersonnelSearch.Nodes.Add(trvControlStation);
                }
                //this.cmbControlStation_PersonnelSearch.DataSource = ControlStationsList;
                //this.cmbControlStation_PersonnelSearch.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ControlStation_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ControlStation_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ControlStation_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }
        protected void CallBack_cmbWorkGroups_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbWorkGroups_PersonnelSearch.Dispose();
            this.Fill_cmbWorkGroups_PersonnelSearch();
            this.ErrorHiddenField_WorkGroups_PersonnelSearch.RenderControl(e.Output);
            this.cmbWorkGroups_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbWorkGroups_PersonnelSearch()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<WorkGroup> WorkGroupsList = this.PersonnelSearchBusiness.GetAllWorkGroup();
                this.cmbWorkGroups_PersonnelSearch.DataSource = WorkGroupsList;
                this.cmbWorkGroups_PersonnelSearch.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbRuleGroups_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbRuleGroups_PersonnelSearch.Dispose();
            this.Fill_cmbRuleGroups_PersonnelSearch();
            this.ErrorHiddenField_RuleGroups_PersonnelSearch.RenderControl(e.Output);
            this.cmbRuleGroups_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbRuleGroups_PersonnelSearch()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<RuleCategory> RuleGroupsList = this.PersonnelSearchBusiness.GetAllRuleGroup();
                this.cmbRuleGroups_PersonnelSearch.DataSource = RuleGroupsList;
                this.cmbRuleGroups_PersonnelSearch.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RuleGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RuleGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RuleGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbCalculationRange_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbCalculationRange_PersonnelSearch.Dispose();
            this.Fill_cmbCalculationRange_PersonnelSearch();
            this.ErrorHiddenField_CalculationRange_PersonnelSearch.RenderControl(e.Output);
            this.cmbCalculationRange_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbCalculationRange_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<CalculationRangeGroup> CalculationRangeGroupList = this.PersonnelSearchBusiness.GetAllDateRanges();
                this.cmbCalculationRange_PersonnelSearch.DataSource = CalculationRangeGroupList;
                this.cmbCalculationRange_PersonnelSearch.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbEmployType_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbEmployType_PersonnelSearch.Dispose();
            this.Fill_cmbEmployType_PersonnelSearch();
            this.ErrorHiddenField_EmployType_PersonnelSearch.RenderControl(e.Output);
            this.cmbEmployType_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbEmployType_PersonnelSearch()
        {
            Fill_trvEmployType_PersonnelSearch();
        }
        private void Fill_trvEmployType_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<EmploymentType> EmploymentTypesList = this.PersonnelSearchBusiness.GetAllEmploymentTypes();
                foreach (EmploymentType employmentItem in EmploymentTypesList)
                {
                    TreeViewNode trvEmployType = new TreeViewNode();
                    trvEmployType.Text = employmentItem.Name;
                    trvEmployType.Value = employmentItem.ID.ToString();
                    trvEmployType.ID = employmentItem.ID.ToString();
                    trvEmployType.ShowCheckBox = true;
                    this.trvEmployType_PersonnelSearch.Nodes.Add(trvEmployType);
                }
                //this.cmbEmployType_PersonnelSearch.DataSource = EmploymentTypesList;
                //this.cmbEmployType_PersonnelSearch.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_EmployType_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_EmployType_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_EmployType_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbDepartmentSearchResult_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartmentSearchResult_PersonnelSearch.Dispose();
            this.Fill_cmbDepartmentSearchResult_PersonnelSearch(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_DepartmentSearchResult_PersonnelSearch.RenderControl(e.Output);
            this.cmbDepartmentSearchResult_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbDepartmentSearchResult_PersonnelSearch(string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {
                IList<Department> departmentList = this.DepartmentBusiness.SearchDepartment(DepartmentSearchFields.NotSpec, SearchTerm);

                foreach (Department departItem in departmentList)
                {

                    ComboBoxItem departCmbItem = new ComboBoxItem(departItem.Name);
                    departCmbItem.Text = departItem.Name;
                    DepartmentObj departmentObj = new DepartmentObj()
                    {
                        ID = departItem.ID.ToString(),
                        Name = departItem.Name,
                        CustomCode = departItem.CustomCode
                    };
                    departCmbItem.Value = this.JsSerializer.Serialize(departmentObj);
                    this.cmbDepartmentSearchResult_PersonnelSearch.Items.Add(departCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbOrganizationPostSearchResult_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbOrganizationPostSearchResult_PersonnelSearch.Dispose();
            this.Fill_cmbOrganizationPostSearchResult_PersonnelSearch(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_OrganizationPostSearchResult_PersonnelSearch.RenderControl(e.Output);
            this.cmbOrganizationPostSearchResult_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbOrganizationPostSearchResult_PersonnelSearch(string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {

                IList<OrganizationUnit> organizationList = this.OrganizationBusiness.SearchOrganization(OrganizationSearchFields.NotSpec, SearchTerm);

                foreach (OrganizationUnit organItem in organizationList)
                {

                    ComboBoxItem organCmbItem = new ComboBoxItem(organItem.Name);
                    organCmbItem.Text = organItem.Name;
                    OrganizationPostObj organizationPostObj = new OrganizationPostObj()
                    {
                        ID = organItem.ID.ToString(),
                        Name = organItem.Name,
                        CustomCode = organItem.CustomCode
                    };
                    organCmbItem.Value = this.JsSerializer.Serialize(organizationPostObj);
                    this.cmbOrganizationPostSearchResult_PersonnelSearch.Items.Add(organCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OrganizationPostSearchResult_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPostSearchResult_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPostSearchResult_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbGrade_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbGrade_PersonnelSearch.Dispose();
            this.Fill_cmbGrade_PersonnelSearch();
            this.ErrorHiddenField_Grade_PersonnelSearch.RenderControl(e.Output);
            this.cmbGrade_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbGrade_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<GTS.Clock.Model.BaseInformation.Grade> gradeList = PersonnelSearchBusiness.GetAllGrade();
                cmbGrade_PersonnelSearch.DataSource = gradeList;
                cmbGrade_PersonnelSearch.DataBind();


            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Grade_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Grade_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Grade_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //DNN Note: Add CostCenter----------
        protected void CallBack_cmbCostCenter_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbCostCenter_PersonnelSearch.Dispose();
            this.Fill_cmbCostCenter_PersonnelSearch();
            this.ErrorHiddenField_CostCenter_PersonnelSearch.RenderControl(e.Output);
            this.cmbCostCenter_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbCostCenter_PersonnelSearch()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<GTS.Clock.Model.BaseInformation.CostCenter> CostCenterList = PersonnelSearchBusiness.GetAllCostCenter();
                cmbCostCenter_PersonnelSearch.DataSource = CostCenterList;
                cmbCostCenter_PersonnelSearch.DataBind();


            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CostCenter_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CostCenter_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CostCenter_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        //End of DNN Note----------

        protected void CallBack_cmbUIValidationRulesGroup_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            //AttackDefender.CSRFDefender(this.Page);
            //this.cmbUiValidationRulesGroup_PersonnelSearch.Dispose();
            //this.Fill_cmbUiValidationRulesGroup_PersonnelSearch();
            //this.ErrorHiddenField_UiValidationRulesGroup_PersonnelSearch.RenderControl(e.Output);
            //this.cmbUiValidationRulesGroup_PersonnelSearch.RenderControl(e.Output);
            AttackDefender.CSRFDefender(this.Page);
            this.cmbUIValidationRulesGroup_PersonnelSearch.Dispose();
            this.Fill_cmbUIValidationRulesGroup_PersonnelSearch();
            this.ErrorHiddenField_UIValidationRulesGroup_PersonnelSearch.RenderControl(e.Output);
            this.cmbUIValidationRulesGroup_PersonnelSearch.RenderControl(e.Output);
        }
        private void Fill_cmbUIValidationRulesGroup_PersonnelSearch()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<UIValidationGroup> UiValidationGroupList = bUIValidationGroup.GetAllUiValidationGroup();
                foreach (UIValidationGroup uiValidationItem in UiValidationGroupList)
                {
                    TreeViewNode trvEmployType = new TreeViewNode();
                    trvEmployType.Text = uiValidationItem.Name;
                    trvEmployType.Value = uiValidationItem.ID.ToString();
                    trvEmployType.ID = uiValidationItem.ID.ToString();
                    trvEmployType.ShowCheckBox = true;
                    this.trvUIValidationRulesGroup_PersonnelSearch.Nodes.Add(trvEmployType);
                }
                //this.cmbUiValidationRulesGroup_PersonnelSearch.DataSource = UiValidationGroupList;
                //this.cmbUiValidationRulesGroup_PersonnelSearch.DataBind();                
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RuleGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RuleGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RuleGroups_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }
        protected void CallBack_cmbContract_PersonnelSearch_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbContract_PersonnelSearch.Dispose();
            this.Fill_cmbContract_PersonnelSearch();
            this.ErrorHiddenField_Contract_PersonnelSearch.RenderControl(e.Output);
            this.cmbContract_PersonnelSearch.RenderControl(e.Output);
        }

        private void Fill_cmbContract_PersonnelSearch()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<GTS.Clock.Model.Contracts.Contract> contractsList = this.PersonnelSearchBusiness.GetAllContract();
                this.cmbContract_PersonnelSearch.DataSource = contractsList;
                this.cmbContract_PersonnelSearch.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Contract_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Contract_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Contract_PersonnelSearch.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        [Ajax.AjaxMethod("ViewPrentDepartments_PersonnelSearchPage", "ViewPrentDepartments_PersonnelSearchPage_onCallBack", null, null)]
        public string[] ViewPrentDepartments_PersonnelSearchPage(string depId)
        {
            string[] retMessage = new string[4];
            try
            {
                AttackDefender.CSRFDefender(this.Page);
                string parentDepertments = string.Empty;
                int DepId = int.Parse(this.StringBuilder.CreateString(depId), CultureInfo.InvariantCulture);
                IList<Department> ParentDepartments = this.departmentObj.GetParentDepartments(DepId);
                foreach (Department dep in ParentDepartments)
                {
                    if (dep.Parent != null)
                    {
                        if (parentDepertments == string.Empty)
                            parentDepertments += dep.Name;
                        else
                            parentDepertments += "," + dep.Name;
                    }
                }
                retMessage[2] = JsSerializer.Serialize(parentDepertments);
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

        [Ajax.AjaxMethod("ViewPrentOrganizationPosts_PersonnelSearchPage", "ViewPrentOrganizationPosts_PersonnelSearchPage_onCallBack", null, null)]
        public string[] ViewPrentOrganizationPosts_PersonnelSearchPage(string postId)
        {
            string[] retMessage = new string[4];
            try
            {
                AttackDefender.CSRFDefender(this.Page);
                string parentOrganizationPosts = string.Empty;
                int PostId = int.Parse(this.StringBuilder.CreateString(postId), CultureInfo.InvariantCulture);
                IList<OrganizationUnit> ParentOrganizationPosts = this.OrganizationBusiness.GetParentOrganizationPosts(PostId);
                foreach (OrganizationUnit Post in ParentOrganizationPosts)
                {
                    if (Post.Parent != null)
                    {
                        if (parentOrganizationPosts == string.Empty)
                            parentOrganizationPosts += Post.Name;
                        else
                            parentOrganizationPosts += "," + Post.Name;
                    }
                }
                retMessage[2] = JsSerializer.Serialize(parentOrganizationPosts);
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