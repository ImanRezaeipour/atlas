using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.OverTimeFlow
{
    /// <summary>
    /// تاریخچه تغییرات
    /// </summary>
    public class OverTimePersonDetailHistory : IEntity, IDisposable
    {
        public OverTimePersonDetailHistory()
        {
            this.ModifiedDate = DateTime.Now;
        }

        #region Properties
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// پرسنلی که برای آن ویرایش اعمال شده
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// عنوان مفهومی که ویرایش شده
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// کلید رکورد اصلی که ویرایش شده
        /// </summary>
        public virtual decimal RefrenceId { get; set; }

        /// <summary>
        /// مقدار قبل از ویرایش
        /// </summary>
        public virtual string OldValue { get; set; }

        /// <summary>
        /// مقدار بعد ویرایش
        /// </summary>
        public virtual string NewValue { get; set; }

        /// <summary>
        /// ویرایش کننده
        /// </summary>
        public virtual Person ModifiedBy { get; set; }

        /// <summary>
        /// زمان ویرایش
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// دوره
        /// </summary>
        public virtual DateTime Period { get; set; }

        /// <summary>
        /// IPAddress
        /// </summary>
        public virtual string IPAddress { get; set; }    

            #endregion

            #region IDisposable Members
            public void Dispose()
            {

            }

            #endregion
    }
}
