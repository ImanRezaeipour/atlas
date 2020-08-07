using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.OverTimeFlow
{
    /// <summary>
    /// سرانه اضافه کار تشویقی پرسنل
    /// </summary>
    public class OverTimePersonDetail : IEntity, IDisposable
    {
        public OverTimePersonDetail()
        {

        }
        #region Properties

        public virtual Decimal ID { get; set; }

        public virtual OverTime OverTime { get; set; }

        public virtual Person Person { get; set; }

        public virtual decimal MaxOverTime { get; set; }
        public virtual decimal MaxHoliday { get; set; }
        public virtual decimal MaxNightly { get; set; }
         
        /// <summary>
        /// آخرین ثبت کننده یا ویرایش کننده
        /// </summary>
        public virtual Person ModifiedBy { get; set; }

        /// <summary>
        /// آخرین زمان ثبت یا ویرایش
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        public virtual IList<GTS.Clock.Model.Temp.Temp> TempList { get; set; }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {

        }

        #endregion

    }
}
