using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Threading;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.Security;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.General;
using GTS.Clock.Model.General;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Login : System.Web.UI.Page
    {

        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }

        public CacheSettingsProvider CSP
        {
            get
            {
                return new CacheSettingsProvider();
            }
        }

        public SecurityImageProvider SIP
        {
            get
            {
                return new SecurityImageProvider();
            }
        }

        enum Scripts
        {
            Login_onPageLoad,
            Login_Operations,
            keyboard,
            CryptoJS
        }

        #region interface implement

        public System.Web.UI.WebControls.Login Logincontrol
        {
            get { return theLogincontrol; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Logincontrol.LoggedIn += Logincontrol_LoggedIn;
            this.Logincontrol.LoggingIn += Logincontrol_LoggingIn;
            this.Logincontrol.LoginError += Logincontrol_LoginError;
        }

        protected override void InitializeCulture()
        {
            this.SetCurrentCultureResObjs(LangProv.GetCurrentSysLanguage());
            base.InitializeCulture();
        }

        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckLoginPageReferer();
            ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
            //if (!CheckVersionDatabaseIsValid())
            //    Response.Redirect("~/ValidateVersion.aspx");     
        }
        private bool CheckVersionDatabaseIsValid()
        {
            bool isValidate = true;
            BApplication appBusiness = new BApplication();
            VersionStatus versionStatusObj = appBusiness.GetLastDatabaseVersion();
            string[] atlasVersionStrArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            int[] atlasVersionIntArray = Array.ConvertAll(atlasVersionStrArray, s => int.Parse(s));
            string[] databaseVersionStrArray;
            if (versionStatusObj != null)
            {
                databaseVersionStrArray = versionStatusObj.Version.Split('.');
            }
            else
            {
                databaseVersionStrArray = new string[4];
                isValidate = false;
                return isValidate;
            }
            int[] databaseVersionIntArray = Array.ConvertAll(databaseVersionStrArray, s => int.Parse(s));
            for (int i = 0; i < atlasVersionIntArray.Count(); i++)
            {
                if (atlasVersionIntArray[i] != databaseVersionIntArray[i])
                    isValidate = false;
            }
            return isValidate;
        } 
        protected void Login_Click(object sender, EventArgs e)
        {
        }

        void Logincontrol_LoggingIn(object sender, LoginCancelEventArgs e)
        {
			
            if (Session["IsErrorOccured"] != null)
                if (!this.CheckSecurityCode(bool.Parse(Session["IsErrorOccured"].ToString())))
                    e.Cancel = true;
            if (this.CheckExpiredPersonEmployeeEndDate())
                e.Cancel = true;
        }

        void Logincontrol_LoggedIn(object sender, EventArgs e)
        {
			
            Session["IsErrorOccured"] = null;
            
        }
        private bool CheckExpiredPersonEmployeeEndDate()
        {
            decimal personId=new BUser().GetPersonIdByUsername(Logincontrol.UserName);
            bool IsExpiredPersonEmployeeDate = false;
            if (personId != 0)
            {
            GTS.Clock.Model.Person personObj=new GTS.Clock.Business.BPerson().GetByID(personId);
            
            
                if (personObj.EndEmploymentDate != null && personObj.EndEmploymentDate != Utility.GTSMinStandardDateTime && personObj.EndEmploymentDate < DateTime.Now)
                { 
                    IsExpiredPersonEmployeeDate = true;
                    ((Literal)this.Logincontrol.FindControl("FailureText")).Text = GetLocalResourceObject("EmployeEndDateExpired").ToString();
                }
            }

            return IsExpiredPersonEmployeeDate;

            
            
        }
        void Logincontrol_LoginError(object sender, EventArgs e)
        {
            this.CheckSecurityCode(true);
        }

        private bool CheckSecurityCode(bool isErrorOccured)
        {
            bool success = true;
            Session.Add("IsErrorOccured", isErrorOccured);
            HtmlTableRow SecurityCodeContainer = (HtmlTableRow)this.theLogincontrol.FindControl("SecurityCodeContainer");
            try
            {
                bool SecurityCodeEnabled = false;
                if (WebConfigurationManager.AppSettings["SecurityCodeEnabled"] != null)
                {
                    if (WebConfigurationManager.AppSettings["SecurityCodeEnabled"] != string.Empty)
                        SecurityCodeEnabled = bool.Parse(WebConfigurationManager.AppSettings["SecurityCodeEnabled"]);
                    else
                        SecurityCodeEnabled = false;

                }
                if (SecurityCodeEnabled && isErrorOccured)
                {
                    SecurityCodeContainer.Visible = true;
                    TextBox txtSecurityCode = (TextBox)this.theLogincontrol.FindControl("SecurityCode");
                    if (Session["SecurityCode"] != null && txtSecurityCode.Text != Session["SecurityCode"].ToString())
                    {
                        success = false;
                        Session["SecurityCode"] = null;
                        ((Literal)this.Logincontrol.FindControl("FailureText")).Text = GetLocalResourceObject("IncorrectSecurityCode").ToString();
                        txtSecurityCode.Text = "";
                    }
                    Session["SecurityCode"] = this.SIP.GenerateRandomCode();
                }
                else
                    SecurityCodeContainer.Visible = false;
                return success;
            }
            catch (Exception ex)
            {
                SecurityCodeContainer.Visible = false;
                throw ex;
            }
        }
    }
}