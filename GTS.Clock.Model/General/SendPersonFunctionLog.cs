using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.General
{
    public class SendPersonFunctionLog : IEntity, IDisposable
    {
        public SendPersonFunctionLog()
        {
            this.ModifiedDate = DateTime.Now;
        }

        #region Properties

        /// <summary>
        /// کلید اصلی
        /// </summary>
        public virtual decimal ID { get; set; }

        /// <summary>
        /// پرسنل
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// ارسال شده توسط
        /// </summary>
        public virtual Person ModifiedBy { get; set; }

        /// <summary>
        /// ارسال شده در تاریخ
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// سال کارکرد
        /// </summary>
        public virtual int Year { get; set; }

        /// <summary>
        /// ماه کارکرد
        /// </summary>
        public virtual int Month { get; set; }

        /// <summary>
        /// آدرس IP
        /// </summary>
        public virtual string IPAddress { get; set; }

        /// <summary>
        /// نتیجه فراخوانی وب سرویس
        /// </summary>
        public virtual string Result { get; set; }

        /// <summary>
        /// آبجکت کارکرد ارسال شده
        /// </summary>
        public virtual string JsonObj { get; set; }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {

        }

        #endregion

    }
}
