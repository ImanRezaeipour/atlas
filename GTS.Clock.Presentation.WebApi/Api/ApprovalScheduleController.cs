using GTS.Clock.Business.OverTimeFlow;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Model.OverTimeFlow;
using GTS.Clock.Presentation.WebApi.Common;
using GTS.Clock.Presentation.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GTS.Clock.Presentation.WebApi.Api
{
    /// <summary>
    /// زمان بندی تایید کارکرد ماهیانه پرسنل و کارتابل تایید اضافه کار تشویقی مدیران و کارتابل اضافه کار تشویقی اداری
    /// </summary>
    /// 
    [RoutePrefix("api/approvalschedule")]
    [Authorize]
    public class ApprovalScheduleController : ApiController
    {
        #region Properties

        public BApprovalAttendanceSchedule ApprovalAttendanceScheduleBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BApprovalAttendanceSchedule>();
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
        /// لیست اضافه کار تشویقی را به صورت سفحه بندی شده بر می گرداند
        /// فرمت صفحه بندی مختص dataTablesJs می باشد
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public DataTablePageData<ApprovalAttendanceScheduleProxy> GetList()
        {
            int draw;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            //Paging Info
            draw = Convert.ToInt32(dtParams["draw"]);

            IList<ApprovalAttendanceScheduleProxy> list = null;
            list = this.ApprovalAttendanceScheduleBusiness.GetAllProxy();

            return new DataTablePageData<ApprovalAttendanceScheduleProxy>
            {
                data = list,
                recordsTotal = list.Count(),
                recordsFiltered = list.Count(),
                draw = ++draw
            };
        }

        /// <summary>
        /// واکشی زمان بندی جهت نمایش در فرم ویرایش
        /// </summary>
        /// <param name="id">کلید اصلی رکورد مورد نظر</param>
        /// <returns>پروکسی زمان بندی</returns>
        [HttpGet]
        [Route("getitem/{id}")]
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
                throw ex;
            }
        }

        /// <summary>
        /// ویرایش زمان بندی   
        /// </summary>
        /// <param name="id">کلید اصلی </param>
        /// <param name="viewModel">ویومدل جهت ویرایش داده ها</param>
        [HttpPut]
        [Route("edit/{id}")]
        public void Edit(decimal id, ApprovalAttendanceScheduleProxy viewModel)
        {
            try
            {
                viewModel.ID = id;
                ApprovalAttendanceScheduleBusiness.UpdateApprovalAttendanceScheduleProxy(viewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
