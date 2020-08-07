using Atlas.Api.Controllers;
using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business;
using GTS.Clock.Infrastructure.Exceptions.UI;
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
    public class PersonController : BaseController
    {
        #region Properties
        public BPerson PersonBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BPerson>();
            }
        }

        public ExceptionHandler exceptionHandler
        {
            get
            {
                return new ExceptionHandler();
            }
        }

        public ISearchPerson PersonSearchBusiness
        {
            get
            {
                return (ISearchPerson)BusinessHelper.GetBusinessInstance<BPerson>();
            }
        }

        #endregion

        /// <summary>
        /// جستجوی سریع پرسنل جهت استفاده در Select2Js
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SelectPageData QuickSearch()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var pageNum = s2Param["pageNum"];
            int pageSize = Convert.ToInt32(s2Param["pageSize"]);
            string searchTerm = s2Param["searchTerm"];
            int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

            var count = PersonSearchBusiness.GetPersonInQuickSearchCount(searchTerm);
            var result = PersonSearchBusiness.QuickSearchByPage(pageIndex, pageSize, searchTerm).Select(c => new SelectItem() { id = Convert.ToInt32(c.ID), text = c.FirstName + " " + c.LastName }).ToList();

            return new SelectPageData
            {
                items = result,
                total_count = count
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">کد ملی پرسنل</param>
        /// <returns>آبجکت پرسنل</returns>
        [HttpGet]
        public HttpResponseMessage GetByMelliCode(string code)
        {
            try
            {
                var person = PersonBusiness.GetByBarcode(code);
                return Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch (UIValidationExceptions ex)
            {
                this.exceptionHandler.ApiHandleException("PersonController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.ExceptionList.Count > 0 ? ex.ExceptionList[0].Message : ex.Message);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.ApiHandleException("PersonController", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}