using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.BoxService;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Model.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Utility;

public partial class PublicNews : GTSBasePage
{
    public BMainPageBox PublicNewsBusiness
    {
        get
        {
            return new BMainPageBox();
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
	public StringGenerator StringBuilder
	{
		get
		{
			return new StringGenerator();
		}
	}

    enum Scripts
    {
        PublicNews_onPageLoad,
        PublicNews_Operations,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
		if (!CallBack_bulletedListPublicNews_PublicNews.IsCallback)
		{
			
			SetPublicNewsPageSize_PublicNews_Substitute();
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

    private void Fill_rotrPublicNews_PublicNews(int pageSize,int pageIndex)
    {
        string[] retMessage = new string[4];
        try
        {
            IList<PublicMessage> PublicNewsList = this.PublicNewsBusiness.GetPublicMessagesByPaging(pageSize,pageIndex);
            this.bulletedListPublicNews_PublicNews.DataSource = PublicNewsList;
            this.bulletedListPublicNews_PublicNews.DataBind();
            //this.rotrPublicNews_PublicNews.DataSource = PublicNewsList;
            //this.rotrPublicNews_PublicNews.DataBind();
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }
	protected void CallBack_bulletedListPublicNews_PublicNews_onCallBack(object sender, CallBackEventArgs e)
	{
		AttackDefender.CSRFDefender(this.Page);
		this.SetPublicNewsPageCount_PublicNews(int.Parse(this.StringBuilder.CreateString(e.Parameters[0])));
		this.Fill_rotrPublicNews_PublicNews(int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture));
		this.bulletedListPublicNews_PublicNews.RenderControl(e.Output);
		this.hfPublicNewsPageCount_PublicNews.RenderControl(e.Output);
		this.ErrorHiddenField_PublicNews.RenderControl(e.Output);
	}
	private void SetPublicNewsPageCount_PublicNews(int pageSize)
	{
		string[] retMessage = new string[4];
		int PublicNewsCount = 0;
		try
		{
			PublicNewsCount= this.PublicNewsBusiness.GetAllPublicNewsCount();
			this.hfPublicNewsPageCount_PublicNews.Value = Utility.GetPageCount(PublicNewsCount, pageSize).ToString();
		}
		catch (UIValidationExceptions ex)
		{
			retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
			this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
		}
		catch (UIBaseException ex)
		{
			retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
			this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
		}
		catch (Exception ex)
		{
			retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
			this.ErrorHiddenField_PublicNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
		}
	}
	private void SetPublicNewsPageSize_PublicNews_Substitute()
	{
		this.hfPublicNewsPageSize_PublicNews.Value ="10";
	}

}