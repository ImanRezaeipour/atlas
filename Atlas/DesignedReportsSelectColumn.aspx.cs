using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.Concept;
using GTS.Clock.Model.Concepts;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.UI;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Model.Rules;
public partial class DesignedReportsSelectColumn : GTSBasePage
{
    internal class ColumnObj
    {
        public String KeyColumn { get; set; }
        public String ColumnType { get; set; }
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
        DesignedReportsSelectColumn_onPageLoad,
        DialogDesignedReportsSelectColumn_onPageLoad,
        DialogDesignedReportsSelectColumn_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations
        
    }
    enum ActionGridRowOrder
    {
        Up,
        Down
    }
    public BConcept ConceptBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BConcept>();
        }
    }
    public BReport ReportBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BReport>();
        }
    }
    public BDesignedReportsColumn DesignedReportsColumnBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDesignedReportsColumn>();
        }
    }
    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }
    public StringGenerator StringBuilder
    {
        get
        {
            return new StringGenerator();
        }

    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbColumnName_DesignedReportsSelectColumn.IsCallback && !CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.IsCallback)
        {
            Page DesignedReportsSelectColumnPage = this;
            Ajax.Utility.GenerateMethodScripts(DesignedReportsSelectColumnPage);
            Page UpdateGridOrder_DesignedReportsSelectColumnPage = this;
            Ajax.Utility.GenerateMethodScripts(UpdateGridOrder_DesignedReportsSelectColumnPage);


            this.InitializeSkin();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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

    private void InitializeSkin()
    {
        SkinHelper.InitializeSkin(this.Page);
       
    }
    protected void CallBack_cmbColumnName_DesignedReportsSelectColumn_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbColumnName_DesignedReportsSelectColumn.Dispose();
        GTS.Clock.Infrastructure.ScndCnpPeriodicType periodicType;
        DesignedReportTypeEnum reportType = (DesignedReportTypeEnum)Enum.Parse(typeof(DesignedReportTypeEnum),this.StringBuilder.CreateString(e.Parameters[0]));
        switch (reportType)
        {
            case DesignedReportTypeEnum.Daily:
                periodicType = ScndCnpPeriodicType.NoPeriodic;
                break;
            case DesignedReportTypeEnum.Monthly:
                periodicType = ScndCnpPeriodicType.Periodic;
                break;
            default:
                periodicType = ScndCnpPeriodicType.NoPeriodic;
                break;
        }
        decimal reportId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[1].ToString()), CultureInfo.InvariantCulture);
        this.Fill_cmbColumnName_DesignedReportsSelectColumn(periodicType,reportId);
        this.ErrorHiddenField_ColumnName.RenderControl(e.Output);
        this.cmbColumnName_DesignedReportsSelectColumn.RenderControl(e.Output);
    }

    private void Fill_cmbColumnName_DesignedReportsSelectColumn(ScndCnpPeriodicType priodicType,decimal reportId)
    {
        

        this.cmbColumnName_DesignedReportsSelectColumn.Enabled = true;

        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            // IList<GTS.Clock.Model.Concepts.SecondaryConcept> ColumnList = this.ConceptBusiness.GetAllConceptByPeriodicType(priodicType).Where(c => c.ShowInReport == true && c.KeyColumnName!=null && c.KeyColumnName!="").ToList<SecondaryConcept>() ;
            IList<DesignedColumnProxy> ColumnList = this.DesignedReportsColumnBusiness.GetAllConceptAndTrafficColumnsByPeriodicType(priodicType,reportId);
            foreach (DesignedColumnProxy item in ColumnList)
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Text = item.Name;
                cbItem.Id = item.ID.ToString();
                ColumnObj columnObj = new ColumnObj() { 
                ColumnType = item.ColumnType.ToString(),
                KeyColumn= item.KeyColumn
                
                };


                cbItem.Value = JsSerializer.Serialize(columnObj);
                cmbColumnName_DesignedReportsSelectColumn.Items.Add(cbItem);
            }

            this.cmbColumnName_DesignedReportsSelectColumn.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ColumnName.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ColumnName.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ColumnName.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    
    }
    protected void CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onCallback(object sender, CallBackEventArgs e)
    {
        decimal reportID = decimal.Parse(e.Parameter, CultureInfo.InvariantCulture);
        this.Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn(reportID);
        this.ErrorHiddenField_DesignedReportsSelectColumn.RenderControl(e.Output);
        this.GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.RenderControl(e.Output);
    }
    private void Fill_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn(decimal reportID)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<DesignedReportColumnProxy> ColumnsList = this.DesignedReportsColumnBusiness.GetDesignedReportsColumnsProxyByReportID(reportID);
            
            this.GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.DataSource = ColumnsList;
            this.GridDesignedReportsSelectColumn_DesignedReportsSelectColumn.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_DesignedReportsSelectColumn.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_DesignedReportsSelectColumn.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_DesignedReportsSelectColumn.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }
    [Ajax.AjaxMethod("UpdateColumn_DesignedReportsSelectColumnPage", "UpdateColumn_DesignedReportsSelectColumnPage_onCallBack", null, null)]
    public string[] UpdateColumn_DesignedReportsSelectColumnPage(string state, string SelectedColumnID, string Title, string Active, string ReportID, string ConceptID,string Order,string ColumnType,string TrafficColumnsCount,string KeyColumn)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal ColumnID = 0;
            decimal selectedColumnID = decimal.Parse(this.StringBuilder.CreateString(SelectedColumnID), CultureInfo.InvariantCulture);
            string title = this.StringBuilder.CreateString(Title);
            bool active = Convert.ToBoolean(this.StringBuilder.CreateString(Active) == "" ? "false" : this.StringBuilder.CreateString(Active));
            decimal reportID = decimal.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
            decimal fieldId = decimal.Parse(this.StringBuilder.CreateString(ConceptID), CultureInfo.InvariantCulture);
            Int16 order = Int16.Parse(this.StringBuilder.CreateString(Order));
            int trafficColumnsCount = int.Parse(this.StringBuilder.CreateString(TrafficColumnsCount), CultureInfo.InvariantCulture);
            bool isConcept = false;
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
            DesignedReportTrafficKeyColumn keyColumn=DesignedReportTrafficKeyColumn.AllTraffic;
            GTS.Clock.Model.Report.DesignedReportColumn columnObj = new GTS.Clock.Model.Report.DesignedReportColumn();
            columnObj.ID = selectedColumnID;
            GTS.Clock.Model.Report.Report reportObj = new GTS.Clock.Model.Report.Report();
            reportObj.ID = reportID;
            columnObj.Report = reportObj;
            if (uam != UIActionType.DELETE)
            {
                DesignedReportColumnType columnType = DesignedReportColumnType.None;
                if(ColumnType!=string.Empty)
                    columnType = (DesignedReportColumnType)Enum.Parse(typeof(DesignedReportColumnType), this.StringBuilder.CreateString(ColumnType));
                if(columnType==DesignedReportColumnType.Traffic)
                {
                  keyColumn=  (DesignedReportTrafficKeyColumn)Enum.Parse(typeof(DesignedReportTrafficKeyColumn), this.StringBuilder.CreateString(KeyColumn));
                }
                
                GTS.Clock.Model.Concepts.SecondaryConcept conceptObj = null;
                DesignedReportPersonInfoColumn personInfoObj = null;
                DesignedReportTrafficColumn trafficObj = null;
                PersonParamField personParamFieldObj = null;
                switch (columnType)
                {
                    case DesignedReportColumnType.Concept:
                        conceptObj = new SecondaryConcept();
                        conceptObj.ID = fieldId;
                        columnObj.ColumnType = DesignedReportColumnType.Concept;
                        isConcept = true;
                        
                        break;
                    case DesignedReportColumnType.Traffic:
                        trafficObj = new DesignedReportTrafficColumn();
                        trafficObj.ID = fieldId;
                        trafficObj.Key = keyColumn;
                        columnObj.ColumnType = DesignedReportColumnType.Traffic;
                        
                        break;
                    case DesignedReportColumnType.PersonInfo:
                        personInfoObj = new DesignedReportPersonInfoColumn();
                        personInfoObj.ID = fieldId;
                        columnObj.ColumnType = DesignedReportColumnType.PersonInfo;
                        break;
                    case DesignedReportColumnType.PersonParam:
                        personParamFieldObj = new PersonParamField();
                        personParamFieldObj.ID = fieldId;
                        columnObj.ColumnType = DesignedReportColumnType.PersonParam;
                        break;
                    default:
                        break;
                }
                    
                
                
                
                columnObj.Title = title;
                columnObj.Active = active;
                columnObj.Concept = conceptObj;
                columnObj.PersonInfo = personInfoObj;
                columnObj.Traffic = trafficObj;
                columnObj.PersonParam = personParamFieldObj;
                columnObj.Order = order;
                columnObj.IsConcept = isConcept;
                columnObj.TrafficColumnCount = trafficColumnsCount;
                if (uam != UIActionType.EDIT)
                {
                    IList<GTS.Clock.Model.Report.DesignedReportColumn> designedReportColumnList = DesignedReportsColumnBusiness.GetDesignedReportsColumnsByReportID(reportID);
                    Int16 lastOrder = 0;
                    if (designedReportColumnList.Count > 0)
                        lastOrder = designedReportColumnList.OrderByDescending(o => o.Order).FirstOrDefault().Order;

                    columnObj.Order = (Int16)(lastOrder + 1);
                }
                


            }

            switch (uam)
            {
                case UIActionType.ADD:
                    
                    ColumnID = this.DesignedReportsColumnBusiness.InsertColumn(columnObj);

                    break;
                case UIActionType.EDIT:
                    if (selectedColumnID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoColumnSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    ColumnID = this.DesignedReportsColumnBusiness.UpdateColumn(columnObj);
                    break;
                case UIActionType.DELETE:
                    if (selectedColumnID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoColumnSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    ColumnID= this.DesignedReportsColumnBusiness.DeleteColumn(columnObj);
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
            retMessage[3] = ColumnID.ToString();
           
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
    [Ajax.AjaxMethod("UpdateGridOrder_DesignedReportsSelectColumnPage", "UpdateGridOrder_DesignedReportsSelectColumnPage_onCallBack", null, null)]
    public string[] UpdateGridOrder_DesignedReportsSelectColumnPage(string Type,string OrderRow,string ReportID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[6];

        try
        {
            decimal reportId = int.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
            int selectedColumnOrder = int.Parse(this.StringBuilder.CreateString(OrderRow), CultureInfo.InvariantCulture);
            ActionGridRowOrder type = (ActionGridRowOrder)Enum.Parse(typeof(ActionGridRowOrder), this.StringBuilder.CreateString(Type));
            IList<DesignedReportColumn> columnList = DesignedReportsColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
            GTS.Clock.Model.Report.DesignedReportColumn selectedColumn = columnList.Where(s => s.Order == selectedColumnOrder).FirstOrDefault();
            GTS.Clock.Model.Report.DesignedReportColumn targetColumn = new GTS.Clock.Model.Report.DesignedReportColumn();
            int changeCountColumnOrder = 0;
            switch (type)
            {
                case ActionGridRowOrder.Up:
                    targetColumn = columnList.Where(t => t.Order < selectedColumnOrder).OrderByDescending(m=>m.Order).FirstOrDefault();
                    changeCountColumnOrder = selectedColumnOrder - targetColumn.Order;
                    selectedColumn.Order = (Int16)(selectedColumn.Order - changeCountColumnOrder);
                    targetColumn.Order = (Int16)(targetColumn.Order + changeCountColumnOrder);

                    break;
                case ActionGridRowOrder.Down:
                    targetColumn = columnList.Where(t => t.Order > selectedColumnOrder).OrderBy(m => m.Order).FirstOrDefault();
                    changeCountColumnOrder = targetColumn.Order - selectedColumnOrder;
                    selectedColumn.Order = (Int16)(selectedColumn.Order + changeCountColumnOrder);
                    targetColumn.Order = (Int16)(targetColumn.Order - changeCountColumnOrder);
                    break;
                default:

                    break;
            }
            if (targetColumn != null)
            {
                decimal targetId = DesignedReportsColumnBusiness.UpdateColumn(targetColumn);
            }
            decimal columnId = DesignedReportsColumnBusiness.UpdateColumn(selectedColumn);

            UIActionType uam = UIActionType.EDIT;

           


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
            retMessage[3] = columnId.ToString();
            retMessage[4] = selectedColumn.Order.ToString();
            retMessage[5] = targetColumn.Order.ToString();
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


    protected void CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbPersonInfoColumnName_DesignedReportsSelectColumn.Dispose();

        decimal reportId = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0].ToString()), CultureInfo.InvariantCulture);
        this.Fill_cmbPersonInfoColumnName_DesignedReportsSelectColumn(reportId);
        this.ErrorHiddenField_PersonInfoColumnName.RenderControl(e.Output);
        this.cmbPersonInfoColumnName_DesignedReportsSelectColumn.RenderControl(e.Output);
    }

    private void Fill_cmbPersonInfoColumnName_DesignedReportsSelectColumn(decimal reportId)
    {


        this.cmbPersonInfoColumnName_DesignedReportsSelectColumn.Enabled = true;

        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<DesignedColumnProxy> ColumnList = this.DesignedReportsColumnBusiness.GetAllDesignedReportsPersonInfoProxyColumns(reportId);

            foreach (DesignedColumnProxy item in ColumnList)
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Text = item.Name;
                cbItem.Id = item.ID.ToString();
                cbItem.Value = item.ColumnType.ToString();
                cmbPersonInfoColumnName_DesignedReportsSelectColumn.Items.Add(cbItem);
            }

            this.cmbPersonInfoColumnName_DesignedReportsSelectColumn.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PersonInfoColumnName.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PersonInfoColumnName.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PersonInfoColumnName.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }
}