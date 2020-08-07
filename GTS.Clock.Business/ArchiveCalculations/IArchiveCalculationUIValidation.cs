using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.ArchiveCalculations
{
    /// <summary>
    /// اینترفیس قوانین واسط کاربری ویرایش نتایج محاسبات
    /// </summary>
   public interface IArchiveCalculationUIValidation
    {
       /// <summary>
       /// انجام اعتبارسنجی
       /// </summary>
       /// <param name="personId">کد پرسنلی</param>
       void DoValidate(decimal personId);
    }
}
