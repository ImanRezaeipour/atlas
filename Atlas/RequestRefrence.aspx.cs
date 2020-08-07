using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.RequestFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RequestRefrence : GTSBasePage
{
    public enum RequestCaller
    {
        RegisteredRequest
    }

    public BKartabl KartableBusiness
    {
        get
        {
            return (BKartabl)(BusinessHelper.GetBusinessInstance<BKartabl>());

        }
    }

    public BRequest RequestBusiness
    {
        get
        {
            return new BRequest();
        }
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

    enum Scripts
    {
        RequestRefrence_onPageLoad,
        DialogRequestRefrence_Operations,
        DialogWaiting_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        if (!CallBack_GridRequestRefrence_RequestRefrence.IsCallback)
        {
            Page RequestRefrence = this;
            Ajax.Utility.GenerateMethodScripts(RequestRefrence);
    
            SkinHelper.InitializeSkin(this.Page);
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

    public void CallBack_GridRequestRefrence_RequestRefrence_onCallBack(object sender, CallBackEventArgs e)
    {
        AttackDefender.CSRFDefender(this.Page);

        var RequestID = decimal.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
        var RefrenceType = Convert.ToString(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);

        this.Fill_GridRequestRefrence_RequestRefrence(RequestID, RefrenceType);
        this.ErrorHiddenField_Refrence_RequestRefrence.RenderControl(e.Output);
        this.GridRequestRefrence_RequestRefrence.RenderControl(e.Output);
    }

    private void Fill_GridRequestRefrence_RequestRefrence(decimal RequestID, string RefrenceType)
    {

        string[] retMessage = new string[4];
        try
        {
            IList<KartablProxy> requestList = new List<KartablProxy>();
            GTS.Clock.Model.RequestFlow.Request request = null;
            switch (RefrenceType.ToLower())
            {
                case "ischild":
                    request = this.RequestBusiness.GetByID(RequestID).RequestChildList.FirstOrDefault();
                    break;
                case "isparent":
                    request = this.RequestBusiness.GetByID(RequestID);
                    break;
            }
            KartablProxy proxy = this.KartableBusiness.ConvertRegisterRequestToProxy(request);
            requestList.Add(proxy);

            this.GridRequestRefrence_RequestRefrence.DataSource = requestList;
            this.GridRequestRefrence_RequestRefrence.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_Refrence_RequestRefrence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_Refrence_RequestRefrence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_Refrence_RequestRefrence.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
}