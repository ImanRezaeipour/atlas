using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.BaseInformation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTS.Clock.Presentaion.WebForms
{

    public partial class DutyPlace_ReportParameter : GTSBasePage
    {
        public BDutyPlace DutyBusiness
        {
            get
            {
                return new BDutyPlace();
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
            DutyPlace_ReportParameter_onPageLoad,
            DutyPlace_ReportParameter_Operations,
            Alert_Box,
            DialogWaiting_Operations
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            Page DutyPlace_ReportParameterForm = this;
            Ajax.Utility.GenerateMethodScripts(DutyPlace_ReportParameterForm);
            this.SetReportParameterID_DutyPlace_ReportParameter();
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
        private void SetReportParameterID_DutyPlace_ReportParameter()
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("ReportParametersID"))
                this.ReportParameterID.Value = this.StringBuilder.CreateString(HttpContext.Current.Request.QueryString["ReportParametersID"]);
        }
        public string GetParameterValue(decimal fileId, decimal uiParamerId, string actionId, List<decimal> dutyPlaceIdList, bool isContainsSubDuty)
        {
            try
            {

                IList<decimal> dutyPlaceIds = new List<decimal>();
                foreach (decimal item in dutyPlaceIdList)
                {
                    if (!dutyPlaceIds.Contains(item))
                        dutyPlaceIds.Add(item);
                    if (isContainsSubDuty)
                        GetChildDutyPlaces(item, dutyPlaceIds);
                }

                string result = "@dutyPlaceIds=0;";
                if (dutyPlaceIdList.Count > 0)
                    result = String.Format("@dutyPlaceIds={0};", string.Join(",", dutyPlaceIds.Select(x => x.ToString()).ToArray()));
                //else
                //    result = String.Format("@dutyPlaceIds={0};@IsSelectedDuty={1};", "","0");
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetChildDutyPlaces(decimal dutyPlaceId, IList<decimal> dutyPlaceIds)
        {
            IList<DutyPlace> dutyPlaceList = new BDutyPlace().GetDutyPlaceChilds(dutyPlaceId);
            foreach (DutyPlace item in dutyPlaceList)
            {
                if (!dutyPlaceIds.Contains(item.ID))
                    dutyPlaceIds.Add(item.ID);

                GetChildDutyPlaces(item.ID, dutyPlaceIds);
            }



        }
        [Ajax.AjaxMethod("Register_DutyPlace_ReportParameterPage", "Register_DutyPlace_ReportParameterPage_onCallBack", null, null)]
        public string[] Register_DutyPlace_ReportParameterrPage(string ReportParameterID, string ReportParameterActionID, string ReportFileID, string dutyPlace, string IsContainsSubDuty)
        {
            string[] retMessage = new string[4];
            this.InitializeCulture();
            try
            {
                AttackDefender.CSRFDefender(this.Page);
                string RetValue = string.Empty;
                decimal reportParameterID = decimal.Parse(this.StringBuilder.CreateString(ReportParameterID), CultureInfo.InvariantCulture);
                ReportParameterActionID = this.StringBuilder.CreateString(ReportParameterActionID);
                decimal reportFileID = decimal.Parse(this.StringBuilder.CreateString(ReportFileID), CultureInfo.InvariantCulture);
                dutyPlace = this.StringBuilder.CreateString(dutyPlace);
                List<decimal> dutyPlaceIdList = new AdvancedPersonnelSearchProvider().CreateIdListFromSerializationStringId(dutyPlace);
                bool isContainsSubDuty = Convert.ToBoolean(this.StringBuilder.CreateString(IsContainsSubDuty));

                RetValue = this.GetParameterValue(reportFileID, reportParameterID, ReportParameterActionID, dutyPlaceIdList, isContainsSubDuty);

                retMessage[0] = HttpContext.GetLocalResourceObject("~/DutyPlace_ReportParameter.aspx", "RetSuccessType").ToString();
                retMessage[1] = HttpContext.GetLocalResourceObject("~/DutyPlace_ReportParameter.aspx", "EditComplete").ToString();
                retMessage[2] = "success";
                retMessage[3] = RetValue;

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
        protected void CallBack_cmbDutyPlace_ReportParameter_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbDutyPlace_ReportParameter.Dispose();
            this.Fill_cmbDutyPlace_ReportParameter();
            this.ErrorHiddenField_DutyPlace_ReportParameter.RenderControl(e.Output);
            this.cmbDutyPlace_ReportParameter.RenderControl(e.Output);
        }

        private void Fill_cmbDutyPlace_ReportParameter()
        {
            this.Fill_trvDutyPlace_ReportParameter();
        }

        private void Fill_trvDutyPlace_ReportParameter()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                IList<DutyPlace> dutyPlcaeList = this.DutyBusiness.GetAll();
                DutyPlace rootDuty = this.DutyBusiness.GetDutyPalcesTree();
                TreeViewNode rootDutyNode = new TreeViewNode();
                rootDutyNode.ShowCheckBox = true;
                rootDutyNode.ID = rootDuty.ID.ToString();
                string rootDepNodeText = string.Empty;
                if (GetLocalResourceObject("OrgNode_trvDutyPlace_ReportParameter") != null)
                    rootDepNodeText = GetLocalResourceObject("OrgNode_trvDutyPlace_ReportParameter").ToString();
                else
                    rootDepNodeText = rootDuty.Name;
                rootDutyNode.Text = rootDepNodeText;
                rootDutyNode.Value = rootDuty.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\TreeView\\folder.gif"))
                    rootDutyNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvDutyPlace_ReportParameter.Nodes.Add(rootDutyNode);
                rootDutyNode.Expanded = true;

                this.GetChildDuty_trvDutyPlace_ReportParameter(dutyPlcaeList, rootDutyNode, rootDuty);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_DutyPlace_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_DutyPlace_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_DutyPlace_ReportParameter.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        private void GetChildDuty_trvDutyPlace_ReportParameter(IList<DutyPlace> dutyPlacesList, TreeViewNode parentDutyNode, DutyPlace parentDutyPlace)
        {
            foreach (DutyPlace childDuty in this.DutyBusiness.GetDutyPlaceChilds(parentDutyPlace.ID))
            {
                TreeViewNode childDepNode = new TreeViewNode();
                childDepNode.ShowCheckBox = true;
                childDepNode.ID = childDuty.ID.ToString();
                childDepNode.Text = childDuty.Name;
                childDepNode.Value = childDuty.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    childDepNode.ImageUrl = "Images/TreeView/folder.gif";
                parentDutyNode.Nodes.Add(childDepNode);
                try
                {
                    if (parentDutyNode.Parent.Parent == null)
                        parentDutyNode.Expanded = true;
                }
                catch
                { }
                if (this.DutyBusiness.GetDutyPlaceChilds(childDuty.ID).Count > 0)
                    this.GetChildDuty_trvDutyPlace_ReportParameter(dutyPlacesList, childDepNode, childDuty);
            }
        }
    }

}