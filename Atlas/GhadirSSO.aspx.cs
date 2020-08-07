using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.BoxService;
using GTS.Clock.Business.Security;
using System.Web.UI.Design;
using GTS.Clock.Business.UI;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.AppSettings;
public partial class GhadirSSO : GTSBasePage
{
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
    public BGhadirSSOService GhadirSSOServiceBusiness
    {
        get
        {
            return BusinessHelper.GetBusinessInstance<BGhadirSSOService>();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GhadirSSOServiceBusiness.CheckGhadirSSOServiceLoadAccess();
		LoginToWebRest_GhadirSSO();
    }

	protected override void InitializeCulture()
	{
		this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
		base.InitializeCulture();
	}
	private void LoginToWebRest_GhadirSSO()
	{
		string[] retMessage = new string[3];
		try
		{

            string key = GhadirSSOServiceBusiness.GetKeySSOByBarcode(BUser.CurrentUser.Person.BarCode);

            string webRestUrl = GhadirSSOServiceBusiness.GetWebRestAddressURL();
			Response.Redirect(webRestUrl + "?KeyLogin=" + key,false);

		}
		catch (Exception ex)
		{
			retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
			Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
		}
	}
	private void SetCurrentCultureResObjs(string LangID)
	{
		//Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
		Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
	}
}