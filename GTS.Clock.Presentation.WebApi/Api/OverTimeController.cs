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
    /// api سرانه اضافه کار تشویقی, شب کاری تشویقی , تعطیل کاری تشویقی
    /// </summary>
    /// 
    [RoutePrefix("api/overtime")]
    [Authorize]
    public class OverTimeController : ApiController
    {
        #region Properties

        public BOverTimeDetail OverTimeDetailBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BOverTimeDetail>();
            }
        }

        #endregion

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public OverTimeController()
        {

        }

        /// <summary>
        /// لیست اضافه کار تشویقی را به صورت سفحه بندی شده بر می گرداند
        /// فرمت صفحه بندی مختص dataTablesJs می باشد
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public DataTablePageData<OverTimeProxy> GetList()
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
            decimal departmentId = string.IsNullOrEmpty(dtParams["department"]) ? 1 : Convert.ToDecimal(dtParams["department"]);
            string departmentName = dtParams["departmentName"];
            int year = Convert.ToInt32(dtParams["year"]);
            int month = Convert.ToInt32(dtParams["month"]);
            //Other

            //Get Sort Info
            string sortColIndex = dtParams["order[0][column]"].ToString();
            string sortColName = dtParams["columns[" + sortColIndex + "][data]"];
            string sortDirection = dtParams["order[0][dir]"];

            int pageIndex = start / pageSize;

            int recordsTotal;
            IList<OverTimeProxy> list = null;
            list = this.OverTimeDetailBusiness.GetDetails(departmentId, departmentName, year, month, (pageIndex < 1 ? 0 : pageIndex), pageSize, out recordsTotal);

            return new DataTablePageData<OverTimeProxy>
            {
                data = list,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,
                draw = ++draw
            };
        }

        /// <summary>
        /// واکشی اضافه کاری تشویقی یک مدیریت در یک ماه مشخص جهت نمایش در فرم ویرایش
        /// </summary>
        /// <param name="id">کلید اصلی رکورد مورد نظر</param>
        /// <returns>پروکسی اضافه کاری تشویقی</returns>
        [HttpGet]
        [Route("getitem/{id}")]
        public OverTimeProxy GetItem(decimal id)
        {
            try
            {
                var overTimeDetail = OverTimeDetailBusiness.GetByID(id);
                OverTimeProxy overTimeProxy = OverTimeDetailBusiness.ConvertToProxy(overTimeDetail);
                return overTimeProxy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// لیست ماه های سرانه اضافه کار تشویقی فعال جهت استفاده در dropdown به صورت صفحه بندی شده
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetActivePeriod")]
        public SelectPageData GetActivePeriod()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var page = s2Param["page"];
            var size = s2Param["size"];
            var q = s2Param["q"];

            int pageIndex = page == null ? 0 : Convert.ToInt32(page) - 1;
            int pageSize = Convert.ToInt32(size);

            IList<OverTime> overTimes = OverTimeDetailBusiness.GetActivePeriod().ToList();
            IQueryable<SelectItem> result = overTimes.Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.Date.Year + "-" + c.Date.Month }).AsQueryable();

            if (q != null)
            {
                result = result.Where(item => item.text.Contains(q.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = overTimes.Count
            };
        }

        [HttpPost]
        [Route("approve")]
        public void ApproveOverTime(DateModel model)
        {
            try
            {
                OverTimeDetailBusiness.ApproveOverTime(model.year, model.month);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("IsAproved")]
        public bool IsAprovedOverTime(DateModel model)
        {
            try
            {
                return OverTimeDetailBusiness.IsApprovedOverTime(model.year, model.month);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ویرایش اضافه کاری تشویقی یک مدیریت در یک ماه مشخص   
        /// </summary>
        /// <param name="id">کلید اصلی </param>
        /// <param name="viewModel">ویومدل جهت ویرایش داده ها</param>
        [HttpPut]
        [Route("edit/{id}")]
        public void Edit(decimal id, OverTimeProxy viewModel)
        {
            try
            {
                OverTimeDetail model = OverTimeDetailBusiness.GetByID(id);

                model.MaxHoliday = viewModel.MaxHoliday;
                model.MaxNightly = viewModel.MaxNightly;
                model.MaxOverTime = viewModel.MaxOverTime;

                OverTimeDetailBusiness.UpdateOverTimeDetail(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class DateModel
    {
        public int year { get; set; }
        public int month { get; set; }
    }
}
