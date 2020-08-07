using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using GTS.Clock.Presentaion.Forms.App_Code;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.BaseInformation;
using System.IO;
using System.Web.Script.Serialization;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class MissionLocations : GTSBasePage
    {
        public BDutyPlace MissionLocationBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDutyPlace>();
            }
        }
        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
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

        public StringGenerator StringBuilder
        {
            get
            {
                return new StringGenerator();
            }
        }
        internal class MissionLocationObj
        {
            
            public string Name { get; set; }
            public string CustomCode { get; set; }
            public string ParentID { get; set; }
        }
        enum Scripts
        {
            MissionLocations_onPageLoad,
            tbMissionLocations_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction.IsCallback)
            {
                Page MissionLocationsPage = this;
                Ajax.Utility.GenerateMethodScripts(MissionLocationsPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckMissionLocationsLoadAccess_MissionLocations();
            }
        }

        private void CheckMissionLocationsLoadAccess_MissionLocations()
        {
            string[] retMessage = new string[4];
            try
            {
                this.MissionLocationBusiness.CheckMissionLocationsLoadAccess();
            }
            catch (UIBaseException ex)
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

        protected void CallBack_trvMissionLocationsIntroduction_MissionLocationsIntroduction_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction();
            this.ErrorHiddenField_MissionLocations.RenderControl(e.Output);
            this.trvMissionLocationsIntroduction_MissionLocationsIntroduction.RenderControl(e.Output);
        }

        private void Fill_trvMissionLocationsIntroduction_MissionLocationsIntroduction()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();

                DutyPlace rootMissLoc = this.MissionLocationBusiness.GetDutyPalcesTree();
                TreeViewNode rootMissLocNode = new TreeViewNode();
                rootMissLocNode.ID = rootMissLoc.ID.ToString();
                string rootMissLocNodeText = string.Empty;
                if (GetLocalResourceObject("MissLocNode_trvMissionLocationsIntroduction_MissionLocationsIntroduction") != null)
                    rootMissLocNodeText = GetLocalResourceObject("MissLocNode_trvMissionLocationsIntroduction_MissionLocationsIntroduction").ToString();
                else
                    rootMissLocNodeText = rootMissLoc.Name;
                rootMissLocNode.Text = rootMissLocNodeText;
                rootMissLocNode.Value = rootMissLoc.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\TreeView\\folder.gif"))
                    rootMissLocNode.ImageUrl = "Images/TreeView/folder.gif";
                this.trvMissionLocationsIntroduction_MissionLocationsIntroduction.Nodes.Add(rootMissLocNode);
                rootMissLocNode.Expanded = true;

                GetChildMissionLocation_trvMissionLocationsIntroduction_MissionLocationIntroduction(rootMissLocNode, rootMissLoc);
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionLocations.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }

        }

        private void GetChildMissionLocation_trvMissionLocationsIntroduction_MissionLocationIntroduction(TreeViewNode parentMissionLocationNode, DutyPlace parentMissionLocation)
        {
            foreach (DutyPlace childMissionLocation in this.MissionLocationBusiness.GetDutyPlaceChilds(parentMissionLocation.ID))
            {
                TreeViewNode childMissionLocationNode = new TreeViewNode();
                childMissionLocationNode.ID = childMissionLocation.ID.ToString();
                childMissionLocationNode.Text = childMissionLocation.Name;
                childMissionLocationNode.Value = childMissionLocation.CustomCode;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PathHelper.GetModulePath_Nuke() + "\\Images\\TreeView\\folder.gif"))
                    childMissionLocationNode.ImageUrl = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                parentMissionLocationNode.Nodes.Add(childMissionLocationNode);
                try
                {
                    if (parentMissionLocationNode.Parent.Parent == null)
                        parentMissionLocationNode.Expanded = true;
                }
                catch
                { }
                if (this.MissionLocationBusiness.GetDutyPlaceChilds(childMissionLocation.ID).Count > 0)
                    this.GetChildMissionLocation_trvMissionLocationsIntroduction_MissionLocationIntroduction(childMissionLocationNode, childMissionLocation);
            }

        }

        [Ajax.AjaxMethod("UpdateMissionLocation_MissionLocationsPage", "UpdateMissionLocation_MissionLocationsPage_onCallBack", null, null)]
        public string[] UpdateMissionLocation_MissionLocationsPage(string state, string ParentMissionLocationID, string SelectedMissionLocationID, string MissionLocationCode, string MissionLocationName)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal MissionLocationID = 0;
                decimal selectedMissionLocationID = decimal.Parse(this.StringBuilder.CreateString(SelectedMissionLocationID), CultureInfo.InvariantCulture);
                decimal parentMissionLocationID = decimal.Parse(this.StringBuilder.CreateString(ParentMissionLocationID), CultureInfo.InvariantCulture);
                MissionLocationCode = this.StringBuilder.CreateString(MissionLocationCode);
                MissionLocationName = this.StringBuilder.CreateString(MissionLocationName);
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                DutyPlace missionLocation = new DutyPlace();
                if (uam != UIActionType.DELETE)
                {
                    missionLocation.CustomCode = MissionLocationCode;
                    missionLocation.Name = MissionLocationName;
                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        missionLocation.ParentID = selectedMissionLocationID;
                        missionLocation.ID = 0;
                        MissionLocationID = this.MissionLocationBusiness.InsertMissionLocation(missionLocation, uam);
                        break;
                    case UIActionType.EDIT:
                        if (selectedMissionLocationID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoMissionLocationSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                        {
                            missionLocation.ParentID = parentMissionLocationID;
                            missionLocation.ID = selectedMissionLocationID;
                        }
                        MissionLocationID = this.MissionLocationBusiness.UpdateMissionLocation(missionLocation, uam);
                        break;
                    case UIActionType.DELETE:
                        if (selectedMissionLocationID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoMissionLocationSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        else
                            missionLocation.ID = selectedMissionLocationID;
                        MissionLocationID = this.MissionLocationBusiness.DeleteMissionLocation(missionLocation, uam);
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
                retMessage[3] = MissionLocationID.ToString();
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
        protected void CallBack_cmbMissionSearchResult_MissionLocationsIntroduction_onCallBack(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.cmbMissionSearchResult_MissionLocationsIntroduction.Dispose();
            this.Fill_cmbMissionSearchResult_MissionLocationsIntroduction(this.StringBuilder.CreateString(e.Parameter));
            this.ErrorHiddenField_MissionSearchResult_MissionLocationsIntroduction.RenderControl(e.Output);
            this.cmbMissionSearchResult_MissionLocationsIntroduction.RenderControl(e.Output);
        }

        private void Fill_cmbMissionSearchResult_MissionLocationsIntroduction(string SearchTerm)
        {
            string[] retMessage = new string[4];
            try
            {

                IList<DutyPlace> dutyPlaceList = this.MissionLocationBusiness.SearchMissionLocations(DutyPlaceSearchFields.DutyPlaceName, SearchTerm);

                foreach (DutyPlace dutyItem in dutyPlaceList)
                {

                    ComboBoxItem missionLocationCmbItem = new ComboBoxItem(dutyItem.Name);
                    missionLocationCmbItem.Id = dutyItem.ID.ToString();
                    
                    MissionLocationObj missionObj = new MissionLocationObj() { 
                    
                    Name=dutyItem.Name,
                    CustomCode=dutyItem.CustomCode,
                    ParentID=dutyItem.ParentID.ToString()

                    };

                    missionLocationCmbItem.Value = this.JsSerializer.Serialize(missionObj);
                    this.cmbMissionSearchResult_MissionLocationsIntroduction.Items.Add(missionLocationCmbItem);
                }
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_MissionSearchResult_MissionLocationsIntroduction.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_MissionSearchResult_MissionLocationsIntroduction.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_MissionSearchResult_MissionLocationsIntroduction.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }


    }
}