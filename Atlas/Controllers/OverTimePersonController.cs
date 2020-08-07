using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.OverTimeFlow;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.OverTimeFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Rules;
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
    public class OverTimePersonController : BaseController
    {
        #region Properties

        public BOverTimePersonDetail OverTimePersonDetailBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BOverTimePersonDetail>();
            }
        }

        public BPersonApprovalAttendance personApprovalAttendanceBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPersonApprovalAttendance>();
            }
        }

        public BPerson PersonBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPerson>();
            }
        }

        public BSubstitute SubstituteBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BSubstitute>();
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
        public OverTimePersonController()
        {

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
            if (dtParams["length"] != null)
                pageSize = Convert.ToInt32(dtParams["length"]);
            else
                pageSize = 20;

            //Get Filter Info 

            int departmentId = Convert.ToInt32(dtParams["departmentId"]);
            int year = Convert.ToInt32(dtParams["year"]);
            int month = Convert.ToInt32(dtParams["month"]);
            string searchKey = dtParams["person"].ToString().Trim();
            string directManager = dtParams["directManager"].ToString().Trim();
            //Other
            try
            {
                decimal managerPersonId = BUser.CurrentUser.Person.ID;
                if (dtParams["substituteManager"] != null && !string.IsNullOrEmpty(dtParams["substituteManager"]))
                {
                    string substituteManagers = dtParams["substituteManager"].ToString().Trim();
                    if (!string.IsNullOrEmpty(substituteManagers))
                    {
                        managerPersonId = Convert.ToDecimal(substituteManagers);
                        //if (departmentId == 0)
                        //    departmentId = (int)PersonBusiness.GetByID(managerPersonId).Department.ID;
                    }
                }
                else
                {
                    //if (departmentId == 0)
                    //    departmentId = (int)PersonBusiness.GetByID(BUser.CurrentUser.Person.ID).Department.ID;
                }

                int pageIndex = start / pageSize;

                int recordsTotal = 0;
                IList<OverTimePersonProxy> list = OverTimePersonDetailBusiness.GetAllByDepartmentId(year, month, departmentId, managerPersonId, searchKey, pageIndex, pageSize, out recordsTotal);

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
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
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
        public DataTablePageData<OverTimeTotalPersonProxy> ListTotal()
        {
            try
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
                string directManager = dtParams["directManager"].ToString().Trim();
                //Other
                decimal managerPersonId = BUser.CurrentUser.Person.ID;
                if (dtParams["substituteManager"] != null && !string.IsNullOrEmpty(dtParams["substituteManager"]))
                {
                    string substituteManagers = dtParams["substituteManager"].ToString().Trim();
                    if (!string.IsNullOrEmpty(substituteManagers))
                    {
                        managerPersonId = Convert.ToDecimal(substituteManagers);
                        //if (departmentId == 0)
                        //    departmentId = (int)PersonBusiness.GetByID(managerPersonId).Department.ID;
                    }
                }
                else
                {
                    //if (departmentId == 0)
                    //    departmentId = (int)PersonBusiness.GetByID(BUser.CurrentUser.Person.ID).Department.ID;
                }

                OverTimeTotalPersonProxy result = OverTimePersonDetailBusiness.GetTotalByDepartmentId(year, month, departmentId, managerPersonId, "");
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
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                throw ex;
            }
        }

        [HttpGet]
        public OverTimePersonDetailViewModel GetItem(decimal id)
        {
            try
            {
                OverTimePersonDetail overTimePersonDetail = OverTimePersonDetailBusiness.GetByID(id);
                OverTimePersonDetailViewModel obj = new OverTimePersonDetailViewModel();

                obj.Id = overTimePersonDetail.ID;
                obj.Date = overTimePersonDetail.OverTime.Date;
                obj.PersonFullName = overTimePersonDetail.Person.FirstName + " " + overTimePersonDetail.Person.LastName;

                //----------------------------------------------------------------------------------
                var IsEditableOverTimeObj = overTimePersonDetail.Person.PersonTASpec.GetParamValue(overTimePersonDetail.Person.ID, "IsEditableOverTime", DateTime.Now);
                bool IsEditableOverTime = IsEditableOverTimeObj != null ? Utility.ToInteger(IsEditableOverTimeObj.Value) > 0 : true;

                var IsEditableHolidayObj = overTimePersonDetail.Person.PersonTASpec.GetParamValue(overTimePersonDetail.Person.ID, "IsEditableHolidayWork", DateTime.Now);
                bool IsEditableHoliday = IsEditableHolidayObj != null ? Utility.ToInteger(IsEditableHolidayObj.Value) > 0 : true;

                var IsEditableNightObj = overTimePersonDetail.Person.PersonTASpec.GetParamValue(overTimePersonDetail.Person.ID, "IsEditableNightWork", DateTime.Now);
                bool IsEditableNight = IsEditableNightObj != null ? Utility.ToInteger(IsEditableNightObj.Value) > 0 : true;

                //----------------------------------------------------------------------------------
                obj.HasOverTime = overTimePersonDetail.Person.PersonTASpec.OverTimeWork && IsEditableOverTime;
                obj.HasHolidayWork = overTimePersonDetail.Person.PersonTASpec.HolidayWork && IsEditableHoliday;
                obj.HasNightWork = overTimePersonDetail.Person.PersonTASpec.NightWork && IsEditableNight;

                var isApprove = personApprovalAttendanceBusiness.CheckIsDuplicate(overTimePersonDetail.OverTime.Date, overTimePersonDetail.Person.ID);
                if (!isApprove)
                {
                    obj.HasOverTime = false;
                    obj.HasHolidayWork = false;
                    obj.HasNightWork = false;
                }

                obj.IsApprove = isApprove;
                //----------------------------------------------------------------------------------
                //if (obj.HasOverTime)
                obj.MaxOverTime = overTimePersonDetail.MaxOverTime;
                //if (obj.HasNightWork)
                obj.MaxNightWork = overTimePersonDetail.MaxNightly;
                //if (obj.HasHolidayWork)
                obj.MaxHolidayWork = overTimePersonDetail.MaxHoliday;

                return obj;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage Edit(decimal id, OverTimeProxy viewModel)
        {
            try
            {
                decimal ManagerPersonId;
                if (viewModel.ManagerPersonId == 0)
                    ManagerPersonId = BUser.CurrentUser.Person.ID;
                else
                    ManagerPersonId = viewModel.ManagerPersonId;
                //------------------------------------------------------------------------
                Person person = PersonBusiness.GetByID(ManagerPersonId);
                //تشخیص اینکه طرف معاون هست یا خیر - جهت اعمال محدودیت زمان بندی تایید به تفکیک مدیران و معاونین
                ApprovalScheduleType approvalType = ApprovalScheduleType.Manager;

                var personParamAssistance = person.PersonTASpec.GetParamValue(person.ID, "IsAssistance", DateTime.Now);
                bool stateAssistance = personParamAssistance != null ? Utility.ToInteger(personParamAssistance.Value) > 0 : false;
                if (stateAssistance)
                    approvalType = ApprovalScheduleType.Assistance;

                OverTimePersonDetailBusiness.UpdateOverTimePersonDetail(id, viewModel, approvalType, ManagerPersonId);
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public SelectPageData GetSubstituteManager()
        {
            try
            {
                IList<Substitute> SubstituteList = this.SubstituteBusiness.GetAllSubstitutes(string.Empty, false).ToList();
                IList<SelectItem> result = new List<SelectItem>();
                //لیست جانشین هایی رو لازم داریم که در حال حاضر جانشین کاربر جاری باشند
                foreach (Substitute item in SubstituteList.Where(c => c.FromDate <= DateTime.Now && c.ToDate >= DateTime.Now))
                {
                    if (item.Manager.Person != null)
                    {
                        if (item.Person.ID == BUser.CurrentUser.Person.ID)
                            result.Add(new SelectItem() { id = Convert.ToInt32(item.Manager.Person.ID), text = item.Manager.Person.FirstName + " " + item.Manager.Person.LastName });
                    }
                    else if (item.Manager.Person == null && item.Manager.ThePerson != null)
                    {
                        if (item.Person.ID == BUser.CurrentUser.Person.ID)
                            result.Add(new SelectItem() { id = Convert.ToInt32(item.Manager.ThePerson.ID), text = item.Manager.ThePerson.FirstName + " " + item.Manager.ThePerson.LastName });
                    }
                }
                return new SelectPageData
                {
                    items = result,
                    total_count = result.Count
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                throw ex;
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
}
