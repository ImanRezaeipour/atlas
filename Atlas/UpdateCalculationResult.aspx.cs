using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using System.Reflection;
using System.Data;
using System.Web.Script.Serialization;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.ArchiveCalculations;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure;

public partial class UpdateCalculationResult : GTSBasePage
{
    public BArchiveCalculator UpdateCalculationResultBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BArchiveCalculator>();
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

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    enum Scripts
    {
        UpdateCalculationResult_onPageLoad,
        DialogUpdateCalculationResult_Operations,
        Alert_Box,
        DropDownDive,
        HelpForm_Operations,
        DialogWaiting_Operations
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!this.CallBack_cmbPersonnel_UpdateCalculationResult.IsCallback && !this.CallBack_GridSettings_UpdateCalculationResult.IsCallback && !this.CallBack_GridUpdateCalculationResult_UpdateCalculationResult.IsCallback)
        {
            Page UpdateCalculationResultPage = this;
            Ajax.Utility.GenerateMethodScripts(UpdateCalculationResultPage);

            this.CheckUpdateCalculationResultLoadAccess_UpdateCalculationResult();
            this.SetPersonnelPageSize_cmbPersonnel_UpdateCalculationResult();
            this.SetCalculationResultPageSize_UpdateCalculationResult();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            this.Fill_cmbYear_UpdateCalculationResult();
            this.Fill_cmbMonth_UpdateCalculationResult();
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

    private void SetPersonnelPageSize_cmbPersonnel_UpdateCalculationResult()
    {
        this.hfPersonnelPageSize_UpdateCalculationResult.Value = this.cmbPersonnel_UpdateCalculationResult.DropDownPageSize.ToString();
    }

    private void SetCalculationResultPageSize_UpdateCalculationResult()
    {
        this.hfCalculationResultPageSize_UpdateCalculationResult.Value = this.GridUpdateCalculationResult_UpdateCalculationResult.PageSize.ToString();
    }

    private void Fill_cmbYear_UpdateCalculationResult()
    {
        this.operationYearMonthProvider.GetOperationYear(this.cmbYear_UpdateCalculationResult, this.hfCurrentYear_UpdateCalculationResult,0);
    }

    private void Fill_cmbMonth_UpdateCalculationResult()
    {
        this.operationYearMonthProvider.GetOperationMonth(this.Page, this.cmbMonth_UpdateCalculationResult, this.hfCurrentMonth_UpdateCalculationResult,0);
    }

    private void CheckUpdateCalculationResultLoadAccess_UpdateCalculationResult()
    {
        string[] retMessage = new string[4];
        try
        {
            this.UpdateCalculationResultBusiness.CheckUpdateCalculationResultLoadAccess();
        }
        catch (BaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            Response.Redirect("WhitePage.aspx?" + typeof(IllegalServiceAccess).Name + "=" + retMessage[1]);
        }
    }

    protected void CallBack_cmbPersonnel_UpdateCalculationResult_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        this.cmbPersonnel_UpdateCalculationResult.Dispose();
        this.SetPersonnelPageCount_cmbPersonnel_UpdateCalculationResult((PersonnelLoadState)Enum.Parse(typeof(PersonnelLoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.Fill_cmbPersonnel_UpdateCalculationResult((PersonnelLoadState)Enum.Parse(typeof(PersonnelLoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
        this.cmbPersonnel_UpdateCalculationResult.RenderControl(e.Output);
        this.hfPersonnelCount_UpdateCalculationResult.RenderControl(e.Output);
        this.hfPersonnelPageCount_UpdateCalculationResult.RenderControl(e.Output);
        this.ErrorHiddenField_Personnel_UpdateCalculationResult.RenderControl(e.Output);
    }

    private void SetPersonnelPageCount_cmbPersonnel_UpdateCalculationResult(PersonnelLoadState Ls, int pageSize, string SearchTerm)
    {
        string[] retMessage = new string[4];
        int PersonnelCount = 0;
        try
        {
            switch (Ls)
            {
                case PersonnelLoadState.Normal:
                    PersonnelCount = this.PersonSearchBusiness.GetPersonCount();
                    break;
                case PersonnelLoadState.Search:
                    PersonnelCount = this.PersonSearchBusiness.GetPersonInQuickSearchCount(SearchTerm);
                    break;
                case PersonnelLoadState.AdvancedSearch:
                    PersonnelCount = this.PersonSearchBusiness.GetPersonInAdvanceSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm));
                    break;
                default:
                    break;
            }
            this.hfPersonnelCount_UpdateCalculationResult.Value = PersonnelCount.ToString();
            this.hfPersonnelPageCount_UpdateCalculationResult.Value = Utility.GetPageCount(PersonnelCount, pageSize).ToString();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    private void Fill_cmbPersonnel_UpdateCalculationResult(PersonnelLoadState Ls, int pageSize, int pageIndex, string SearchTerm)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<Person> PersonnelList = null;
            switch (Ls)
            {
                case PersonnelLoadState.Normal:
                    PersonnelList = this.PersonSearchBusiness.GetAllPerson(pageIndex, pageSize);
                    break;
                case PersonnelLoadState.Search:
                    PersonnelList = this.PersonSearchBusiness.QuickSearchByPage(pageIndex, pageSize, SearchTerm);
                    break;
                case PersonnelLoadState.AdvancedSearch:
                    PersonnelList = this.PersonSearchBusiness.GetPersonInAdvanceSearch(this.APSProv.CreateAdvancedPersonnelSearchProxy(SearchTerm), pageIndex, pageSize);
                    break;
            }
            foreach (Person personItem in PersonnelList)
            {
                ComboBoxItem personCmbItem = new ComboBoxItem(personItem.FirstName + " " + personItem.LastName);
                personCmbItem["BarCode"] = personItem.BarCode;
                personCmbItem["CardNum"] = personItem.CardNum;
                personCmbItem.Id = personItem.ID.ToString();
                this.cmbPersonnel_UpdateCalculationResult.Items.Add(personCmbItem);
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Personnel_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Personnel_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Personnel_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

    protected void CallBack_GridUpdateCalculationResult_UpdateCalculationResult_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);
        string[] retMessage = new string[4];
        int Year = int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        int Month = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
        int PageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture);
        int PageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[3]), CultureInfo.InvariantCulture);
        PersonnelLoadState PLS = (PersonnelLoadState)Enum.Parse(typeof(PersonnelLoadState), this.StringBuilder.CreateString(e.Parameters[4]));
        string PersonnelSearchTerm = this.StringBuilder.CreateString(e.Parameters[5]);

        try
        {
            this.BuildGrid_UpdateCalculationResult(this.GridUpdateCalculationResult_UpdateCalculationResult);
            this.SetCalculationResultPageCount_UpdateCalculationResult(Year, Month, PLS, PersonnelSearchTerm, PageSize);
            this.Fill_GridUpdateCalculationResult_UpdateCalculationResult(Year, Month, PLS, PersonnelSearchTerm, PageIndex, PageSize);
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            this.ErrorHiddenField_UpdateCalculationResult.RenderControl(e.Output);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            this.ErrorHiddenField_UpdateCalculationResult.RenderControl(e.Output);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            this.ErrorHiddenField_UpdateCalculationResult.RenderControl(e.Output);
        }
        this.hfCalculationResultPageCount_UpdateCalculationResult.RenderControl(e.Output);
        this.ErrorHiddenField_UpdateCalculationResult.RenderControl(e.Output);
        this.GridUpdateCalculationResult_UpdateCalculationResult.RenderControl(e.Output);
    }

    private void SetCalculationResultPageCount_UpdateCalculationResult(int Year, int Month, PersonnelLoadState PLS, string PersonnelSearchTerm, int PageSize)
    {
        int PersonnelCount = 0;
        switch (PLS)
        {
            case PersonnelLoadState.Normal:
                PersonnelCount = this.UpdateCalculationResultBusiness.GetSearchCount(string.Empty, Year, Month);
                break;
            case PersonnelLoadState.Search:
                PersonnelCount = this.UpdateCalculationResultBusiness.GetSearchCount(PersonnelSearchTerm, Year, Month);
                break;
            case PersonnelLoadState.AdvancedSearch:
                PersonnelCount = this.UpdateCalculationResultBusiness.GetSearchCount(this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm), Year, Month);
                break;
        }
        this.hfCalculationResultCount_UpdateCalculationResult.Value = PersonnelCount.ToString();
        this.hfCalculationResultPageCount_UpdateCalculationResult.Value = Utility.GetPageCount(PersonnelCount, this.GridUpdateCalculationResult_UpdateCalculationResult.PageSize).ToString();
    }

    private void Fill_GridUpdateCalculationResult_UpdateCalculationResult(int Year, int Month, PersonnelLoadState PLS, string PersonnelSearchTerm, int PageIndex, int PageSize)
    {
        string[] retMessage = new string[4];
        this.InitializeCulture();

        IList<ArchiveCalcValuesProxy> ArchiveCalcValuesProxyLst = null;
        switch (PLS)
        {
            case PersonnelLoadState.Normal:
                ArchiveCalcValuesProxyLst = this.UpdateCalculationResultBusiness.GetArchiveValues(Year, Month, string.Empty, PageIndex, PageSize);
                break;
            case PersonnelLoadState.Search:
                ArchiveCalcValuesProxyLst = this.UpdateCalculationResultBusiness.GetArchiveValues(Year, Month, PersonnelSearchTerm, PageIndex, PageSize);
                break;
            case PersonnelLoadState.AdvancedSearch:
                ArchiveCalcValuesProxyLst = this.UpdateCalculationResultBusiness.GetArchiveValues(Year, Month, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm), PageIndex, PageSize);
                break;
        }
        this.GridUpdateCalculationResult_UpdateCalculationResult.DataSource = ArchiveCalcValuesProxyLst;
        this.GridUpdateCalculationResult_UpdateCalculationResult.DataBind();
    }

