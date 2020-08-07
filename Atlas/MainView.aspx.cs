using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Business.UI;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.AppSetting;

public partial class MainView : GTSBasePage
{
    public BLanguage LangProv
    {
        get
        {
            return new BLanguage();
        }
    }
    internal class DashboardSettingsObj
    {
        
        public string DashboardID1 { get; set; }

        public string DashboardID2 { get; set; }
        public string DashboardID3 { get; set; }
        public string DashboardID4 { get; set; }
        public string DashboardName1 { get; set; }
        public string DashboardName2 { get; set; }
        public string DashboardName3 { get; set; }
        public string DashboardName4 { get; set; }
    }
    enum Scripts
    {
        MainView_onPageLoad,
        tbMainView_Operations,
        DialogMainViewMaximizedPart_onPageLoad,
        DialogWaiting_Operations
    }
    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }
    public BUserSettings UserSettingBusiness
    {
        get
        {
            return new BUserSettings();
        }
    }
    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        Page MainViewPage = this;
        Ajax.Utility.GenerateMethodScripts(MainViewPage);
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

    [Ajax.AjaxMethod("GetDashboards_MainViewPage", "GetDashboards_MainViewPage_onCallBack", null, null)]
    public string[] GetDashboards_MainViewPage(string s)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];    
        
        try
        {
            DashboardSettings dashboardSetting = UserSettingBusiness.GetDashboardSetting();
            DashboardSettingsObj dashboardSettingObj = new DashboardSettingsObj();
            
            if (dashboardSetting.Dashboard1 != null)
                dashboardSettingObj.DashboardName1 = dashboardSetting.Dashboard1.Name;
            if (dashboardSetting.Dashboard2 != null)
                dashboardSettingObj.DashboardName2 = dashboardSetting.Dashboard2.Name;
            if (dashboardSetting.Dashboard3 != null)
                dashboardSettingObj.DashboardName3 = dashboardSetting.Dashboard3.Name;
            if (dashboardSetting.Dashboard4 != null)
                dashboardSettingObj.DashboardName4 = dashboardSetting.Dashboard4.Name;
            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("OperationCompleted").ToString();
            retMessage[2] = "success";
            retMessage[3] = this.JsSerializer.Serialize(dashboardSettingObj);
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