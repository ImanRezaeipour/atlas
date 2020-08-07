using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Charts;
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
    [RoutePrefix("api/Department")]
    [Authorize]
    public class DepartmentController : ApiController
    {
        #region Properties

        public BDepartment DepartmentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BDepartment>();
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
        [Route("GetOrganizations")]
        public SelectPageData GetOrganizations()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var page = s2Param["page"];
            var size = s2Param["size"];
            var q = s2Param["q"];

            int pageIndex = page == null ? 0 : Convert.ToInt32(page) - 1;
            int pageSize = Convert.ToInt32(size);

            IList<Department> departments = DepartmentBusiness.GetAll().Where(c => c.DepartmentType == DepartmentType.Organization).ToList();
            IQueryable<SelectItem> result = departments.Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.Name }).AsQueryable();

            if (q != null)
            {
                result = result.Where(item => item.text.Contains(q.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = departments.Count
            };
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت کاربر جاری به عنوان یک مدیر را بر می گرداند 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllManagerDepartmentTree")]
        public IList<TreeItem> GetAllManagerDepartmentTree()
        {
            var personId = BUser.CurrentUser.Person.ID;
            var manager = ManagerBusiness.GetManager(personId);
            IList<Department> departments = DepartmentBusiness.GetAllManagerDepartmentTree(personId);
            return departments.Select(c => new TreeItem() { id = (Int32)c.ID, parentid = (Int32)c.ParentID, text = c.Name }).ToList();
        }
    }
}
