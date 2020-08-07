using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.ServiceProvider.Proxy
{
    /// <summary>
    /// پروکسی اطلاعات حضور غیاب
    /// </summary>
    public class TAProxy
    {
        /// <summary>
        /// کد پرسنلی
        /// </summary>
        public string PersonCode { get; set; }

        /// <summary>
        /// مجموع حضور
        /// </summary>
        public int HozourTime { get; set; }

        /// <summary>
        /// مجموع ماموریت ساعتی و روزانه
        /// </summary>
        public int MamuriatTime { get; set; }

        /// <summary>
        /// مجموع کسر کار ساعتی و روزانه
        /// </summary>
        public int KasreKarTime { get; set; }

        /// <summary>
        /// مجموع کارکرد
        /// </summary>
        public int KarkerdSum { get; set; }

    }
}