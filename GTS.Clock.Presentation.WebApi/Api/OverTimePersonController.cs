using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.OverTimeFlow;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.OverTimeFlow;
using GTS.Clock.Presentation.WebApi.Common;
using GTS.Clock.Presentation.WebApi.Models;
using GTS.Clock.Presentation.WebApi.ViewModels.OverTimePersonDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GTS.Clock.Presentation.WebApi.Api
{
    [RoutePrefix("api/overtimeperson")]
    [Authorize]
    public class OverTimePersonController : ApiController
    {
        #region Properties

        public BOverTimePersonDetail OverTimePersonDetailBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BOverTimePersonDetail>();
            }
        }

        #endregion

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public OverTimePersonController()
        {

        }

        [HttpGet]
        [Route("list")]
        public DataTablePageData<OverTimePersonProxy> GetList()
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

            int departmentId = Convert.ToInt32(dtParams["departmentId"]);
            int year = Convert.ToInt32(dtParams["year"]);
            int month = Convert.ToInt32(dtParams["month"]);
            string searchKey = dtParams["person"].ToString().Trim();
            string directManager = dtParams["directManager"].ToString().Trim();
            string substituteManagers = dtParams["substituteManager"].ToString().Trim();
            //Other
            decimal managerPersonId = BUser.CurrentUser.Person.ID;
            if (string.IsNullOrEmpty(substituteManagers))
                managerPersonId = Convert.ToDecimal(substituteManagers);

            int pageIndex = start / pageSize;

            int recordsTotal = 0;
            IList<OverTimePersonProxy> list = OverTimePersonDetailBusiness.GetAllByDepartmentId(year, month, departmentId, managerPersonId, searchKey, pageIndex, pageSize, out recordsTotal);

            //--------------------------------------------------------------------------------------
            return new DataTablePageData<OverTimePersonProxy>
            {
                data = list,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,
                draw = ++draw
            };
        }

        [HttpGet]
        [Route("listTotal")]
        public DataTablePageData<OverTimeTotalPersonProxy> GetListTotal()
        {
            int draw;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            //Paging Info
            draw = Convert.ToInt32(dtParams["draw"]);

            //Get Filter Info 

            int departmentId = Convert.ToInt32(dtParams["departmentId"]);
            int year = Convert.ToInt32(dtParams["year"]);
            int month = Convert.ToInt32(dtParams["month"]);
            //Other

            OverTimeTotalPersonProxy result = OverTimePersonDetailBusiness.GetTotalByDepartmentId(year, month, departmentId, BUser.CurrentUser.Person.ID, "");
            IList<OverTimeTotalPersonProxy> list = new List<OverTimeTotalPersonProxy>();
            if (result != null)
                list.Add(result);

            //--------------------------------------------------------------------------------------
            return new DataTablePageData<OverTimeTotalPersonProxy>
          {
              data = list,
              recordsTotal = 1,
              recordsFiltered = 1,
              draw = ++draw
          };
        }

        [HttpGet]
        [Route("getitem/{id}")]
        public OverTimePersonDetailViewModel GetItem(decimal id)
        {
            try
            {
                OverTimePersonDetail overTimePersonDetail = OverTimePersonDetailBusiness.GetByID(id);
                OverTimePersonDetailViewModel obj = new OverTimePersonDetailViewModel();

                obj.Id = overTimePersonDetail.ID;
                obj.Date = overTimePersonDetail.OverTime.Date;
                obj.PersonFullName = overTimePersonDetail.Person.FirstName + " " + overTimePersonDetail.Person.LastName;

                obj.HasOverTime = overTimePersonDetail.Person.PersonTASpec.HasPeyment;
                obj.HasHolidayWork = overTimePersonDetail.Person.PersonTASpec.HolidayWork;
                obj.HasNightWork = overTimePersonDetail.Person.PersonTASpec.NightWork;

                if (obj.HasOverTime)
                    obj.MaxOverTime = overTimePersonDetail.MaxOverTime;
                if (obj.HasNightWork)
                    obj.MaxNightWork = overTimePersonDetail.MaxNightly;
                if (obj.HasHolidayWork)
                    obj.MaxHolidayWork = overTimePersonDetail.MaxHoliday;

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("edit/{id}")]
        public void Edit(decimal id, OverTimeProxy viewModel)
        {
            try
            {
                OverTimePersonDetail model = OverTimePersonDetailBusiness.GetByID(id);

                model.MaxHoliday = viewModel.MaxHoliday;
                model.MaxNightly = viewModel.MaxNightly;
                model.MaxOverTime = viewModel.MaxOverTime;

                OverTimePersonDetailBusiness.UpdateOverTimePersonDetail(model, ApprovalScheduleType.Manager, BUser.CurrentUser.Person.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("validateOverTime")]
        public ValidationData ValidateMaxOverTime(decimal MaxOverTime)
        {
            if (MaxOverTime <= 120)
                return new ValidationData() { valid = true };
            else
                return new ValidationData() { valid = false };
        }

        [HttpGet]
        [Route("validateNightWork")]
        public ValidationData ValidateMaxNightWork(decimal MaxNightWork)
        {
            if (MaxNightWork <= 120)
                return new ValidationData() { valid = true };
            else
                return new ValidationData() { valid = false };
        }

        [HttpGet]
        [Route("validateHolidayWork")]
        public ValidationData ValidateMaxHolidayWork(decimal MaxHolidayWork)
        {
            if (MaxHolidayWork <= 120)
                return new ValidationData() { valid = true };
            else
                return new ValidationData() { valid = false };
        }

    }

}
