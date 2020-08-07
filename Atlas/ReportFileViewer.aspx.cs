
using ClosedXML.Excel;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class ReportFileViewer : GTSBasePage
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] retMessage = new string[3];
                MemoryStream xlsMemoryStream = new MemoryStream();
                try
                {


                
                string stiReportGUID = HttpContext.Current.Request.QueryString["ReportGUID"];
               
                Dictionary<XLWorkbook,string> reportDic = (Dictionary<XLWorkbook,string>)SessionHelper.GetSessionValue(SessionHelper.ReportOutPutFile);
                XLWorkbook workbook = reportDic.Keys.FirstOrDefault();
                string tempFileName = reportDic.Values.FirstOrDefault() + " - " + stiReportGUID;


                    try
                    {


                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + tempFileName + ".xlsx");
                        using (MemoryStream excelMemoryStream = new MemoryStream())
                        {
                            workbook.SaveAs(excelMemoryStream);
                            excelMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();

                        }




                    }
                    catch (System.Threading.ThreadAbortException abrtEx)
                    {


                    }
                    catch (Exception ex)
                    {

                    }





                }
                catch (UIValidationExceptions ex)
                {
                    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                    Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
                }
                catch (UIBaseException ex)
                {
                    retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                    Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage));
                }
                catch (Exception ex)
                {
                    if (ex is OutOfMemoryException && !HttpContext.Current.Request.QueryString.AllKeys.Contains("OME"))
                    {
                        string url = "ReportFileViewer.aspx?";
                        foreach (string key in HttpContext.Current.Request.QueryString.AllKeys)
                        {
                            url += key + "=" + HttpContext.Current.Request.QueryString[key] + "&";
                        }
                        url += "OME=true";
                        Response.Redirect(url);
                    }
                    else
                    {
                        retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                        Response.Redirect("WhitePage.aspx?Error=" + this.exceptionHandler.CreateErrorMessage(retMessage) + "&ReportGUID=" + HttpContext.Current.Request.QueryString["ReportGUID"]);
                    }
                }
                try
                {
                    Response.Clear();
                    Response.BinaryWrite(xlsMemoryStream.ToArray());
                    xlsMemoryStream.Close();
                    Response.End();
                }
                catch (Exception ex)
                {


                }
            }
        }


        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(this.LangProv.GetCurrentLanguage());
            base.InitializeCulture();
        }

        /// <summary>
        /// تنظیم زبان انتخابی کاربر 
        /// </summary>
        /// <param name="LangID"></param>
        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }
    }

}