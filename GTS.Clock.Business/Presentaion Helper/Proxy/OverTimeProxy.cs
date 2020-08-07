using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class OverTimeProxy
    {
        public decimal ID { get; set; }
        /// <summary>
        /// ماه/سال مربوطه
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// باز/بسته بودن تخصیص سرانه
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// معاونت
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        ///  کلید اصلی پرسنلی که یا به عنوان مدیر مستفیم می باشد یا یه عنوان جانشین مدیر
        /// </summary>
        public decimal ManagerPersonId { get; set; }

        /// <summary>
        /// تعداد پرسنل اضافه کار
        /// </summary>
        public int OverTimePersonCount { get; set; }
        /// <summary>
        /// تعداد پرسنل شب کاری معاونت
        /// </summary>
        public int NightlyPersonCount { get; set; }
        /// <summary>
        /// تعداد پرسنل تعطیل کاری معاونت
        /// </summary>
        public int HolidayPersonCount { get; set; }

        /// <summary>
        /// سرانه اضافه کاری تشویقی معاونت
        /// </summary>
        public decimal MaxOverTime { get; set; }
        /// <summary>
        ///  سرانه تعطیل کاری تشویقی معاونت به درصد
        /// </summary>
        public decimal MaxHoliday { get; set; }
        /// <summary>
        /// سرانه شب کاری تشویقی معاونت
        /// </summary>
        public decimal MaxNightly { get; set; }

        /// <summary>
        /// تعداد روزهای تعطیل در ماه انتخاب شده
        /// </summary>
        public int MonthHolidayCount { get; set; }

        /// <summary>
        /// مجموع اضافه کاری تشویقی
        /// </summary>
        public decimal TotalOverTime { get { return this.OverTimePersonCount * this.MaxOverTime; } }
        /// <summary>
        /// مجموع تعطیل کاری تشویقی
        /// </summary>
        public decimal TotalHoliday { get { return this.HolidayPersonCount * this.MaxHoliday * this.MonthHolidayCount / 100; } }

        /// <summary>
        /// مجموع شب کاری تشویقی
        /// </summary>
        public decimal TotalNightly { get { return this.NightlyPersonCount * this.MaxNightly; } }


        public decimal OldValueMaxOverTime { get; set; }
        public decimal OldValueMaxHoliday { get; set; }
        public decimal OldValueMaxNightly { get; set; }
    }
}
