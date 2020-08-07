using GTS.Clock.Business.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business.UI;
using GTS.Clock.Business;
using System.Web.Script.Serialization;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.Concepts;



public partial class MonthlyExceptionShifts : GTSBasePage
{
    public BWorkGroupCalendar PeriodRepeatBusiness
    {
        get
        {
            SysLanguageResource Slr = SysLanguageResource.Parsi;
            switch (this.LangProv.GetCurrentSysLanguage())
            {
                case "fa-IR":
                    Slr = SysLanguageResource.Parsi;
                    break;
                case "en-US":
                    Slr = SysLanguageResource.English;
                    break;
            }
            return new BWorkGroupCalendar(Slr);
        }
    }

    internal class MonthlyExceptionShift
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string CustomCode { get; set; }
        public string ShortcutsKey { get; set; }
    }
    public BCalendarType CalendarTypeBusiness
    {
        get
        {
            return new BCalendarType();
        }
    }
    public BExceptionShift ExceptionShiftsBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BExceptionShift>();
        }
    }

    public ISearchPerson PersonSearchBusiness
    {
        get
        {
            return (ISearchPerson)BusinessHelper.GetBusinessInstance<BPerson>();
        }
    }

    public AdvancedPersonnelSearchProvider APSProv
    {
        get
        {
            return new AdvancedPersonnelSearchProvider();
        }
    }

    public enum PersonnelLoadState
    {
        Normal,
        Search,
        AdvancedSearch
    }
    public enum ShiftLoadState
    {
        Normal,
        Search,
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

    public OperationYearMonthProvider operationYearMonthProvider
    {
        get
        {
            return new OperationYearMonthProvider();
        }
    }

    internal class PersonnelLoadStateConditions
    {
        public string PersonnelLoadState { get; set; }
        public string PersonnelSearchTerm { get; set; }
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
        MonthlyExceptionShifts_onPageLoad,
        DialogMonthlyExceptionShifts_Operations,
        Alert_Box,
        HelpForm_Operations,
        KeyboardMappings,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts.IsCallback && !this.CallBack_ShiftsView_MonthlyExceptionShifts.IsCallback)
        {
            Page MonthlyExceptionShiftsPage = this;
            Ajax.Utility.GenerateMethodScripts(MonthlyExceptionShiftsPage);

            this.CheckMonthlyExceptionShiftsLoadAccess_MonthlyExceptionShifts();
            this.SetMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            ScriptHelper.InitializeDefiantScripts(this.Page);
            this.Fill_cmbYear_MonthlyExceptionShifts();
            this.Fill_cmbMonth_MonthlyExceptionShifts();
            this.CreateShiftsObject();
        }
    }
   private void CreateShiftsObject()
   {   
        BShift bshift = new BShift();
        IList<MonthlyExceptionShift> MonthlyExceptionShiftList = new List<MonthlyExceptionShift>();
        IList<Shift> ShiftList = bshift.GetAll();
        foreach(Shift shift in ShiftList)
        {
            MonthlyExceptionShift monthlyExceptionShift = new MonthlyExceptionShift();
            monthlyExceptionShift.Id = shift.ID;
            monthlyExceptionShift.Name = shift.Name;
            monthlyExceptionShift.ShortcutsKey = shift.ShortcutsKey;
            monthlyExceptionShift.CustomCode = shift.CustomCode;
            MonthlyExceptionShiftList.Add(monthlyExceptionShift);
        }
        this.hfShiftsObject_MonthlyExceptionShifts.Value = JsSerializer.Serialize(MonthlyExceptionShiftList);
    }
    private void CheckMonthlyExceptionShiftsLoadAccess_MonthlyExceptionShifts()
    {
        string[] retMessage = new string[4];
        try
        {
            this.ExceptionShiftsBusiness.CheckMonthlyExceptionShiftsLoadAccess();
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
        ////Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
    }

    private void SetMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts()
    {
        this.hfMonthlyExceptionShiftsPageSize_MonthlyExceptionShifts.Value = this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.PageSize.ToString();
    }

    private void Fill_cmbYear_MonthlyExceptionShifts()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_MonthlyExceptionShifts, this.hfCurrentYear_MonthlyExceptionShifts, 0);
    }

    private void Fill_cmbMonth_MonthlyExceptionShifts()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_MonthlyExceptionShifts, this.hfCurrentMonth_MonthlyExceptionShifts, 0);
    }

    protected void CallBack_GridMonthlyExceptionShifts_MonthlyExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        int Year = int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        int Month = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
        int PageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture);
        int PageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture);       
        string SearchItem = this.StringBuilder.CreateString(e.Parameters[4]);       
        this.SetMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts(Year, Month , PageSize, SearchItem);
        this.InitializeMonthlyCalendar(Year, Month);
        this.Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(Year, Month, PageSize, PageIndex, SearchItem);       
        this.hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts.RenderControl(e.Output);
        this.ErrorHiddenField_MonthlyExceptionShifts.RenderControl(e.Output);
        this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.RenderControl(e.Output);       
    }

    private void SetMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts(int Year, int Month, int PageSize, string SearchItem)
    {
        int PersonnelCount = 0;
        PersonnelCount = this.PersonSearchBusiness.GetPersonCountByMonthlyExceptionShift(Year, Month, SearchItem);
        //if (PersonId != 0)
        //{
        //    PersonnelCount = 1;
        //}
        //else
        //{
            //switch (PLS)
            //{
                //case PersonnelLoadState.Normal:
                   // PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                    //break;
               // case PersonnelLoadState.Search:
                   // PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(PersonnelSearchTerm);
                   // break;
              //  case PersonnelLoadState.AdvancedSearch:
                   // PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm));
                    //break;
            //}
       // }
            this.hfMonthlyExceptionShiftsCount_MonthlyExceptionShifts.Value = PersonnelCount.ToString();
            this.hfMonthlyExceptionShiftsPageCount_MonthlyExceptionShifts.Value = Utility.GetPageCount(PersonnelCount, this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.PageSize).ToString();
        
    }

    private void Fill_GridMonthlyExceptionShifts_MonthlyExceptionShifts(int Year, int Month, int PageSize, int PageIndex, string SearchItem)
    {
        string[] retMessage = new string[4];
        IList<MonthlyExceptionShiftProxy> MonthlyExceptionShiftProxyList = null;
        try
        {

            MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsList(Year, Month, PageIndex, PageSize, SearchItem);
            this.InitializeCulture();

            //if (PersonId != 0)
            //{
            //    MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsListBySearch(Year, Month, PersonId, PageIndex, PageSize);
            //}
            //else
            //{
            //    switch (PLS)
            //    {
            // case PersonnelLoadState.Normal:

            // break;
            // case PersonnelLoadState.Search:
            //MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsListByQuickSerch(Year, Month, PersonnelSearchTerm, PageIndex, PageSize);
            // break;
            //case PersonnelLoadState.AdvancedSearch:
            // MonthlyExceptionShiftProxyList = this.ExceptionShiftsBusiness.GetMonthlyExceptionShiftsListByAdvancedSearch(Year, Month, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm), PageIndex, PageSize);
            // break;
            //}
            //}
            this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.DataSource = MonthlyExceptionShiftProxyList;
            this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (OutOfExpectedRangeException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    [Ajax.AjaxMethod("UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage", "UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage_onCallBack", null, null)]
    public string[] UpdateMonthlyExceptionShifts_MonthlyExceptionShiftsPage(string state, string PersonnelID, string Year, string Month, string StrDayShiftCol)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
        string WarningMessage = string.Empty;

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            int ExceptionShiftFailedCount = 0;
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
            int year = int.Parse(this.StringBuilder.CreateString(Year), CultureInfo.InvariantCulture);
            int month = int.Parse(this.StringBuilder.CreateString(Month), CultureInfo.InvariantCulture);
            IList<DateTime> MonthDatesList = this.ExceptionShiftsBusiness.GetMonthDatesList(year, month);
            switch (uam)
            {
                case UIActionType.EDIT:
                    IList<string> DaysShiftList = this.CreateDaysShiftList_MonthlyExceptionShifts(this.StringBuilder.CreateString(StrDayShiftCol), MonthDatesList.Count());
                    this.ExceptionShiftsBusiness.UpdatePersonnelMonthlyExceptionShifts(personnelID, MonthDatesList, DaysShiftList, out ExceptionShiftFailedCount);
                    break;
                case UIActionType.DELETE:
                    this.ExceptionShiftsBusiness.DeletePersonnelMonthlyExceptionShifts(personnelID, MonthDatesList);
                    break;
            }

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            string SuccessMessageBody = string.Empty;
            switch (uam)
            {
                case UIActionType.EDIT:
                    SuccessMessageBody = GetLocalResourceObject("EditComplete").ToString();
                    break;
                case UIActionType.DELETE:
                    SuccessMessageBody = GetLocalResourceObject("DeleteComplete").ToString();
                    break;
            }
            if (ExceptionShiftFailedCount > 0)
                WarningMessage += string.Format(GetLocalResourceObject("WarningMessageExceptionShift").ToString(), ExceptionShiftFailedCount);
                if (WarningMessage != string.Empty)
                {
                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                    retMessage[1] = WarningMessage;
                    retMessage[2] = "warning";                    
                }
                else
                {
                    retMessage[1] = SuccessMessageBody;
                    retMessage[2] = "success";
                }


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
    [Ajax.AjaxMethod("UpdatePeriodRepeats_MonthlyExceptionShiftsPage", "UpdatePeriodRepeats_MonthlyExceptionShifts_onCallBack", null, null)]
    public string[] UpdatePeriodRepeats_MonthlyExceptionShiftsPage(string Year, string Month, string FromDay, string ToDay, string PersonId, string StrHolidaysList, string StrDayShiftCol)
    {
        this.InitializeCulture();
        string[] retMessage = new string[4];
        try
        {
            string WarningMessage = string.Empty;
            int ExceptionShiftFailedCount = 0;
            int year = int.Parse(this.StringBuilder.CreateString(Year), CultureInfo.InvariantCulture);
            int month = int.Parse(this.StringBuilder.CreateString(Month), CultureInfo.InvariantCulture);
            int fromDay = int.Parse(this.StringBuilder.CreateString(FromDay), CultureInfo.InvariantCulture);
            int toDay = int.Parse(this.StringBuilder.CreateString(ToDay), CultureInfo.InvariantCulture);
            int personId = int.Parse(this.StringBuilder.CreateString(PersonId), CultureInfo.InvariantCulture);
            IList<DateTime> MonthDatesList = this.ExceptionShiftsBusiness.GetMonthDatesList(year, month);
            //Dictionary<string, object> DaysShiftDic = (Dictionary<string, object>)JsSerializer.DeserializeObject(this.StringBuilder.CreateString(StrDayShiftCol));
            IList<string> DaysShiftList = this.CreateDaysShiftList_MonthlyExceptionShifts(this.StringBuilder.CreateString(StrDayShiftCol), MonthDatesList.Count());
            IList<decimal> HolidaysList = this.CreateHolidaysList_MonthlyExceptionShifts(this.StringBuilder.CreateString(StrHolidaysList));
            IList<string> daysShiftList = ExceptionShiftsBusiness.UpdatePersonnelPeriodRepeateMonthlyExceptionShifts(year, month, fromDay, toDay, personId, HolidaysList, DaysShiftList, MonthDatesList, out ExceptionShiftFailedCount);
            if (ExceptionShiftFailedCount > 0)
                WarningMessage += string.Format(GetLocalResourceObject("WarningMessageExceptionShift").ToString(), ExceptionShiftFailedCount);
            if (WarningMessage != string.Empty)
            {
                retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                retMessage[1] = WarningMessage;
                retMessage[2] = "warning";
            }
            else
            {
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("EditComplete").ToString();
                retMessage[2] = "success";
            }           
            retMessage[3] = JsSerializer.Serialize(daysShiftList);
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
    private IList<decimal> CreateHolidaysList_MonthlyExceptionShifts(string StrHolidaysList)
    {
        IList<decimal> HolidaysList = new List<decimal>();
        if (StrHolidaysList != string.Empty)
        {
            string[] HolidaysListCol = StrHolidaysList.Split(new char[] { '#' });
            foreach (string HolidaysListItem in HolidaysListCol)
            {
                if (HolidaysListItem != string.Empty)
                    HolidaysList.Add(decimal.Parse(HolidaysListItem, CultureInfo.InvariantCulture));
            }
        }
        return HolidaysList;
    }
    private IList<string> CreateDaysShiftList_MonthlyExceptionShifts(string StrDayShiftCol, int MonthDaysCount)
    {
        Dictionary<string, object> DaysShiftDic = (Dictionary<string, object>)this.JsSerializer.DeserializeObject(StrDayShiftCol);
        IList<string> DaysShiftList = new List<string>();
        int dayCounter = 0;
        foreach (string key in DaysShiftDic.Keys)
        {
            if (dayCounter < MonthDaysCount)
            {
                DaysShiftList.Add(DaysShiftDic[key].ToString());
                dayCounter++;
            }
        }
        return DaysShiftList;
    }

    private void InitializeMonthlyCalendar(int year, int month)
    {
        DateTime DateDay;
        DateTime MonthStart = new DateTime();
        DateTime MonthEnd = new DateTime();
        int MonthDayCount = 0;
        int day = 0;
        string DayWithDateDay = string.Empty;
        string DayWeek = string.Empty;
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        GregorianCalendar gCal = new GregorianCalendar();
        ComponentArt.Web.UI.GridColumn DateColumn = null;

        IList<GTS.Clock.Model.Concepts.Calendar> calendarlist = null;

        switch (BLanguage.CurrentSystemLanguage)
        {
            case LanguagesName.Parsi:

                MonthDayCount = pc.GetDaysInMonth(year, month);
                MonthStart = pc.ToDateTime(year, month, 1, 0, 0, 0, 0);
                MonthEnd = pc.ToDateTime(year, month, MonthDayCount, 0, 0, 0, 0);
                calendarlist = this.CalendarTypeBusiness.GetMonthlyHoliday(MonthStart, MonthEnd);
                for (int i = 1; i <= MonthDayCount; i++)
                {
                    DateDay = pc.ToDateTime(year, month, i, 0, 0, 0, 0);
                    DayOfWeek Day = pc.GetDayOfWeek(DateDay);
                    string DateDayPersian = Utility.ToPersianDate(DateDay);
                    day = GetDayOfWeek(Day);
                    DayWeek = this.GetAxises_Calendar(day);
                    DayWithDateDay = DayWeek + "  " + DateDayPersian;

                    DateColumn = new ComponentArt.Web.UI.GridColumn();
                    DateColumn.HeadingText = DayWithDateDay;
                    DateColumn.DefaultSortDirection = GridSortDirection.Ascending;
                    DateColumn.Align = ComponentArt.Web.UI.TextAlign.Center;
                    DateColumn.Width = 75;
                    DateColumn.DataField = "Day" + i;
                    GTS.Clock.Model.Concepts.Calendar calendar = calendarlist.Where(x => x.Date == DateDay).FirstOrDefault();
                    if (calendar != null)
                        DateColumn.HeadingTextCssClass = "HeadingTextRed";
                    else
                        DateColumn.HeadingTextCssClass = "HeadingText";
                    this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.Levels[0].Columns.Add(DateColumn);
                }

                DateColumn = new ComponentArt.Web.UI.GridColumn();
                DateColumn.AllowSorting = InheritBool.False;
                DateColumn.DataCellClientTemplateId = "EditTemplate";
                DateColumn.EditControlType = GridColumnEditControlType.EditCommand;
                DateColumn.Width = 50;
                DateColumn.Align = ComponentArt.Web.UI.TextAlign.Center;
                this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.Levels[0].Columns.Add(DateColumn);
                break;
            case LanguagesName.English:

                MonthDayCount = gCal.GetDaysInMonth(year, month);
                MonthStart = gCal.ToDateTime(year, month, 1, 0, 0, 0, 0);
                MonthEnd = gCal.ToDateTime(year, month, MonthDayCount, 0, 0, 0, 0);
                calendarlist = this.CalendarTypeBusiness.GetMonthlyHoliday(MonthStart, MonthEnd);

                for (int i = 1; i <= MonthDayCount; i++)
                {
                    DateDay = gCal.ToDateTime(year, month, i, 0, 0, 0, 0);
                    DayOfWeek Day = gCal.GetDayOfWeek(DateDay);
                    day = GetDayOfWeek(Day);
                    DayWeek = this.GetAxises_Calendar(day);
                    DayWithDateDay = DayWeek + "  " + DateDay;


                    DateColumn = new ComponentArt.Web.UI.GridColumn();
                    DateColumn.HeadingText = DayWithDateDay;
                    DateColumn.DefaultSortDirection = GridSortDirection.Ascending;
                    DateColumn.Align = ComponentArt.Web.UI.TextAlign.Center;
                    DateColumn.Width = 75;
                    DateColumn.DataField = "Day" + i;
                    GTS.Clock.Model.Concepts.Calendar calendar = calendarlist.Where(x => x.Date == DateDay).FirstOrDefault();
                    if (calendar != null)
                        DateColumn.HeadingTextCssClass = "HeadingTextRed";
                    else
                        DateColumn.HeadingTextCssClass = "HeadingText";
                    this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.Levels[0].Columns.Add(DateColumn);
                }

                DateColumn = new ComponentArt.Web.UI.GridColumn();
                DateColumn.AllowSorting = InheritBool.False;
                DateColumn.DataCellClientTemplateId = "EditTemplate";
                DateColumn.EditControlType = GridColumnEditControlType.EditCommand;
                DateColumn.Width = 50;
                DateColumn.Align = ComponentArt.Web.UI.TextAlign.Center;
                this.GridMonthlyExceptionShifts_MonthlyExceptionShifts.Levels[0].Columns.Add(DateColumn);
                break;
        }




    }
    public string GetAxises_Calendar(int day)
    {
        string[] retMessage = new string[3];

        string CurrentLangID = string.Empty;
        string SysLangID = string.Empty;
        string Identifier = string.Empty;
        string retAxises = string.Empty;
        this.InitializeCulture();
        CurrentLangID = this.LangProv.GetCurrentLanguage();
        SysLangID = this.LangProv.GetCurrentSysLanguage();
        switch (SysLangID)
        {
            case "en-US":
                switch (CurrentLangID)
                {
                    case "en-US":
                        Identifier = "ee";
                        break;
                    case "fa-IR":
                        Identifier = "ef";
                        break;
                }
                break;
            case "fa-IR":
                switch (CurrentLangID)
                {
                    case "en-US":
                        Identifier = "fe";
                        break;
                    case "fa-IR":
                        Identifier = "ff";
                        break;
                }
                break;
        }

        string splitter = "@";
        splitter = string.Empty;
        retAxises += GetLocalResourceObject("lblDay" + day + "" + Identifier + "").ToString() + splitter;
        return retAxises;
    }
    private int GetDayOfWeek(DayOfWeek DayNumber)
    {
        int DayWeek = -1;
        string LangID = this.LangProv.GetCurrentLanguage();
        switch (LangID)
        {
            case "fa-IR":
                switch (DayNumber)
                {
                    case DayOfWeek.Friday:
                        DayWeek = 7;
                        break;
                    case DayOfWeek.Monday:
                        DayWeek = 3;
                        break;
                    case DayOfWeek.Saturday:
                        DayWeek = 1;
                        break;
                    case DayOfWeek.Sunday:
                        DayWeek = 2;
                        break;
                    case DayOfWeek.Thursday:
                        DayWeek = 6;
                        break;
                    case DayOfWeek.Tuesday:
                        DayWeek = 4;
                        break;
                    case DayOfWeek.Wednesday:
                        DayWeek = 5;
                        break;
                    default:
                        break;
                }

                break;
            case "en-US":
                switch (DayNumber)
                {
                    case DayOfWeek.Friday:
                        DayWeek = 5;
                        break;
                    case DayOfWeek.Monday:
                        DayWeek = 1;
                        break;
                    case DayOfWeek.Saturday:
                        DayWeek = 6;
                        break;
                    case DayOfWeek.Sunday:
                        DayWeek = 7;
                        break;
                    case DayOfWeek.Thursday:
                        DayWeek = 4;
                        break;
                    case DayOfWeek.Tuesday:
                        DayWeek = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        DayWeek = 3;
                        break;
                    default:
                        break;
                }
                break;
        }
        return DayWeek;
    }
    protected void CallBack_ShiftsView_MonthlyExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
    {
        string[] retMessage = new string[4];
        try
        {
            BShift bshift = new BShift();
            IList<Shift> shiftList = null;
            ShiftLoadState State = (ShiftLoadState)Enum.Parse(typeof(ShiftLoadState), this.StringBuilder.CreateString(e.Parameters[0]));            
            switch (State)
            {
                case ShiftLoadState.Normal:
                    shiftList = bshift.GetAll();
                    break;
                case ShiftLoadState.Search:
                    string searchValue = this.StringBuilder.CreateString(e.Parameters[1]);
                    shiftList = bshift.GetShiftsAccordingToSearch(searchValue);
                    break;
            }
            this.GridShiftsView_MonthlyExceptionShifts.DataSource = shiftList;
            this.GridShiftsView_MonthlyExceptionShifts.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ShiftsView_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ShiftsView_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ShiftsView_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        this.ErrorHiddenField_ShiftsView_MonthlyExceptionShifts.RenderControl(e.Output);
        this.GridShiftsView_MonthlyExceptionShifts.RenderControl(e.Output);

    }
    protected void CallBack_cmbFromDay_MonthlyExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbFromDay_MonthlyExceptionShifts.Dispose();
        this.Fill_cmbFromDay_MonthlyExceptionShifts(int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.RenderControl(e.Output);
        this.cmbFromDay_MonthlyExceptionShifts.RenderControl(e.Output);
    }
    private void Fill_cmbFromDay_MonthlyExceptionShifts(int Year, int Month)
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_cmbDayControl_PeriodRepeat(this.cmbFromDay_MonthlyExceptionShifts, Year, Month);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBack_cmbToDay_MonthlyExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbToDay_MonthlyExceptionShifts.Dispose();
        this.Fill_cmbToDay_MonthlyExceptionShifts(int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture));
        this.ErrorHiddenField_ToDay_MonthlyExceptionShifts.RenderControl(e.Output);
        this.cmbToDay_MonthlyExceptionShifts.RenderControl(e.Output);
    }
    private void Fill_cmbToDay_MonthlyExceptionShifts(int Year, int Month)
    {
        string[] retMessage = new string[4];
        try
        {
            this.Fill_cmbDayControl_PeriodRepeat(this.cmbToDay_MonthlyExceptionShifts, Year, Month);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_FromDay_MonthlyExceptionShifts.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    private void Fill_cmbDayControl_PeriodRepeat(ComboBox cmbDay, int Year, int Month)
    {
        int DayCount = 0;
        GregorianCalendar gCal = new GregorianCalendar();
        PersianCalendar pCal = new PersianCalendar();
        // int year = pCal.GetYear(new DateTime(Year, 1, 1));
        switch (this.LangProv.GetCurrentSysLanguage())
        {
            case "fa-IR":
                DayCount = pCal.GetDaysInMonth(Year, Month);
                break;
            case "en-US":
                DayCount = gCal.GetDaysInMonth(Year, Month);
                break;
        }
        for (int i = 1; i <= DayCount; i++)
        {
            ComboBoxItem cmbItemDay = new ComboBoxItem();
            cmbItemDay.Text = cmbItemDay.Value = i.ToString();
            cmbDay.Items.Add(cmbItemDay);
        }
        cmbDay.SelectedIndex = 0;
    }
    protected void CallBack_cmbHolidays_MonthlyExceptionShifts_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbHolidays_MonthlyExceptionShifts.Dispose();
        this.Fill_cmbHolidays_MonthlyExceptionShifts();
        this.ErrorHiddenField_Holidays.RenderControl(e.Output);
        this.cmbHolidays_MonthlyExceptionShifts.RenderControl(e.Output);
    }
    private void Fill_cmbHolidays_MonthlyExceptionShifts()
    {
        string[] retMessage = new string[4];
        try
        {
            IList<CalendarType> HolidaysList = this.PeriodRepeatBusiness.GetAllHolidayTypes();
            foreach (CalendarType holidayItem in HolidaysList)
            {
                TreeViewNode trvNodeHoliday = new TreeViewNode();
                trvNodeHoliday.Text = holidayItem.Name;
                trvNodeHoliday.Value = holidayItem.ID.ToString();
                trvNodeHoliday.ShowCheckBox = true;
                this.trvHolidays_MonthlyExceptionShifts.Nodes.Add(trvNodeHoliday);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Holidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Holidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Holidays.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }

}