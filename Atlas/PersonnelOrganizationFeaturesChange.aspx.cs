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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.Charts;
using System.IO;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Charts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Contracts;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class PersonnelOrganizationFeaturesChange : GTSBasePage
    {
        public BChangeOrganicInfo PersonnelOrganizationFeaturesBusiness
        {
            get 
            {
                return BusinessHelper.GetBusinessInstance<BChangeOrganicInfo>();
            }
        }

        public BDepartment DepartmentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDepartment>();
            }
        }

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

        public JavaScriptSerializer  JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }

        enum Scripts
        {
            PersonnelOrganizationFeaturesChange_onPageLoad,
            tbPersonnelOrganizationFeaturesChange_TabStripMenus_Operations,
            DropDownDive,
            HelpForm_Operations,
            Alert_Box,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange.IsCallback && !CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange.IsCallback && !CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange.IsCallback && !CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange.IsCallback && !CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange.IsCallback && !CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange.IsCallback && !CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange.IsCallback)
            {
                Page PersonnelOrganizationFeaturesPage = this;
                Ajax.Utility.GenerateMethodScripts(PersonnelOrganizationFeaturesPage);

                this.CheckPersonnelOrganizationFeaturesChangeLoadAccess_PersonnelOrganizationFeaturesChange();
                this.ViewCurrentLangCalendars_PersonnelOrganizationFeaturesChange();
                this.SetCurrentDate_PersonnelOrganizationFeaturesChange();
                this.SetPersonnelPageSize_cmbPersonnel_PersonnelOrganizationFeaturesChange();
                //this.SetPersonnelPageCount_cmbPersonnel_PersonnelOrganizationFeaturesChange(LoadState.Normal, this.cmbPersonnel_PersonnelOrganizationFeaturesChange.DropDownPageSize, string.Empty);
                this.hfPersonnelCount_PersonnelOrganizationFeaturesChange.Value = "0";
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            }
        }

        private void CheckPersonnelOrganizationFeaturesChangeLoadAccess_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            try
            {
                this.PersonnelOrganizationFeaturesBusiness.CheckPersonnelOrganizationFeaturesChangeLoadAccess();
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }

        private void ViewCurrentLangCalendars_PersonnelOrganizationFeaturesChange()
        {
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    this.Container_pdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_pdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_pdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_pdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_pdpFromDate_Contract_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_pdpToDate_Contract_PersonnelOrganizationFeaturesChange.Visible = true;
                    break;
                case "en-US":
                    this.Container_gdpFromDate_CalculationRange_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_gdpFromDate_RulesGroup_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_gdpFromDate_WorkGroup_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_gdpToDate_RulesGroup_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_gdpFromDate_Contract_PersonnelOrganizationFeaturesChange.Visible = true;
                    this.Container_gdpToDate_Contract_PersonnelOrganizationFeaturesChange.Visible = true;
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

        private void SetCurrentDate_PersonnelOrganizationFeaturesChange()
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
            this.hfCurrentDate_PersonnelOrganizationFeaturesChange.Value = strCurrentDate;
        }


        private void SetPersonnelPageSize_cmbPersonnel_PersonnelOrganizationFeaturesChange()
        {
            this.hfPersonnelPageSize_PersonnelOrganizationFeaturesChange.Value = this.cmbPersonnel_PersonnelOrganizationFeaturesChange.DropDownPageSize.ToString();
        }

        private void SetPersonnelPageCount_cmbPersonnel_PersonnelOrganizationFeaturesChange(LoadState Ls, int pageSize, string SearchTerm)
        {
            string[] retMessage = new string[4];
            int PersonnelCount = 0;
            try
            {
                switch (Ls)
                {
                    case LoadState.Normal:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(string.Empty);
                        //PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                        //PersonnelCount = 0;
                        break;
                    case LoadState.Search:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(SearchTerm);
                        break;
                    case LoadState.AdvancedSearch:
                        PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                        break;
                }
                this.hfPersonnelPageCount_PersonnelOrganizationFeaturesChange.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
                this.hfPersonnelCount_PersonnelOrganizationFeaturesChange.Value = PersonnelCount.ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbPersonnel_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbPersonnel_PersonnelOrganizationFeaturesChange.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_PersonnelOrganizationFeaturesChange((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_PersonnelOrganizationFeaturesChange((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_PersonnelOrganizationFeaturesChange.Enabled = true;
            this.cmbPersonnel_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.hfPersonnelPageCount_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.hfPersonnelCount_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_PersonnelOrganizationFeaturesChange(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                    this.cmbPersonnel_PersonnelOrganizationFeaturesChange.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbDepartment_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDepartment_PersonnelOrganizationFeaturesChange.Dispose();
            this.Fill_cmbDepartment_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_Department_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.cmbDepartment_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbDepartment_PersonnelOrganizationFeaturesChange()
        {
            this.Fill_trvDepartment_PersonnelOrganizationFeaturesChange();    
        }

        private void Fill_trvDepartment_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<Department> departmentsList = this.DepartmentBusiness.GetAll();
                Department rootDep = this.DepartmentBusiness.GetDepartmentsTree();
                TreeViewNode rootDepNode = new TreeViewNode();
                rootDepNode.ID = rootDep.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDepartment_PersonnelOrganizationFeaturesChange") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDepartment_PersonnelOrganizationFeaturesChange").ToString();
                else
                    rootDepNodeText = rootDep.Name;
                rootDepNode.Text = rootDepNodeText;
                rootDepNode.Value = rootDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                    rootDepNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                this.trvDepartment_PersonnelOrganizationFeaturesChange.Nodes.Add(rootDepNode);
                rootDepNode.Expanded = true;

                this.GetChildDepartment_trvDepartment_PersonnelOrganizationFeaturesChange(departmentsList, rootDepNode, rootDep);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Department_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Department_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Department_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void GetChildDepartment_trvDepartment_PersonnelOrganizationFeaturesChange(IList<Department> departmentsList, TreeViewNode parentDepartmentNode, Department parentDepartment)
        {
            foreach (Department childDep in this.DepartmentBusiness.GetDepartmentChilds(parentDepartment.ID, departmentsList))
            {
                TreeViewNode childDepNode = new TreeViewNode();
                childDepNode.ID = childDep.ID.ToString();
                childDepNode.Text = childDep.Name;
                childDepNode.Value = childDep.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\folder.gif"))
                    childDepNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                parentDepartmentNode.Nodes.Add(childDepNode);
                try
                {
                    if (parentDepartmentNode.Parent.Parent == null)
                        parentDepartmentNode.Expanded = true;
                }
                catch
                { }
                if (this.DepartmentBusiness.GetDepartmentChilds(childDep.ID, departmentsList).Count > 0)
                    this.GetChildDepartment_trvDepartment_PersonnelOrganizationFeaturesChange(departmentsList, childDepNode, childDep);
            }
        }

        protected void CallBack_cmbEmployType_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbEmployType_PersonnelOrganizationFeaturesChange.Dispose();
            this.Fill_cmbEmployType_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_EmployType_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.cmbEmployType_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbEmployType_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            IList<EmploymentType> EmployTypesList = null;
            try
            {
                EmployTypesList = this.PersonnelOrganizationFeaturesBusiness.GetAllEmploymentTypes();
                this.cmbEmployType_PersonnelOrganizationFeaturesChange.DataSource = EmployTypesList;
                this.cmbEmployType_PersonnelOrganizationFeaturesChange.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_EmployType_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_EmployType_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_EmployType_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbWorkGroup_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbWorkGroup_PersonnelOrganizationFeaturesChange.Dispose();
            this.Fill_cmbWorkGroup_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_WorkGroup_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.cmbWorkGroup_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbWorkGroup_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            IList<WorkGroup> WorkGroupsList = null;
            try
            {
                WorkGroupsList = this.PersonnelOrganizationFeaturesBusiness.GetAllWorkGroup();
                this.cmbWorkGroup_PersonnelOrganizationFeaturesChange.DataSource = WorkGroupsList;
                this.cmbWorkGroup_PersonnelOrganizationFeaturesChange.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkGroup_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbCalculationRange_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbCalculationRange_PersonnelOrganizationFeaturesChange.Dispose();
            this.Fill_cmbCalculationRange_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_CalculationRange_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.cmbCalculationRange_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbCalculationRange_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];

            this.InitializeCulture();
            try
            {
                IList<CalculationRangeGroup> CalculationRangeGroupList = this.PersonnelOrganizationFeaturesBusiness.GetAllDateRanges();
                this.cmbCalculationRange_PersonnelOrganizationFeaturesChange.DataSource = CalculationRangeGroupList;
                this.cmbCalculationRange_PersonnelOrganizationFeaturesChange.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_CalculationRange_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_cmbRulesGroup_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbRulesGroup_PersonnelOrganizationFeaturesChange.Dispose();
            this.Fill_cmbRulesGroup_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_RulesGroup_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.cmbRulesGroup_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbRulesGroup_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            IList<RuleCategory> RulesGroupList = null;
            try
            {
                RulesGroupList = this.PersonnelOrganizationFeaturesBusiness.GetAllRuleGroup();
                this.cmbRulesGroup_PersonnelOrganizationFeaturesChange.DataSource = RulesGroupList;
                this.cmbRulesGroup_PersonnelOrganizationFeaturesChange.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_RulesGroup_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_RulesGroup_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_RulesGroup_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        protected void CallBack_GridPersonnelProblems_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_GridPersonnelProblems_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_PersonnelProblems_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.GridPersonnelProblems_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_GridPersonnelProblems_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            try
            {
                if(SessionHelper.GetSessionValue(SessionHelper.PersonChangeOrganizationProblem)!= null)
                {
                    IList<ChangeInfoErrorProxy> changeIngoErrorProxyList = (IList<ChangeInfoErrorProxy>)SessionHelper.GetSessionValue(SessionHelper.PersonChangeOrganizationProblem);
                    GridPersonnelProblems_PersonnelOrganizationFeaturesChange.DataSource = changeIngoErrorProxyList;
                    GridPersonnelProblems_PersonnelOrganizationFeaturesChange.DataBind();
                    SessionHelper.SaveSessionValue(SessionHelper.PersonChangeOrganizationProblem, null);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_PersonnelProblems_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_PersonnelProblems_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_PersonnelProblems_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChangePage", "UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChangePage_onCallBack", null, null)]
        public string[] UpdatePersonnelOrganizationFeature_PersonnelOrganizationFeaturesChangePage(string PersonnelLoadState, string PersonnelID, string SearchTerm, string StrObjPersonnelOrganizationFeaturesTarget)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];            

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                bool OperationState = false;
                LoadState Ls = (LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(PersonnelLoadState));
                decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
                SearchTerm = this.StringBuilder.CreateString(SearchTerm);
                OrganicInfoProxy PersonnelOrganizationFeaturesTargetProxy = this.CreatePersonnelOrganizationFeaturesTarget(this.StringBuilder.CreateString(StrObjPersonnelOrganizationFeaturesTarget));
                IList<ChangeInfoErrorProxy> PersonnelProblemsList = null;
                if (personnelID == -1)
                {
                    switch (Ls)
                    {
                        case LoadState.Normal:
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoPersonnelSelected").ToString()), retMessage);
                            break;
                        case LoadState.Search:
                            OperationState = this.PersonnelOrganizationFeaturesBusiness.ChangeInfo(SearchTerm, PersonnelOrganizationFeaturesTargetProxy, out PersonnelProblemsList);
                            break;
                        case LoadState.AdvancedSearch:
                            OperationState = this.PersonnelOrganizationFeaturesBusiness.ChangeInfo(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), PersonnelOrganizationFeaturesTargetProxy, out PersonnelProblemsList);
                            break;
                    }
                }
                else
                    OperationState = this.PersonnelOrganizationFeaturesBusiness.ChangeInfo(personnelID, PersonnelOrganizationFeaturesTargetProxy, out PersonnelProblemsList);
                SessionHelper.SaveSessionValue(SessionHelper.PersonChangeOrganizationProblem, PersonnelProblemsList);
                if (PersonnelProblemsList.Count > 0)
                    OperationState = false;
                if (OperationState)
                {
                    retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                    retMessage[1] = GetLocalResourceObject("OperationComplete").ToString();
                    retMessage[2] = "success";
                }
                else
                {
                    retMessage[0] = GetLocalResourceObject("RetErrorType").ToString();
                    retMessage[1] = GetLocalResourceObject("OperationNotComplete").ToString();
                    retMessage[2] = "error";
                }
                retMessage[3] = OperationState.ToString().ToLower();
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

        private OrganicInfoProxy CreatePersonnelOrganizationFeaturesTarget(string StrObjPersonnelOrganizationFeaturesTarget)
        {
            OrganicInfoProxy PersonnelOrganizationFeaturesTargetProxy = new OrganicInfoProxy();
            Dictionary<string, object> ParamDic = (Dictionary<string, object>)this.JsSerializer.DeserializeObject(StrObjPersonnelOrganizationFeaturesTarget);
            if (decimal.Parse(ParamDic["DepartmentID"].ToString(), CultureInfo.InvariantCulture) != -1)
                PersonnelOrganizationFeaturesTargetProxy.DepartmentID = decimal.Parse(ParamDic["DepartmentID"].ToString(), CultureInfo.InvariantCulture);
            if (decimal.Parse(ParamDic["EmployTypeID"].ToString(), CultureInfo.InvariantCulture) != -1)
                PersonnelOrganizationFeaturesTargetProxy.EmploymentTypeID = decimal.Parse(ParamDic["EmployTypeID"].ToString(), CultureInfo.InvariantCulture);
            if (decimal.Parse(ParamDic["WorkGroupID"].ToString(), CultureInfo.InvariantCulture) != -1)
                PersonnelOrganizationFeaturesTargetProxy.WorkGroupID = decimal.Parse(ParamDic["WorkGroupID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic["WorkGroupFromDate"].ToString() != string.Empty)
                PersonnelOrganizationFeaturesTargetProxy.WorkGroupFromDate = ParamDic["WorkGroupFromDate"].ToString();
            if (decimal.Parse(ParamDic["CalculationRangeID"].ToString(), CultureInfo.InvariantCulture) != -1)
                PersonnelOrganizationFeaturesTargetProxy.DateRangeID = decimal.Parse(ParamDic["CalculationRangeID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic["CalculationRangeFromDate"].ToString() != string.Empty)
                PersonnelOrganizationFeaturesTargetProxy.DateRangeFromDate = ParamDic["CalculationRangeFromDate"].ToString();
            if (decimal.Parse(ParamDic["RuleGroupID"].ToString(), CultureInfo.InvariantCulture) != -1)
                PersonnelOrganizationFeaturesTargetProxy.RuleGroupID = decimal.Parse(ParamDic["RuleGroupID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic["RuleGroupFromDate"].ToString() != string.Empty)
                PersonnelOrganizationFeaturesTargetProxy.RuleGroupFromDate = ParamDic["RuleGroupFromDate"].ToString();
            if (ParamDic["RuleGroupToDate"].ToString() != string.Empty)
                PersonnelOrganizationFeaturesTargetProxy.RuleGroupToDate = ParamDic["RuleGroupToDate"].ToString();
            if (decimal.Parse(ParamDic["ContractID"].ToString(), CultureInfo.InvariantCulture) != -1)
                PersonnelOrganizationFeaturesTargetProxy.ContractID = decimal.Parse(ParamDic["ContractID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic["ContractFromDate"].ToString() != string.Empty)
                PersonnelOrganizationFeaturesTargetProxy.ContractFromDate = ParamDic["ContractFromDate"].ToString();
            if (ParamDic["ContractToDate"].ToString() != string.Empty)
                PersonnelOrganizationFeaturesTargetProxy.ContractToDate = ParamDic["ContractToDate"].ToString();
            return PersonnelOrganizationFeaturesTargetProxy;
        }

        protected void CallBack_cmbContract_PersonnelOrganizationFeaturesChange_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbContract_PersonnelOrganizationFeaturesChange.Dispose();
            this.Fill_cmbContract_PersonnelOrganizationFeaturesChange();
            this.ErrorHiddenField_Contract_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
            this.cmbContract_PersonnelOrganizationFeaturesChange.RenderControl(e.Output);
        }

        private void Fill_cmbContract_PersonnelOrganizationFeaturesChange()
        {
            string[] retMessage = new string[4];
            IList<GTS.Clock.Model.Contracts.Contract> contractsList = null;
            try
            {
                contractsList = this.PersonnelOrganizationFeaturesBusiness.GetAllContract();
                this.cmbContract_PersonnelOrganizationFeaturesChange.DataSource = contractsList;
                this.cmbContract_PersonnelOrganizationFeaturesChange.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Contract_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Contract_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Contract_PersonnelOrganizationFeaturesChange.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

    }
}