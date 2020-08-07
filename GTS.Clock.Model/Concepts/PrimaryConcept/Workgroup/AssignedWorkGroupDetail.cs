﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Utility;
using System.Globalization;

namespace GTS.Clock.Model
{
    public class AssignedWorkGroupDetail : BasePairableConceptValue<ShiftPair>, IEntity
    {
        #region Properties

        public virtual string WorkGroupName
        { 
            get; 
            set; 
        }

        public virtual DateTime Date
        {
            get;
            set;
        }

        public override int Value
        {
            get
            {
                return this.PairValues;
            }
        }

        #endregion

        #region Methods

        ///// <summary>
        ///// مقدار کارکردلازم ماهانه را برمیگرداند
        ///// </summary>
        ///// <returns></returns>
        //public int Total()
        //{
        //    PersianDateTime shiftDate = new PersianDateTime(this.Date);
        //    return this.Pairs.Where(x => shiftDate.PersianCalendar.GetYear(x.Date) == shiftDate.Year && 
        //                                shiftDate.PersianCalendar.GetMonth(x.Date) == shiftDate.Month)
        //                        .Sum(x => x.Value);
        //}

        //public int TotalDay()
        //{
        //    PersianDateTime shiftDate = new PersianDateTime(this.Date);
        //    return this.Pairs.Where(x => shiftDate.PersianCalendar.GetYear(x.Date) == shiftDate.Year &&
        //                                shiftDate.PersianCalendar.GetMonth(x.Date) == shiftDate.Month)
        //                        .Count(); 
        //}

        #endregion
    }
}
