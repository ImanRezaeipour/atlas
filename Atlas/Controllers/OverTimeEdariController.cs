using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business;
using GTS.Clock.Business.ArchiveCalculations;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.OverTimeFlow;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.OverTimeFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Atlas.Api.Controllers
{
    [DnnAuthorize]
    public class OverTimeEdariController : BaseController
    {
        #region Properties

        public BOverTimePersonDetail OverTimePersonDetailBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BOverTimePersonDetail>();
            }
        }

        public BArchiveCalculator UpdateCalculationResultBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BArchiveCalculator>();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        #endregion

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public OverTimeEdariController()
        {

        }

        [HttpGet]
        
        public HttpResponseMessage OrganizationList()
        {
            int draw;
            int pageSize;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            //Paging Info
            draw = Convert.ToInt32(dtParams["draw"]);
            int start = Convert.ToInt32(dtParams["start"]);
            pageSize = Convert.ToInt32(dtParams["length"]);
            //Get Filter Info 

            try
            {
                int organizationId = Convert.ToInt32(dtParams["organizationId"]);
                int year = Convert.ToInt32(dtParams["year"]);
                int month = Convert.ToInt32(dtParams["month"]);
                string searchKey = dtParams["person"].ToString().Trim();
                //Other
                decimal managerPersonId = BUser.CurrentUser.Person.ID;

                int pageIndex = start / pageSize;
                int recordsTotal = 0;

                OverTimeTotalPersonProxy result = OverTimePersonDetailBusiness.GetOrganizationTotal(year, month, organizationId);
                IList<OverTimeTotalPersonProxy> list = new List<OverTimeTotalPersonProxy>();
                if (result != null)
                    list.Add(result);
                //--------------------------------------------------------------------------------------
                var DT = new DataTablePageData<OverTimeTotalPersonProxy>
                {
                    data = list,
                    recordsTotal = 1,
                    recordsFiltered = 1,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                var DT = new DataTablePageData<OverTimeTotalPersonProxy>
                {
                    data = null,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
        }

        [HttpGet]
        public HttpResponseMessage list()
        {
            int draw;
            int pageSize;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            //Paging Info
            draw = Convert.ToInt32(dtParams["draw"]);
            int start = Convert.ToInt32(dtParams["start"]);
            pageSize = Convert.ToInt32(dtParams["length"]);
            //Get Filter Info 

            try
            {
                int costCenterId = Convert.ToInt32(dtParams["costCenterId"]);
                int employmentTypeId = Convert.ToInt32(dtParams["employmentTypeId"]);
                int departmentId = Convert.ToInt32(dtParams["departmentId"]);
                int asistantId = Convert.ToInt32(dtParams["asistantId"]);
                if (departmentId == 0 && asistantId > 0)
                    departmentId = asistantId;

                int year = Convert.ToInt32(dtParams["year"]);
                int month = Convert.ToInt32(dtParams["month"]);
                string searchKeyPerson = dtParams["person"].ToString().Trim();
                string searchKeyCardNum = dtParams["cardnum"].ToString().Trim();
                string searchKeyNationalCode = dtParams["nationalcode"].ToString().Trim();
                //Other
                decimal managerPersonId = BUser.CurrentUser.Person.ID;

                int pageIndex = start / pageSize;
                int recordsTotal = 0;
                IList<OverTimePersonProxy> list = OverTimePersonDetailBusiness.GetAllByAthorize(year, month, departmentId, costCenterId, employmentTypeId, searchKeyPerson,searchKeyCardNum,searchKeyNationalCode, pageIndex, pageSize, out recordsTotal);

                //--------------------------------------------------------------------------------------
                var DT = new DataTablePageData<OverTimePersonProxy>
                {
                    data = list,
                    recordsTotal = recordsTotal,
                    recordsFiltered = recordsTotal,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                var DT = new DataTablePageData<OverTimePersonProxy>
                {
                    data = null,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
        }

        [HttpGet]
        public HttpResponseMessage functionlist()
        {
            int draw;
            int pageSize;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            //Paging Info
            draw = Convert.ToInt32(dtParams["draw"]);
            int start = Convert.ToInt32(dtParams["start"]);
            pageSize = Convert.ToInt32(dtParams["length"]);
            //Get Filter Info 

            try
            {
                int costCenterId = Convert.ToInt32(dtParams["costCenterId"]);
                int employmentTypeId = Convert.ToInt32(dtParams["employmentTypeId"]);
                int departmentId = Convert.ToInt32(dtParams["organizationId"]);
                //departmentId = departmentId == -1 ? 1 : departmentId;
                int year = Convert.ToInt32(dtParams["year"]);
                int month = Convert.ToInt32(dtParams["month"]);
                string searchKey = dtParams["person"].ToString().Trim();


                int pageIndex = start / pageSize;
                int recordsTotal = 0;
                IList<FunctionProxy> list = OverTimePersonDetailBusiness.GetFunctionsByAthorize(year, month, departmentId, costCenterId, employmentTypeId, searchKey, pageIndex, pageSize, out recordsTotal);

                //--------------------------------------------------------------------------------------
                var DT = new DataTablePageData<FunctionProxy>
                {
                    data = list,
                    recordsTotal = recordsTotal,
                    recordsFiltered = recordsTotal,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                var DT = new DataTablePageData<OverTimePersonProxy>
                {
                    data = null,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
        }

        [HttpGet]
        public HttpResponseMessage listTotal()
        {
            int draw;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            //Paging Info
            draw = Convert.ToInt32(dtParams["draw"]);

            //Get Filter Info 
            try
            {
                //int costCenterId = Convert.ToInt32(dtParams["costCenterId"]);
                int departmentId = Convert.ToInt32(dtParams["departmentId"]);
                int asistantId = Convert.ToInt32(dtParams["asistantId"]);
                if (departmentId == 0 && asistantId > 0)
                    departmentId = asistantId;

                int year = Convert.ToInt32(dtParams["year"]);
                int month = Convert.ToInt32(dtParams["month"]);
                //Other

                OverTimeTotalPersonProxy result = OverTimePersonDetailBusiness.GetTotalByAthorize(year, month, departmentId, 0, "");
                IList<OverTimeTotalPersonProxy> list = new List<OverTimeTotalPersonProxy>();
                if (result != null)
                    list.Add(result);

                //--------------------------------------------------------------------------------------
                var DT = new DataTablePageData<OverTimeTotalPersonProxy>
              {
                  data = list,
                  recordsTotal = 1,
                  recordsFiltered = 1,
                  draw = ++draw
              };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                var DT = new DataTablePageData<OverTimeTotalPersonProxy>
                {
                    data = null,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    draw = ++draw
                };
                return Request.CreateResponse(HttpStatusCode.OK, DT);
            }
        }

        [HttpGet]
        public HttpResponseMessage getItem(decimal id)
        {
            try
            {
                OverTimePersonDetail overTimePersonDetail = OverTimePersonDetailBusiness.GetByID(id);
                OverTimePersonDetailProxy obj = new OverTimePersonDetailProxy();

                obj.Id = overTimePersonDetail.ID;
                obj.Date = overTimePersonDetail.OverTime.Date;
                obj.PersonFullName = overTimePersonDetail.Person.FirstName + " " + overTimePersonDetail.Person.LastName;

                //------------------------------------------------------------------------------------------------------
                obj.HasOverTime = overTimePersonDetail.Person.PersonTASpec.OverTimeWork;
                obj.HasHolidayWork = overTimePersonDetail.Person.PersonTASpec.HolidayWork;
                obj.HasNightWork = overTimePersonDetail.Person.PersonTASpec.NightWork;

                if (obj.HasOverTime)
                    obj.MaxOverTime = overTimePersonDetail.MaxOverTime;
                if (obj.HasNightWork)
                    obj.MaxNightWork = overTimePersonDetail.MaxNightly;
                if (obj.HasHolidayWork)
                    obj.MaxHolidayWork = overTimePersonDetail.MaxHoliday;
                //--------------------------------------------------------------------------------------------------------
                var pDate = new GTS.Clock.Infrastructure.Utility.PersianDateTime(overTimePersonDetail.OverTime.Date);
                var ArchiveValue = UpdateCalculationResultBusiness.GetArchiveValues(pDate.Year, pDate.Month, overTimePersonDetail.Person.ID).FirstOrDefault();
                if (ArchiveValue != null)
                {
                    obj.P1 = obj.P1Old = ArchiveValue.P1;
                    obj.P2 = obj.P2Old = ArchiveValue.P2;
                    obj.P3 = obj.P3Old = string.IsNullOrEmpty(ArchiveValue.P3.Trim()) ? "00:00" : ArchiveValue.P3;
                    obj.P4 = obj.P4Old = string.IsNullOrEmpty(ArchiveValue.P4.Trim()) ? "00:00" : ArchiveValue.P4;
                    obj.P5 = obj.P5Old = string.IsNullOrEmpty(ArchiveValue.P5.Trim()) ? "00:00" : ArchiveValue.P5;
                    obj.P6 = obj.P6Old = ArchiveValue.P6;
                    obj.P7 = obj.P7Old = ArchiveValue.P7;
                    obj.P8 = obj.P8Old = ArchiveValue.P8;
                    obj.P9 = obj.P9Old = ArchiveValue.P9;
                    obj.P10 = obj.P10Old = string.IsNullOrEmpty(ArchiveValue.P10.Trim()) ? "00:00" : ArchiveValue.P10;
                    obj.P11 = obj.P11Old = ArchiveValue.P11;
                    obj.P12 = obj.P12Old = string.IsNullOrEmpty(ArchiveValue.P12.Trim()) ? "00:00" : ArchiveValue.P12;
                    obj.IsArchiveEnable = true;
                }
                //--------------------------------------------------------------------------------------------------------

                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);

                return Request.CreateResponse(HttpStatusCode.OK, new OverTimePersonDetailViewModel());
            }
        }

        [HttpPost]
        public HttpResponseMessage sendItems(ApproveViewModel viewModel)
        {
            try
            {
                int costCenterId = Convert.ToInt32(viewModel.costCenterId);
                int employmentTypeId = Convert.ToInt32(viewModel.employmentTypeId);
                int departmentId = Convert.ToInt32(viewModel.departmentId);
                int asistantId = Convert.ToInt32(viewModel.asistantId);

                if (departmentId == 0 && asistantId > 0)
                    departmentId = asistantId;
                departmentId = departmentId == -1 ? 1 : departmentId;

                var result = OverTimePersonDetailBusiness.SendPersonsFunctionList(viewModel.year, viewModel.month, departmentId, costCenterId, employmentTypeId, viewModel.person);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Edit(decimal id, OverTimePersonDetailProxy viewModel)
        {
            try
            {
                OverTimePersonDetailBusiness.UpdateOverTimePersonDetailByAdministrative(id, viewModel, ApprovalScheduleType.Administrative, BUser.CurrentUser.Person.ID);
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimeEdariController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        public ValidationData ValidateOverTime(decimal MaxOverTime)
        {
            if (MaxOverTime <= 120)
                return new ValidationData() { valid = true };
            else
                return new ValidationData() { valid = false };
        }

        [HttpGet]
        public ValidationData ValidateNightWork(decimal MaxNightWork)
        {
            if (MaxNightWork <= 120)
                return new ValidationData() { valid = true };
            else
                return new ValidationData() { valid = false };
        }

        [HttpGet]
        public ValidationData ValidateHolidayWork(decimal MaxHolidayWork)
        {
            if (MaxHolidayWork <= 120)
                return new ValidationData() { valid = true };
            else
                return new ValidationData() { valid = false };
        }

    }


    public class ApproveViewModel
    {
        public int year { get; set; }
        public int month { get; set; }
        public decimal? organizationId { get; set; }
        public decimal? asistantId { get; set; }
        public decimal? departmentId { get; set; }
        public decimal? employmentTypeId { get; set; }
        public decimal? costCenterId { get; set; }
        public string person { get; set; }

    }

}