    private void BuildGrid_UpdateCalculationResult(ComponentArt.Web.UI.Grid grid)
    {
        IList<ArchiveFieldMap> ArchiveFieldMapList = this.UpdateCalculationResultBusiness.GetArchiveGridSettings();
        GridColumnCollection Gcc = grid.Levels[0].Columns;
        for (int i = 5; i < Gcc.Count - 1; i++)
        {
            ArchiveFieldMap archiveFieldMap = ArchiveFieldMapList.Where(afm => afm.PId.ToLower() == Gcc[i].DataField.ToLower()).FirstOrDefault();
            if (archiveFieldMap != null)
            {
                Gcc[i].Visible = archiveFieldMap.Visible;
                Gcc[i].HeadingText = archiveFieldMap.Title;
                Gcc[i].Width = archiveFieldMap.ColumnSize;
            }
            else
                Gcc[i].Visible = false;
        }
    }

    protected void CallBack_GridSettings_UpdateCalculationResult_onCallBack(object sender, CallBackEventArgs e)
    {
        string[] retMessage = new string[4];
        try
        {
            AttackDefender.CSRFDefender(this.Page);
            Dictionary<string, string> SettingsColArray = null;
            if (e.Parameters[0] == "Set")
                SettingsColArray = this.CreateRecievedColumnsArray_UpdateCalculationResult(e.Parameters[1], "Set");

            IList<ArchiveFieldMap> ArchiveFieldMapList = this.GetVisibleColumns_GridSettings_UpdateCalculationResult(e.Parameters[0], SettingsColArray);

            switch (e.Parameters[0])
            {
                case "Get":
                    this.GridSettings_UpdateCalculationResult.DataSource = ArchiveFieldMapList;
                    this.GridSettings_UpdateCalculationResult.DataBind();
                    break;
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridSettings_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridSettings_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridSettings_UpdateCalculationResult.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

        this.ErrorHiddenField_GridSettings_UpdateCalculationResult.RenderControl(e.Output);
        this.GridSettings_UpdateCalculationResult.RenderControl(e.Output);
    }

    private IList<ArchiveFieldMap> GetVisibleColumns_GridSettings_UpdateCalculationResult(string State, Dictionary<string, string> SettingsColArray)
    {
        IList<ArchiveFieldMap> ArchiveFieldMapList = new List<ArchiveFieldMap>();
        switch (State)
        {
            case "Get":
                ArchiveFieldMapList = this.UpdateCalculationResultBusiness.GetArchiveGridSettings();
                break;
            case "Set":
                foreach (string key in SettingsColArray.Keys)
                {
                    ArchiveFieldMap archiveFieldMap = new ArchiveFieldMap();
                    archiveFieldMap.PId = key;
                    archiveFieldMap.Visible = bool.Parse(SettingsColArray[key]);
                    ArchiveFieldMapList.Add(archiveFieldMap);
                }
                this.UpdateCalculationResultBusiness.SetArchiveGridSettings(ArchiveFieldMapList);
                break;
        }
        return ArchiveFieldMapList;
    }

    private Dictionary<string, string> CreateRecievedColumnsArray_UpdateCalculationResult(string RecievedStr, string State)
    {
        string[] ColStrArray = RecievedStr.Split(new char[] { ':' });
        Dictionary<string, string> ColArray = new Dictionary<string, string>();
        string[] ColStr;
        foreach (string Col in ColStrArray)
        {
            switch (State)
            {
                case "Get":
                    break;
                case "Set":
                    ColStr = Col.Split(new char[] { '%' });
                    ColArray.Add(this.StringBuilder.CreateString(ColStr[0]), ColStr[1]);
                    break;
            }
        }
        return ColArray;
    }

    [Ajax.AjaxMethod("ArchiveCalculationResult_UpdateCalculationResultPage", "ArchiveCalculationResult_UpdateCalculationResultPage_onCallBack", null, null)]
    public string[] ArchiveCalculationResult_UpdateCalculationResultPage(string Year, string Month, string PersonnelID, string personnelLoadState, string PersonnelSearchTerm, string IsForceArchiveCalculationResult, string IsForceOverrideArchiveCalculationResult)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            int year = int.Parse(this.StringBuilder.CreateString(Year), CultureInfo.InvariantCulture);
            int month = int.Parse(this.StringBuilder.CreateString(Month), CultureInfo.InvariantCulture);
            PersonnelLoadState PLS = (PersonnelLoadState)Enum.Parse(typeof(PersonnelLoadState), this.StringBuilder.CreateString(personnelLoadState));
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
            PersonnelSearchTerm = this.StringBuilder.CreateString(PersonnelSearchTerm);
            bool isForceArchiveCalculationResult = bool.Parse(this.StringBuilder.CreateString(IsForceArchiveCalculationResult));
            bool isForceOverrideArchiveCalculationResult = bool.Parse(this.StringBuilder.CreateString(IsForceOverrideArchiveCalculationResult));

            this.UpdateCalculationResultBusiness.CheckArchiveDataAccess();

            if (!isForceArchiveCalculationResult)
            {
                if (personnelID != 0)
                {
                    switch (this.UpdateCalculationResultBusiness.IsArchiveExsits(year, month, personnelID))
                    {
                        case ArchiveExistsConditions.NotExists:
                            this.UpdateCalculationResultBusiness.ArchiveData(year, month, personnelID, true);
                            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                            retMessage[1] = GetLocalResourceObject("ArchiveComplete").ToString();
                            retMessage[2] = "success";
                            retMessage[3] = ArchiveExistsConditions.NotExists.ToString();
                            break;
                        case ArchiveExistsConditions.SomeExists:
                            retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                            retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.SomeExists.ToString()).ToString();
                            retMessage[2] = "warning";
                            retMessage[3] = ArchiveExistsConditions.SomeExists.ToString();
                            break;
                        case ArchiveExistsConditions.AllExists:
                            retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                            retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.AllExists.ToString()).ToString();
                            retMessage[2] = "warning";
                            retMessage[3] = ArchiveExistsConditions.AllExists.ToString();
                            break;
                    }
                }
                else
                {
                    switch (PLS)
                    {
                        case PersonnelLoadState.Normal:
                            switch (this.UpdateCalculationResultBusiness.IsArchiveExsits(year, month, string.Empty))
                            {
                                case ArchiveExistsConditions.NotExists:
                                    this.UpdateCalculationResultBusiness.ArchiveData(year, month, string.Empty, true);
                                    retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                                    retMessage[1] = GetLocalResourceObject("ArchiveComplete").ToString();
                                    retMessage[2] = "success";
                                    retMessage[3] = ArchiveExistsConditions.NotExists.ToString();
                                    break;
                                case ArchiveExistsConditions.SomeExists:
                                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                                    retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.SomeExists.ToString()).ToString();
                                    retMessage[2] = "warning";
                                    retMessage[3] = ArchiveExistsConditions.SomeExists.ToString();
                                    break;
                                case ArchiveExistsConditions.AllExists:
                                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                                    retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.AllExists.ToString()).ToString();
                                    retMessage[2] = "warning";
                                    retMessage[3] = ArchiveExistsConditions.AllExists.ToString();
                                    break;
                            }
                            break;
                        case PersonnelLoadState.Search:
                            switch (this.UpdateCalculationResultBusiness.IsArchiveExsits(year, month, PersonnelSearchTerm))
                            {
                                case ArchiveExistsConditions.NotExists:
                                    this.UpdateCalculationResultBusiness.ArchiveData(year, month, PersonnelSearchTerm, true);
                                    retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                                    retMessage[1] = GetLocalResourceObject("ArchiveComplete").ToString();
                                    retMessage[2] = "success";
                                    retMessage[3] = ArchiveExistsConditions.NotExists.ToString();
                                    break;
                                case ArchiveExistsConditions.SomeExists:
                                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                                    retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.SomeExists.ToString()).ToString();
                                    retMessage[2] = "warning";
                                    retMessage[3] = ArchiveExistsConditions.SomeExists.ToString();
                                    break;
                                case ArchiveExistsConditions.AllExists:
                                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                                    retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.AllExists.ToString()).ToString();
                                    retMessage[2] = "warning";
                                    retMessage[3] = ArchiveExistsConditions.AllExists.ToString();
                                    break;
                            }
                            break;
                        case PersonnelLoadState.AdvancedSearch:
                            PersonAdvanceSearchProxy personAdvanceSearchProxy = this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm);
                            switch (this.UpdateCalculationResultBusiness.IsArchiveExsits(year, month, personAdvanceSearchProxy))
                            {
                                case ArchiveExistsConditions.NotExists:
                                    this.UpdateCalculationResultBusiness.ArchiveData(year, month, personAdvanceSearchProxy, true);
                                    retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                                    retMessage[1] = GetLocalResourceObject("ArchiveComplete").ToString();
                                    retMessage[2] = "success";
                                    retMessage[3] = ArchiveExistsConditions.NotExists.ToString();
                                    break;
                                case ArchiveExistsConditions.SomeExists:
                                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                                    retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.SomeExists.ToString()).ToString();
                                    retMessage[2] = "warning";
                                    retMessage[3] = ArchiveExistsConditions.SomeExists.ToString();
                                    break;
                                case ArchiveExistsConditions.AllExists:
                                    retMessage[0] = GetLocalResourceObject("RetWarningType").ToString();
                                    retMessage[1] = GetLocalResourceObject(ArchiveExistsConditions.AllExists.ToString()).ToString();
                                    retMessage[2] = "warning";
                                    retMessage[3] = ArchiveExistsConditions.AllExists.ToString();
                                    break;
                            }
                            break;
                    }
                }
            }
            else
            {
                if (personnelID != 0)
                    this.UpdateCalculationResultBusiness.ArchiveData(year, month, personnelID, isForceOverrideArchiveCalculationResult);
                else
                {
                    switch (PLS)
                    {
                        case PersonnelLoadState.Normal:
                            this.UpdateCalculationResultBusiness.ArchiveData(year, month, string.Empty, isForceOverrideArchiveCalculationResult);
                            break;
                        case PersonnelLoadState.Search:
                            this.UpdateCalculationResultBusiness.ArchiveData(year, month, PersonnelSearchTerm, isForceOverrideArchiveCalculationResult);
                            break;
                        case PersonnelLoadState.AdvancedSearch:
                            this.UpdateCalculationResultBusiness.ArchiveData(year, month, this.APSProv.CreateAdvancedPersonnelSearchProxy(PersonnelSearchTerm), isForceOverrideArchiveCalculationResult);
                            break;
                    }
                }
                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("ArchiveComplete").ToString();
                retMessage[2] = "success";
                retMessage[3] = ArchiveExistsConditions.NotExists.ToString();
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

    [Ajax.AjaxMethod("UpdateCalculationResult_UpdateCalculationResultPage", "UpdateCalculationResult_UpdateCalculationResultPage_onCallBack", null, null)]
    public string[] UpdateCalculationResult_UpdateCalculationResultPage(string state, string PersonnelID, string Year, string Month, string StrFieldsValCol)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            AttackDefender.CSRFDefender(this.Page);
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            decimal personnelID = decimal.Parse(this.StringBuilder.CreateString(PersonnelID), CultureInfo.InvariantCulture);
            int year = int.Parse(this.StringBuilder.CreateString(Year), CultureInfo.InvariantCulture);
            int month = int.Parse(this.StringBuilder.CreateString(Month), CultureInfo.InvariantCulture);
            ArchiveCalcValuesProxy archiveCalcValuesProxy = this.CreateFieldsValList_UpdateCalculationResult(this.StringBuilder.CreateString(StrFieldsValCol));
            switch (uam)
            {
                case UIActionType.EDIT:
                   archiveCalcValuesProxy= this.UpdateCalculationResultBusiness.SetArchiveValues(year, month, personnelID, archiveCalcValuesProxy);
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
            retMessage[1] = SuccessMessageBody;
            retMessage[2] = "success";
            retMessage[3] = JsSerializer.Serialize(archiveCalcValuesProxy); 
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

    private ArchiveCalcValuesProxy CreateFieldsValList_UpdateCalculationResult(string StrDayShiftCol)
    {
        Dictionary<string, object> FieldsValDic = (Dictionary<string, object>)this.JsSerializer.DeserializeObject(StrDayShiftCol);
        ArchiveCalcValuesProxy archiveCalcValuesProxy = new ArchiveCalcValuesProxy();
        foreach (string key in FieldsValDic.Keys)
        {
            PropertyInfo PInfo = typeof(ArchiveCalcValuesProxy).GetProperty(key);
            PInfo.SetValue(archiveCalcValuesProxy, FieldsValDic[key].ToString(), null);
        }
        return archiveCalcValuesProxy;
    }

}