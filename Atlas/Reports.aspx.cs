using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Model.Report;
using System.IO;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using Stimulsoft.Report;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;

public partial class Reports : GTSBasePage
{
    internal class TargetDetails
    {
        public string TargetType { get; set; }
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public bool HasParameter { get; set; }
        public bool IsDesigned { get; set; }
        public string DesignedTypeCustomCode { get; set; }
        public bool IsContainsForm { get; set; }
    }

    public enum TargetType
    {
        ReportGroup,
        Report
    }

    public BReport ReportBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BReport>();
        }
    }

    public BReportParameter ReportParameterBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BReportParameter>();
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
    public BRole RoleBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BRole>();
        }
    }

    enum Scripts
    {
        Reports_onPageLoad,
        tbReports_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbReportFiles_Reports.IsCallback && !CallBack_trvReports_Reports.IsCallback && !CallBack_trvReportAccessLevels_Reports.IsCallback)
        {
            Page ReportsPage = this;
            Ajax.Utility.GenerateMethodScripts(ReportsPage);

            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckReportsLoadAccess_Reports();
        }
    }

    private void CheckReportsLoadAccess_Reports()
    {
        string[] retMessage = new string[4];
        try
        {
            this.ReportBusiness.CheckReportsLoadAccess();
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

    protected void CallBack_trvReports_Reports_onCallBack(object sender, CallBackEventArgs e)
    {
        //AttackDefender.CSRFDefender(this.Page);
        this.Fill_trvReports_Reports((LoadSttate)Enum.Parse(typeof(LoadSttate), StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[1]));
        this.ErrorHiddenField_Reports.RenderControl(e.Output);
        this.trvReports_Reports.RenderControl(e.Output);
    }

    private void Fill_trvReports_Reports(LoadSttate Ls, string SearchItem)
    {
        string[] retMessage = new string[4];
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        try
        {
            this.InitializeCulture();

            IList<Report> reportsList = this.ReportBusiness.GetAllReports();
            TreeViewNode rootRepGroupNode = new TreeViewNode();
            Report rootRepGroup = reportsList.Where(x => x.ParentId == 0).FirstOrDefault();
            if (rootRepGroup != null)
            {
                rootRepGroupNode.ID = rootRepGroup.ID.ToString();
                string rootRepGroupNodeText = string.Empty;
                if (GetLocalResourceObject("ReportsNode_trvReports_Reports") != null)
                    rootRepGroupNodeText = GetLocalResourceObject("ReportsNode_trvReports_Reports").ToString();
                else
                    rootRepGroupNodeText = rootRepGroup.Name;
                rootRepGroupNode.Text = rootRepGroupNodeText;



                TargetDetails targetDetails = new TargetDetails();
                targetDetails.TargetType = TargetType.ReportGroup.ToString();
                rootRepGroupNode.Value = JsSerializer.Serialize(targetDetails);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\group.png"))
                    rootRepGroupNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/group.png";
                this.trvReports_Reports.Nodes.Add(rootRepGroupNode);
                rootRepGroupNode.Expanded = true;



                switch (Ls)
                {
                    case LoadSttate.Normal:
                        this.GetChildItem_trvReports_Reports(rootRepGroupNode, rootRepGroup, reportsList);
                        break;
                    case LoadSttate.Search:
                        if (!string.IsNullOrEmpty(SearchItem))
                            this.GetChildItem_trvReports_Reports(rootRepGroupNode, rootRepGroup, reportsList.Where(c => c.Name.Contains(SearchItem) || c.IsReport == false).ToList());
                        else
                            this.GetChildItem_trvReports_Reports(rootRepGroupNode, rootRepGroup, reportsList);
                        break;
                }


            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void GetChildItem_trvReports_Reports(TreeViewNode parentReportNode, Report parentReport, IList<Report> reportsList)
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        string imageName = string.Empty;
        foreach (Report childItem in reportsList.Where(x => x.ParentId == parentReport.ID))
        {
            TreeViewNode childItemNode = new TreeViewNode();
            childItemNode.ID = childItem.ID.ToString();
            childItemNode.Text = childItem.Name;
            TargetDetails targetDetails = new TargetDetails();
            if (childItem.IsReport)
            {
                if (!childItem.IsDesignedReport)
                {
                    targetDetails.TargetType = TargetType.Report.ToString();
                    targetDetails.FileID = childItem.ReportFile.ID.ToString();
                    targetDetails.FileName = childItem.ReportFile.Name;
                    targetDetails.Description = childItem.ReportFile.Description;
                    targetDetails.HasParameter = childItem.HasParameter;
                    targetDetails.IsDesigned = childItem.IsDesignedReport;
                    targetDetails.DesignedTypeCustomCode = "";
                    targetDetails.IsContainsForm = childItem.IsContainsForm;
                }
                else
                {
                    targetDetails.TargetType = TargetType.Report.ToString();
                    targetDetails.FileID = "0";
                    targetDetails.FileName = "";
                    targetDetails.Description = childItem.Description;
                    targetDetails.HasParameter = childItem.HasParameter;
                    targetDetails.IsDesigned = childItem.IsDesignedReport;
                    targetDetails.DesignedTypeCustomCode = childItem.DesignedType.CustomCode.ToString();
                    targetDetails.IsContainsForm = childItem.IsContainsForm;
                }
                imageName = "report.png";
            }
            else
            {
                targetDetails.TargetType = TargetType.ReportGroup.ToString();
                imageName = "group.png";
            }
            childItemNode.Value = JsSerializer.Serialize(targetDetails);
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\" + imageName))
                childItemNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/" + imageName;
            parentReportNode.Nodes.Add(childItemNode);
            try
            {
                if (parentReportNode.Parent.Parent == null)
                    parentReportNode.Expanded = true;
            }
            catch
            { }
            if (this.ReportBusiness.GetReportChilds(childItem.ID).Count > 0)
                this.GetChildItem_trvReports_Reports(childItemNode, childItem, reportsList);
        }
    }

    protected void CallBack_cmbReportFiles_Reports_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbReportFiles_Reports.Dispose();
        this.Fill_cmbReportFiles_Reports();
        this.ErrorHiddenField_ReportFiles_Reports.RenderControl(e.Output);
        this.cmbReportFiles_Reports.RenderControl(e.Output);
    }

    private void Fill_cmbReportFiles_Reports()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<ReportFile> ReportFilesList = this.ReportBusiness.GetAllReportFiles();
            foreach (ReportFile reportFileItem in ReportFilesList)
            {
                ComboBoxItem reportFileCmbItem = new ComboBoxItem(reportFileItem.Description);
                reportFileCmbItem["Name"] = reportFileItem.Name;
                reportFileCmbItem.Value = reportFileItem.ID.ToString();
                this.cmbReportFiles_Reports.Items.Add(reportFileCmbItem);
            }
            this.cmbReportFiles_Reports.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReportFiles_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReportFiles_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReportFiles_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateReport_ReportsPage", "UpdateReport_ReportsPage_onCallBack", null, null)]
    public string[] UpdateReport_ReportsPage(string state, string mode, string SelectedTargetID, string TargetName, string ReportFileID, string ReportParentID, string IsDesignedReport)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            //AttackDefender.CSRFDefender(this.Page);
            TargetType targetType = (TargetType)Enum.Parse(typeof(TargetType), this.StringBuilder.CreateString(mode));
            decimal TargetID = 0;
            decimal selectedTargetID = decimal.Parse(this.StringBuilder.CreateString(SelectedTargetID), CultureInfo.InvariantCulture);
            TargetName = this.StringBuilder.CreateString(TargetName);
            decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID), CultureInfo.InvariantCulture);
            decimal reportParentID = decimal.Parse(this.StringBuilder.CreateString(ReportParentID), CultureInfo.InvariantCulture);
            bool isDesignedReport = bool.Parse(this.StringBuilder.CreateString(IsDesignedReport));
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            Report report = new Report();

            switch (uam)
            {
                case UIActionType.ADD:
                    report.ParentId = selectedTargetID;
                    report.ID = 0;
                    break;
                case UIActionType.EDIT:
                    if (selectedTargetID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoItemSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    else
                    {
                        //report.ParentId = parentTargetID;
                        report.ParentId = reportParentID;
                        report.ID = selectedTargetID;
                    }
                    break;
                case UIActionType.DELETE:
                    if (selectedTargetID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoItemSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    else
                        report.ID = selectedTargetID;
                    break;
            }

            switch (targetType)
            {
                case TargetType.ReportGroup:
                    if (uam != UIActionType.DELETE)
                        report.Name = TargetName;
                    switch (uam)
                    {
                        case UIActionType.ADD:
                            TargetID = this.ReportBusiness.InsertReportGroup(report, uam);
                            break;
                        case UIActionType.EDIT:
                            this.ReportBusiness.CheckUpdateAccess();
                            TargetID = this.ReportBusiness.UpdateReportGroup(report, uam);
                            break;
                        case UIActionType.DELETE:
                            this.ReportBusiness.CheckDeleteAccess();
                            TargetID = this.ReportBusiness.DeleteReportGroup(report, uam);
                            break;
                    }
                    break;
                case TargetType.Report:
                    switch (uam)
                    {
                        case UIActionType.ADD:
                            TargetID = this.ReportBusiness.InsertReport(selectedTargetID, reportFileID, TargetName);
                            break;
                        case UIActionType.EDIT:
                            this.ReportBusiness.CheckUpdateAccess();
                            TargetID = this.ReportBusiness.UpdateReport(selectedTargetID, reportFileID, TargetName, reportParentID, isDesignedReport);
                            break;
                        case UIActionType.DELETE:
                            this.ReportBusiness.CheckDeleteAccess();
                            TargetID = this.ReportBusiness.DeleteReport(selectedTargetID);
                            break;
                    }
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
            retMessage[3] = TargetID.ToString();
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

    [Ajax.AjaxMethod("GetReport_ReportsPage", "GetReport_ReportsPage_onCallBack", null, null)]
    public string[] GetReport_ReportsPage(string ReportFileID)
    {
        this.InitializeCulture();
        string[] retMessage = new string[4];
        try
        {
            decimal reportID = 0;
            decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID), CultureInfo.InvariantCulture);
            PersonAdvanceSearchProxy PersonFilterProxy = new PersonAdvanceSearchProxy();
            IList<ReportUIParameter> ReportParametersList = new List<ReportUIParameter>();
            decimal groupingTypeId = -1;
            StiReport stiReport = this.ReportParameterBusiness.GetReport(reportFileID, PersonFilterProxy, ReportParametersList, false, reportID, groupingTypeId, false, false, false, GTS.Clock.Infrastructure.ReportOutPutType.Report);

            Dictionary<string, StiReport> SysReportsDic = new Dictionary<string, StiReport>();
            string stiReportGUID = Guid.NewGuid().ToString();
            if (Session["SysReports"] == null)
                Session.Add("SysReports", SysReportsDic);
            SysReportsDic = (Dictionary<string, StiReport>)Session["SysReports"];
            if (!SysReportsDic.Keys.Contains(stiReportGUID))
                SysReportsDic.Add(stiReportGUID, stiReport);
            Session["SysReports"] = SysReportsDic;

            //DNN Note
            string currentPage = "~/DesktopModules/Atlas/" + HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
            retMessage[0] = HttpContext.GetLocalResourceObject(currentPage, "RetSuccessType").ToString();
            retMessage[1] = HttpContext.GetLocalResourceObject(currentPage, "EditComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = stiReportGUID;

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
    protected void CallBack_trvReportAccessLevels_Reports_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_trvReportAccessLevels_Reports(decimal.Parse(this.StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_ReportAccessLevels.RenderControl(e.Output);
        this.trvReportAccessLevels_Reports.RenderControl(e.Output);
    }

    private void Fill_trvReportAccessLevels_Reports(decimal ReportID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();

            Report report = this.ReportBusiness.GetByID(ReportID);
            IList<Role> rolesList = this.RoleBusiness.GetAll();
            Role rootRole = this.RoleBusiness.GetRoleTree();
            TreeViewNode rootRoleNode = new TreeViewNode();
            rootRoleNode.ID = rootRole.ID.ToString();
            string rootRoleNodeText = string.Empty;
            if (GetLocalResourceObject("RolesNode_trvReportAccessLevels_Reports") != null)
                rootRoleNodeText = GetLocalResourceObject("RolesNode_trvReportAccessLevels_Reports").ToString();
            else
                rootRoleNodeText = rootRole.Name;
            rootRoleNode.Text = rootRoleNodeText;
            rootRoleNode.Value = rootRole.CustomCode;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif"))
                rootRoleNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
            this.trvReportAccessLevels_Reports.Nodes.Add(rootRoleNode);
            rootRoleNode.Expanded = true;

            this.GetChildReportAccessLevels_trvReportAccessLevels_Reports(rolesList, rootRoleNode, report, rootRole);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReportAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReportAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReportAccessLevels.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    private void GetChildReportAccessLevels_trvReportAccessLevels_Reports(IList<Role> rolesList, TreeViewNode parentRoleNode, Report report, Role parentRole)
    {
        foreach (Role childRole in this.RoleBusiness.GetRoleChilds(parentRole.ID, rolesList))
        {
            TreeViewNode childRoleNode = new TreeViewNode();
            childRoleNode.ID = childRole.ID.ToString();
            childRoleNode.Text = childRole.Name;
            childRoleNode.Value = childRole.CustomCode;
            childRoleNode.ShowCheckBox = true;
            if (report.AccessRoleList.Where(role => role.ID == childRole.ID).Count() > 0)
                childRoleNode.Checked = true;
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
                this.GetChildReportAccessLevels_trvReportAccessLevels_Reports(rolesList, childRoleNode, report, childRole);
        }
    }

    [Ajax.AjaxMethod("UpdateReportAccessLevelsAsign_ReportPage", "UpdateReportAccessLevelsAsign_ReportPage_onCallBack", null, null)]
    public string[] UpdateReportAccessLevelsAsign_ReportPage(string ReportID, string StrReportAccessLevelsList)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();

            AttackDefender.CSRFDefender(this.Page);

            decimal reportID = decimal.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
            IList<Role> RolesList = this.CreateRolesList_Reports(this.StringBuilder.CreateString(StrReportAccessLevelsList));
            this.ReportBusiness.UpdateRoleAccess(reportID, RolesList);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("EditComplete").ToString();
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

    private IList<Role> CreateRolesList_Reports(string StrAccessLevelsList)
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        object[] ObjAccessLevelsList = (object[])JsSerializer.DeserializeObject(StrAccessLevelsList);
        IList<Role> RolesList = new List<Role>();
        foreach (object objAccessLevelsList in ObjAccessLevelsList)
        {
            RolesList.Add(this.RoleBusiness.GetByID(decimal.Parse(objAccessLevelsList.ToString(), CultureInfo.InvariantCulture)));
        }
        return RolesList;
    }
    protected void CallBack_cmbReportGroup_Reports_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbReportGroup_Reports.Dispose();
        this.Fill_cmbReportGroup_Reports();
        this.ErrorHiddenField_ReportGroup_Reports.RenderControl(e.Output);
        this.cmbReportGroup_Reports.RenderControl(e.Output);
    }
    private void Fill_cmbReportGroup_Reports()
    {
        string[] retMessage = new string[4];
        try
        {
            string rootRepGroupNodeText = string.Empty;
            IList<Report> ReportList = this.ReportBusiness.GetReportGroups();
            foreach (Report reportItem in ReportList)
            {
                string rootNode = string.Empty;
                if (reportItem.Name == "RootUpdated")
                    rootRepGroupNodeText = GetLocalResourceObject("ReportsNode_trvReports_Reports").ToString();
                else
                    rootRepGroupNodeText = reportItem.Name;

                ComboBoxItem reportCmbItem = new ComboBoxItem(rootRepGroupNodeText);
                reportCmbItem.Value = reportItem.ID.ToString();
                this.cmbReportGroup_Reports.Items.Add(reportCmbItem);
            }
            this.cmbReportGroup_Reports.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReportGroup_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReportGroup_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReportGroup_Reports.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }


}