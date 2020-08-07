using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Leave
{
    /// <summary>
    /// جهت درست و معتبر بودن مقادیر برگشتی لازم است قبل از فراخوانی این سرویس ها
    /// محاسبات انجام گردد
    /// </summary>
    public interface ILeaveInfo
    {
        /// <summary>
        /// مانده مرخصی تا آخر ماه را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="day">روز</param>
        /// <param name="minutes">دقیقه</param>
        void GetRemainLeaveToEndOfMonth(decimal personId, int year, int month, int toDay, out int day, out int minutes);
        
        /// <summary>
        /// مانده مرخصی تا آخر سال را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="day">روز</param>
        /// <param name="minutes">دقیق</param>
        void GetRemainLeaveToEndOfYear(decimal personId, int year, int month,int toDay, out int day, out int minutes);
        
        /// <summary>
        /// مرخصی استفاده شده از ابتدای سال را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="day">روز</param>
        /// <param name="minutes">ماه</param>
        void GetUsedLeaveToEndOfYear(decimal personId, int year, int month, int toDay, out int day, out int minutes);
    }
}
