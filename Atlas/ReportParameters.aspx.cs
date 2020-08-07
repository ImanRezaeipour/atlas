using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Reporting;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Utility;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Report;
using Stimulsoft.Report;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Charts;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.Contracts;
namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ReportParameters : GTSBasePage
    {
        public BReportParameter ReportParameterBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BReportParameter>();
            }
        }
        public BDepartment DepartmentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDepartment>();
            }
        }
        public enum LoadState
        {
            Normal,
            Search
        }

        public enum PersonnelFilterType
        {
            Personal,
            Group,
            SelectInGroup
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
        internal class PersonnelDetails
        {
            public string ID { get; set; }
            public string OrganizationPostID { get; set; }
            public string OrganizationPostName { get; set; }
            public string RoleID { get; set; }
            public string RoleName { get; set; }
        }

        internal class OrganizationPostNodeValue
        {
            public string CustomCode { get; set; }
            public string ParentPath { get; set; }
            public string PersonnelName { get; set; }
            public string PersonnelCode { get; set; }
            public string PersonnelID { get; set; }
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

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }
        public BOrganizationUnit OrganizationBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BOrganizationUnit>();
            }
        }
        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)BusinessHelper.GetBusinessInstance<BPerson>();
            }
        }

        enum Scripts
        {
            ReportParameters_onPageLoad,
            DialogReportParameters_onPageLoad,
            DialogReportParameters_Operations,
            DialogInternalReportParameters_onPageLoad,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbCalculationRange_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbControlStation_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbDepartment_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbEmployType_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbMarriageState_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbMilitaryState_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters.IsCallback && !CallBack_cmbPersonnel_PersonalFilter_ReportParameters.IsCallback && !CallBack_cmbRuleGroup_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbSex_GroupFilter_ReportParameters.IsCallback && !CallBack_cmbWorkGroup_GroupFilter_ReportParameters.IsCallback && !CallBack_GridReportParameters_ReportParameters.IsCallback && !CallBack_GridPersonnel_PersonnelSelect.IsCallback && !CallBack_cmbContract_GroupFilter_ReportParameters.IsCallback)
            {
                System.Web.UI.Page ReportParametersPage = this;
                Ajax.Utility.GenerateMethodScripts(ReportParametersPage);

                this.CheckReportParametersLoadAccess_ReportParameters();
                this.ViewCurrentLangCalendars_ReportParameters();
                this.SetCurrentDate_ReportParameters();
                this.SetPersonnelPageSize_cmbPersonnel_ReportParameters();
                this.SetSelectivePersonnelPageSize_PersonnelSelect();
                this.InitializeSkin();
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckReportParametersLoadAccess_ReportParameters()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ReportParameterBusiness.CheckReportParametersLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }


        private void ViewCurrentLangCalendars_ReportParameters()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_RuleGroup_GroupFilter_ReportParameters.Visible = true;
                    this.Container_pdpRunFromDate_GroupFilter_ReportParameters.Visible = true;
                    this.Container_pdpToDate_RuleGroup_GroupFilter_ReportParameters.Visible = true;
                    this.Container_pdpWorkGroup_GroupFilter_ReportParameters.Visible = true;
                    this.Container_pdpFromDate_Contract_GroupFilter_ReportParameters.Visible = true;
                    this.Container_pdpToDate_Contract_GroupFilter_ReportParameters.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpFromDate_RuleGroup_GroupFilter_ReportParameters.Visible = true;
                    this.Container_gdpRunFromDate_GroupFilter_ReportParameters.Visible = true;
                    this.Container_gdpToDate_RuleGroup_GroupFilter_ReportParameters.Visible = true;
                    this.Container_gdpWorkGroup_GroupFilter_ReportParameters.Visible = true;
                    this.Container_gdpFromDate_Contract_GroupFilter_ReportParameters.Visible = true;
                    this.Container_gdpToDate_Contract_GroupFilter_ReportParameters.Visible = true;
                    break;
            }
        }

        private void InitializeSkin()
        {
            SkinHelper.InitializeSkin(this.Page);
            SkinHelper.SetRelativeTabStripImageBaseUrl(this.Page, this.TabStripReportParameters);
        }

        [Ajax.AjaxMethod("GetLoadonDemandError_ReportParametersPage", "GetLoadonDemandError_ReportParametersPage_onCallBack", null, null)]
        public string GetLoadonDemandError_ReportParametersPage()
        {
            this.InitializeCulture();
            AttackDefender.CSRFDefender(this.Page);
            string retError = string.Empty;
            if (Session["LoadonDemandError_ReportParametersPage"] != null)
            {
                retError = Session["LoadonDemandError_ReportParametersPage"].ToString();
                Session["LoadonDemandError_ReportParametersPage"] = null;
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


        private void SetPersonnelPageSize_cmbPersonnel_ReportParameters()
        {
            this.hfPersonnelPageSize_ReportParameters.Value = this.cmbPersonnel_PersonalFilter_ReportParameters.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_PersonalFilter_ReportParameters(LoadState Ls, int pageSize, GTS.Clock.Business.Proxy.PersonAdvanceSearchProxy personAdvanceSearchProxy)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.ReportParameterBusiness.GetAllPaeronsCount();
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.ReportParameterBusiness.GetAllPaeronsCount(personAdvanceSearchProxy);
                        break;
                    default:
                        break;
                }
                this.hfPersonnelPageCount_PersonalFilter_ReportParameters.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
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

        private void SetCurrentDate_ReportParameters()
        {
            string strCurrentDate = string.Empty;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "en-US":
                    strCurrentDate = DateTime.Now.ToShortDateString();
                    break;
                case "fa-IR":
                    strCurrentDate = this.LangProv.GetSysDateString(DateTime.Now);
                    break;
            }
            this.hfCurrentDate_ReportParameters.Value = strCurrentDate;
        }

        protected void CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbCalculationRange_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbCalculationRange_GroupFilter_ReportParameters();
            this.ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbCalculationRange_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbCalculationRange_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<CalculationRangeGroup> CalculationRangesList = this.ReportParameterBusiness.GetAllDateRanges();
                this.cmbCalculationRange_GroupFilter_ReportParameters.DataSource = CalculationRangesList;
                this.cmbCalculationRange_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbControlStation_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbControlStation_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbControlStation_GroupFilter_ReportParameters();
            this.ErrorHiddenField_ControlStation_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbControlStation_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbControlStation_GroupFilter_ReportParameters()
        {
            Fill_trvControlStation_GroupFilter_ReportParameters();
        }
        private void Fill_trvControlStation_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<ControlStation> ControlStationsList = this.ReportParameterBusiness.GetAllControlStations();
                foreach (ControlStation controlStationItem in ControlStationsList)
                {
                    TreeViewNode trvControlStation = new TreeViewNode();
                    trvControlStation.Text = controlStationItem.Name;
                    trvControlStation.Value = controlStationItem.ID.ToString();
                    trvControlStation.ID = controlStationItem.ID.ToString();
                    trvControlStation.ShowCheckBox = true;
                    this.trvControlStation_GroupFilter_ReportParameters.Nodes.Add(trvControlStation);
                }
                //this.cmbControlStation_GroupFilter_ReportParameters.DataSource = ControlStationsList;
                //this.cmbControlStation_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_ControlStation_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_ControlStation_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_ControlStation_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbDepartment_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartment_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbDepartment_GroupFilter_ReportParameters();
            this.ErrorHiddenField_Department_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbDepartment_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbDepartment_GroupFilter_ReportParameters()
        {
            this.Fill_trvDepartment_GroupFilter_ReportParameters();
        }

        private void Fill_trvDepartment_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Department> departmentsList = this.ReportParameterBusiness.GetAllDepartments();
                Department rootDep = this.ReportParameterBusiness.GetDepartmentRoot();
                TreeViewNode rootDepNode = new TreeViewNode();
                rootDepNode.ShowCheckBox = true;
                rootDepNode.ID = rootDep.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDepartment_GroupFilter_ReportParameters") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDepartment_GroupFilter_ReportParameters").ToString();
                else
                    rootDepNodeText = rootDep.Name;
                rootDepNode.Text = rootDepNodeText;
                rootDepNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                    rootDepNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                this.trvDepartment_GroupFilter_ReportParameters.Nodes.Add(rootDepNode);
                rootDepNode.Expanded = true;

                this.GetChildDepartment_trvDepartment_GroupFilter_ReportParameters(departmentsList, rootDepNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Department_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Department_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Department_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildDepartment_trvDepartment_GroupFilter_ReportParameters(IList<Department> departmentsList, TreeViewNode parentDepartmentNode, Department parentDepartment)
        {
            foreach (Department childDep in this.ReportParameterBusiness.GetDepartmentChilds(parentDepartment.ID, departmentsList))
            {
                TreeViewNode childDepNode = new TreeViewNode();
                childDepNode.ShowCheckBox = true;
                childDepNode.ID = childDep.ID.ToString();
                childDepNode.Text = childDep.Name;
                childDepNode.Value = childDep.CustomCode;
                childDepNode.ImageUrl = parentDepartmentNode.ImageUrl;

                parentDepartmentNode.Nodes.Add(childDepNode);
                try
                {
                    if (parentDepartmentNode.Parent != null)
                        if (parentDepartmentNode.Parent.Parent == null)
                            parentDepartmentNode.Expanded = true;
                }
                catch
                { }
                if (this.ReportParameterBusiness.GetDepartmentChilds(childDep.ID, departmentsList).Count > 0)
                    this.GetChildDepartment_trvDepartment_GroupFilter_ReportParameters(departmentsList, childDepNode, childDep);
            }
        }


        protected void CallBack_cmbEmployType_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbEmployType_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbEmployType_GroupFilter_ReportParameters();
            this.ErrorHiddenField_EmployType_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbEmployType_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbEmployType_GroupFilter_ReportParameters()
        {
            Fill_trvEmployType_GroupFilter_ReportParameters();
        }
        private void Fill_trvEmployType_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<EmploymentType> EmployTypesList = this.ReportParameterBusiness.GetAllEmploymentTypes();
                foreach (EmploymentType employmentItem in EmployTypesList)
                {
                    TreeViewNode trvEmployType = new TreeViewNode();
                    trvEmployType.Text = employmentItem.Name;
                    trvEmployType.Value = employmentItem.ID.ToString();
                    trvEmployType.ID = employmentItem.ID.ToString();
                    trvEmployType.ShowCheckBox = true;
                    this.trvEmployType_GroupFilter_ReportParameters.Nodes.Add(trvEmployType);
                }
                //this.cmbEmployType_GroupFilter_ReportParameters.DataSource = EmployTypesList;
                //this.cmbEmployType_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_EmployType_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_EmployType_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_EmployType_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbUIValidationGroup_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbUIValidationGroup_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbUIValidationGroup_GroupFilter_ReportParameters();
            this.ErrorHiddenField_UIValidationGroup_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbUIValidationGroup_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbUIValidationGroup_GroupFilter_ReportParameters()
        {
            Fill_trvUIValidationGroup_GroupFilter_ReportParameters();
        }
        private void Fill_trvUIValidationGroup_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<UIValidationGroup> UIValidationGroupList = this.ReportParameterBusiness.GetAllUIValidationGroup();
                foreach (UIValidationGroup uiValidationItem in UIValidationGroupList)
                {
                    TreeViewNode trvEmployType = new TreeViewNode();
                    trvEmployType.Text = uiValidationItem.Name;
                    trvEmployType.Value = uiValidationItem.ID.ToString();
                    trvEmployType.ID = uiValidationItem.ID.ToString();
                    trvEmployType.ShowCheckBox = true;
                    this.trvUIValidationGroup_GroupFilter_ReportParameters.Nodes.Add(trvEmployType);
                }
                //this.cmbEmployType_GroupFilter_ReportParameters.DataSource = UIValidationGroupList;
                //this.cmbEmployType_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_UIValidationGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_UIValidationGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_UIValidationGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_cmbMarriageState_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMarriageState_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbMarriageState_GroupFilter_ReportParameters();
            this.ErrorHiddenField_MarriageState_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbMarriageState_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbMarriageState_GroupFilter_ReportParameters()
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
                    this.cmbMarriageState_GroupFilter_ReportParameters.Items.Add(cmbItemMaritalStatus);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MarriageState_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MarriageState_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MarriageState_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMilitaryState_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbMilitaryState_GroupFilter_ReportParameters();
            this.ErrorHiddenField_MilitaryState_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbMilitaryState_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbMilitaryState_GroupFilter_ReportParameters()
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
                    this.cmbMilitaryState_GroupFilter_ReportParameters.Items.Add(cmbItemMilitaryStatus);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MilitaryState_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbOrganizationPost_PersonalFilter_ReportParameters.Dispose();
            this.Fill_cmbOrganizationPost_PersonalFilter_ReportParameters();
            this.ErrorHiddenField_OrganizationPost_PersonalFilter_ReportParameters.RenderControl(e.Output);
            this.cmbOrganizationPost_PersonalFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbOrganizationPost_PersonalFilter_ReportParameters()
        {
            this.Fill_trvOrganizationPost_PersonalFilter_ReportParameters();
        }

        private void Fill_trvOrganizationPost_PersonalFilter_ReportParameters()
        {
            string imageUrl = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif";
            string imagePath = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                OrganizationUnit rootOrgPost = this.ReportParameterBusiness.GetOrganizationUnitRoot();
                TreeViewNode rootOrgPostNode = new TreeViewNode();
                rootOrgPostNode.ID = rootOrgPost.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvOrganizationPost_PersonalFilter_ReportParameters") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvOrganizationPost_PersonalFilter_ReportParameters").ToString();
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
                this.trvOrganizationPost_PersonalFilter_ReportParameters.Nodes.Add(rootOrgPostNode);
                IList<OrganizationUnit> OrganizationUnitChildList = this.ReportParameterBusiness.GetOrganizationUnitChilds(rootOrgPost.ID);
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
                this.ErrorHiddenField_OrganizationPost_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPost_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbPersonnel_PersonalFilter_ReportParameters.Dispose();
            GTS.Clock.Business.Proxy.PersonAdvanceSearchProxy personAdvanceSearchProxy = this.CreatePersonnelSearchProxy_ReportParameters(this.StringBuilder.CreateString(e.Parameters[3]));
            this.SetPersonnelPageCount_cmbPersonnel_PersonalFilter_ReportParameters((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), personAdvanceSearchProxy);
            this.Fill_cmbPersonnel_PersonalFilter_ReportParameters((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), personAdvanceSearchProxy);
            this.hfPersonnelPageCount_PersonalFilter_ReportParameters.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.RenderControl(e.Output);
            this.cmbPersonnel_PersonalFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_PersonalFilter_ReportParameters(LoadState Ls, int pageSize, int pageIndex, GTS.Clock.Business.Proxy.PersonAdvanceSearchProxy personAdvanceSearchProxy)
        {
            string[] retMessage = new string[4];
            IList<Person> PersonList = null;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonList = this.ReportParameterBusiness.GetAllPersons(pageIndex, pageSize);
                        break;
                    case LoadState.Search:
                        PersonList = this.ReportParameterBusiness.GetAllPersons(personAdvanceSearchProxy, pageIndex, pageSize);
                        break;
                }
                foreach (Person personItem in PersonList)
                {
                    ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                    personCmbItem["BarCode"] = personItem.BarCode;
                    personCmbItem["CardNum"] = personItem.CardNum;
                    PersonnelDetails personnelDetails = new PersonnelDetails();
                    personnelDetails.ID = personItem.ID.ToString();
                    personnelDetails.OrganizationPostID = personItem.OrganizationUnit.ID.ToString();
                    personnelDetails.OrganizationPostName = personItem.OrganizationUnit.Name;
                    personnelDetails.RoleID = personItem.User.Role.ID.ToString();
                    personnelDetails.RoleName = personItem.User.Role.Name;
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    personCmbItem.Value = jsSerializer.Serialize(personnelDetails);
                    this.cmbPersonnel_PersonalFilter_ReportParameters.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonalFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private GTS.Clock.Business.Proxy.PersonAdvanceSearchProxy CreatePersonnelSearchProxy_ReportParameters(string SearchTerm)
        {
            GTS.Clock.Business.Proxy.PersonAdvanceSearchProxy personAdvanceSearchProxy = new GTS.Clock.Business.Proxy.PersonAdvanceSearchProxy();
            if (SearchTerm != string.Empty)
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                Dictionary<string, object> ParamsDic = (Dictionary<string, object>)jsSerializer.DeserializeObject(SearchTerm);
                personAdvanceSearchProxy.FirstName = ParamsDic["FirstName"].ToString();
                personAdvanceSearchProxy.LastName = ParamsDic["LastName"].ToString();
                personAdvanceSearchProxy.FatherName = ParamsDic["FatherName"].ToString();
                personAdvanceSearchProxy.PersonCode = ParamsDic["PersonnelNumber"].ToString();
                personAdvanceSearchProxy.CartNumber = ParamsDic["CardNumber"].ToString();
                if (ParamsDic["PersonActivateState"].ToString() != "null")
                    personAdvanceSearchProxy.PersonActivateState = Convert.ToBoolean(ParamsDic["PersonActivateState"].ToString());
                personAdvanceSearchProxy.OrganizationUnitId = decimal.Parse(ParamsDic["OrganizationPostID"].ToString(), CultureInfo.InvariantCulture);
            }
            return personAdvanceSearchProxy;
        }

        protected void CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbRuleGroup_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbRuleGroup_GroupFilter_ReportParameters();
            this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbRuleGroup_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbRuleGroup_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<RuleCategory> RuleGroupsList = this.ReportParameterBusiness.GetAllRuleGroups();
                this.cmbRuleGroup_GroupFilter_ReportParameters.DataSource = RuleGroupsList;
                this.cmbRuleGroup_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbSex_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbSex_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbSex_GroupFilter_ReportParameters();
            this.ErrorHiddenField_Sex_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbSex_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbSex_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (PersonSex personSexItem in Enum.GetValues(typeof(PersonSex)))
                {
                    ComboBoxItem cmbItemPersonSex = new ComboBoxItem(GetLocalResourceObject(personSexItem.ToString()).ToString());
                    cmbItemPersonSex.Value = personSexItem.ToString();
                    cmbItemPersonSex.Id = ((int)personSexItem).ToString();
                    this.cmbSex_GroupFilter_ReportParameters.Items.Add(cmbItemPersonSex);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Sex_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Sex_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Sex_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbWorkGroup_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbWorkGroup_GroupFilter_ReportParameters();
            this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbWorkGroup_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbWorkGroup_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<WorkGroup> WorkGroupsList = this.ReportParameterBusiness.GetAllWorkGroups();
                this.cmbWorkGroup_GroupFilter_ReportParameters.DataSource = WorkGroupsList;
                this.cmbWorkGroup_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridReportParameters_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            string[] paramArray = e.Parameters;
            decimal reportFileId = decimal.Parse(this.StringBuilder.CreateString(paramArray[0]), CultureInfo.InvariantCulture);
            decimal reportId = decimal.Parse(this.StringBuilder.CreateString(paramArray[1]), CultureInfo.InvariantCulture);
            bool reportIsDesigned = bool.Parse(this.StringBuilder.CreateString(paramArray[2]));
            this.Fill_GridReportParameters_ReportParameters(reportFileId, reportId, reportIsDesigned);
            this.ErroHiddenField_ReportParameters_ReportParameters.RenderControl(e.Output);
            this.GridReportParameters_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_GridReportParameters_ReportParameters(decimal ReportFileID, decimal reportId, bool isDesigned)
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {


                if (ReportFileID != 0 || reportId != 0)
                {
                    IList<ReportUIParameter> ReportParametersList = this.ReportParameterBusiness.GetUIReportParameters(ReportFileID, reportId, isDesigned);
                    this.GridReportParameters_ReportParameters.DataSource = ReportParametersList;
                    this.GridReportParameters_ReportParameters.DataBind();


                }


            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("GetReport_ReportParametersPage", "GetReport_ReportParametersPage_onCallBack", null, null)]
        public string[] GetReport_ReportParametersPage(string ReportID, string ReportFileID, string personnelFilterType, string StrPersonnelFilterObj, string StrReportParametersList, string IsDesigned, string GroupingTypeID, string GroupingByNewPage, string IsOnlyForCurrentUser, string OutPutType)
        {
            this.InitializeCulture();
            string[] retMessage = new string[6];
            try
            {

                decimal reportID = decimal.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
                AttackDefender.CSRFDefender(this.Page);
                decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID), CultureInfo.InvariantCulture);
                bool isDesigned = bool.Parse(this.StringBuilder.CreateString(IsDesigned));
                string stiReportGUID = Guid.NewGuid().ToString();
                bool isOnlyForCurrentUser = bool.Parse(this.StringBuilder.CreateString(IsOnlyForCurrentUser));


                PersonnelFilterType PFT = (PersonnelFilterType)Enum.Parse(typeof(PersonnelFilterType), this.StringBuilder.CreateString(personnelFilterType));
                PersonAdvanceSearchProxy PersonFilterProxy = this.CreatePersonnelFilterProxy_ReportParameters(PFT, this.StringBuilder.CreateString(StrPersonnelFilterObj));
                IList<ReportUIParameter> ReportParametersList = this.CreateReportParametersList_ReportParameters(this.StringBuilder.CreateString(StrReportParametersList));
                bool isPersonFilterProxyValued = this.CheckPersonnelFilterProxyIsValued_ReportParameters(PFT, PersonFilterProxy);
                decimal groupingTypeId = decimal.Parse(StringBuilder.CreateString(GroupingTypeID), CultureInfo.InvariantCulture);
                bool groupingByNewPage = bool.Parse(StringBuilder.CreateString(GroupingByNewPage));
                ReportOutPutType reportOutPutType = (ReportOutPutType)Enum.Parse(typeof(ReportOutPutType), this.StringBuilder.CreateString(OutPutType));
                if (PFT != PersonnelFilterType.Personal)
                    isOnlyForCurrentUser = false;
                StiReport stiReport = this.ReportParameterBusiness.GetReport(reportFileID, PersonFilterProxy, ReportParametersList, isDesigned, reportID, groupingTypeId, isPersonFilterProxyValued, groupingByNewPage, isOnlyForCurrentUser, reportOutPutType);

                Dictionary<string, StiReport> SysReportsDic = new Dictionary<string, StiReport>();

                if (Session["SysReports"] == null)
                    Session.Add("SysReports", SysReportsDic);
                SysReportsDic = (Dictionary<string, StiReport>)Session["SysReports"];
                if (!SysReportsDic.Keys.Contains(stiReportGUID))
                    SysReportsDic.Add(stiReportGUID, stiReport);
                Session["SysReports"] = SysReportsDic;

                string currentPage = "~/DesktopModules/Atlas/" + HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
                retMessage[0] = HttpContext.GetLocalResourceObject(currentPage, "RetSuccessType").ToString();
                retMessage[1] = HttpContext.GetLocalResourceObject(currentPage, "EditComplete").ToString();
                retMessage[2] = "success";
                retMessage[3] = stiReportGUID;
                retMessage[4] = isDesigned.ToString();
                retMessage[5] = reportOutPutType.ToString();
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

        private PersonAdvanceSearchProxy CreatePersonnelFilterProxy_ReportParameters(PersonnelFilterType PFT, string StrPersonnelFilterObj)
        {
            PersonAdvanceSearchProxy personFilterProxy = new PersonAdvanceSearchProxy();
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, object> ParamsDic = (Dictionary<string, object>)JsSerializer.DeserializeObject(StrPersonnelFilterObj);
            switch (PFT)
            {
                case PersonnelFilterType.Personal:
                    personFilterProxy.PersonId = decimal.Parse(ParamsDic["PersonnelID"].ToString(), CultureInfo.InvariantCulture);
                    break;
                case PersonnelFilterType.Group:
                    if (ParamsDic["Active"].ToString() == string.Empty)
                        personFilterProxy.PersonActivateState = null;
                    else
                        personFilterProxy.PersonActivateState = bool.Parse(ParamsDic["Active"].ToString());
                    if (int.Parse(ParamsDic["Sex"].ToString(), CultureInfo.InvariantCulture) != -1)
                        personFilterProxy.Sex = (PersonSex)Enum.ToObject(typeof(PersonSex), int.Parse(ParamsDic["Sex"].ToString(), CultureInfo.InvariantCulture));
                    personFilterProxy.Education = ParamsDic["Education"].ToString();
                    if (int.Parse(ParamsDic["MilitaryState"].ToString(), CultureInfo.InvariantCulture) != -1)
                        personFilterProxy.Military = (MilitaryStatus)Enum.ToObject(typeof(MilitaryStatus), int.Parse(ParamsDic["MilitaryState"].ToString(), CultureInfo.InvariantCulture));
                    if (int.Parse(ParamsDic["MarriageState"].ToString(), CultureInfo.InvariantCulture) != -1)
                        personFilterProxy.MaritalStatus = (MaritalStatus)Enum.ToObject(typeof(MaritalStatus), int.Parse(ParamsDic["MarriageState"].ToString(), CultureInfo.InvariantCulture));
                    personFilterProxy.DepartmentListId = new AdvancedPersonnelSearchProvider().CreateIdListFromSerializationStringId(ParamsDic["DepartmentID"].ToString());
                    personFilterProxy.IncludeSubDepartments = bool.Parse(ParamsDic["IsContainsSubDepartment"].ToString());
                    personFilterProxy.WorkGroupId = int.Parse(ParamsDic["WorkGroupID"].ToString(), CultureInfo.InvariantCulture);
                    personFilterProxy.WorkGroupFromDate = ParamsDic["WorkGroupFromDate"].ToString();
                    personFilterProxy.RuleGroupId = decimal.Parse(ParamsDic["RuleGroupID"].ToString(), CultureInfo.InvariantCulture);
                    personFilterProxy.RuleGroupFromDate = ParamsDic["RuleGroupFromDate"].ToString();
                    personFilterProxy.RuleGroupToDate = ParamsDic["RuleGroupToDate"].ToString();
                    personFilterProxy.CalculationDateRangeId = decimal.Parse(ParamsDic["CalculationRangeID"].ToString(), CultureInfo.InvariantCulture);
                    personFilterProxy.CalculationFromDate = ParamsDic["CalculationRangeFromDate"].ToString();
                    personFilterProxy.ControlStationListId = new AdvancedPersonnelSearchProvider().CreateIdListFromSerializationStringId(ParamsDic["ControlStationID"].ToString());
                    personFilterProxy.EmploymentTypeListId = new AdvancedPersonnelSearchProvider().CreateIdListFromSerializationStringId(ParamsDic["EmployTypeID"].ToString());
                    personFilterProxy.UIValidationGroupListId = new AdvancedPersonnelSearchProvider().CreateIdListFromSerializationStringId(ParamsDic["UIValidationGroupID"].ToString());
                    personFilterProxy.GradeId = decimal.Parse(ParamsDic["GradeID"].ToString(), CultureInfo.InvariantCulture);
                    personFilterProxy.ContractId = decimal.Parse(ParamsDic["ContractID"].ToString(), CultureInfo.InvariantCulture);
                    personFilterProxy.ContractFromDate = ParamsDic["ContractFromDate"].ToString();
                    personFilterProxy.ContractToDate = ParamsDic["ContractToDate"].ToString();
                    personFilterProxy.CostCenterId = decimal.Parse(ParamsDic["CostCenterID"].ToString(), CultureInfo.InvariantCulture);
                    break;
                case PersonnelFilterType.SelectInGroup:
                    personFilterProxy.PersonIdList = this.CreateSelectivePersonnelIDList_ReportParameters(ParamsDic["PersonnelIDList"].ToString());
                    break;
            }
            return personFilterProxy;
        }

        private bool CheckPersonnelFilterProxyIsValued_ReportParameters(PersonnelFilterType PFT, PersonAdvanceSearchProxy personAdvanceSearchProxy)
        {
            bool isProxyValued = false;
            switch (PFT)
            {
                case PersonnelFilterType.Personal:
                    if (personAdvanceSearchProxy.PersonId != 0)
                        isProxyValued = true;
                    break;
                case PersonnelFilterType.Group:
                    if (personAdvanceSearchProxy.Sex != null)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.Education != null && personAdvanceSearchProxy.Education != string.Empty)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.Military != null)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.MaritalStatus != null)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.DepartmentListId.Count > 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.WorkGroupId != 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.RuleGroupId != 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.CalculationDateRangeId != 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.ControlStationId != 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.EmploymentType != 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.EmploymentTypeListId.Count > 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.UIValidationGroupListId.Count > 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.GradeId != 0)
                        isProxyValued = true;
                    if (personAdvanceSearchProxy.CostCenterId != 0)
                        isProxyValued = true;
                    break;
                case PersonnelFilterType.SelectInGroup:
                    if (personAdvanceSearchProxy.PersonIdList.Count > 0)
                        isProxyValued = true;
                    break;
            }
            return isProxyValued;
        }

        public IList<decimal> CreateSelectivePersonnelIDList_ReportParameters(string StrSelectivePersonnelIDList)
        {
            IList<decimal> SelectivePersonnelIDList = new List<decimal>();
            string[] SelectivePersonnelIDCol = StrSelectivePersonnelIDList.Split(new char[] { '#' });
            foreach (string SelectivePersonnelIDColItem in SelectivePersonnelIDCol)
            {
                if (SelectivePersonnelIDColItem != null && SelectivePersonnelIDColItem != string.Empty)
                    SelectivePersonnelIDList.Add(decimal.Parse(SelectivePersonnelIDColItem, CultureInfo.InvariantCulture));
            }
            return SelectivePersonnelIDList;
        }

        private IList<ReportUIParameter> CreateReportParametersList_ReportParameters(string StrReportParametersList)
        {
            IList<ReportUIParameter> ReportParameterList = new List<ReportUIParameter>();
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            object[] ParamsBatch = (object[])JsSerializer.DeserializeObject(StrReportParametersList);
            foreach (object paramsBatchItem in ParamsBatch)
            {
                Dictionary<string, object> ParamsDic = (Dictionary<string, object>)paramsBatchItem;
                ReportUIParameter reportUIParameter = new ReportUIParameter()
                {
                    ID = decimal.Parse(ParamsDic["ID"].ToString(), CultureInfo.InvariantCulture),
                    Value = ParamsDic["Value"].ToString(),
                    ActionId = ParamsDic["ActionID"].ToString()
                };
                ReportParameterList.Add(reportUIParameter);
            }
            return ReportParameterList;
        }

        protected void CallBack_GridPersonnel_PersonnelSelect_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.SetSelectivePersonnelPageCount_PersonnelSelect(this.StringBuilder.CreateString(e.Parameters[2]));
            this.Fill_GridPersonnel_PersonnelSelect(int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[2]));
            this.hfPersonnelCount_Personnel_PersonnelSelect.RenderControl(e.Output);
            this.hfPersonnelPageCount_Personnel_PersonnelSelect.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_PersonnelSelect.RenderControl(e.Output);
            this.GridPersonnel_PersonnelSelect.RenderControl(e.Output);
        }

        private void SetSelectivePersonnelPageSize_PersonnelSelect()
        {
            this.hfPersonnelPageSize_Personnel_PersonnelSelect.Value = this.GridPersonnel_PersonnelSelect.PageSize.ToString();
        }

        private void SetSelectivePersonnelPageCount_PersonnelSelect(string SearchTerm)
        {
            int PersonnelCount = 0;
            PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
            this.hfPersonnelCount_Personnel_PersonnelSelect.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_Personnel_PersonnelSelect.Value = Utility.GetPageCount(PersonnelCount, this.GridPersonnel_PersonnelSelect.PageSize).ToString();
        }

        private void Fill_GridPersonnel_PersonnelSelect(int pageSize, int pageIndex, string SearchTerm)
        {
            string[] retMessage = new string[4];
            IList<Person> PersonnelList = null;
            try
            {
                this.InitializeCulture();
                PersonnelList = this.PersonSearchBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize, PersonCategory.Public);
                this.GridPersonnel_PersonnelSelect.DataSource = PersonnelList;
                this.GridPersonnel_PersonnelSelect.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelSelect.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelSelect.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelSelect.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelSelect.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }
        protected void CallBack_cmbDepartmentSearchResult_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartmentSearchResult_ReportParameters.Dispose();
            this.Fill_cmbDepartmentSearchResult_ReportParameters(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_DepartmentSearchResult_ReportParameters.RenderControl(e.Output);
            this.cmbDepartmentSearchResult_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbDepartmentSearchResult_ReportParameters(string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {

                IList<Department> departmentList = this.DepartmentBusiness.SearchDepartment(DepartmentSearchFields.DepartmentName, SearchTerm);

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
                    this.cmbDepartmentSearchResult_ReportParameters.Items.Add(departCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DepartmentSearchResult_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbOrganizationPostSearchResult_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbOrganizationPostSearchResult_ReportParameters.Dispose();
            this.Fill_cmbOrganizationPostSearchResult_ReportParameters(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_OrganizationPostSearchResult_ReportParameters.RenderControl(e.Output);
            this.cmbOrganizationPostSearchResult_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbOrganizationPostSearchResult_ReportParameters(string SearchTerm)
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
                    this.cmbOrganizationPostSearchResult_ReportParameters.Items.Add(organCmbItem);
                }

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_OrganizationPostSearchResult_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_OrganizationPostSearchResult_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_OrganizationPostSearchResult_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbGrade_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbGrade_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbGrade_GroupFilter_ReportParameters();
            this.ErrorHiddenField_Grade_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbGrade_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbGrade_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                IList<GTS.Clock.Model.BaseInformation.Grade> gradeList = PersonSearchBusiness.GetAllGrade();
                cmbGrade_GroupFilter_ReportParameters.DataSource = gradeList;
                cmbGrade_GroupFilter_ReportParameters.DataBind();

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Grade_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Grade_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Grade_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //DNN Note
        protected void CallBack_cmbCostCenter_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbCostCenter_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbCostCenter_GroupFilter_ReportParameters();
            this.ErrorHiddenField_CostCenter_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbCostCenter_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbCostCenter_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                IList<GTS.Clock.Model.BaseInformation.CostCenter> CostCenterList = PersonSearchBusiness.GetAllCostCenter();
                cmbCostCenter_GroupFilter_ReportParameters.DataSource = CostCenterList;
                cmbCostCenter_GroupFilter_ReportParameters.DataBind();

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CostCenter_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CostCenter_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CostCenter_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        //END of DNN Note

        protected void CallBack_cmbGroupingReport_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbGroupingReport_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbGroupingReport_GroupFilter_ReportParameters();
            this.ErrorHiddenField_GroupingReport_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbGroupingReport_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbGroupingReport_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                foreach (GroupingReport groupingReportItem in Enum.GetValues(typeof(GroupingReport)))
                {
                    ComboBoxItem cmbItemGrouping = new ComboBoxItem(GetLocalResourceObject(groupingReportItem.ToString()).ToString());
                    cmbItemGrouping.Value = ((int)groupingReportItem).ToString();
                    cmbItemGrouping.Id = ((int)groupingReportItem).ToString();
                    this.cmbGroupingReport_GroupFilter_ReportParameters.Items.Add(cmbItemGrouping);
                }
                //ComboBoxItem cmbItemNone = new ComboBoxItem(GetLocalResourceObject("None").ToString());
                //cmbItemNone.Value = "-1";

                //this.cmbGroupingReport_GroupFilter_ReportParameters.Items.Insert(0, cmbItemNone);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_GroupingReport_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_GroupingReport_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_GroupingReport_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbContract_GroupFilter_ReportParameters_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbContract_GroupFilter_ReportParameters.Dispose();
            this.Fill_cmbContract_GroupFilter_ReportParameters();
            this.ErrorHiddenField_Contract_GroupFilter_ReportParameters.RenderControl(e.Output);
            this.cmbContract_GroupFilter_ReportParameters.RenderControl(e.Output);
        }

        private void Fill_cmbContract_GroupFilter_ReportParameters()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<GTS.Clock.Model.Contracts.Contract> contractsList = this.ReportParameterBusiness.GetAllContracts();
                this.cmbContract_GroupFilter_ReportParameters.DataSource = contractsList;
                this.cmbContract_GroupFilter_ReportParameters.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

    }
}