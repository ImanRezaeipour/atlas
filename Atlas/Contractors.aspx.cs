using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.UI;
using GTS.Clock.Business.BaseInformation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business;
using GTS.Clock.Business.Contracts;
using ComponentArt.Web.UI;
using GTS.Clock.Model.Contracts;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Presentaion.WebForms
{
    public partial class Contractors : GTSBasePage
    {
        public BContractors ContractorsBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BContractors>();
            }
        }
        //public BContractors ContractorsBusiness
        //{
        //    get
        //    {
        //        return new BContractors();
        //    }
        //}

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
        enum Scripts
        {
            Contractors_onPageLoad,
            tbContractors_TabStripMenus_Operations,
            Alert_Box,
            HelpForm_Operations,
            DialogWaiting_Operations,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefererValidationProvider.CheckReferer();
            if (!CallBack_GridContractors_Contractors.IsCallback)
            {
                Page ContractorsPage = this;
                Ajax.Utility.GenerateMethodScripts(ContractorsPage);
                SetContractorsPageSize_Contractor();
                SkinHelper.InitializeSkin(this.Page);
                ScriptHelper.InitializeScripts(this.Page, typeof(Scripts));
                this.CheckContractorsLoadAccess_Contractors();
            }
        }
        private void CheckContractorsLoadAccess_Contractors()
        {
            string[] retMessage = new string[4];
            try
            {
                this.ContractorsBusiness.CheckContractorsLoadAccess();
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
        protected void CallBack_GridContractors_Contractors_onCallBack(object sender, CallBackEventArgs e)
        {
            int pageIndex = int.Parse(this.StringBuilder.CreateString(e.Parameters[0]), CultureInfo.InvariantCulture);
            int pageSize = int.Parse(this.StringBuilder.CreateString(e.Parameters[1]), CultureInfo.InvariantCulture);
            string SearchValue = this.StringBuilder.CreateString(e.Parameters[2]);
            this.SetContractorsPageCount_Contractors(SearchValue);
            this.Fill_GridContractors_Contractors(pageIndex, pageSize, SearchValue);
            this.GridContractors_Contractors.RenderControl(e.Output);
            this.hfContractorsCount_Contractors.RenderControl(e.Output);
            this.hfContractorsPageCount_Contractors.RenderControl(e.Output);
            this.ErrorHiddenField_Contractors.RenderControl(e.Output);
        }
        private void SetContractorsPageCount_Contractors(string searchValue)
        {            
            int ContractorCount = this.ContractorsBusiness.GetContractorByPagingCount(searchValue);
            this.hfContractorsCount_Contractors.Value = ContractorCount.ToString();
            this.hfContractorsPageCount_Contractors.Value = Utility.GetPageCount(ContractorCount, this.GridContractors_Contractors.PageSize).ToString();
        }
        private void SetContractorsPageSize_Contractor()
        {
            this.hfContractorsPageSize_Contractors.Value = this.GridContractors_Contractors.PageSize.ToString();
        }

        private void Fill_GridContractors_Contractors(int pageIndex, int pageSize, string searchValue)
        {
            string[] retMessage = new string[4];
            IList<Contractor> ContractorList = null;
            try
            {
                this.InitializeCulture();
                ContractorList = this.ContractorsBusiness.GetContractorByPaging(pageIndex, pageSize, searchValue);
                this.GridContractors_Contractors.DataSource = ContractorList;
                this.GridContractors_Contractors.DataBind();
            }
            catch (UIValidationExceptions ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, ex, retMessage);
                this.ErrorHiddenField_Contractors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            catch (UIBaseException ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIBaseException, ex, retMessage);
                this.ErrorHiddenField_Contractors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
            //catch (OutOfExpectedRangeException ex)
            //{
            //    retMessage = this.exceptionHandler.HandleException(this.Page, ex, retMessage);
            //    this.ErrorHiddenField_Contractors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            //}
            catch (Exception ex)
            {
                retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.Exception, ex, retMessage);
                this.ErrorHiddenField_Contractors.Value = this.exceptionHandler.CreateErrorMessage(retMessage);
            }
        }
         [Ajax.AjaxMethod("UpdateContractor_ContractorsPage", "UpdateContractor_ContractorsPage_onCallBack", null, null)]
        public string[] UpdateContractor_ContractorsPage(string state, string ContractorId, string Name, string Code, string EconomicCode, string Organization, string Tel, string Fax, string Email, string Address, string Description, string Isdefault)
        {
            this.InitializeCulture();

            string[] retMessage = new string[4];
            try
            {
                decimal ContractorID = 0;
                UIActionType uam = (UIActionType)Enum.Parse(typeof(UIActionType), this.StringBuilder.CreateString(state).ToUpper());
                decimal SelectedContractorId = decimal.Parse(this.StringBuilder.CreateString(ContractorId), CultureInfo.InvariantCulture);
                Name = this.StringBuilder.CreateString(Name);
                Code = this.StringBuilder.CreateString(Code);
                Organization = this.StringBuilder.CreateString(Organization);
                Tel = this.StringBuilder.CreateString(Tel);
                Fax = this.StringBuilder.CreateString(Fax);
                Email = this.StringBuilder.CreateString(Email);
                Address = this.StringBuilder.CreateString(Address);
                Description = this.StringBuilder.CreateString(Description);
                EconomicCode = this.StringBuilder.CreateString(EconomicCode);
                bool IsDefault = bool.Parse(this.StringBuilder.CreateString(Isdefault));
                Contractor contractor = new Contractor();
                contractor.ID = SelectedContractorId;
                contractor.IsDefault = IsDefault;
                if(uam != UIActionType.DELETE)
                {                    
                    contractor.Name = Name;
                    contractor.Code = Code;
                    contractor.EconomicCode = EconomicCode;
                    contractor.Organization = Organization;
                    contractor.Tel = Tel;
                    contractor.Fax = Fax;
                    contractor.Email = Email;
                    contractor.Address = Address;
                    contractor.Description = Description;                    
                }
                switch (uam)
                {
                    case UIActionType.ADD:
                        contractor.ID = 0;
                        ContractorID = this.ContractorsBusiness.InsertContractor(contractor, uam);
                        break;
                    case UIActionType.EDIT:
                        if (SelectedContractorId == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoContractorSelectedforEdit").ToString()), retMessage);
                            return retMessage;
                        }
                        ContractorID = this.ContractorsBusiness.UpdateContractor(contractor, uam);
                        break;
                    case UIActionType.DELETE:
                        if (SelectedContractorId == 0)
                        {
                            retMessage = this.exceptionHandler.HandleException(this.Page, ExceptionTypes.UIValidationExceptions, new Exception(GetLocalResourceObject("NoContractorSelectedforDelete").ToString()), retMessage);
                            return retMessage;
                        }
                        ContractorID = this.ContractorsBusiness.DeleteContractor(contractor, uam);
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
                retMessage[3] = ContractorID.ToString();
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