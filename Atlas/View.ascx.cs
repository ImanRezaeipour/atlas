/*
' Copyright (c) 2015  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using System.Web.Script.Serialization;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.CustomValidator;
using System.Threading;
using System.Globalization;
using GTS.Clock.Business;
using System.Collections;
using ComponentArt.Web.UI;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Security;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Web;

namespace Christoc.Modules.Atlas
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from AtlasModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    /// AtlasModuleBase
    public partial class View : GTSBasePageNuke, IActionable
    {
        public BCustomValidation bCustomValidation
        {
            get
            {
                return new BCustomValidation();
            }
        }

        public BUserSettings bUserSetting
        {
            get
            {
                return new BUserSettings();
            }
        }
        public BUser bUser
        {
            get
            {
                return new BUser();
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

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        enum MenuItemType
        {
            Top,
            SkinsGroup,
            Skin
        }

        enum NavBarItemType
        {
            UsingBySubstitiute,
            Normal
        }
        enum LanguageType
        {
            RightToLeft,
            LeftToRight
        }
        public string CurrentUILangID { get; set; }
        public string CurrentSysLangID { get; set; }

        public OperationYearMonthProvider operationYearMonthProvider
        {
            get
            {
                return new OperationYearMonthProvider();
            }
        }

        public JavaScriptSerializer JsSerializer
        {
            get
            {
                return new JavaScriptSerializer();
            }
        }
        enum Css
        {
            tabStyle,
            navStyle,
            multiPage,
            dialog,
            iframe,
            tableStyle,
            mainpage,
            label,
            dockMenu

        }
        enum Scripts
        {
            NavBarMain_Operations,
            MainTabStrip_Operations,
            MainForm_onPageLoad,
            MainForm_Operations,
            DialogPersonnelMainInformation_onPageLoad,
            DialogCalendar_onPageLoad,
            DialogPersonnelSearch_onPageLoad,
            DialogExceptionShifts_onPageLoad,
            DialogLeaveBudget_onPageLoad,
            DialogUserInterfaceAccessLevels_onPageLoad,
            DialogMonthlyOperationGridSchema_onPageLoad,
            DialogMonthlyOperationGanttChartSchema_onPageLoad,
            DialogUnderManagementPersonnel_onPageLoad,
            DialogKartable_onPageLoad,
            DialogLoading_Operations,
            DialogEndorsementFlowState_onPageLoad,
            DialogRegisteredRequests_onPageLoad,
            DialogCalculationRange_onPageLoad,
            DialogRulesGroupUpdate_onPageLoad,
            DialogMasterRulesView_onPageLoad,
            DialogReportParameters_onPageLoad,
            DialogSubstituteSettings_onPageLoad,
            DialogOperators_onPageLoad,
            DialogLeaveReserve_onPageLoad,
            DialogMasterDataAccessLevels_onPageLoad,
            DialogSendPrivateMessage_onPageLoad,
            DialogUiValidationRules_onPageLoad,
            DialogUserSettings_onPageLoad,
            DialogTrafficsTransfer_onPageLoad,
            DialogSystemReports_onPageLoad,
            DialogMonthlyExceptionShifts_onPageLoad,
            DialogUpdateCalculationResult_onPageLoad,
            DialogDesignedReportsSelectColumn_onPageLoad,
            DialogReportParametersConditions_onPageLoad,
            DialogConceptsManagement_onPageLoad,
            DialogRulesManagement_onPageLoad,
            DialogConceptRuleEditor_onPageLoad,
            DialogExpressionsManagement_onPageLoad,
            DialogDefinePhysicians_onPageLoad,
            DialogDefineIllness_onPageLoad,
            DialogOnlineTraffics_onPageLoad,
            DialogFlowConditions_onPageLoad,
            TopToolBar_Operations,
            DockedMenu,
            DockMenuOperations,
            DialogRequestHistory_onPageLoad,
            DialogRequestRefrence_onPageLoad,
            DialogRulePrecards_onPageLoad,
            DialogWorkFlowDetail_onPageLoad,
            DialogRuleGenerator_onPageLoad
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            InitializeCulture();
            base.SetUserAndAthorize();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (DotNetNuke.Framework.AJAX.IsInstalled())
                {
                    DotNetNuke.Framework.AJAX.RegisterScriptManager();
                }
                if (!Page.IsPostBack)
                {
                    RegisterClientScripts();
                    // RefererValidationProvider.CheckMainPageReferer();
                    Page MainForm = this.Page;
                    Ajax.Utility.GenerateMethodScripts(MainForm);
                    this.SetCurrentVersion();
                    // this.SetCurrentUser();
                    this.GetCustomValidation();
                    CacheSettingsProvider.ClearCache();
                    this.CreateAccessNotAllowdNavBarItemsList();
                    this.GetCurrentLangID();
                    this.InitializeSkin();
                    this.InitializeMonthlyOperationSchema();
                    ScriptHelper.InitializeScriptsNuke(this.Page, this, typeof(Scripts));
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        private void RegisterClientScripts()
        {
            String scriptText = "<script type=\"text/javascript\">" + " var clientPrefix = '" + this.ClientID + "_';var ModulePath='DesktopModules/Atlas/';</" + "script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegistClientId", scriptText);

        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        private void GetCustomValidation()
        {
            this.bCustomValidation.GetCustomValidation();
        }
        private void SetCurrentVersion()
        {
            this.lblCurrentVersion.Text = this.lblCurrentVersion.Text + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void InitializeSkin()
        {
            SkinHelper.InitializeSkinNuke(this.Page, this, typeof(Css));
            //this.SetRelativeSkinHeaderFlash();
            this.SetRelativeSkinHeaderLogo();
            SkinHelper.SetRelativeTabStripImageBaseUrlNuke(this.Page, this.TabStripMenus, this);
        }

        private void InitializeMonthlyOperationSchema()
        {
            this.hfMonthlyOperationSchema_MainForm.Value = ((MonthlyOperationSchema)MonthlyOperationSchemaHelper.InitializeMonthlyOperationSchema(this.Page)).ToString();

        }
        private void SetRelativeSkinHeaderLogo()
        {
            // this.imgHeaderLogo.Src = SkinHelper.GetRelativeHeaderLogo(this.Page);
        }


        private void GetCurrentLangID()
        {
            this.hfCurrentUILangID.Value = CurrentUILangID;
            this.hfCurrentSysLangID.Value = CurrentSysLangID;
        }

        private void SetCurrentUser()
        {
            lblCurrentUser.Text = GetLocalResourceObject("WelcomeMessage").ToString() + " " + this.GetLocalResourceObject(BUser.CurrentUser.Person.Sex.ToString()).ToString() + " " + BUser.CurrentUser.Person.LastName;
        }


        private void InitializeCulture()
        {
            this.CurrentUILangID = this.LangProv.GetCurrentLanguage();
            this.CurrentUILangID = this.CurrentUILangID;
            this.CurrentSysLangID = this.LangProv.GetCurrentSysLanguage();
            this.SetCurrentCultureResObjs(this.CurrentUILangID);
            this.SetResourceInControls();

        }

        private void SetCurrentCulture()
        {
            if (this.CurrentUILangID != null)
            {
                try
                {
                    GTS.Clock.Business.AppSettings.SupportedLangs sl = (GTS.Clock.Business.AppSettings.SupportedLangs)Enum.Parse(typeof(GTS.Clock.Business.AppSettings.SupportedLangs), this.CurrentUILangID.Replace("-", string.Empty));
                    this.LangProv.SetCurrentLanguage(this.CurrentUILangID);
                    //Response.Redirect("/MainPage");
                    Response.Redirect(Request.RawUrl);
                }
                catch
                {
                }
            }
        }
        private void CheckApplicationIsAvailable()
        {
            bool IsAvailable = true;
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = Server.MapPath("~/web.config");
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["IsUnderConstruction"] != null)
            {
                try
                {
                    bool IsUnderConstructon = bool.Parse(config.AppSettings.Settings["IsUnderConstruction"].Value);
                    IsAvailable = IsUnderConstructon && BUser.CurrentUser.Role.CustomCode != "1" ? false : true;
                }
                catch (Exception)
                {
                }
            }
            if (!IsAvailable)
                Response.Redirect("UnderConstruction.aspx");
        }
        private void SetCurrentCultureResObjs(string LangID)
        {
            try
            {
                Session["LangID"] = LangID;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
                //Localization.SetLanguage("fa-IR");
                //this.PortalSettings.CultureCode = "fa-IR";
                //this.PortalSettings.DefaultLanguage = "fa-IR";


                //Localization.SetThreadCultures(CultureInfo.CreateSpecificCulture(LangID), this.PortalSettings);

            }
            catch
            { }
        }

        protected void imgbtnPersian_onClick(object sender, ImageClickEventArgs e)
        {
            this.CurrentUILangID = "fa-IR";
            this.SetCurrentCulture();
            this.operationYearMonthProvider.ResetOperationYearMonth();
        }

        protected void ImgbtnEnglish_onClick(object sender, ImageClickEventArgs e)
        {
            this.CurrentUILangID = "en-US";
            this.SetCurrentCulture();
            this.operationYearMonthProvider.ResetOperationYearMonth();
        }

        protected void imgbtnLogOut_onClick(object sender, ImageClickEventArgs e)
        {
            //Application.Lock();
            //Application["UserOnlineCount"] = Convert.ToInt32(Application["UserOnlineCount"]) - 1;
            //Application.UnLock();
            BUser.ClearUserCach();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();


        }
        [Ajax.AjaxMethod("ChangeSkin_MainPage", "ChangeSkin_MainPage_onCallBack", null, null)]
        public string[] ChangeSkin_MainPage(string SkinID)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];

            decimal skinID = decimal.Parse(this.StringBuilder.CreateString(SkinID), CultureInfo.InvariantCulture);

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                SkinHelper.SetCurrentSkin(skinID);

                retMessage[0] = GetLocalResourceObject("RetSuccessType").ToString();
                retMessage[1] = GetLocalResourceObject("SkinChangeComplete").ToString();
                retMessage[2] = "success";
                retMessage[3] = skinID.ToString();
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
        private void CreateAccessNotAllowdNavBarItemsList()
        {
            ArrayList AccessNotAllowdNavBarItemsList = new ArrayList();
            foreach (NavBarItem nvbItem in base.AccessNotAllowdNavBarItemsList)
            {
                AccessNotAllowdNavBarItemsList.Add(nvbItem.ID);
            }
            this.hfAccessNoAllowdNavBarItemsList.Value = this.JsSerializer.Serialize(AccessNotAllowdNavBarItemsList);
        }

        private void SetResourceInControls()
        {

            this.MainForm.Style[HtmlTextWriterStyle.Direction] = GetLocalResourceObject("MainForm.dir").ToString();
            this.lblCurrentVersion.Text = GetLocalResourceObject("lblCurrentVersion.Text").ToString();
            this.TdImageButtonLogOut.Style[HtmlTextWriterStyle.TextAlign] = GetLocalResourceObject("InverseAlignObj.align").ToString();
            //Navbar***************************
            this.NavBarMain.CssClass = GetLocalResourceObject("NavBarMain.CssClass").ToString();
            this.NavBarMain.DefaultItemTextAlign = (TextAlign)Enum.Parse(typeof(TextAlign), GetLocalResourceObject("NavBarMain.DefaultItemTextAlign").ToString());
            this.NavBarMain.ItemLooks[0].CssClass = GetLocalResourceObject("TopItemLook.CssClass").ToString();
            this.NavBarMain.ItemLooks[0].ActiveCssClass = GetLocalResourceObject("TopItemLook.ActiveCssClass").ToString();
            this.NavBarMain.ItemLooks[0].HoverCssClass = GetLocalResourceObject("TopItemLook.HoverCssClass").ToString();
            this.NavBarMain.ItemLooks[1].CssClass = GetLocalResourceObject("Level2ItemLook.CssClass").ToString();
            this.NavBarMain.ItemLooks[1].HoverCssClass = GetLocalResourceObject("Level2ItemLook.HoverCssClass").ToString();
            this.NavBarMain.ItemLooks[2].CssClass = GetLocalResourceObject("Level2SelectedItemLook.CssClass").ToString();
            this.NavBarMain.ItemLooks[2].HoverCssClass = GetLocalResourceObject("Level2SelectedItemLook.HoverCssClass").ToString();
            this.NavBarMain.FindItemById("nvbItemBasicDefinitions_NavBarMain").SubGroupCssClass = GetLocalResourceObject("nvbItemBasicDefinitions_NavBarMain.SubGroupCssClass").ToString();
            this.NavBarMain.FindItemById("nvbItemLateralOperations_NavBarMain").SubGroupCssClass = GetLocalResourceObject("nvbItemLateralOperations_NavBarMain.SubGroupCssClass").ToString();
            this.NavBarMain.FindItemById("nvbItemPresenceAndAbsenceOperation_NavBarMain").SubGroupCssClass = GetLocalResourceObject("nvbItemPresenceAndAbsenceOperation_NavBarMain.SubGroupCssClass").ToString();


            // BasicDefinitions ******
            this.NavBarMain.FindItemById("nvbItemBasicDefinitions_NavBarMain").Text = GetLocalResourceObject("nvbItemsBasicDefinitions_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonnelIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemPersonnelIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemEmployTypesIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemEmployTypesIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemCorporationsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemCorporationsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemDepartmentsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemDepartmentsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPostsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemPostsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemRulesGroupIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemRulesGroupIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemShiftPairTypesIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemShiftPairTypesIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemShiftIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemShiftIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkHeatIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemWorkHeatIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPhysicianIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemPhysicianIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemIllnessIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemIllnessIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemGradeIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemGradeIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemCostCenterIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemCostCenterIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemFlowGroupIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemFlowGroupIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemMissionLocationsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemMissionLocationsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemControlStationIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemControlStationIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemMachineIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemMachineIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemYearlyHolidaysIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemYearlyHolidaysIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkGroupsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemWorkGroupsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemContractIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemContractIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemContractorsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemContractorsIntroduction_NavBarMain.Text").ToString();
            // End BasicDefinitions ******


            // JustificationOperation*****عملیات مجوز
            this.NavBarMain.FindItemById("nvbItemJustificationOperation_NavBarMain").Text = GetLocalResourceObject("nvbItemJustificationOperation_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemRegisteredRequests_NavBarMain").Text = GetLocalResourceObject("nvbItemRegisteredRequests_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemRegisteredRequests_NavBarMain").Width = new System.Web.UI.WebControls.Unit(80);
            this.NavBarMain.FindItemById("nvbItemKartable_NavBarMain").Text = GetLocalResourceObject("nvbItemKartable_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemSurveyedRequests_NavBarMain").Text = GetLocalResourceObject("nvbItemSurveyedRequests_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemSpecialRequests_NavBarMain").Text = GetLocalResourceObject("nvbItemSpecialRequests_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemManagerMasterMonthlyOperationReport_NavBarMain").Text = GetLocalResourceObject("nvbItemManagerMasterMonthlyOperationReport_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain").Text = GetLocalResourceObject("nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkFlowsView_NavBarMain").Text = GetLocalResourceObject("nvbItemWorkFlowsView_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemMasterManagersIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemMasterManagersIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemSubstituteIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemSubstituteIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkFlowDetail_NavBarMain").Text = GetLocalResourceObject("nvbItemWorkFlowDetail_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemRequestSubstituteKartable_NavBarMain").Text = GetLocalResourceObject("nvbItemRequestSubstituteKartable_NavBarMain.Text").ToString();
            // End JustificationOperation ****عملیات مجوز


            // PresenceAndAbsenceOperation ****
            this.NavBarMain.FindItemById("nvbItemPresenceAndAbsenceOperation_NavBarMain").Text = GetLocalResourceObject("nvbItemPresenceAndAbsenceOperation_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemCalculationRangeDefinition_NavBarMain").Text = GetLocalResourceObject("nvbItemCalculationRangeDefinition_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemUiValidation_NavBarMain").Text = GetLocalResourceObject("nvbItemUiValidation_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemExceptionShiftsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemExceptionShiftsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemMasterLeaveRemains_NavBarMain").Text = GetLocalResourceObject("nvbItemMasterLeaveRemains_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemTrafficsControl_NavBarMain").Text = GetLocalResourceObject("nvbItemTrafficsControl_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemSentry_NavBarMain").Text = GetLocalResourceObject("nvbItemSentry_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemCalculationsResultsUpdate_NavBarMain").Text = GetLocalResourceObject("nvbItemCalculationsResultsUpdate_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertime_NavBarMain").Text = GetLocalResourceObject("nvbItemOvertime_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemApprovalSchedulelist_NavBarMain").Text = GetLocalResourceObject("nvbItemApprovalSchedulelist_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertimePersonlist_NavBarMain").Text = GetLocalResourceObject("nvbItemOvertimePersonlist_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertimeEdarilist_NavBarMain").Text = GetLocalResourceObject("nvbItemOvertimeEdarilist_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertimeSendFunctionlist_NavBarMain").Text = GetLocalResourceObject("nvbItemOvertimeSendFunctionlist_NavBarMain.Text").ToString();
            // End PresenceAndAbsenceOperation ****

            //  LateralOperations ****
            this.NavBarMain.FindItemById("nvbItemLateralOperations_NavBarMain").Text = GetLocalResourceObject("nvbItemLateralOperations_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemRolesIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemRolesIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemUsersIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemUsersIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPasswordChange_NavBarMain").Text = GetLocalResourceObject("nvbItemPasswordChange_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonnelOrganizationFeaturesChange_NavBarMain").Text = GetLocalResourceObject("nvbItemPersonnelOrganizationFeaturesChange_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemMasterPublicNews_NavBarMain").Text = GetLocalResourceObject("nvbItemMasterPublicNews_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPrivateMessage_NavBarMain").Text = GetLocalResourceObject("nvbItemPrivateMessage_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemCalculations_NavBarMain").Text = GetLocalResourceObject("nvbItemCalculations_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonalUserSettings_NavBarMain").Text = GetLocalResourceObject("nvbItemPersonalUserSettings_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemManagementUserSettings_NavBarMain").Text = GetLocalResourceObject("nvbItemManagementUserSettings_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemOnlineTraffics_NavBarMain").Text = GetLocalResourceObject("nvbItemOnlineTraffics_NavBarMain.Text").ToString();
            //this.NavBarMain.FindItemById("nvbItemConceptRuleMasterOperation_NavBarMain").Text = GetLocalResourceObject("nvbItemConceptRuleMasterOperation_NavBarMain.Text").ToString();
            //this.NavBarMain.FindItemById("nvbItemExpressionsOperation_NavBarMain").Text = GetLocalResourceObject("nvbItemExpressionsOperation_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemWebRest_NavBarMain").Text = GetLocalResourceObject("nvbItemWebRest_NavBarMain.Text").ToString();
            // End LateralOperations *****

            // Reports****گزارش ها
            this.NavBarMain.FindItemById("nvbItemReports_NavBarMain").Text = GetLocalResourceObject("nvbItemReports_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemReportsIntroduction_NavBarMain").Text = GetLocalResourceObject("nvbItemReportsIntroduction_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemSystemReports_NavBarMain").Text = GetLocalResourceObject("nvbItemSystemReports_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemDesignedReports_NavBarMain").Text = GetLocalResourceObject("nvbItemDesignedReports_NavBarMain.Text").ToString();
            // End Reports ****گزارش ها


            // RuleGenerator***قانون ساز
            this.NavBarMain.FindItemById("nvbItemRuleGenerator_NavBarMain").Text = GetLocalResourceObject("nvbItemRuleGenerator_NavBarMain.Text").ToString();
            this.NavBarMain.FindItemById("nvbItemRulesManagement_NavBarMain").Text = GetLocalResourceObject("nvbItemRulesManagement_NavBarMain.Text").ToString();
            //this.NavBarMain.FindItemById("nvbItemConceptsManagement_NavBarMain").Text = GetLocalResourceObject("nvbItemConceptsManagement_NavBarMain.Text").ToString();
            //this.NavBarMain.FindItemById("nvbItemExpressionsOperation_NavBarMain").Text = GetLocalResourceObject("nvbItemExpressionsOperation_NavBarMain.Text").ToString();
            //End RuleGenerator***قانون ساز

            LanguageType languageType;
            //right Navbar Resources
            switch (BLanguage.CurrentLocalLanguage)
            {
                case GTS.Clock.Infrastructure.LanguagesName.Unknown:
                    languageType = LanguageType.LeftToRight;
                    break;
                case GTS.Clock.Infrastructure.LanguagesName.Parsi:
                    languageType = LanguageType.RightToLeft;
                    break;
                case GTS.Clock.Infrastructure.LanguagesName.English:
                    languageType = LanguageType.LeftToRight;
                    break;
                default:
                    languageType = LanguageType.LeftToRight;
                    break;
            }

            //switch (languageType)
            //{
            //    case LanguageType.LeftToRight:
            //        // BasicDefinitions ******
            //        this.NavBarMain.FindItemById("nvbItemBasicDefinitions_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemsBasicDefinitions_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPersonnelIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPersonnelIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemEmployTypesIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemEmployTypesIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemCorporationsIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemCorporationsIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemDepartmentsIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemDepartmentsIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPostsIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPostsIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemRulesGroupIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemRulesGroupIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemShiftPairTypesIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemShiftPairTypesIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemShiftIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemShiftIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemWorkHeatIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemWorkHeatIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPreCardIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPreCardIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPhysicianIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPhysicianIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemIllnessIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemIllnessIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemGradeIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemGradeIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemFlowGroupIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemFlowGroupIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemMissionLocationsIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemMissionLocationsIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemControlStationIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemControlStationIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemMachineIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemMachineIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemYearlyHolidaysIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemYearlyHolidaysIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        //End BasicDefinitions ******

            //        // JustificationOperation*****
            //        this.NavBarMain.FindItemById("nvbItemJustificationOperation_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemJustificationOperation_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemRegisteredRequests_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemRegisteredRequests_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemKartable_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemKartable_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemSurveyedRequests_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemSurveyedRequests_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemSpecialRequests_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemSpecialRequests_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemManagerMasterMonthlyOperationReport_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemManagerMasterMonthlyOperationReport_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemWorkFlowsView_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemWorkFlowsView_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemMasterManagersIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemMasterManagersIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemSubstituteIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemSubstituteIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        // End JustificationOperation ****

            //        // PresenceAndAbsenceOperation ****
            //        this.NavBarMain.FindItemById("nvbItemPresenceAndAbsenceOperation_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPresenceAndAbsenceOperation_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemCalculationRangeDefinition_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemCalculationRangeDefinition_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemUiValidation_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemUiValidation_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemExceptionShiftsIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemExceptionShiftsIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemMasterLeaveRemains_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemMasterLeaveRemains_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemTrafficsControl_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemTrafficsControl_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemSentry_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemSentry_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemCalculationsResultsUpdate_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemCalculationsResultsUpdate_NavBarMain.Look-RightIconUrl").ToString();
            //        // End PresenceAndAbsenceOperation ****

            //        //  LateralOperations ****
            //        this.NavBarMain.FindItemById("nvbItemLateralOperations_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemLateralOperations_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemRolesIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemRolesIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemUsersIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemUsersIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPasswordChange_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPasswordChange_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPersonnelOrganizationFeaturesChange_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPersonnelOrganizationFeaturesChange_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemMasterPublicNews_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemMasterPublicNews_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPrivateMessage_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPrivateMessage_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemCalculations_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemCalculations_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemPersonalUserSettings_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemPersonalUserSettings_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemManagementUserSettings_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemManagementUserSettings_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemOnlineTraffics_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemOnlineTraffics_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemConceptRuleMasterOperation_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemConceptRuleMasterOperation_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemExpressionsOperation_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemExpressionsOperation_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemWebRest_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemWebRest_NavBarMain.Look-RightIconUrl").ToString();
            //        // End LateralOperations *****

            //        // Reports****
            //        this.NavBarMain.FindItemById("nvbItemReports_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemReports_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemReportsIntroduction_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemReportsIntroduction_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemSystemReports_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemSystemReports_NavBarMain.Look-RightIconUrl").ToString();
            //        this.NavBarMain.FindItemById("nvbItemDesignedReports_NavBarMain").Look.RightIconUrl = GetLocalResourceObject("nvbItemDesignedReports_NavBarMain.Look-RightIconUrl").ToString();
            //        // End Reports ****

            //        break;
            //    case LanguageType.RightToLeft:
            // BasicDefinitions ******تعاریف پایه
            this.NavBarMain.FindItemById("nvbItemBasicDefinitions_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemsBasicDefinitions_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonnelIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPersonnelIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemEmployTypesIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemEmployTypesIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemCorporationsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemCorporationsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemDepartmentsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemDepartmentsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPostsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPostsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemRulesGroupIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemRulesGroupIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemShiftPairTypesIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemShiftPairTypesIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemShiftIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemShiftIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkHeatIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemWorkHeatIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPreCardIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPreCardIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPhysicianIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPhysicianIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemIllnessIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemIllnessIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemGradeIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemGradeIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemCostCenterIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemCostCenterIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemFlowGroupIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemFlowGroupIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemMissionLocationsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemMissionLocationsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemControlStationIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemControlStationIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemMachineIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemMachineIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemYearlyHolidaysIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemYearlyHolidaysIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkGroupsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemWorkGroupsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemContractIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemContractIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemContractorsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemContractorsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            // End BasicDefinitions ******تعاریف پایه

            // JustificationOperation*****عملیات مجوز
            this.NavBarMain.FindItemById("nvbItemJustificationOperation_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemJustificationOperation_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemRegisteredRequests_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemRegisteredRequests_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemKartable_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemKartable_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemSurveyedRequests_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemSurveyedRequests_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemSpecialRequests_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemSpecialRequests_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemManagerMasterMonthlyOperationReport_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemManagerMasterMonthlyOperationReport_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPersonnelMasterMonthlyOperationReport_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkFlowsView_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemWorkFlowsView_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemMasterManagersIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemMasterManagersIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemSubstituteIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemSubstituteIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemWorkFlowDetail_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemWorkFlowDetail_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemRequestSubstituteKartable_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemRequestSubstituteKartable_NavBarMain.Look-LeftIconUrl").ToString();
            // End JustificationOperation ****عملیات مجوز

            // PresenceAndAbsenceOperation ****عملیات حضور و غیاب
            this.NavBarMain.FindItemById("nvbItemPresenceAndAbsenceOperation_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPresenceAndAbsenceOperation_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemCalculationRangeDefinition_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemCalculationRangeDefinition_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemUiValidation_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemUiValidation_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemExceptionShiftsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemExceptionShiftsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemMasterLeaveRemains_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemMasterLeaveRemains_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemTrafficsControl_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemTrafficsControl_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemSentry_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemSentry_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemCalculationsResultsUpdate_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemCalculationsResultsUpdate_NavBarMain.Look-LeftIconUrl").ToString();

            this.NavBarMain.FindItemById("nvbItemOvertime_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemOvertime_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemApprovalSchedulelist_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemApprovalSchedulelist_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertimePersonlist_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemOvertimePersonlist_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertimeEdarilist_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemOvertimeEdarilist_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemOvertimeSendFunctionlist_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemOvertimeSendFunctionlist_NavBarMain.Look-LeftIconUrl").ToString();
            // End PresenceAndAbsenceOperation ****عملیات حضور و غیاب

            //  LateralOperations ****عملیات جانبی
            this.NavBarMain.FindItemById("nvbItemLateralOperations_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemLateralOperations_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemRolesIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemRolesIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemUsersIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemUsersIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPasswordChange_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPasswordChange_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonnelOrganizationFeaturesChange_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPersonnelOrganizationFeaturesChange_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemMasterPublicNews_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemMasterPublicNews_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPrivateMessage_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPrivateMessage_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemCalculations_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemCalculations_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemPersonalUserSettings_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemPersonalUserSettings_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemManagementUserSettings_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemManagementUserSettings_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemOnlineTraffics_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemOnlineTraffics_NavBarMain.Look-LeftIconUrl").ToString();
            //this.NavBarMain.FindItemById("nvbItemConceptRuleMasterOperation_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemConceptRuleMasterOperation_NavBarMain.Look-LeftIconUrl").ToString();
            //this.NavBarMain.FindItemById("nvbItemExpressionsOperation_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemExpressionsOperation_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemWebRest_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemWebRest_NavBarMain.Look-LeftIconUrl").ToString();
            // End LateralOperations *****عملیات جانبی

            // Reports****گزارش ها
            this.NavBarMain.FindItemById("nvbItemReports_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemReports_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemReportsIntroduction_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemReportsIntroduction_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemSystemReports_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemSystemReports_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemDesignedReports_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemDesignedReports_NavBarMain.Look-LeftIconUrl").ToString();
            // End Reports ****گزارش ها

            // RuleGenerator***قانون ساز
            this.NavBarMain.FindItemById("nvbItemRuleGenerator_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemRuleGenerator_NavBarMain.Look-LeftIconUrl").ToString();
            //this.NavBarMain.FindItemById("nvbItemConceptsManagement_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemConceptsManagement_NavBarMain.Look-LeftIconUrl").ToString();
            this.NavBarMain.FindItemById("nvbItemRulesManagement_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemRulesManagement_NavBarMain.Look-LeftIconUrl").ToString();
            //this.NavBarMain.FindItemById("nvbItemExpressionsOperation_NavBarMain").Look.LeftIconUrl = GetLocalResourceObject("nvbItemExpressionsOperation_NavBarMain.Look-LeftIconUrl").ToString();
            // END RuleGenerator***قانون ساز


            //        break;
            //    default:
            //        break;
            //}
            this.TabStripMenus.ItemLooks[0].LeftIconUrl = GetLocalResourceObject("DefaultTabLook.LeftIconUrl").ToString();
            this.TabStripMenus.ItemLooks[0].RightIconUrl = GetLocalResourceObject("DefaultTabLook.RightIconUrl").ToString();

            this.TabStripMenus.ItemLooks[1].LeftIconUrl = GetLocalResourceObject("SelectedTabLook.LeftIconUrl").ToString();
            this.TabStripMenus.ItemLooks[1].RightIconUrl = GetLocalResourceObject("SelectedTabLook.RightIconUrl").ToString();

            this.TabStripMenus.FindItemById("tbWelcome_TabStripMenus").Text = GetLocalResourceObject("tbWelcome_TabStripMenus.Text").ToString();

            this.hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu.Value = GetLocalResourceObject("hfdmItemPersonnelMasterMonthlyOperationReport_DockMenu.Value").ToString();
            this.hfdmItemManagerMasterMonthlyOperationReport_DockMenu.Value = GetLocalResourceObject("hfdmItemManagerMasterMonthlyOperationReport_DockMenu.Value").ToString();
            this.hfdmItemPersonnelIntroduction_DockMenu.Value = GetLocalResourceObject("hfdmItemPersonnelIntroduction_DockMenu.Value").ToString();
            this.hfdmItemReportsIntroduction_DockMenu.Value = GetLocalResourceObject("hfdmItemReportsIntroduction_DockMenu.Value").ToString();
            this.hfdmItemKartable_DockMenu.Value = GetLocalResourceObject("hfdmItemKartable_DockMenu.Value").ToString();
            this.hfdmItemSurveyedRequests_DockMenu.Value = GetLocalResourceObject("hfdmItemSurveyedRequests_DockMenu.Value").ToString();
            this.hfdmItemRegisteredRequests_DockMenu.Value = GetLocalResourceObject("hfdmItemRegisteredRequests_DockMenu.Value").ToString();
            this.hfdmItemTrafficOperationByOperator_DockMenu.Value = GetLocalResourceObject("hfdmItemTrafficOperationByOperator_DockMenu.Value").ToString();
            this.hfdmItemWorkFlowsView_DockMenu.Value = GetLocalResourceObject("hfdmItemWorkFlowsView_DockMenu.Value").ToString();
            this.hfdmItemWelcome_DockMenu.Value = GetLocalResourceObject("hfdmItemWelcome_DockMenu.Value").ToString();
            this.hfErrorType_MainForm.Value = GetLocalResourceObject("hfErrorType_MainForm.Value").ToString();
            this.hfConnectionError_MainForm.Value = GetLocalResourceObject("hfConnectionError_MainForm.Value").ToString();
            this.hfTitle_qlItemHome.Value = GetLocalResourceObject("hfTitle_qlItemHome.Value").ToString();
            this.hfTitle_qlItemWorkFlowsView.Value = GetLocalResourceObject("hfTitle_qlItemWorkFlowsView.Value").ToString();
            this.hfTitle_qlItemTrafficsControl.Value = GetLocalResourceObject("hfTitle_qlItemTrafficsControl.Value").ToString();
            this.hfTitle_qlItemRegisteredRequests.Value = GetLocalResourceObject("hfTitle_qlItemRegisteredRequests.Value").ToString();
            this.hfTitle_qlItemSurveyedRequests.Value = GetLocalResourceObject("hfTitle_qlItemSurveyedRequests.Value").ToString();
            this.hfTitle_qlItemKartable.Value = GetLocalResourceObject("hfTitle_qlItemKartable.Value").ToString();
            this.hfTitle_qlItemReportsIntroduction.Value = GetLocalResourceObject("hfTitle_qlItemReportsIntroduction.Value").ToString();
            this.hfTitle_qlItemPersonnelIntroduction.Value = GetLocalResourceObject("hfTitle_qlItemPersonnelIntroduction.Value").ToString();
            this.hfTitle_qlItemManagerMasterMonthlyOperationReport.Value = GetLocalResourceObject("hfTitle_qlItemManagerMasterMonthlyOperationReport.Value").ToString();
            this.hfTitle_qlItemPersonnelMasterMonthlyOperationReport.Value = GetLocalResourceObject("hfTitle_qlItemPersonnelMasterMonthlyOperationReport.Value").ToString();


        }
    }
}