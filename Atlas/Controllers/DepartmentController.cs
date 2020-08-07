using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
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
    public class DepartmentController : BaseController
    {
        #region Properties

        public BDepartment DepartmentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDepartment>();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public BUser UserBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BUser>();
            }
        }

        public BManager ManagerBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BManager>();
            }
        }
        public BPerson PersonBusinesss
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPerson>();
            }
        }


        #endregion

        public DepartmentController()
        {

        }

        /// <summary>
        /// لیست ارگان ها یا سازمان ها را به صورت صفحه بندی برمی گرداند
        /// در فیلتر سرانه اضافه کار تشویقی استفاده شده است
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SelectPageData GetOrganizations()  
        {
            try
            {
                var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var pageNum = s2Param["pageNum"];
                int pageSize = Convert.ToInt32(s2Param["pageSize"]);
                string searchTerm = s2Param["searchTerm"];
                int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

                IList<Department> departments = DepartmentBusiness.GetAll().Where(c => c.DepartmentType == DepartmentType.Organization).ToList();
                IQueryable<SelectItem> result = departments.Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.Name }).AsQueryable();

                if (searchTerm != null)
                {
                    result = result.Where(item => item.text.Contains(searchTerm.ToString()));
                }

                return new SelectPageData
                {
                    items = result.Skip(pageIndex * pageSize).Take(pageSize),
                    total_count = departments.Count
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست معاونت های یک سازمان را به صورت صفحه بندی برمی گرداند
        /// در فیلتر سرانه اضافه کار تشویقی استفاده شده است
        /// </summary>
        /// <param name="id">کلید سازمان</param>
        /// <returns></returns>
        [HttpGet]
        public SelectPageData GetAssistances(decimal id)
        {
            try
            {
                var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var pageNum = s2Param["pageNum"];
                int pageSize = Convert.ToInt32(s2Param["pageSize"]);
                string searchTerm = s2Param["searchTerm"];
                int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

                IList<Department> departments = DepartmentBusiness.GetAll()
                    .Where(c =>
                                c.DepartmentType == DepartmentType.Assistance &&
                                c.ParentPath.Contains(String.Format(",{0},", id))
                    )
                    .ToList();
                IQueryable<SelectItem> result = departments.Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.Name }).AsQueryable();

                if (searchTerm != null)
                {
                    result = result.Where(item => item.text.Contains(searchTerm.ToString()));
                }

                return new SelectPageData
                {
                    items = result.Skip(pageIndex * pageSize).Take(pageSize),
                    total_count = departments.Count
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست اداره های یک معاونت را به صورت صفحه بندی برمی گرداند
        /// در فیلتر سرانه اضافه کار تشویقی استفاده شده است
        /// </summary>
        /// <param name="id">کلید معاونت</param>
        /// <returns></returns>
        [HttpGet]
        public SelectPageData GetManagements(decimal id)
        {
            try
            {
                var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var pageNum = s2Param["pageNum"];
                int pageSize = Convert.ToInt32(s2Param["pageSize"]);
                string searchTerm = s2Param["searchTerm"];
                int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

                IList<Department> departments = DepartmentBusiness.GetAll()
                     .Where(c =>
                                 c.DepartmentType == DepartmentType.Management &&
                                 c.ParentPath.Contains(String.Format(",{0},", id))
                     )
                     .ToList();
                IQueryable<SelectItem> result = departments.Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.Name }).AsQueryable();

                if (searchTerm != null)
                {
                    result = result.Where(item => item.text.Contains(searchTerm.ToString()));
                }

                return new SelectPageData
                {
                    items = result.Skip(pageIndex * pageSize).Take(pageSize),
                    total_count = departments.Count
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت کاربر جاری به عنوان یک مدیر را بر می گرداند 
        /// </summary>
        /// <param name="id">کلید اصلی پرسنل</param>
        /// <returns>لیست بخش های سازمان</returns>
        [HttpGet]
        public IList<TreeItem> GetAllManagerDepartmentTree(decimal id)
        {
            try
            {
                decimal managerPersonId;
                if (id == 0)
                    managerPersonId = BUser.CurrentUser.Person.ID;
                else
                    managerPersonId = id;

                var manager = ManagerBusiness.GetManager(managerPersonId);
                IList<Department> departments = DepartmentBusiness.GetAllManagerDepartmentTree_JustOrgan(manager.ID);
                return departments.Select(c => new TreeItem() { id = (Int32)c.ID, parentid = (Int32)c.ParentID, text = c.Name }).ToList();
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("OverTimePersonController", ex);
                return new List<TreeItem>();
            }
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت کاربر جاری به عنوان یک مدیر را بر می گرداند 
        /// </summary>
        /// <param name="MelliCode">کد ملی پرسنل</param>
        /// <returns>لیست بخش های سازمان</returns>
        [HttpGet]
        public HttpResponseMessage GetAllManagerDepartmentTreeByMelliCode(string code)
        {
            try
            {
                var person = PersonBusinesss.GetByBarcode(code);
                if (person == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new List<TreeItem>());

                decimal managerPersonId = person.ID;

                var manager = ManagerBusiness.GetManager(managerPersonId);
                IList<Department> departments = DepartmentBusiness.GetAllManagerDepartmentTree_JustOrgan(manager.ID);

                var result = departments.Select(c => new TreeItem() { id = (Int32)c.ID, parentid = (Int32)c.ParentID, text = c.Name }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("DepartmentController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("DepartmentController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
