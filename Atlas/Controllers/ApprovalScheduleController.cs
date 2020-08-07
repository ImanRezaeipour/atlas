using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business.OverTimeFlow;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Infrastructure.Exceptions.UI;
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
    /// <summary>
    /// زمان بندی تایید کارکرد ماهیانه پرسنل و کارتابل تایید اضافه کار تشویقی مدیران و کارتابل اضافه کار تشویقی اداری
    /// </summary>

    [DnnAuthorize]
    public class ApprovalScheduleController : BaseController
    {
        #region Properties

        public BApprovalAttendanceSchedule ApprovalAttendanceScheduleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BApprovalAttendanceSchedule>();
            }
        }

        public BApprovalAttendanceScheduleException ApprovalAttendanceScheduleExceptionBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BApprovalAttendanceScheduleException>();
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
        public ApprovalScheduleController()
        {

        }

        /// <summary>
        /// لیست زمانبندی را به صورت سفحه بندی شده بر می گرداند
        /// فرمت صفحه بندی مختص dataTablesJs می باشد
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DataTablePageData<ApprovalAttendanceScheduleProxy> list()
        {
            try
            {
                int draw;
                //خواندن اطلاعات ارسالی از طرف گرید
                var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);

                //Paging Info
                draw = Convert.ToInt32(dtParams["draw"]);
                decimal costCenterId = string.IsNullOrEmpty(dtParams["costCenter"]) ? 0 : Convert.ToDecimal(dtParams["costCenter"]);

                IList<ApprovalAttendanceScheduleProxy> list = null;
                if (costCenterId == 0)
                    list = new List<ApprovalAttendanceScheduleProxy>();
                else
                    list = this.ApprovalAttendanceScheduleBusiness.GetProxyByCostCenter(costCenterId);

                return new DataTablePageData<ApprovalAttendanceScheduleProxy>
                {
                    data = list,
                    recordsTotal = list.Count(),
                    recordsFiltered = list.Count(),
                    draw = ++draw
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست استثناء زمانبندی را به صورت سفحه بندی شده بر می گرداند
        /// فرمت صفحه بندی مختص dataTablesJs می باشد 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DataTablePageData<ApprovalAttendanceScheduleExceptionProxy> listException()
        {
            try
            {
                int draw;
                //خواندن اطلاعات ارسالی از طرف گرید
                var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);

                //Paging Info
                draw = Convert.ToInt32(dtParams["draw"]);
                decimal ApprovalAttendanceScheduleID = string.IsNullOrEmpty(dtParams["approvalAttendanceSchedule"]) ? 0 : Convert.ToDecimal(dtParams["approvalAttendanceSchedule"]);

                IList<ApprovalAttendanceScheduleExceptionProxy> list = null;
                list = this.ApprovalAttendanceScheduleExceptionBusiness.GetListProxyByApprovalAttendanceScheduleID(ApprovalAttendanceScheduleID);

                return new DataTablePageData<ApprovalAttendanceScheduleExceptionProxy>
                {
                    data = list,
                    recordsTotal = list.Count(),
                    recordsFiltered = list.Count(),
                    draw = ++draw
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// واکشی زمان بندی جهت نمایش در فرم ویرایش
        /// </summary>
        /// <param name="id">کلید اصلی رکورد مورد نظر</param>
        /// <returns>پروکسی زمان بندی</returns>
        [HttpGet]
        public ApprovalAttendanceScheduleProxy GetItem(decimal id)
        {
            try
            {
                var item = ApprovalAttendanceScheduleBusiness.GetByID(id);
                var proxy = ApprovalAttendanceScheduleBusiness.ConvertToProxy(item);
                return proxy;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// ویرایش زمان بندی   
        /// </summary>
        /// <param name="id">کلید اصلی </param>
        /// <param name="viewModel">ویومدل جهت ویرایش داده ها</param>
        [HttpPost]
        public HttpResponseMessage Edit(decimal id, ApprovalAttendanceScheduleProxy viewModel)
        {
            try
            {
                viewModel.ID = id;
                ApprovalAttendanceScheduleBusiness.UpdateApprovalAttendanceScheduleProxy(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// واکشی استثناء زمانبندی جهت نمایش در فرم ویرایش 
        /// </summary>
        /// <param name="id">کلید اصلی</param>
        /// <returns></returns>
        [HttpGet]
        public ApprovalAttendanceScheduleExceptionProxy GetExceptionItem(decimal id)
        {
            try
            {
                var item = ApprovalAttendanceScheduleExceptionBusiness.GetByID(id);
                var proxy = ApprovalAttendanceScheduleExceptionBusiness.ConvertToProxy(item);
                return proxy;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// درج استثناء زمانبندی
        /// </summary>
        /// <param name="viewModel">ویومدل آبجکت استثناء جهت درج</param>
        /// <returns></returns>
        [HttpPost]
        public int createException(ApprovalAttendanceScheduleExceptionProxy viewModel)
        {
            ApprovalAttendanceScheduleException obj = new ApprovalAttendanceScheduleException()
            {
                ApprovalAttendanceSchedule = new ApprovalAttendanceSchedule() { ID = viewModel.ApprovalAttendanceScheduleID },
                DateFrom = viewModel.DateFrom,
                DateTo = viewModel.DateTo,
                Person = new GTS.Clock.Model.Person() { ID = viewModel.PersonID }
            };
            var result = ApprovalAttendanceScheduleExceptionBusiness.InsertApprovalAttendanceScheduleException(obj);
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// ویرایش استثناء زمانبندی
        /// </summary>
        /// <param name="id">کلید اصلی</param>
        /// <param name="viewModel">ویومدل آبجکت استثناء جهت ویرایش</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage EditException(decimal id, ApprovalAttendanceScheduleExceptionProxy viewModel)
        {
            try
            {
                ApprovalAttendanceScheduleException obj = new ApprovalAttendanceScheduleException()
                {
                    ID = id,
                    ApprovalAttendanceSchedule = new ApprovalAttendanceSchedule() { ID = viewModel.ApprovalAttendanceScheduleID },
                    DateFrom = viewModel.DateFrom,
                    DateTo = viewModel.DateTo,
                    Person = new GTS.Clock.Model.Person() { ID = viewModel.PersonID }
                };
                ApprovalAttendanceScheduleExceptionBusiness.UpdateApprovalAttendanceScheduleException(obj);
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// حذف استثناء زمانبندی
        /// </summary>
        /// <param name="id">کلید اصلی</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DeleteException(int id)
        {
            try
            {
                var obj = ApprovalAttendanceScheduleExceptionBusiness.GetByID(id);
                ApprovalAttendanceScheduleExceptionBusiness.DeleteApprovalAttendanceScheduleException(obj);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("ApprovalScheduleController", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
