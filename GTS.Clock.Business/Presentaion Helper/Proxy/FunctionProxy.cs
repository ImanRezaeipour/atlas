using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class FunctionProxy
    {
        /// <summary>
        /// کد ملی پرسنل در سیستم منابع انسانی شهرداری
        /// </summary>
        public string HRM_NationalNo { get; set; }

        /// <summary>
        /// شناسه ی پرسنلی در سامانه پرسنلی احکام
        /// </summary>
        public decimal PersonnelId { get; set; }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string PersonnelFullName { get; set; }

        /// <summary>
        /// شماره پرسنلی
        /// </summary>
        public string PersonnelCode { get; set; }

        /// <summary>
        ///کد مرکز هزینه
        /// </summary>
        public string CurrentServiceLocationId { get; set; }

        /// <summary>
        /// روز تعطیل کاری
        /// </summary>
        public int PersonnelWorkingHolidaysDay { get; set; }

        /// <summary>
        /// ساعات تعطیل کاری
        /// </summary>
        public decimal PersonnelWorkingHolidays { get; set; }

        /// <summary>
        /// روز شب کاری
        /// </summary>
        public int PersonnelNightWorkDay { get; set; }

        /// <summary>
        /// ساعت شب کاری
        /// </summary>
        public decimal PersonnelNightWorkHours { get; set; }

        /// <summary>
        /// مرخصی های خارج از تعطیلی
        /// </summary>
        public int DeservedFunctionOutHoliday { get; set; }

        /// <summary>
        /// مرخصی های بین تعطیلی
        /// </summary>
        public int DeservedFunctionInHoliday { get; set; }

        /// <summary>
        /// روز بدون حقوق
        /// </summary>
        public int PaylessDay { get; set; }

        /// <summary>
        /// ماه کارکرد شمسی
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// سال کارکرد شمسی
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// نهار تعطیل
        /// </summary>
        public decimal HolidayFunctionDay { get; set; }

        /// <summary>
        /// نهار
        /// </summary>
        public decimal RealFunctionDay { get; set; }

        /// <summary>
        /// ساعت اضافه کاری
        /// </summary>
        public decimal PersonnelOverTimeHours { get; set; }

        /// <summary>
        /// ماموریت ساعتی
        /// </summary>
        public decimal PersonnelMissionHours { get; set; }

        /// <summary>
        /// روزهای غیبت
        /// </summary>
        public decimal PersonnelNoEnter { get; set; }

        /// <summary>
        /// روزهای کارکرد
        /// </summary>
        public int PersonnelFunctionDay { get; set; }

        /// <summary>
        /// استعلاجی
        /// </summary>
        public decimal PersonnelIllnessDay { get; set; }

        /// <summary>
        /// غیبت ساعتی
        /// </summary>
        public decimal PersonnelAbsence { get; set; }

        /// <summary>
        /// ساعت حضور برای استخدام های ساعتی
        /// </summary>
        public int PersonnelHourPresent { get; set; }

        /// <summary>
        /// روزهای کاری ماه
        /// </summary>
        public int FunctionDay { get; set; }

        /// <summary>
        ///پست سازمانی 
        /// </summary>
        public string PostType { get; set; }
         
        /// <summary>
        /// کلید بخش سازمانی
        /// </summary>
        public decimal DepartmentID { get; set; }

        
          
    }
}
