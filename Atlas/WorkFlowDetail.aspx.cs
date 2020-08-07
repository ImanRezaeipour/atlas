using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business;
using GTS.Clock.Model;
using System.IO;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Charts;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class WorkFlowDetail : GTSBasePage
    {
        public enum LoadStateSearch
        {
            Normal,
            WorkFlow,
            Manager,
            Operator,
            Substitute,
            Personnel
        }
        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch
        }
        public AdvancedPersonnelSearchProvider APSProv
        {
            get
            {
                return new AdvancedPersonnelSearchProvider();
            }
        }
        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)(new BPerson());
            }
        }
        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }
        public BPrecardAccessGroup AccessGroupBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPrecardAccessGroup>();
            }
        }
        public BSubstitute SubstituteBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BSubstitute>();
            }
        }
        public BOperator OperatorBusiness
        {

            get
            {
                return BusinessHelper.GetBusinessInstance<BOperator>();
            }
        }
        public BFlow FlowBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BFlow>();
            }
        }
        public BPerson PersonBusiness
        {
            get
            {
                return new BPerson();
            }
        }
        public BManager ManagerBusiness
        {
            get
            {
                return new BManager();
            }
        }
        public BFlowGroup FlowGroupBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BFlowGroup>();
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
            DialogWorkFlowDetail_Operations,
            WorkFlowDetail_onPageLoad,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations,
            DropDownDive,
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!CallBack_cmbSearchField_WorkFlowDetail.IsCallback && !CallBack_cmbPersonnel_WorkFlowDetail.IsCallback)
            {
                RefererValidationProvider.CheckReferer();
                Page WorkFlowDetailPage = this;
                Ajax.Utility.GenerateMethodScripts(WorkFlowDetailPage);
                this.SetPersonnelPageSize_cmbPersonnel_WorkFlowDetail();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckWorkFlowDetailLoadAccess_WorkFlowDetail();
            }
            
        }
        private void SetPersonnelPageSize_cmbPersonnel_WorkFlowDetail()
        {
            this.hfPersonnelPageSize_WorkFlowDetail.Value = this.cmbPersonnel_WorkFlowDetail.DropDownPageSize.ToString();
        }
        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        /// <summary>
        /// تنظیم زبان انتخابی کاربر 
        /// </summary>
        /// <param name="LangID"></param>
        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }
        private void CheckWorkFlowDetailLoadAccess_WorkFlowDetail()
        {
            string[] retMessage = new string[4];
            try
            {
                this.FlowGroupBusiness.CheckWorkFlowDetailLoadAccess();
            }
            catch (BaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
            }
        }
        protected void CallBack_cmbSearchField_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            this.cmbSearchField_WorkFlowDetail.Dispose();
            this.Fill_cmbSearchField_WorkFlowDetail();
            this.ErrorHiddenField_WorkFlowDetailSearchField.RenderControl(e.Output);
            this.cmbSearchField_WorkFlowDetail.RenderControl(e.Output);
        }
        private void Fill_cmbSearchField_WorkFlowDetail()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                foreach (WorkFlowDetailSearch workFlowDetail in Enum.GetValues(typeof(WorkFlowDetailSearch)))
                {
                    ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                    cbItem.Text = GetLocalResourceObject(workFlowDetail.ToString()).ToString();
                    cbItem.Value = (workFlowDetail).ToString();
                    this.cmbSearchField_WorkFlowDetail.Items.Add(cbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlowDetailSearchField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlowDetailSearchField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlowDetailSearchField.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_GridManagers_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            string SearchValue = this.StringBuilder.CreateString(e.Parameters[0]);
             LoadStateSearch State = ( LoadStateSearch)Enum.Parse(typeof( LoadStateSearch), this.StringBuilder.CreateString(e.Parameters[1]));
            decimal ID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
            bool MatchWholeWord = bool.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_GridManagers_WorkFlowDetail(SearchValue, State, ID, MatchWholeWord);
            this.GridManagers_WorkFlowDetail.RenderControl(e.Output);
            this.ErrorHiddenField_Managers_WorkFlowDetail.RenderControl(e.Output);
        }
        private void Fill_GridManagers_WorkFlowDetail(string searchValue, LoadStateSearch state, decimal Id, bool matchWholeWord)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<Manager> ManagerList = null;
                switch (state)
                {
                    case  LoadStateSearch.Normal:
                        ManagerList = this.ManagerBusiness.GetAllManagers(searchValue, matchWholeWord);
                        break;
                    case  LoadStateSearch.WorkFlow:
                        ManagerList = this.FlowBusiness.GetManagerFlow(Id);
                        break;
                    case  LoadStateSearch.Substitute:
                        ManagerList = this.ManagerBusiness.GetSubstituteManagers(Id);
                        break;
                }
                this.GridManagers_WorkFlowDetail.DataSource = ManagerList;
                this.GridManagers_WorkFlowDetail.DataBind();

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_GridWorkFlows_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            string SearchValue = this.StringBuilder.CreateString(e.Parameters[0]);
             LoadStateSearch State = ( LoadStateSearch)Enum.Parse(typeof( LoadStateSearch), this.StringBuilder.CreateString(e.Parameters[1]));
            decimal ID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
            bool IsMatchWholWord = bool.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_GridWorkFlows_WorkFlowDetail(SearchValue, State, ID, IsMatchWholWord);
            this.GridWorkFlows_WorkFlowDetail.RenderControl(e.Output);
            this.ErrorHiddenField_WorkFlows_WorkFlowDetail.RenderControl(e.Output);
        }
        private void Fill_GridWorkFlows_WorkFlowDetail(string searchValue, LoadStateSearch state, decimal Id, bool isMatchWholWord)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<Flow> FlowList = null;
                switch (state)
                {
                    case  LoadStateSearch.Normal:
                        FlowList = this.FlowBusiness.SearchFlow(FlowSearchFields.FlowName, searchValue, isMatchWholWord);
                        break;
                    case  LoadStateSearch.Manager:
                        FlowList = this.FlowBusiness.GetManagerWorkFlows(Id);
                        break;
                    case  LoadStateSearch.Operator:
                        FlowList = this.FlowBusiness.GetOperatorWorkFlows(Id);
                        break;
                    case LoadStateSearch.Personnel:
                        FlowList = this.FlowBusiness.GetPersonnlWorkFlows(Id);
                        break;
                }
                this.GridWorkFlows_WorkFlowDetail.DataSource = FlowList;
                this.GridWorkFlows_WorkFlowDetail.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_GridOperator_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            string SearchValue = this.StringBuilder.CreateString(e.Parameters[0]);
             LoadStateSearch State = ( LoadStateSearch)Enum.Parse(typeof( LoadStateSearch), this.StringBuilder.CreateString(e.Parameters[1]));
            decimal ID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
            bool IsMatchWholWord = bool.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_GridOperator_WorkFlowDetail(SearchValue, State, ID, IsMatchWholWord);
            this.GridOperator_WorkFlowDetail.RenderControl(e.Output);
            this.ErrorHiddenField_Operator_WorkFlowDetail.RenderControl(e.Output);
        }
        private void Fill_GridOperator_WorkFlowDetail(string searchValue, LoadStateSearch state, decimal Id, bool isMatchWholWord)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<Operator> OperatorList = null;
                switch (state)
                {
                    case  LoadStateSearch.Normal:
                        OperatorList = this.OperatorBusiness.GetAllOperators(searchValue, isMatchWholWord);
                        break;
                    case  LoadStateSearch.WorkFlow:
                        OperatorList = this.OperatorBusiness.GetAllByFlowId(Id);
                        break;
                }

                this.GridOperator_WorkFlowDetail.DataSource = OperatorList;
                this.GridOperator_WorkFlowDetail.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_GridSubstitute_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            string SearchValue = this.StringBuilder.CreateString(e.Parameters[0]);
             LoadStateSearch State = ( LoadStateSearch)Enum.Parse(typeof( LoadStateSearch), this.StringBuilder.CreateString(e.Parameters[1]));
            decimal ID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[2]));
            bool IsMatchWholWord = bool.Parse(this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_GridSubstitute_WorkFlowDetail(SearchValue, State, ID, IsMatchWholWord);
            this.GridSubstitute_WorkFlowDetail.RenderControl(e.Output);
            this.ErrorHiddenField_Substitute_WorkFlowDetail.RenderControl(e.Output);
        }
        private void Fill_GridSubstitute_WorkFlowDetail(string searchValue, LoadStateSearch state, decimal Id, bool isMatchWholWord)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<GTS.Clock.Model.RequestFlow.Substitute> SubstituteList = null;
                switch (state)
                {
                    case  LoadStateSearch.Normal:
                        SubstituteList = this.SubstituteBusiness.GetAllSubstitutes(searchValue, isMatchWholWord);
                        break;
                    case  LoadStateSearch.Manager:
                        SubstituteList = this.SubstituteBusiness.GetAllByManager(Id, string.Empty);
                        break;
                }
                this.GridSubstitute_WorkFlowDetail.DataSource = SubstituteList;
                this.GridSubstitute_WorkFlowDetail.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_WorkFlows_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_trvUnderManagementPersonnel_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            Fill_trvUnderManagementPersonnel_WorkFlowDetail(decimal.Parse(this.StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture));
            this.ErrorHiddenField_UnderManagementPersonnel_WorkFlowDetail.RenderControl(e.Output);
            this.trvUnderManagementPersonnel_WorkFlowDetail.RenderControl(e.Output);
        }
        private void Fill_trvUnderManagementPersonnel_WorkFlowDetail(decimal flowID)
        {
            string[] retMessage = new string[4];
            string imageUrl = "Images\\TreeView\\folder.gif";
            string imagePath = "Images/TreeView/folder.gif";
            this.InitializeCulture();
            try
            {
                Department rootDepartment = this.FlowBusiness.GetDepartmentRoot();
                if (SessionHelper.HasSessionValue(SessionHelper.OrganizationWorkFlowDepartments))
                    SessionHelper.ClearSessionValue(SessionHelper.OrganizationWorkFlowDepartments);
                IList<Department> departmentsList = new BDepartment().GetAll();
                SessionHelper.SaveSessionValue(SessionHelper.OrganizationWorkFlowDepartments, departmentsList);
                TreeViewNode rootDepartmentNode = new TreeViewNode();
                rootDepartmentNode.ID = rootDepartment.ID.ToString();
                string rootOrgPostNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvPosts_Post") != null)
                    rootOrgPostNodeText = GetLocalResourceObject("OrgNode_trvPosts_Post").ToString();
                else
                    rootOrgPostNodeText = rootDepartment.Name;
                rootDepartmentNode.Text = rootOrgPostNodeText;
                rootDepartmentNode.Value = rootDepartment.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    rootDepartmentNode.ImageUrl = imagePath;
                this.trvUnderManagementPersonnel_WorkFlowDetail.Nodes.Add(rootDepartmentNode);
                IList<Department> DepartmentChildList = this.FlowBusiness.GetDepartmentChilds(rootDepartment.ID, flowID, departmentsList);
                foreach (Department childDepartment in DepartmentChildList)
                {
                    TreeViewNode childOrgPostNode = new TreeViewNode();
                    childOrgPostNode.ID = childDepartment.ID.ToString();
                    childOrgPostNode.Text = childDepartment.Name;
                    childOrgPostNode.Value = ((int)UnderManagmentTypes.Department).ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                        childOrgPostNode.ImageUrl = imagePath;
                    childOrgPostNode.ContentCallbackUrl = "XmlUnderManagementPersonnelLoadonDemand.aspx?FlowID=" + flowID + "&ParentDepartmentID=" + childDepartment.ID + "&LangID=" + this.LangProv.GetCurrentLanguage();
                    if (this.FlowBusiness.GetDepartmentChilds(childDepartment.ID, flowID, departmentsList).Count > 0 || this.FlowBusiness.GetDepartmentPerson(childDepartment.ID).Count > 0)
                        childOrgPostNode.Nodes.Add(new TreeViewNode());
                    rootDepartmentNode.Nodes.Add(childOrgPostNode);
                }
                if (DepartmentChildList.Count > 0 || this.FlowBusiness.GetDepartmentPerson(rootDepartment.ID).Count > 0)
                    rootDepartmentNode.Expanded = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        protected void CallBack_trvAccessLevel_AccessGroups_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal AccessGroupId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]));
            this.Fill_trvAccessLevel_AccessGroups(AccessGroupId);
            this.hfAccessLevelsList_AccessGroups.RenderControl(e.Output);
            this.ErrorHiddenField_AccessLevel.RenderControl(e.Output);
            this.trvAccessLevel_AccessGroups.RenderControl(e.Output);
        }

        private void Fill_trvAccessLevel_AccessGroups(decimal accessGroupID)
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                string ImageUrl = "Images/TreeView/folder.gif";
                string ImagePath = "Images\\TreeView\\folder.gif";
                this.hfAccessLevelsList_AccessGroups.Value = string.Empty;
                IList<PrecardGroups> PrecardGroupsList = this.AccessGroupBusiness.GetPrecardTree(accessGroupID);
                foreach (PrecardGroups precardGroupsItem in PrecardGroupsList)
                {
                    TreeViewNode trvNodePrecardGroups = new TreeViewNode();
                    trvNodePrecardGroups.Text = precardGroupsItem.Name;
                    trvNodePrecardGroups.ID = precardGroupsItem.ID.ToString();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                        trvNodePrecardGroups.ImageUrl = ImageUrl;
                    IList<Precard> PrecardList = this.AccessGroupBusiness.GetPrecardGroupChilds(precardGroupsItem.ID);
                    PrecardList = PrecardList.Where(x => x.ContainInPrecardAccessGroup).ToList();
                    if (PrecardList.Count != 0)
                    {
                        foreach (Precard precardItem in PrecardList)
                        {
                            TreeViewNode trvNodePreCard = new TreeViewNode();
                            trvNodePreCard.Text = precardItem.Name;
                            trvNodePreCard.ID = precardItem.ID.ToString();
                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImagePath))
                                trvNodePreCard.ImageUrl = ImageUrl;
                            trvNodePrecardGroups.Nodes.Add(trvNodePreCard);
                        }
                        this.trvAccessLevel_AccessGroups.Nodes.Add(trvNodePrecardGroups);
                    }
                    else
                    {
                        trvNodePrecardGroups.Dispose();
                    }
                }

            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_AccessLevel.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }
        private void CreateAccessLevelsList_AccessGroups(string ID, bool Checked, string parentID)
        {
            if (Checked)
                this.hfAccessLevelsList_AccessGroups.Value += "ID=" + ID + "%Ch=" + Checked.ToString().ToLower() + "%P=" + parentID + "#";
        }

        //protected void trvAccessLevel_AccessGroups_NodeExpanded(object sender, TreeViewNodeEventArgs e)
        //{

        //}
        protected void CallBack_cmbPersonnel_WorkFlowDetail_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbPersonnel_WorkFlowDetail.Dispose();
            this.SetPersonnelPageCount_cmbPersonnel_WorkFlowDetail((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_cmbPersonnel_WorkFlowDetail((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.cmbPersonnel_WorkFlowDetail.Enabled = true;
            this.cmbPersonnel_WorkFlowDetail.RenderControl(e.Output);
            this.hfPersonnelPageCount_WorkFlowDetail.RenderControl(e.Output);
            this.ErrorHiddenField_Personnel_WorkFlowDetail.RenderControl(e.Output);
        }

        private void Fill_cmbPersonnel_WorkFlowDetail(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
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
                    this.cmbPersonnel_WorkFlowDetail.Items.Add(personCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        private void SetPersonnelPageCount_cmbPersonnel_WorkFlowDetail(LoadState Ls, int pageSize, string SearchTerm)
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
                this.hfPersonnelPageCount_WorkFlowDetail.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Personnel_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Personnel_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Personnel_WorkFlowDetail.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateUnderManagmentPersons_WorkFlowDetailPage", "UpdateUnderManagmentPersons_WorkFlowDetailPage_onCallBack", null, null)]
        public string[] UpdateUnderManagmentPersons_WorkFlowDetailPage(string flowId)
        {
            this.InitializeCulture();

            string[] retMessage = new string[3];

            try
            {
                decimal FlowId = decimal.Parse(this.StringBuilder.CreateString(flowId), CultureInfo.InvariantCulture);
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                string SuccessMessageBody = string.Empty;
                FlowBusiness.UpdateUnderManagmentPersons(FlowId);
                SuccessMessageBody = GetLocalResourceObject("RetrieveUnderManagementPersonnelsComplete").ToString();
                retMessage[1] = SuccessMessageBody;
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
