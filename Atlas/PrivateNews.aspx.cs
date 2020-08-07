﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Business.AppSettings;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business.UI;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Business.BoxService;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Security;

public partial class PrivateNews : GTSBasePage
{
    public BMainPageBox PrivateNewsBusiness
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

    enum Scripts
    {
        PrivateNews_onPageLoad,
        PrivateNews_Operation,
        Alert_Box
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RefererValidationProvider.CheckReferer();
        this.Fill_PrivateNews();
        SkinHelper.InitializeSkin(this.Page);
        ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
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

    private void Fill_PrivateNews()
    {
        string[] retMessage = new string[4];
        try
        {
            
            IList<KartablSummary> PrivateNewsList = this.PrivateNewsBusiness.GetKartablSummary(BUser.CurrentUser.ID);

            this.Form.Controls.Add(new LiteralControl("<table style='width:100%; font-family:Arial; font-size:small'>"));
            foreach (KartablSummary privateNewsListItem in PrivateNewsList)
            {
                string eventValue = "";
                string styleValue = "";
                string lastRequestDate = DateTime.Now.ToShortDateString();
                if (privateNewsListItem.Value!="" && privateNewsListItem.Value!="0")
                {
                    string keyItem = privateNewsListItem.Key;
                    eventValue = "onclick=InitializeLink_PrivateNews(" + "'" + keyItem + "','" + lastRequestDate + "');";
                    styleValue = "cursor:pointer;";
                    
                }

                this.Form.Controls.Add(new LiteralControl("<tr><td style='width:90%;" + styleValue + "' class='HeaderLabel' " + eventValue + ">" + privateNewsListItem.Title + "</td><td style='width:10%' class='HeaderLabel' align='center'><input type='text' class='TextBoxes' style='width:70%; height:15px; text-align:center;" + styleValue + "' readonly='readonly'" + eventValue + " value='" + privateNewsListItem.Value + "'/></td></tr>"));
            }
            this.Controls.Add(new LiteralControl("</table>"));
        }
        catch (UIValidationExceptions ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
            this.ErrorHiddenField_PrivateNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (UIBaseException ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
            this.ErrorHiddenField_PrivateNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
        catch (Exception ex)
        {
            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
            this.ErrorHiddenField_PrivateNews.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
        }
    }

}