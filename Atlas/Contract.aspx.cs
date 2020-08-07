using ComponentArt.Web.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Contracts;
using GTS.Clock.Business.UI;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Contract : GTSBasePage
    {
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
            Contract_onPageLoad,
            tbContract_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations
        }
        public enum LoadState
        {
            Normal,
            Search,
            AdvancedSearch

        }
        public BContract ContractBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BContract>();
            }
        }
        public BContractors ContractorBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BContractors>();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBcak_cmbContractor_Contract.IsCallback && !CallBack_GridContracts_Contract.IsCallback)
            {
                Page MachinesPage = this;
                Ajax.Utility.GenerateMethodScripts(MachinesPage);
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                SetContractPageSize_Contract();
                this.CheckContractLoadAccess_Contract();
            }
        }
        private void SetContractPageSize_Contract()
        {
            this.hfContractsPageSize_Contract.Value = this.GridContracts_Contract.PageSize.ToString();
        }
        private void CheckContractLoadAccess_Contract()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ContractBusiness.CheckContractLoadAccess();
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

        /// <summary>
        /// تنظیم زبان انتخابی کاربر
        /// </summary>
        /// <param name="LangID"></param>
        private void SetCurrentCultureResObjs(string LangID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangID);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangID);
        }

        protected void CallBack_GridContracts_Contract_OnCallBack(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.SetContractsPageCount_Contract((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), this.StringBuilder.CreateString(e.Parameters[3]));
            this.Fill_GridContracts_Contract((LoadState)Enum.Parse(typeof(LoadState), this.StringBuilder.CreateString(e.Parameters[0])), int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture), int.Parse(this.StringBuilder.CreateString(e.Parameters[2]), CultureInfo.InvariantCulture), this.StringBuilder.CreateString(e.Parameters[3]));
            this.GridContracts_Contract.RenderControl(e.Output);
            this.hfContractsCount_Contract.RenderControl(e.Output);
            this.hfContractsPageCount_Contract.RenderControl(e.Output);
            this.ErrorHiddenField_Contract.RenderControl(e.Output);
        }
        private void Fill_GridContracts_Contract(LoadState Ls, int pageSize, int pageIndex, string SearchTerm)
        {
            IList<GTS.Clock.Model.Contracts.Contract> contractList = new List<GTS.Clock.Model.Contracts.Contract>();
            string[] retMessage = new string[4];

            try
            {
                this.InitializeCulture();
                switch (Ls)
                {
                    case LoadState.Normal:
                        contractList = this.ContractBusiness.GetContratsByPaging(SearchTerm, pageIndex, pageSize);
                        break;
                    case LoadState.Search:
                        contractList = this.ContractBusiness.GetContratsByPaging(SearchTerm, pageIndex, pageSize);
                        break;
                }
                this.GridContracts_Contract.DataSource = contractList;
                this.GridContracts_Contract.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Contract.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Contract.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (OutOfExpectedRangeException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
                this.ErrorHiddenField_Contract.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Contract.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
        private void SetContractsPageCount_Contract(LoadState Ls, string SearchTerm)
        {
            int UsersCount = 0;
            switch (Ls)
            {
                case LoadState.Normal:
                    UsersCount = this.ContractBusiness.GetContratsByPagingCount(string.Empty);
                    break;
                case LoadState.Search:
                    UsersCount = this.ContractBusiness.GetContratsByPagingCount(SearchTerm);
                    break;
            }
            this.hfContractsCount_Contract.Value = UsersCount.ToString();
            this.hfContractsPageCount_Contract.Value = Utility.GetPageCount(UsersCount, this.GridContracts_Contract.PageSize).ToString();
        }
        protected void CallBcak_cmbContractor_Contract_onCallback(object sender, CallBackEventArgs e)
        {
            AttackDefender.CSRFDefender(this.Page);
            this.Fill_cmbContractor_Contract();
            this.ErrorHiddenField_Contractor.RenderControl(e.Output);
            this.cmbContractor_Contract.RenderControl(e.Output);
        }

        private void Fill_cmbContractor_Contract()
        {
            string[] retMessage = new string[4];
            try
            {
                this.InitializeCulture();
                IList<GTS.Clock.Model.Contracts.Contractor> contractorsList = this.ContractorBusiness.GetAll();
                foreach (GTS.Clock.Model.Contracts.Contractor item in contractorsList)
                {
                    item.Name = item.Code + " - " + item.Name;
                }
                this.cmbContractor_Contract.DataSource = contractorsList;
                this.cmbContractor_Contract.DataBind();
                this.cmbContractor_Contract.Enabled = true;
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Contractor.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Contractor.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Contractor.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }

        [Ajax.AjaxMethod("UpdateContract_ContractPage", "UpdateContract_ContractPage_onCallBack", null, null)]
        public string[] UpdateContract_ContractPage(string state, string SelectedContractID, string Title, string Code, string ContractorID, string Description, string IsDefault)
        {
            this.InitializeCulture();

            string[] retMessage = new string[5];

            try
            {
                AttackDefender.CSRFDefender(this.Page);
                decimal contractIdChangedIsDefault = 0;
                decimal ContractID = 0;
                decimal selectedContractID = decimal.Parse(this.StringBuilder.CreateString(SelectedContractID), CultureInfo.InvariantCulture);
                Code = this.StringBuilder.CreateString(Code);
                Title = this.StringBuilder.CreateString(Title);
                Description = this.StringBuilder.CreateString(Description);
                decimal contractorID = decimal.Parse(this.StringBuilder.CreateString(ContractorID), CultureInfo.InvariantCulture);
                bool isDefault = Convert.ToBoolean(this.StringBuilder.CreateString(IsDefault));
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());

                GTS.Clock.Model.Contracts.Contract contractObj = new GTS.Clock.Model.Contracts.Contract();
                contractObj.ID = selectedContractID;
                contractObj.IsDefault = isDefault;
                if (uam != UIActionType.DELETE)
                {
                    contractObj.Code = Code;
                    GTS.Clock.Model.Contracts.Contractor contractorObj = new GTS.Clock.Model.Contracts.Contractor();
                    contractorObj.ID = contractorID;
                    contractObj.Contractor = contractorObj;
                    contractObj.Description = Description;
                    contractObj.Title = Title;

                }

                switch (uam)
                {
                    case UIActionType.ADD:
                        ContractID = this.ContractBusiness.InsertContract(contractObj, uam, out contractIdChangedIsDefault);
                        break;
                    case UIActionType.EDIT:
                        if (selectedContractID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoContractSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        ContractID = this.ContractBusiness.UpdateContract(contractObj, uam, out contractIdChangedIsDefault);
                        break;
                    case UIActionType.DELETE:
                        if (selectedContractID == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoContractSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        ContractID = this.ContractBusiness.DeleteContract(contractObj, uam);
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
                retMessage[3] = ContractID.ToString();
                retMessage[4] = contractIdChangedIsDefault.ToString();
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

}