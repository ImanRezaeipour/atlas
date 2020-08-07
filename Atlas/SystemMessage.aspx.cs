using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Infrastructure.Exceptions;

public partial class SystemMessage : GTSBasePage
{
    public BPrivateMessage SystemMessageBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BPrivateMessage>();
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

    enum Scripts
    {
        SystemMessage_onPageLoad,
        DialogSystemMessage_Operations,
        DialogWaiting_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RefererValidationProvider.CheckReferer();

            Page SystemMessagePage = this;
            Ajax.Utility.GenerateMethodScripts(SystemMessagePage);

            this.CheckSystemMessagesLoadAccess_SystemMessage();
            SkinHelper.InitializeSkin(this.Page);
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
        }
    }

    private void CheckSystemMessagesLoadAccess_SystemMessage()
    {
        string[] retMessage = new string[4];
        try
        {
            this.SystemMessageBusiness.CheckSystemMessageLoadAccess();
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

    [Ajax.AjaxMethod("SendSystemMessage_SystemMessagePage", "SendSystemMessage_SystemMessagePage_onCallBack", null, null)]
    public string[] SendSystemMessage_SystemMessagePage(string Subject, string Message)
    {
        this.InitializeCulture();

        string[] retMessage = new string[4];

        try
        {
            Subject = this.StringBuilder.CreateString(Subject);
            Message = this.StringBuilder.CreateString(Message);

            this.SystemMessageBusiness.NewMessage(Subject, Message);

            retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
            retMessage[1] = GetLocalResourceObject("SendComplete").ToString();
            retMessage[2] = "success";
            retMessage[3] = "";

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