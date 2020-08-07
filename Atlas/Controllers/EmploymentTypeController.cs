using Atlas.Api.Controllers;
using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business.BaseInformation;
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
    public class EmploymentTypeController : BaseController
    {
        #region Properties

        public BEmployment  EmploymentBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BEmployment>();
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

        public EmploymentTypeController()
        {

        }

        [HttpGet]
        public SelectPageData list()
        {
            try
            {
                var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var pageNum = s2Param["pageNum"];
                int pageSize = Convert.ToInt32(s2Param["pageSize"]);
                string searchTerm = s2Param["searchTerm"];
                int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

                IList<GTS.Clock.Model.BaseInformation.EmploymentType> employmentTypes = EmploymentBusiness.GetAll().ToList();

                IQueryable<SelectItem> result = employmentTypes.Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.Name }).AsQueryable();

                if (searchTerm != null)
                {
                    result = result.Where(item => item.text.Contains(searchTerm.ToString()));
                }

                return new SelectPageData
                {
                    items = result.Skip(pageIndex * pageSize).Take(pageSize),
                    total_count = employmentTypes.Count
                };
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("EmploymentTypeController", ex);
                throw ex;
            }
        }
    }
}