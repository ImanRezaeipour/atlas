using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Presentaion.Forms.App_Code;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Model.Concepts;
using System.Web.Script.Serialization;
using GTS.Clock.Business.Proxy;

public partial class YearlyHolidays : GTSBasePage
{

    public class SecondaryConceptView
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public bool IsUsedByYearlyHoliday { get; set; }
    }
    enum WorkGropsDataMode
    {
        Default,
        CheckAll,
        UnCheckAll
    }
    public BYearlyHolidayWorkGroups YearlyHolidayWorkGroupBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BYearlyHolidayWorkGroups>();
        }
    }
   
    public BCalendarType YearlyHolidaysBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BCalendarType>();
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

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    internal class CurrentYearObj
    {
        public string Year { get; set; }
        public string UIYear { get; set; }
        public string Index { get; set; }
    }

    enum Scripts
    {
        YearlyHolidays_onPageLoad,
        tbYearlyHolidays_TabStripMenus_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridYearlyHolidays_YearlyHolidays.IsCallback)
        {
            Page YearlyHolidaysPage = this;
            Ajax.Utility.GenerateMethodScripts(YearlyHolidaysPage);
            this.Fill_cmbYear_YearlyHolidays();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.CheckYearlyHolidaysLoadAccess_YearlyHolidays();
        }
    }

    private void CheckYearlyHolidaysLoadAccess_YearlyHolidays()
    {
        string[] retMessage = new string[4];
        try
        {
            this.YearlyHolidaysBusiness.CheckYearlyHolidaysLoadAccess();
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    private void Fill_cmbYear_YearlyHolidays()
    {
        int CurrentYear = DateTime.Now.Year;
        string CurrentUIYear = string.Empty;
        int CurrentYearIndex = 0;
        int YearCounter = 0;
        string cmbItemText = string.Empty;
        string SysLangID = this.LangProv.GetCurrentSysLanguage();
        PersianCalendar pCal = new PersianCalendar();
        for (int i = CurrentYear - 4; i <= (CurrentYear + 4); i++)
        {
            ComboBoxItem cmbItemYear = new ComboBoxItem(i.ToString());
            cmbItemYear.Value = i.ToString();
            switch (SysLangID)
            {
                case "fa-IR":
                    cmbItemText = pCal.GetYear(new DateTime(i, 12, 1)).ToString();
                    break;
                case "en-US":
                    cmbItemText = i.ToString();
                    break;
            }
            cmbItemYear.Text = cmbItemText;
            this.cmbYear_YearlyHolidays.Items.Add(cmbItemYear);
            ++YearCounter;
            if (i == CurrentYear)
            {
                CurrentUIYear = cmbItemText;
                CurrentYearIndex = YearCounter - 1;
            }
        }

        CurrentYearObj currentYearObj = new CurrentYearObj();
        currentYearObj.Year = CurrentYear.ToString();
        currentYearObj.UIYear = CurrentUIYear;
        currentYearObj.Index = CurrentYearIndex.ToString();
        this.hfCurrentYear_YearlyHolidays.Value = this.JsSerializer.Serialize(currentYearObj);
        this.cmbYear_YearlyHolidays.SelectedIndex = CurrentYearIndex;
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

    protected void CallBack_GridYearlyHolidays_YearlyHolidays_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.Fill_GridYearlyHolidays_YearlyHolidays();
        this.GridYearlyHolidays_YearlyHolidays.RenderControl(e.Output);
        this.ErrorHiddenField_YearlyHolidays.RenderControl(e.Output);
    }

    private void Fill_GridYearlyHolidays_YearlyHolidays()
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<CalendarType> YearlyHolidaysGroupList = this.YearlyHolidaysBusiness.GetAll();
            this.GridYearlyHolidays_YearlyHolidays.DataSource = YearlyHolidaysGroupList;
            this.GridYearlyHolidays_YearlyHolidays.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateYearlyHolidaysGroup_YearlyHolidaysPage", "UpdateYearlyHolidaysGroup_YearlyHolidaysPage_onCallBack", null, null)]
    public string[] UpdateYearlyHolidaysGroup_YearlyHolidaysPage(string state, string SelectedYearlyHolidayGroupID, string YearlyHolidayGroupCustomCode, string YearlyHolidayGroupTitle, string YearlyHolidayGroupDescription)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            decimal YearlyHolidayGroupID = 0;
            decimal selectedYearlyHolidayGroupID = decimal.Parse(this.StringBuilder.CreateString(SelectedYearlyHolidayGroupID), CultureInfo.InvariantCulture);
            YearlyHolidayGroupCustomCode = this.StringBuilder.CreateString(YearlyHolidayGroupCustomCode);
            YearlyHolidayGroupTitle = this.StringBuilder.CreateString(YearlyHolidayGroupTitle);
            YearlyHolidayGroupDescription = this.StringBuilder.CreateString(YearlyHolidayGroupDescription);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            CalendarType yearlyHolidaysGroup = new CalendarType();

            yearlyHolidaysGroup.ID = selectedYearlyHolidayGroupID;
            if (uam != UIActionType.DELETE)
            {
                yearlyHolidaysGroup.CustomCode = YearlyHolidayGroupCustomCode;
                yearlyHolidaysGroup.Name = YearlyHolidayGroupTitle;
                yearlyHolidaysGroup.Description = YearlyHolidayGroupDescription;
            }

            switch (uam)
            {
                case UIActionType.ADD:
                    YearlyHolidayGroupID = this.YearlyHolidaysBusiness.InsertYearlyHolidaysGroup(yearlyHolidaysGroup, uam);
                    break;
                case UIActionType.EDIT:
                    if (selectedYearlyHolidayGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoYearlyHolidaysGroupSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    YearlyHolidayGroupID = this.YearlyHolidaysBusiness.UpdateYearlyHolidaysGroup(yearlyHolidaysGroup, uam);
                    break;
                case UIActionType.DELETE:
                    if (selectedYearlyHolidayGroupID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoYearlyHolidaysGroupSelectedforDelete").ToString()), retMessage);
                        return retMessage;
                    }
                    YearlyHolidayGroupID = this.YearlyHolidaysBusiness.DeleteYearlyHolidaysGroup(yearlyHolidaysGroup, uam);
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
            retMessage[3] = YearlyHolidayGroupID.ToString();
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
    protected void CallBack_GridAssignedWorkGroups_YearlyHolidays_onCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        decimal selectedHolidayID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        int year = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]),CultureInfo.InvariantCulture);
        Fill_GridAssignedWorkGroups_YearlyHolidays(selectedHolidayID , year ,e);
        this.GridAssignedWorkGroups_YearlyHolidays.RenderControl(e.Output);
        this.ErrorHiddenField_GridAssignedWorkGroups_YearlyHolidays.RenderControl(e.Output);
        this.CheckListHiddenField_YearlyHolidays.RenderControl(e.Output);
    }
    private void Fill_GridAssignedWorkGroups_YearlyHolidays(decimal holidayId , int year ,CallBackEventArgs e)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<WorkGroupProxy> WorkGroupProxyList = new List<WorkGroupProxy>();
             IList<WorkGroup> WorkGroupList = this.YearlyHolidayWorkGroupBusiness.GetALLWorkGroup();
            WorkGropsDataMode wdm = (WorkGropsDataMode)Enum.Parse(typeof(WorkGropsDataMode), e.Parameters[2]);
            switch (wdm)
            { 
                case  WorkGropsDataMode.CheckAll:
                    WorkGroupProxyList = this.CreateWorkGroupsProxyList_onCheckChange(WorkGroupList,true);
                     this.GridAssignedWorkGroups_YearlyHolidays.DataSource = WorkGroupProxyList;
                    this.GridAssignedWorkGroups_YearlyHolidays.DataBind();
                 break;

                case WorkGropsDataMode.UnCheckAll:
                     WorkGroupProxyList = this.CreateWorkGroupsProxyList_onCheckChange(WorkGroupList,false);
                     this.GridAssignedWorkGroups_YearlyHolidays.DataSource = WorkGroupProxyList;
                    this.GridAssignedWorkGroups_YearlyHolidays.DataBind();
                 break;
                case WorkGropsDataMode.Default:
                   WorkGroupProxyList = this.YearlyHolidayWorkGroupBusiness.GetWorkGroup(holidayId,year);
                     this.GridAssignedWorkGroups_YearlyHolidays.DataSource = WorkGroupProxyList;
                     this.CreatStringWorkGroupsId(WorkGroupProxyList);
                     this.GridAssignedWorkGroups_YearlyHolidays.DataBind();
                    break;
                default:
                    break;

            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridAssignedWorkGroups_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridAssignedWorkGroups_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridAssignedWorkGroups_YearlyHolidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    public IList<WorkGroupProxy> CreateWorkGroupsProxyList_onCheckChange(IList<WorkGroup> WorkGroupList, bool Checked)
    {
        IList<WorkGroupProxy> WorkGroupProxyList = new List<WorkGroupProxy>();
        foreach(WorkGroup  WorkGroupItem in WorkGroupList )
        {
            WorkGroupProxy workgroupproxy = new WorkGroupProxy();
            workgroupproxy.ID = WorkGroupItem.ID;
            workgroupproxy.WorkGroupCode = WorkGroupItem.CustomCode;
            workgroupproxy.WorkGroupName = WorkGroupItem.Name;
            if (Checked)
            {
                workgroupproxy.IsUsedByYearlyHoliday = true;
              
            }
            else
                workgroupproxy.IsUsedByYearlyHoliday = false;
            WorkGroupProxyList.Add(workgroupproxy);
        }
        this.CreatStringWorkGroupsId( WorkGroupProxyList);
        return WorkGroupProxyList;
    }
    public void CreatStringWorkGroupsId(IList<WorkGroupProxy> WorkGroupProxyList)
    {
        string CheckList = string.Empty;
        string splitter = "#";
        CheckList = splitter;
        foreach (WorkGroupProxy WorkGroupProxyItem in WorkGroupProxyList)
        { 
            if(WorkGroupProxyItem.IsUsedByYearlyHoliday)
            {
                CheckList += WorkGroupProxyItem.ID + splitter;
            }
        }
        this.CheckListHiddenField_YearlyHolidays.Value = CheckList;
    }

    [Ajax.AjaxMethod("UpdateAssignedWorkGroups_YearlyHolidaysPage", "UpdateAssignedWorkGroups_YearlyHolidaysPage_onCallBack", null, null)]
    public string[] UpdateAssignedWorkGroups_YearlyHolidaysPage(string state, string SelectedHolidayID, string WorkGroupIdList, string Year)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            //decimal CalculationRangeID = 0;
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            SelectedHolidayID = this.StringBuilder.CreateString(SelectedHolidayID);
            WorkGroupIdList = this.StringBuilder.CreateString(WorkGroupIdList);
            decimal selectedHolidayID = decimal.Parse(SelectedHolidayID, CultureInfo.InvariantCulture);
            int year = int.Parse(this.StringBuilder.CreateString(Year),CultureInfo.InvariantCulture);
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            List<decimal> Concepts = this.CreateWorkGroupList_YearlyHolidays(WorkGroupIdList);

            switch (uam)
            {
                case UIActionType.EDIT:
                    if (selectedHolidayID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoCalculationRangeSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    // CalculationRangeID = this.YearlyHolidayWorkGroup.UpdateYearlyHolidayWorkGroups(selectedHolidayID, WorkGroupList);
                    this.YearlyHolidayWorkGroupBusiness.UpdateYearlyHolidayWorkGroups(selectedHolidayID , Concepts , year);

                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
            }
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            //retMessage[3] = CalculationRangeID.ToString();
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

    private List<decimal> CreateWorkGroupList_YearlyHolidays(string WorkGroupList)
    {
        List<decimal> WorkGroupListArray = new List<decimal>();
        string[] WorkGroupListParts = WorkGroupList.Split(new char[] { '#' });
        foreach (string workgroupListPart in WorkGroupListParts)
        {
            if (workgroupListPart != string.Empty)
                WorkGroupListArray.Add(decimal.Parse(workgroupListPart, CultureInfo.InvariantCulture));
        }
        return WorkGroupListArray;
    }

}