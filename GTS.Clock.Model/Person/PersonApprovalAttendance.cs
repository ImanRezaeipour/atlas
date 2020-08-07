using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model
{
    /// <summary>
    /// تایید کارکرد ماهانه پرسنل
    /// </summary>
    public class PersonApprovalAttendance : IEntity
    {
        #region Properties

        /// <summary>
        /// شناسه اصلی
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// ماه و سال کارکرد در قالب اولین روز آن ما
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// تایید شده یا خیر
        /// </summary>
        public virtual Boolean Approved { get; set; }

        /// <summary>
        /// شناسه پرسنلی که کارکرد برای آن تایید شده است
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// زمان تایید
        /// </summary>
        public virtual DateTime RegisterDatetime { get; set; }

        /// <summary>
        /// شناسه پرسنلی که تایید کرده 
        /// </summary>
        public virtual Person RegisterPerson { get; set; }

        #endregion
    }
}
