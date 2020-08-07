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
using GTS.Clock.Business.Concept;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Security;
using GTS.Clock.Business;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Reporting;

public partial class ReportParametersConditions : GTSBasePage
{
    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }

    enum ActionGridRowOrder
    {
        Up,
        Down
    }
    enum Scripts
    {
        ReportParametersConditions_onPageLoad,
        DialogReportParametersConditions_onPageLoad,
        DialogReportParametersConditions_Operations,
        Alert_Box,
        HelpForm_Operations,
        DialogWaiting_Operations

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
    public BConcept ConceptBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BConcept>();
        }
    }
    public BDesignedReportCondition ConditionBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDesignedReportCondition>();
        }
    }
    public BDesignedReportGroupColumn GroupColumnBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDesignedReportGroupColumn>();
        }
    }
    public BDesignedReportsColumn DesignedReportColumnBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDesignedReportsColumn>();
        }
    }
    public BDesignedReportGroupColumn DesignedReportGroupColumnBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BDesignedReportGroupColumn>();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_cmbField_ReportParametersConditions.IsCallback && !CallBack_txtConditions_ReportParametersConditions.IsCallback && !CallBack_txtOrders_ReportParametersConditions.IsCallback)
        {
            Page ReportParametersConditionsPage = this;
            Ajax.Utility.GenerateMethodScripts(ReportParametersConditionsPage);



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
    protected void CallBack_txtConditions_ReportParametersConditions_onCallBack(object sender, CallBackEventArgs e)
    {
        decimal reportId = decimal.Parse(this.StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture);
        decimal personId=BUser.CurrentUser.Person.ID;
        this.Fill_txtField_ReportParametersConditions(reportId,personId);
        hfConditionValue_ReportParametersConditions.RenderControl(e.Output);
        hfTrafficConditionValue_ReportParametersConditions.RenderControl(e.Output);
        txtConditions_ReportParametersConditions.RenderControl(e.Output);
        hfConditionID_ReportParametersConditions.RenderControl(e.Output);
        ErrorHiddenField_txtConditions_ReportParametersConditions.RenderControl(e.Output);
    }
    private void Fill_txtField_ReportParametersConditions(decimal reportId,decimal personId)
    {
        string[] retMessage = new string[4];
        try
        {
            GTS.Clock.Model.Report.DesignedReportCondition designedReportConditionObj = ConditionBusiness.GetDesignedReportCondition(reportId, personId);
            if (designedReportConditionObj != null)
            {
                txtConditions_ReportParametersConditions.Value = designedReportConditionObj.ConditionText;
                hfConditionValue_ReportParametersConditions.Value = designedReportConditionObj.ConditionValue;
                hfTrafficConditionValue_ReportParametersConditions.Value = designedReportConditionObj.TrafficConditionValue;
                hfConditionID_ReportParametersConditions.Value = designedReportConditionObj.ID.ToString();
                
            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_txtConditions_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_txtConditions_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_txtConditions_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBack_txtOrders_ReportParametersConditions_onCallBack(object sender, CallBackEventArgs e)
    {
        decimal reportId = decimal.Parse(this.StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture);
        decimal personId = BUser.CurrentUser.Person.ID;
        this.Fill_txtFieldOrder_ReportParametersConditions(reportId, personId);
        hfOrderValue_ReportParametersConditions.RenderControl(e.Output);
        txtOrders_ReportParametersConditions.RenderControl(e.Output);
        hfOrderID_ReportParametersConditions.RenderControl(e.Output);
        ErrorHiddenField_txtOrders_ReportParametersConditions.RenderControl(e.Output);
    }
    private void Fill_txtFieldOrder_ReportParametersConditions(decimal reportId, decimal personId)
    {
        string[] retMessage = new string[4];
        try
        {
            GTS.Clock.Model.Report.DesignedReportCondition designedReportConditionObj = ConditionBusiness.GetDesignedReportCondition(reportId, personId);
            if (designedReportConditionObj != null)
            {
                txtOrders_ReportParametersConditions.Value = designedReportConditionObj.OrderText;
                hfOrderValue_ReportParametersConditions.Value = designedReportConditionObj.OrderValue;
                hfOrderID_ReportParametersConditions.Value = designedReportConditionObj.ID.ToString();

            }
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_txtOrders_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_txtOrders_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_txtOrders_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
    protected void CallBack_cmbField_ReportParametersConditions_onCallBack(object sender, CallBackEventArgs e)
    {
        this.cmbField_ReportParametersConditions.Dispose();

        decimal reportId = decimal.Parse(StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture);
        this.Fill_cmbField_ReportParametersConditions(reportId);
        this.ErrorHiddenField_ReportParametersConditions.RenderControl(e.Output);
        this.cmbField_ReportParametersConditions.RenderControl(e.Output);
        
    }
    private void Fill_cmbField_ReportParametersConditions(decimal reportId)
    {


        this.cmbField_ReportParametersConditions.Enabled = true;
       
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();

            IList<GTS.Clock.Model.Report.DesignedReportColumn> ColumnList = this.DesignedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
           
            foreach (GTS.Clock.Model.Report.DesignedReportColumn item in ColumnList)
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Text = item.Title;
                if (item.IsConcept)
                {
                    cbItem.Id = item.Concept.ID.ToString();
                    cbItem.Value = item.Concept.KeyColumnName;
                }
                else if(item.PersonInfo!=null)
                {
                    cbItem.Id = item.PersonInfo.ID.ToString();
                    cbItem.Value = item.ColumnName;
                }
                else if(item.Traffic!=null)
                {
                    cbItem.Id = item.Traffic.ID.ToString();
                    if (item.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic)
                        cbItem.Value = item.ColumnName + "_First";
                    else if (item.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic)
                        cbItem.Value = item.ColumnName + "_Last";
                    else
                        cbItem.Value = item.ColumnName + "_All";

                }
                cmbField_ReportParametersConditions.Items.Add(cbItem);
            }
            //ComponentArt.Web.UI.ComboBoxItem cbItemPerson = new ComboBoxItem();
            //cbItemPerson.Text = "شماره پرسنلی";
            //cbItemPerson.Value = "Prs_BarCode";
            //cmbField_ReportParametersConditions.Items.Insert(0, cbItemPerson);

            //ComponentArt.Web.UI.ComboBoxItem cbItemName = new ComboBoxItem();
            //cbItemName.Text = "نام";
            //cbItemName.Value = "Prs_FirstName";
            //cmbField_ReportParametersConditions.Items.Insert(1, cbItemName);

            //ComponentArt.Web.UI.ComboBoxItem cbItemFamily = new ComboBoxItem();
            //cbItemFamily.Text = "نام خانوادگی";
            //cbItemFamily.Value = "Prs_LastName";
            //cmbField_ReportParametersConditions.Items.Insert(2, cbItemFamily);

            //ComponentArt.Web.UI.ComboBoxItem cbItemDate = new ComboBoxItem();
            //cbItemDate.Text = "تاریخ";
            //cbItemDate.Value = "Date";
            //cmbField_ReportParametersConditions.Items.Insert(3, cbItemDate);
         
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }
    [Ajax.AjaxMethod("UpdateConditions_ReportParametersConditionsPage", "UpdateConditions_ReportParametersConditionsPage_onCallBack", null, null)]
    public string[] UpdateConditions_ReportParametersConditionsPage(string ConditionID, string ReportID, string ConditionText, string ConditionValue, string OrderText, string OrderValue, string TrafficConditionValue)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];
          
        try
        {
            decimal conditionID = decimal.Parse(this.StringBuilder.CreateString(ConditionID), CultureInfo.InvariantCulture);
            decimal reportID = decimal.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
            string conditionText="";
            if(ConditionText!=null)
             conditionText = this.StringBuilder.CreateString(ConditionText,StringGeneratorExceptionType.ReportCondition);
            string conditionValue ="";
            if(ConditionValue!=null)
              conditionValue = this.StringBuilder.CreateString(ConditionValue,StringGeneratorExceptionType.ReportCondition);

            string trafficConditionValue = "";
            if (trafficConditionValue != null)
                trafficConditionValue = this.StringBuilder.CreateString(TrafficConditionValue, StringGeneratorExceptionType.ReportCondition);
            string orderText = "";
            if (OrderText != null)
                orderText = this.StringBuilder.CreateString(OrderText, StringGeneratorExceptionType.ReportCondition);

            string orderValue = "";
            if (OrderValue != null)
                orderValue = this.StringBuilder.CreateString(OrderValue, StringGeneratorExceptionType.ReportCondition);
          
            GTS.Clock.Model.Security.User currentUser = BUser.CurrentUser;

            GTS.Clock.Model.Report.DesignedReportCondition conditionObj = new GTS.Clock.Model.Report.DesignedReportCondition();
            conditionObj.ID = conditionID;
            GTS.Clock.Model.Report.Report reportObj = new GTS.Clock.Model.Report.Report();
            GTS.Clock.Model.Person personObj = new GTS.Clock.Model.Person() { ID = currentUser.Person.ID};
            reportObj.ID = reportID;
                
                conditionObj.ConditionText = conditionText;
                conditionObj.ConditionValue = conditionValue;
                conditionObj.TrafficConditionValue = trafficConditionValue;
                conditionObj.Report = reportObj;
                conditionObj.Person = personObj; ;
                conditionObj.OrderText = orderText;
                conditionObj.OrderValue = orderValue;
                

                UIActionType uam;
                if (conditionID == 0)
                {
                    conditionID = ConditionBusiness.InsertCondition(conditionObj);
                    uam = UIActionType.ADD;
                }
                else
                {
                   
                    conditionID = ConditionBusiness.UpdateCondition(conditionObj);
                    uam=UIActionType.EDIT;
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
            retMessage[3] = conditionID.ToString();
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

    protected void CallBack_GridGroupColumns_ReportParametersConditions_onCallback(object sender, CallBackEventArgs e)
    {
        decimal reportId = decimal.Parse(StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture);
        decimal personId = BUser.CurrentUser.Person.ID;
        this.Fill_GridGroupColumns_ReportParametersConditions(reportId, personId);
        this.ErrorHiddenField_GridGroupColumn_ReportParametersConditions.RenderControl(e.Output);
        this.GridGroupColumns_ReportParametersConditions.RenderControl(e.Output);
    }
    private void Fill_GridGroupColumns_ReportParametersConditions(decimal reportId, decimal personId)
    {
        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();
            IList<DesignedReportGroupColumn> ColumnsList = this.DesignedReportGroupColumnBusiness.GetDesignedReportGroupColumns(reportId, personId);

            this.GridGroupColumns_ReportParametersConditions.DataSource = ColumnsList;
            this.GridGroupColumns_ReportParametersConditions.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_GridGroupColumn_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_GridGroupColumn_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_GridGroupColumn_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }

    protected void CallBack_cmbGroupColumnField_ReportParametersConditions_onCallback(object sender, CallBackEventArgs e)
    {
        this.cmbGroupColumnField_ReportParametersConditions.Dispose();
        decimal reportId = decimal.Parse(StringBuilder.CreateString(e.Parameter), CultureInfo.InvariantCulture);
        decimal personId = BUser.CurrentUser.Person.ID;
        this.Fill_cmbGroupColumnField_ReportParametersConditions(reportId, personId);
        this.ErrorHiddenField_cmbGroupColumn_ReportParametersConditions.RenderControl(e.Output);
        this.cmbGroupColumnField_ReportParametersConditions.RenderControl(e.Output);
    }
    private void Fill_cmbGroupColumnField_ReportParametersConditions(decimal reportId, decimal personId)
    {


        this.cmbGroupColumnField_ReportParametersConditions.Enabled = true;

        string[] retMessage = new string[4];
        try
        {
            this.InitializeCulture();

            IList<DesignedReportColumn> ColumnList = this.DesignedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId).Where(c => c.PersonInfo != null && c.PersonInfo.IsGroupColumn == true).ToList<DesignedReportColumn>(); ;

            foreach (DesignedReportColumn item in ColumnList)
            {
                ComponentArt.Web.UI.ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Text = item.Title;
                cbItem.Id = item.ID.ToString();
                cmbGroupColumnField_ReportParametersConditions.Items.Add(cbItem);
            }

            this.cmbGroupColumnField_ReportParametersConditions.Enabled = true;
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_cmbGroupColumn_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_cmbGroupColumn_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_cmbGroupColumn_ReportParametersConditions.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }

    }

    [Ajax.AjaxMethod("UpdateGroupColumn_ReportParametersConditionsPage", "UpdateGroupColumn_ReportParametersConditionsPage_onCallBack", null, null)]
    public string[] UpdateGroupColumn_ReportParametersConditionsPage(string state, string GroupColumnID, string ReportID ,string DesignedColumnID,string GroupingNewPage)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal groupColumnID = 0;
            decimal seletedGroupColumnID = decimal.Parse(this.StringBuilder.CreateString(GroupColumnID), CultureInfo.InvariantCulture);
            decimal designedColumnID = decimal.Parse(this.StringBuilder.CreateString(DesignedColumnID), CultureInfo.InvariantCulture);
            decimal reportID = decimal.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
            bool groupingNewPage = bool.Parse(StringBuilder.CreateString(GroupingNewPage));
            UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

            GTS.Clock.Model.Report.DesignedReportGroupColumn groupColumnObj = new GTS.Clock.Model.Report.DesignedReportGroupColumn();
            groupColumnObj.ID = seletedGroupColumnID;
            if (uam != UIActionType.DELETE)
            {
                GTS.Clock.Model.Report.Report reportObj = new GTS.Clock.Model.Report.Report();
                reportObj.ID = reportID;
                decimal personId = BUser.CurrentUser.Person.ID;
                GTS.Clock.Model.Person personObj = new GTS.Clock.Model.Person();
                personObj.ID = personId;
                GTS.Clock.Model.Report.DesignedReportColumn designedColumnObj = new DesignedReportColumn();
                designedColumnObj.ID = designedColumnID;
                groupColumnObj.Person = personObj;
                groupColumnObj.Report = reportObj;
                groupColumnObj.Column = designedColumnObj;
                groupColumnObj.IsGroupingNewPage = groupingNewPage;
                if (uam != UIActionType.EDIT)
                {
                    IList<GTS.Clock.Model.Report.DesignedReportGroupColumn> designedReportGroupColumnList = GroupColumnBusiness.GetDesignedReportGroupColumns(reportID, personId);
                    Int16 lastOrder = 0;
                    if (designedReportGroupColumnList.Count > 0)
                        lastOrder = designedReportGroupColumnList.OrderByDescending(o => o.Order).FirstOrDefault().Order;

                    groupColumnObj.Order = (Int16)(lastOrder + 1);
                }

            }

            switch (uam)
            {
                case UIActionType.ADD:

                    groupColumnID = this.GroupColumnBusiness.InsertGroupColumn(groupColumnObj);

                    break;
               
                case UIActionType.DELETE:
                    if (seletedGroupColumnID == 0)
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoColumnSelectedforEdit").ToString()), retMessage);
                        return retMessage;
                    }
                    groupColumnID = this.GroupColumnBusiness.DeleteGroupColumn(groupColumnObj);
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
            retMessage[3] = groupColumnID.ToString();

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
    [Ajax.AjaxMethod("UpdateGridOrder_ReportParametersConditionsPage", "UpdateGridOrder_ReportParametersConditionsPage_onCallBack", null, null)]
    public string[] UpdateGridOrder_ReportParametersConditionsPage(string Type, string OrderRow, string ReportID)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            decimal reportId = int.Parse(this.StringBuilder.CreateString(ReportID), CultureInfo.InvariantCulture);
            int selectedColumnOrder = int.Parse(this.StringBuilder.CreateString(OrderRow), CultureInfo.InvariantCulture);
            ActionGridRowOrder type = (ActionGridRowOrder)Enum.Parse(typeof(ActionGridRowOrder), this.StringBuilder.CreateString(Type));
            decimal personId = BUser.CurrentUser.Person.ID;
            IList<DesignedReportGroupColumn> columnList = DesignedReportGroupColumnBusiness.GetDesignedReportGroupColumns(reportId,personId);
            GTS.Clock.Model.Report.DesignedReportGroupColumn selectedColumn = columnList.Where(s => s.Order == selectedColumnOrder).FirstOrDefault();
            GTS.Clock.Model.Report.DesignedReportGroupColumn targetColumn = new GTS.Clock.Model.Report.DesignedReportGroupColumn();
            switch (type)
            {
                case ActionGridRowOrder.Up:
                    targetColumn = columnList.Where(t => t.Order == (Int16)(selectedColumnOrder - 1)).FirstOrDefault();
                    selectedColumn.Order = (Int16)(selectedColumn.Order - 1);
                    targetColumn.Order = (Int16)(targetColumn.Order + 1);

                    break;
                case ActionGridRowOrder.Down:
                    targetColumn = columnList.Where(t => t.Order == (Int16)(selectedColumnOrder + 1)).FirstOrDefault();

                    selectedColumn.Order = (Int16)(selectedColumn.Order + 1);
                    targetColumn.Order = (Int16)(targetColumn.Order - 1);
                    break;
                default:

                    break;
            }
            if (targetColumn != null)
            {
                decimal targetId = DesignedReportGroupColumnBusiness.UpdateGroupColumn(targetColumn);
            }
            decimal columnId = DesignedReportGroupColumnBusiness.UpdateGroupColumn(selectedColumn);

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