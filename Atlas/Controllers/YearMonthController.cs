using Atlas.Api.ViewModel;
using DotNetNuke.Web.Api;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
 
using System.Web;
using System.Web.Http;

namespace Atlas.Api.Controllers
{
    [DnnAuthorize]
    public class YearMonthController : BaseController
    {
        static List<SelectItem> yearItems = new List<SelectItem>();
        static List<SelectItem> monthItems = new List<SelectItem>();
        static List<SelectItem> dateRangeOrders = new List<SelectItem>();

        static YearMonthController()
        {
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                var persianDatetime = Utility.ToPersianDateTime(DateTime.Now);
                int currentYear = persianDatetime.Year;
                for (var i = (currentYear - 1); i <= (currentYear + 1); i++)
                {
                    yearItems.Add(new SelectItem { id = i, text = i.ToString() });
                }
                //----------------------------------------------------------------
                monthItems.Add(new SelectItem { id = 1, text = "فروردین" });
                monthItems.Add(new SelectItem { id = 2, text = "اردیبهشت" });
                monthItems.Add(new SelectItem { id = 3, text = "خرداد" });
                monthItems.Add(new SelectItem { id = 4, text = "تیر" });
                monthItems.Add(new SelectItem { id = 5, text = "مرداد" });
                monthItems.Add(new SelectItem { id = 6, text = "شهریور" });
                monthItems.Add(new SelectItem { id = 7, text = "مهر" });
                monthItems.Add(new SelectItem { id = 8, text = "آبان" });
                monthItems.Add(new SelectItem { id = 9, text = "آذر" });
                monthItems.Add(new SelectItem { id = 10, text = "دی" });
                monthItems.Add(new SelectItem { id = 11, text = "بهمن" });
                monthItems.Add(new SelectItem { id = 12, text = "اسفند" });
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                for (var i = (currentYear - 1); i <= (currentYear + 1); i++)
                {
                    yearItems.Add(new SelectItem { id = i, text = i.ToString() });
                }
                //----------------------------------------------------------------
                monthItems.Add(new SelectItem { id = 1, text = "January" });
                monthItems.Add(new SelectItem { id = 2, text = "February" });
                monthItems.Add(new SelectItem { id = 3, text = "March" });
                monthItems.Add(new SelectItem { id = 4, text = "April" });
                monthItems.Add(new SelectItem { id = 5, text = "May" });
                monthItems.Add(new SelectItem { id = 6, text = "June" });
                monthItems.Add(new SelectItem { id = 7, text = "July" });
                monthItems.Add(new SelectItem { id = 8, text = "August" });
                monthItems.Add(new SelectItem { id = 9, text = "September" });
                monthItems.Add(new SelectItem { id = 10, text = "October" });
                monthItems.Add(new SelectItem { id = 11, text = "November" });
                monthItems.Add(new SelectItem { id = 12, text = "December" });
            }

            dateRangeOrders.Add(new SelectItem { id = 1, text = "1" });
            dateRangeOrders.Add(new SelectItem { id = 2, text = "2" });
            dateRangeOrders.Add(new SelectItem { id = 3, text = "3" });
            dateRangeOrders.Add(new SelectItem { id = 4, text = "4" });
            dateRangeOrders.Add(new SelectItem { id = 5, text = "5" });
            dateRangeOrders.Add(new SelectItem { id = 6, text = "6" });
            dateRangeOrders.Add(new SelectItem { id = 7, text = "7" });
            dateRangeOrders.Add(new SelectItem { id = 8, text = "8" });
            dateRangeOrders.Add(new SelectItem { id = 9, text = "9" });
            dateRangeOrders.Add(new SelectItem { id = 10, text = "10" });
            dateRangeOrders.Add(new SelectItem { id = 11, text = "11" });
            dateRangeOrders.Add(new SelectItem { id = 12, text = "12" });
        }

        public SelectPageData GetMonths()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var pageNum = s2Param["pageNum"];
            int pageSize = Convert.ToInt32(s2Param["pageSize"]);
            string searchTerm = s2Param["searchTerm"];
            int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

            IQueryable<SelectItem> result = monthItems.AsQueryable();

            if (searchTerm != null)
            {
                result = result.Where(item => item.text.Contains(searchTerm.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = monthItems.Count
            };
        }

        public SelectPageData GetDateRangeOrders()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var pageNum = s2Param["pageNum"];
            int pageSize = Convert.ToInt32(s2Param["pageSize"]);
            string searchTerm = s2Param["searchTerm"];
            int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

            IQueryable<SelectItem> result = dateRangeOrders.AsQueryable();

            if (searchTerm != null)
            {
                result = result.Where(item => item.text.Contains(searchTerm.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = dateRangeOrders.Count
            };
        }

        public SelectPageData GetYears()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var pageNum = s2Param["pageNum"];
            int pageSize = Convert.ToInt32(s2Param["pageSize"]);
            string searchTerm = s2Param["searchTerm"];
            int pageIndex = pageNum == null ? 0 : Convert.ToInt32(pageNum) - 1;

            IQueryable<SelectItem> result = yearItems.AsQueryable();

            if (searchTerm != null)
            {
                result = result.Where(item => item.text.Contains(searchTerm.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = yearItems.Count
            };
        }
    }
}
